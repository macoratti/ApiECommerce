using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiECommerce.Entities;

public class Pedido
{
    public int Id { get; set; }
    [StringLength(100)]
    public string? Endereco { get; set; }

    [Column(TypeName = "decimal(12,2)")]
    public decimal ValorTotal { get; set; }
    public DateTime DataPedido { get; set; }
    public int UsuarioId { get; set; }
    public ICollection<DetalhePedido>? DetalhesPedido { get; set; }

}
