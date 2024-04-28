using ApiECommerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiECommerce.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<ItemCarrinhoCompra> ItensCarrinhoCompra { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<DetalhePedido> DetalhesPedido { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Categoria>().HasData(
            new Categoria { Id = 1, Nome = "Lanches", UrlImagem = "lanches1.png" },
            new Categoria { Id = 2, Nome = "Combos", UrlImagem = "combos1.png" },
            new Categoria { Id = 3, Nome = "Naturais", UrlImagem = "naturais1.png" },
            new Categoria { Id = 4, Nome = "Bebidas", UrlImagem = "refrigerantes1.png" },
            new Categoria { Id = 5, Nome = "Sucos", UrlImagem = "sucos1.png" },
            new Categoria { Id = 6, Nome = "Sobremesas", UrlImagem = "sobremesas1.png" }
            );

        modelBuilder.Entity<Produto>().HasData(
           new Produto { Id = 1, Nome = "Hamburger padrão", UrlImagem = "hamburger1.jpeg", CategoriaId = 1, Preco = 15, EmEstoque=13,Disponivel=true, MaisVendido = true, Popular = true, Detalhe = "Pão fofinho, hambúrger de carne bovina temperada, cebola, mostarda e ketchup " },
           new Produto { Id = 2, Nome = "CheeseBurger padrão", UrlImagem = "hamburger3.jpeg", CategoriaId = 1, Preco = 18, EmEstoque=10, Disponivel = true, MaisVendido = false, Popular = true, Detalhe = "Pão fofinho, hambúrguer de carne bovina temperada e queijo por todos os lados." },
           new Produto { Id = 3, Nome = "CheeseSalada padrão", UrlImagem = "hamburger4.jpeg", CategoriaId = 1, Preco = 19, EmEstoque = 13, Disponivel = true, MaisVendido = false, Popular = false, Detalhe = "Pão fofinho, hambúrger de carne bovina temperada, cebola,alface, mostarda e ketchup " },
           new Produto { Id = 4, Nome = "Hambúrger, batata fritas, refrigerante ", UrlImagem = "combo1.jpeg", CategoriaId = 2, Preco = 25, EmEstoque = 10, Disponivel = false, MaisVendido = false, Popular = true, Detalhe = "Pão fofinho, hambúrguer de carne bovina temperada e queijo, refrigerante e fritas" },
           new Produto { Id = 5, Nome = "CheeseBurger, batata fritas , refrigerante", UrlImagem = "combo2.jpeg", CategoriaId = 2, Preco = 27, EmEstoque = 13, Disponivel = true, MaisVendido = false, Popular = false, Detalhe = "Pão fofinho, hambúrguer de carne bovina ,refrigerante e fritas, cebola, maionese e ketchup" },
           new Produto { Id = 6, Nome = "CheeseSalada, batata fritas, refrigerante", UrlImagem = "combo3.jpeg", CategoriaId = 2, Preco = 28, EmEstoque = 10, Disponivel = true, MaisVendido = false, Popular = true, Detalhe = "Pão fofinho, hambúrguer de carne bovina ,refrigerante e fritas, cebola, maionese e ketchup" },
           new Produto { Id = 7, Nome = "Lanche Natural com folhas", UrlImagem = "lanche_natural1.jpeg", CategoriaId = 3, Preco = 14, EmEstoque = 13, Disponivel = true, MaisVendido = false, Popular = false, Detalhe = "Pão integral com folhas e tomate" },
           new Produto { Id = 8, Nome = "Lanche Natural e queijo", UrlImagem = "lanche_natural2.jpeg", CategoriaId = 3, Preco = 15, EmEstoque = 10, Disponivel = true, MaisVendido = false, Popular = true, Detalhe = "Pão integral, folhas, tomate e queijo." },
           new Produto { Id = 9, Nome = "Lanche Vegano", UrlImagem = "lanche_vegano1.jpeg", CategoriaId = 3, Preco = 25, EmEstoque = 18, Disponivel = true, MaisVendido = false, Popular = false, Detalhe = "Lanche vegano com ingredientes saudáveis" },
           new Produto { Id = 10, Nome = "Coca-Cola", UrlImagem = "coca_cola1.jpeg", CategoriaId = 4, Preco = 21, EmEstoque = 7, Disponivel = true, MaisVendido = false, Popular = true, Detalhe = "Refrigerante Coca Cola" },
           new Produto { Id = 11, Nome = "Guaraná", UrlImagem = "guarana1.jpeg", CategoriaId = 4, Preco = 25, EmEstoque = 6, Disponivel = true, MaisVendido = false, Popular = false, Detalhe = "Refrigerante de Guaraná" },
           new Produto { Id = 12, Nome = "Pepsi", UrlImagem = "pepsi1.jpeg", CategoriaId = 4, Preco = 21, EmEstoque = 6, Disponivel = true, MaisVendido = false, Popular = false, Detalhe = "Refrigerante Pepsi Cola" },
           new Produto { Id = 13, Nome = "Suco de laranja", UrlImagem = "suco_laranja.jpeg", CategoriaId = 5, Preco = 11, EmEstoque = 10, Disponivel = true, MaisVendido = false, Popular = false, Detalhe = "Suco de laranja saboroso e nutritivo" },
           new Produto { Id = 14, Nome = "Suco de morango", UrlImagem = "suco_morango1.jpeg", CategoriaId = 5, Preco = 15, EmEstoque = 13, Disponivel = true, MaisVendido = false, Popular = false, Detalhe = "Suco de morango fresquinhos" },
           new Produto { Id = 15, Nome = "Suco de uva", UrlImagem = "suco_uva1.jpeg", CategoriaId = 5, Preco = 13, EmEstoque = 10, Disponivel = true, MaisVendido = false, Popular = false, Detalhe = "Suco de uva natural sem acúcar feito com a fruta" },
           new Produto { Id = 16, Nome = "Água", UrlImagem = "agua_mineral1.jpeg", CategoriaId = 4, Preco = 5, EmEstoque = 10, Disponivel = true, MaisVendido = false, Popular = false, Detalhe = "Água mineral natural fresquinha" },
           new Produto { Id = 17, Nome = "Cookies de chocolate", UrlImagem = "cookie1.jpeg", CategoriaId = 6, Preco = 8, EmEstoque = 10, Disponivel = true, MaisVendido = false, Popular = true, Detalhe = "Cookies de Chocolate com pedaços de chocolate" },
           new Produto { Id = 18, Nome = "Cookies de Baunilha", UrlImagem = "cookie2.jpeg", CategoriaId = 6, Preco = 8, EmEstoque = 13, Disponivel = true, MaisVendido = true, Popular = false, Detalhe = "Cookies de baunilha saborosos e crocantes" },
           new Produto { Id = 19, Nome = "Torta Suíca", UrlImagem = "torta_suica1.jpeg", CategoriaId = 6, Preco = 10, EmEstoque = 10, Disponivel = true, MaisVendido = false, Popular = true, Detalhe = "Torta suíca com creme e camadas de doce de leite" }
    );
  }
}





