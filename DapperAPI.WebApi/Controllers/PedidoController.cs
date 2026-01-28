using DapperAPI.Service.DTO.Pedido;
using DapperAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DapperAPI.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PedidoController : ControllerBase
{
    private readonly IPedidoService _pedidoService;

    public PedidoController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpPost("create-order")]
    public async Task<IActionResult> AdicionarPedido([FromBody] AdicionarPedidoRequest pedido)
    {

        var resultado = await _pedidoService.AddPedidoAsync(pedido);

        return Ok(resultado);
    }

    [HttpGet("get-all-orders")]
    public async Task<IActionResult> ObterTodosPedidos()
    {
        var resultado = await _pedidoService.GetAllPedidosAsync();

        return Ok(resultado);
    }

    [HttpGet("get-order-by-id/{id:int}")]
    public async Task<IActionResult> ObterPedidoPorId(int id)
    {
        var resultado = await _pedidoService.GetPedidoByIdAsync(id);

        if (resultado == null)
            return NotFound();
        
        return Ok(resultado);
    }
}
