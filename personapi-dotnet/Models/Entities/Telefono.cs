using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personapi_dotnet.Models.Entities;

[Table("telefono")]
public class Telefono
{
    [Key]
    [MaxLength(15)]
    [Column("num")]
    [Display(Name = "Número")]
    public string Num { get; set; } = string.Empty;

    [MaxLength(45)]
    [Column("oper")]
    [Display(Name = "Operador")]
    public string? Oper { get; set; }

    [Column("duenio")]
    [Display(Name = "Dueño (cédula)")]
    public long? Duenio { get; set; }

    [ForeignKey(nameof(Duenio))]
    public virtual Persona? Persona { get; set; }
}
