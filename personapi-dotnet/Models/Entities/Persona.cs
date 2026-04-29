using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personapi_dotnet.Models.Entities;

[Table("persona")]
public class Persona
{
    [Key]
    [Column("cc")]
    [Display(Name = "Cédula")]
    public long Cc { get; set; }

    [Required]
    [MaxLength(45)]
    [Column("nombre")]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [MaxLength(45)]
    [Column("apellido")]
    [Display(Name = "Apellido")]
    public string Apellido { get; set; } = string.Empty;

    [MaxLength(1)]
    [Column("genero", TypeName = "char(1)")]
    [RegularExpression("[MF]", ErrorMessage = "Género debe ser 'M' o 'F'.")]
    [Display(Name = "Género")]
    public string? Genero { get; set; }

    [Column("edad")]
    [Display(Name = "Edad")]
    public int? Edad { get; set; }

    public virtual ICollection<Estudios> Estudios { get; set; } = new List<Estudios>();
    public virtual ICollection<Telefono> Telefonos { get; set; } = new List<Telefono>();
}
