using DapperAPI.Domain.Interfaces;
using DapperAPI.Domain.Models;
using DapperAPI.Service.DTO.Pedido;
using DapperAPI.Service.Interfaces;

namespace DapperAPI.Service.Services;

public class PedidoService : IPedidoService
{
    public readonly IPedidoRepository _pedidoRepository;

    public PedidoService(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public async Task<int> AddPedidoAsync(AdicionarPedidoRequest pedidoRequest)
    {
        var pedido = new Pedido
        {
            DataPedido = pedidoRequest.DataPedido,
            ClienteId = pedidoRequest.ClienteId
        };

        pedido.Itens = new List<PedidoItem>();

        foreach (var item in pedidoRequest.Itens)
        {
            pedido.Itens.Add(new PedidoItem
            {
                ProdutoId = item.ProdutoId,
                Quantidade = item.Quantidade
            });
        }

        var result = await _pedidoRepository.CreatePedidoAsync(pedido);

        return result;
    }

    public async Task<IEnumerable<PedidoResult>> GetAllPedidosAsync()
    {

        var result = await _pedidoRepository.GetAllPedidosAsync();

        if (result == null)
            return new List<PedidoResult>();

        return result.Select(c => new PedidoResult
                    {
                        Id = c.Id,
                        DataPedido = c.DataPedido,
                        ClienteId = c.ClienteId,
                        ClienteNome = c.Cliente!.Nome,
                        Itens = c.Itens!.Select(i => new PedidoItemResult
                        {
                            Id = i.Id,
                            ProdutoId = i.ProdutoId,
                            ProdutoNome = i.Produto?.Nome,
                            Quantidade = i.Quantidade
                        }).ToList(),
        });
    }

    public async Task<PedidoResult> GetPedidoByIdAsync(int id)
    {
        var result  = await _pedidoRepository.GetPedidoByIdAsync(id);

        if (result == null)
            return null!;

        return new PedidoResult
        {
            Id = result.Id,
            DataPedido = result.DataPedido,
            ClienteId = result.ClienteId,
            Itens = result.Itens!.Select(i => new PedidoItemResult
            {
                Id = i.Id,
                ProdutoId = i.ProdutoId,
                ProdutoNome = i.Produto!.Nome,
                Quantidade = i.Quantidade
            }).ToList(),
        };
    }
}
