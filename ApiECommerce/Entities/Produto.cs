using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiECommerce.Entities;

public class Produto
{
    public int Id { get; set; }
    [StringLength(100)]
    [Required]
    public string? Nome { get; set; }
    [StringLength(200)]
    [Required]
    public string? Detalhe { get; set; }
    [StringLength(200)]
    [Required]
    public string? UrlImagem { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Preco { get; set; }
    public bool Popular { get; set; }
    public bool MaisVendido { get; set; }
    public int EmEstoque { get; set; }
    public bool Disponivel { get; set; }
    public int CategoriaId { get; set; }

    [JsonIgnore]
    public ICollection<DetalhePedido>? DetalhesPedido { get; set; }
    [JsonIgnore]
    public ICollection<ItemCarrinhoCompra>? ItensCarrinhoCompras { get; set; }
}
