using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiECommerce.Entities;

public class Usuario
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string? Nome { get; set; }
    
    [StringLength(150)]
    [Required]
    public string? Email { get; set; }
    
    [StringLength(100)]
    [Required]
    public string? Senha { get; set; }
    
    [StringLength(100)]
    public string? UrlImagem { get; set; }

    [StringLength(80)]
    public string? Telefone { get; set; }
    
    [NotMapped]
    public IFormFile? Imagem { get; set; }
    public ICollection<Pedido>? Pedidos { get; set; }
}
