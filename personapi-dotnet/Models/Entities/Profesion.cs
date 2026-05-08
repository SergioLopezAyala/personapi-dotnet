using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personapi_dotnet.Models.Entities;

[Table("profesion")]
public class Profesion
{
    [Key]
    [Column("id")]
    [Display(Name = "Id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(90)]
    [Column("nom")]
    [Display(Name = "Nombre")]
    public string Nom { get; set; } = string.Empty;

    [Column("des", TypeName = "varchar(max)")]
    [Display(Name = "Descripción")]
    public string? Des { get; set; }

    public virtual ICollection<Estudios> Estudios { get; set; } = new List<Estudios>();
}
