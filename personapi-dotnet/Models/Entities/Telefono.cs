using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
    [Required]
    public long Duenio { get; set; }

    [ForeignKey(nameof(Duenio))]
    [JsonIgnore]
    public virtual Persona? Persona { get; set; }
}
