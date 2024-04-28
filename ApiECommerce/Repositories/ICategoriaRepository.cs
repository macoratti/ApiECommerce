using ApiECommerce.Entities;

namespace ApiECommerce.Repositories;

public interface ICategoriaRepository
{
    Task<IEnumerable<Categoria>> GetCategorias();
}
