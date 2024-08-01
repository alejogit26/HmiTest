using Api.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Data.Entities
{
    public class Municipio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int IdMunicipio { get; set; }

        [RequiredCustom]
        [MaxLengthCustomAttribute(100)]
        [Display(Name = "Nombre municipio")]
        [TextOnlyCustom]
        public string? Nombre { get; set; }

        [Required]
        [MaxLengthCustomAttribute(3)]
        [Display(Name = "Código municipio")]
        [NumbersOnlyCustom]
        public string? Codigo { get; set; }

        [Required]
        public int DepartamentoId { get; set; }

        [NotMapped]
        public string? CodigoCompleto { get; set; }

        public void EstablecerCodigoCompleto(string codigoDepartamento, string codigoMunicipio)
        {
            CodigoCompleto = $"{codigoDepartamento}{codigoMunicipio}";
        }
    }
}
