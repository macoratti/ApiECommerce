using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiECommerce.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UrlImagem = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UrlImagem = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Detalhe = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UrlImagem = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Popular = table.Column<bool>(type: "bit", nullable: false),
                    MaisVendido = table.Column<bool>(type: "bit", nullable: false),
                    EmEstoque = table.Column<int>(type: "int", nullable: false),
                    Disponivel = table.Column<bool>(type: "bit", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Endereco = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ValorTotal = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    DataPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItensCarrinhoCompra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensCarrinhoCompra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensCarrinhoCompra_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalhesPedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Preco = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalhesPedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalhesPedido_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalhesPedido_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Nome", "UrlImagem" },
                values: new object[,]
                {
                    { 1, "Lanches", "lanches1.png" },
                    { 2, "Combos", "combos1.png" },
                    { 3, "Naturais", "naturais1.png" },
                    { 4, "Bebidas", "refrigerantes1.png" },
                    { 5, "Sucos", "sucos1.png" },
                    { 6, "Sobremesas", "sobremesas1.png" }
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "CategoriaId", "Detalhe", "Disponivel", "EmEstoque", "MaisVendido", "Nome", "Popular", "Preco", "UrlImagem" },
                values: new object[,]
                {
                    { 1, 1, "Pão fofinho, hambúrger de carne bovina temperada, cebola, mostarda e ketchup ", true, 13, true, "Hamburger padrão", true, 15m, "hamburger1.jpeg" },
                    { 2, 1, "Pão fofinho, hambúrguer de carne bovina temperada e queijo por todos os lados.", true, 10, false, "CheeseBurger padrão", true, 18m, "hamburger3.jpeg" },
                    { 3, 1, "Pão fofinho, hambúrger de carne bovina temperada, cebola,alface, mostarda e ketchup ", true, 13, false, "CheeseSalada padrão", false, 19m, "hamburger4.jpeg" },
                    { 4, 2, "Pão fofinho, hambúrguer de carne bovina temperada e queijo, refrigerante e fritas", false, 10, false, "Hambúrger, batata fritas, refrigerante ", true, 25m, "combo1.jpeg" },
                    { 5, 2, "Pão fofinho, hambúrguer de carne bovina ,refrigerante e fritas, cebola, maionese e ketchup", true, 13, false, "CheeseBurger, batata fritas , refrigerante", false, 27m, "combo2.jpeg" },
                    { 6, 2, "Pão fofinho, hambúrguer de carne bovina ,refrigerante e fritas, cebola, maionese e ketchup", true, 10, false, "CheeseSalada, batata fritas, refrigerante", true, 28m, "combo3.jpeg" },
                    { 7, 3, "Pão integral com folhas e tomate", true, 13, false, "Lanche Natural com folhas", false, 14m, "lanche_natural1.jpeg" },
                    { 8, 3, "Pão integral, folhas, tomate e queijo.", true, 10, false, "Lanche Natural e queijo", true, 15m, "lanche_natural2.jpeg" },
                    { 9, 3, "Lanche vegano com ingredientes saudáveis", true, 18, false, "Lanche Vegano", false, 25m, "lanche_vegano1.jpeg" },
                    { 10, 4, "Refrigerante Coca Cola", true, 7, false, "Coca-Cola", true, 21m, "coca_cola1.jpeg" },
                    { 11, 4, "Refrigerante de Guaraná", true, 6, false, "Guaraná", false, 25m, "guarana1.jpeg" },
                    { 12, 4, "Refrigerante Pepsi Cola", true, 6, false, "Pepsi", false, 21m, "pepsi1.jpeg" },
                    { 13, 5, "Suco de laranja saboroso e nutritivo", true, 10, false, "Suco de laranja", false, 11m, "suco_laranja.jpeg" },
                    { 14, 5, "Suco de morango fresquinhos", true, 13, false, "Suco de morango", false, 15m, "suco_morango1.jpeg" },
                    { 15, 5, "Suco de uva natural sem acúcar feito com a fruta", true, 10, false, "Suco de uva", false, 13m, "suco_uva1.jpeg" },
                    { 16, 4, "Água mineral natural fresquinha", true, 10, false, "Água", false, 5m, "agua_mineral1.jpeg" },
                    { 17, 6, "Cookies de Chocolate com pedaços de chocolate", true, 10, false, "Cookies de chocolate", true, 8m, "cookie1.jpeg" },
                    { 18, 6, "Cookies de baunilha saborosos e crocantes", true, 13, true, "Cookies de Baunilha", false, 8m, "cookie2.jpeg" },
                    { 19, 6, "Torta suíca com creme e camadas de doce de leite", true, 10, false, "Torta Suíca", true, 10m, "torta_suica1.jpeg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalhesPedido_PedidoId",
                table: "DetalhesPedido",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalhesPedido_ProdutoId",
                table: "DetalhesPedido",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensCarrinhoCompra_ProdutoId",
                table: "ItensCarrinhoCompra",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_UsuarioId",
                table: "Pedidos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaId",
                table: "Produtos",
                column: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalhesPedido");

            migrationBuilder.DropTable(
                name: "ItensCarrinhoCompra");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
