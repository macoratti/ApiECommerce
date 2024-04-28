using ApiECommerce.Entities;
using ApiECommerce.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApiECommerce.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutosController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetProdutos(string tipoProduto, int? categoriaId = null)
    {
        IEnumerable<Produto> produtos;

        if (tipoProduto == "categoria" && categoriaId != null)
        {
            produtos = await _produtoRepository.ObterProdutosPorCategoriaAsync(categoriaId.Value);
        }
        else if (tipoProduto == "popular")
        {
            produtos = await _produtoRepository.ObterProdutosPopularesAsync();
        }
        else if (tipoProduto == "maisvendido")
        {
            produtos = await _produtoRepository.ObterProdutosMaisVendidosAsync();
        }
        else
        {
            return BadRequest("Tipo de produto inválido");
        }

        var dadosProduto = produtos.Select(v => new
        {
            Id = v.Id,
            Nome = v.Nome,
            Preco = v.Preco,
            UrlImagem = v.UrlImagem
        });

        return Ok(dadosProduto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetalheProduto(int id)
    {
        var produto = await _produtoRepository.ObterDetalheProdutoAsync(id);

        if (produto is null)
        {
            return NotFound($"Produto com id={id} não encontrado");
        }

        var dadosProduto = new
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Preco = produto.Preco,
            Detalhe = produto.Detalhe,
            UrlImagem = produto.UrlImagem
        };

        return Ok(dadosProduto);
    }
}
