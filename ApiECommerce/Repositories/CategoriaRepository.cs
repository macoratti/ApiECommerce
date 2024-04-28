using ApiECommerce.Context;
using ApiECommerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiECommerce.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext dbContext;

    public CategoriaRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Categoria>> GetCategorias()
    {
        return await dbContext.Categorias.ToListAsync();
    }
}
