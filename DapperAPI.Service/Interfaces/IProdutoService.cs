using DapperAPI.Service.DTO.Produto;

namespace DapperAPI.Service.Interfaces;

public interface IProdutoService
{
    Task<int> AddProdutoAsync(AdicionarProdutoRequest produto);
    Task<ProdutoResult> GetProdutoByIdAsync(int id);
    Task<IEnumerable<ProdutoResult>> GetAllProdutosAsync();
}
