using ApiECommerce.Entities;

namespace ApiECommerce.Repositories
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> ObterProdutosPorCategoriaAsync(int categoriaId);
        Task<IEnumerable<Produto>> ObterProdutosPopularesAsync();
        Task<IEnumerable<Produto>> ObterProdutosMaisVendidosAsync();
        Task<Produto> ObterDetalheProdutoAsync(int id);
    }
}
