using DapperAPI.Domain.Interfaces;
using DapperAPI.Domain.Models;
using DapperAPI.Service.DTO.Produto;
using DapperAPI.Service.Interfaces;

namespace DapperAPI.Service.Services;

public class ProdutoService : IProdutoService
{
    public readonly IProdutoRepository _produtoRepository;

    public ProdutoService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<int> AddProdutoAsync(AdicionarProdutoRequest produtoRequest)
    {
        var produto = new Produto
        {
            Nome = produtoRequest.Nome,
            Preco = produtoRequest.Preco
        };

        var result = await _produtoRepository.AddProdutoAsync(produto);

        return result;
    }

    public async Task<IEnumerable<ProdutoResult>> GetAllProdutosAsync()
    {

        var result = await _produtoRepository.GetAllProdutosAsync();

        if (result == null)
            return new List<ProdutoResult>();

        return result.Select(c => new ProdutoResult
                    {
                        Id = c.Id,
                        Nome = c.Nome,
                        Preco = c.Preco
                    });
    }

    public async Task<ProdutoResult> GetProdutoByIdAsync(int id)
    {
        var result  = await _produtoRepository.GetProdutoByIdAsync(id);

        if (result == null)
            return null!;

        return new ProdutoResult
        {
            Id = result.Id,
            Nome = result.Nome,
            Preco = result.Preco
        };
    }
}
