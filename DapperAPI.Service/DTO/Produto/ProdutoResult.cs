namespace DapperAPI.Service.DTO.Produto;

public class ProdutoResult
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; } = 0;
}
