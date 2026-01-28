using DapperAPI.Service.DTO.Cliente;
using DapperAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DapperAPI.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClienteController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpPost("create-client")]
    public async Task<IActionResult> AdicionarCliente([FromBody] AdicionarClienteRequest cliente)
    {

        var resultado = await _clienteService.AddClienteAsync(cliente);

        return Ok(resultado);
    }

    [HttpGet("get-all-clients")]
    public async Task<IActionResult> ObterTodosClientes()
    {
        var resultado = await _clienteService.GetAllClientesAsync();

        return Ok(resultado);
    }

    [HttpGet("get-client-by-id/{id:int}")]
    public async Task<IActionResult> ObterClientePorId(int id)
    {
        var resultado = await _clienteService.GetClienteByIdAsync(id);

        if (resultado == null)
            return NotFound();
        
        return Ok(resultado);
    }
}
