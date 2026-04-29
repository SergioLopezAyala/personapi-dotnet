using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personapi_dotnet.Models.Entities;

[Table("estudios")]
public class Estudios
{
    [Column("id_prof")]
    [Display(Name = "Profesión")]
    public int IdProf { get; set; }

    [Column("cc_per")]
    [Display(Name = "Cédula Persona")]
    public long CcPer { get; set; }

    [Column("fecha", TypeName = "date")]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha")]
    public DateTime? Fecha { get; set; }

    [MaxLength(50)]
    [Column("univer")]
    [Display(Name = "Universidad")]
    public string? Univer { get; set; }

    [ForeignKey(nameof(IdProf))]
    public virtual Profesion? Profesion { get; set; }

    [ForeignKey(nameof(CcPer))]
    public virtual Persona? Persona { get; set; }
}
