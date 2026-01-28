using DapperAPI.Domain.Models;

namespace DapperAPI.Domain.Interfaces;

public interface IProdutoRepository
{
    Task<int> AddProdutoAsync(Produto produto);
    Task<Produto> GetProdutoByIdAsync(int id);
    Task<IEnumerable<Produto>> GetAllProdutosAsync();
}
