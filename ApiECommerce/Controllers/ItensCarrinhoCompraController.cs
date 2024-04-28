using ApiECommerce.Context;
using ApiECommerce.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ApiECommerce.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItensCarrinhoCompraController : ControllerBase
{
    private readonly AppDbContext dbContext;

    public ItensCarrinhoCompraController(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    // GET: api/ItensCarrinhoCompra/1
    [HttpGet("{usuarioId}")]
    public async Task<IActionResult> Get(int usuarioId)
    {
        var user = await dbContext.Usuarios.FindAsync(usuarioId);

        if (user is null)
        {
            return NotFound($"Usuário com o id = {usuarioId} não encontrado");
        }

        var itensCarrinho = await (from s in dbContext.ItensCarrinhoCompra.Where(s => s.ClienteId == usuarioId)
                                   join p in dbContext.Produtos on s.ProdutoId equals p.Id
                                   select new
                                   {
                                       Id = s.Id,
                                       Preco = s.PrecoUnitario,
                                       ValorTotal = s.ValorTotal,
                                       Quantidade = s.Quantidade,
                                       ProdutoId = p.Id,
                                       ProdutoNome = p.Nome,
                                       UrlImagem = p.UrlImagem
                                   }).ToListAsync();

        return Ok(itensCarrinho);
    }

    // POST: api/ItensCarrinhoCompra
    // Este método Action trata de uma requisição HTTP do tipo POST para adicionar um
    // novo item ao carrinho de compra ou atualizar a quantidade de um item existente
    // no carrinho. Ele verifica se o item já está no carrinho com base no ID do produto
    // e no ID do cliente. Se o item já estiver no carrinho, sua quantidade é atualizada
    // e o valor total é recalculado. Caso contrário, um novo item é adicionado ao carrinho
    // com as informações fornecidas. Após as operações no banco de dados, o método retorna
    // um código de status 201 (Created).
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ItemCarrinhoCompra itemCarrinhoCompra)
    {
        try
        {
            var carrinhoCompra = await dbContext.ItensCarrinhoCompra.FirstOrDefaultAsync(s =>
                                    s.ProdutoId == itemCarrinhoCompra.ProdutoId &&
                                    s.ClienteId == itemCarrinhoCompra.ClienteId);

            if (carrinhoCompra is not null)
            {
                carrinhoCompra.Quantidade += itemCarrinhoCompra.Quantidade;
                carrinhoCompra.ValorTotal = carrinhoCompra.PrecoUnitario * carrinhoCompra.Quantidade;
            }
            else
            {
                var produto = await dbContext.Produtos.FindAsync(itemCarrinhoCompra.ProdutoId);

                var carrinho = new ItemCarrinhoCompra()
                {
                    ClienteId = itemCarrinhoCompra.ClienteId,
                    ProdutoId = itemCarrinhoCompra.ProdutoId,
                    PrecoUnitario = itemCarrinhoCompra.PrecoUnitario,
                    Quantidade = itemCarrinhoCompra.Quantidade,
                    ValorTotal = (produto!.Preco) * (itemCarrinhoCompra.Quantidade)
                };
                dbContext.ItensCarrinhoCompra.Add(carrinho);
            }
            await dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception)
        {
            // Aqui você pode lidar com a exceção, seja registrando-a, enviando uma resposta de erro adequada para o cliente, etc.
            // Por exemplo, você pode retornar uma resposta de erro 500 (Internal Server Error) com uma mensagem genérica para o cliente.
            return StatusCode(StatusCodes.Status500InternalServerError, 
                "Ocorreu um erro ao processar a solicitação.");
        }
    }

    // PUT /api/ItensCarrinhoCompra?produtoId = 1 & acao = "aumentar"
    // PUT /api/ItensCarrinhoCompra?produtoId = 1 & acao = "diminuir"
    // PUT /api/ItensCarrinhoCompra?produtoId = 1 & acao = "deletar"
    //--------------------------------------------------------------------
    // Este código manipula itens no carrinho de compras de um usuário com base em uma
    // ação("aumentar", "diminuir" ou "deletar") e um ID de produto.
    // Obtém o usuário logado:
    //    Usa o e-mail do usuário logado para buscar o usuário no banco de dados.
    // Busca o item do carrinho do produto:
    // Procura o item no carrinho com base no ID do produto e no ID do cliente (usuário logado).
    // Realiza a ação especificada:
    //    Aumentar:
    //        Se a quantidade for maior que 0, aumenta a quantidade do item em 1.
    //    Diminuir:
    //        Se a quantidade for maior que 1, diminui a quantidade do item em 1.
    //        Se a quantidade for 1, remove o item do carrinho.
    //    Deletar:
    //        Remove o item do carrinho.
    // Atualiza o valor total do item:
    //    Multiplica o preço unitário pela quantidade, atualizando o valor total do item no carrinho.
    // Salva as alterações no banco de dados:
    //    Salva as alterações feitas no item do carrinho no banco de dados.
    // Retorna o resultado:
    //    Se a ação for bem-sucedida, retorna "Ok".
    //    Se o item não for encontrado, retorna "NotFound".
    //    Se a ação for inválida, retorna "BadRequest".
    /// <summary>
    /// Atualiza a quantidade de um item no carrinho de compras do usuário.
    /// </summary>
    /// <param name="produtoId">O ID do produto.</param>
    /// <param name="acao">A ação a ser realizada no item do carrinho. Opções: 'aumentar', 'diminuir' ou 'deletar'.</param>
    /// <returns>Um objeto IActionResult representando o resultado da operação.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)] 
    //[HttpPut("{produtoId}/{acao}")]
    public async Task<IActionResult> Put(int produtoId, string acao)
    {
        // Este codigo recupera o endereço de e-mail do usuário autenticado do token JWT decodificado,
        // Claims representa as declarações associadas ao usuário autenticado
        // Assim somente usuários autenticados poderão acessar este endpoint
        var usuarioEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Email == usuarioEmail);

        if (usuario is null)
        {
            return NotFound("Usuário não encontrado."); 
        }

        var itemCarrinhoCompra = await dbContext.ItensCarrinhoCompra.FirstOrDefaultAsync(s =>
                                               s.ClienteId == usuario!.Id && s.ProdutoId == produtoId);

        if (itemCarrinhoCompra != null)
        {
            if (acao.ToLower() == "aumentar")
            {
                itemCarrinhoCompra.Quantidade += 1;
            }
            else if (acao.ToLower() == "diminuir")
            {
                if (itemCarrinhoCompra.Quantidade > 1)
                {
                    itemCarrinhoCompra.Quantidade -= 1;
                }
                else
                {
                    dbContext.ItensCarrinhoCompra.Remove(itemCarrinhoCompra);
                    await dbContext.SaveChangesAsync();
                    return Ok();
                }
            }
            else if (acao.ToLower() == "deletar")
            {
                // Remove o item do carrinho
                dbContext.ItensCarrinhoCompra.Remove(itemCarrinhoCompra);
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest("Ação Inválida. Use : 'aumentar', 'diminuir', ou 'deletar' para realizar uma ação");
            }

            itemCarrinhoCompra.ValorTotal = itemCarrinhoCompra.PrecoUnitario * itemCarrinhoCompra.Quantidade;
            await dbContext.SaveChangesAsync();
            return Ok($"Operacao : {acao} realizada com sucesso");
        }
        else
        {
            return NotFound("Nenhum item encontrado no carrinho");
        }
    }
}