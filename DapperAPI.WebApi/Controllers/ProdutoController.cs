using DapperAPI.Service.DTO.Produto;
using DapperAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DapperAPI.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutoController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpPost("create-product")]
    public async Task<IActionResult> AdicionarProduto([FromBody] AdicionarProdutoRequest produto)
    {

        var resultado = await _produtoService.AddProdutoAsync(produto);

        return Ok(resultado);
    }

    [HttpGet("get-all-products")]
    public async Task<IActionResult> ObterTodosProdutos()
    {
        var resultado = await _produtoService.GetAllProdutosAsync();

        return Ok(resultado);
    }

    [HttpGet("get-product-by-id/{id:int}")]
    public async Task<IActionResult> ObterProdutoPorId(int id)
    {
        var resultado = await _produtoService.GetProdutoByIdAsync(id);

        if (resultado == null)
            return NotFound();
        
        return Ok(resultado);
    }
}
