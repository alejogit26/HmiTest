using Api.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Data.Entities
{
    public class Departamento
    {
        public Departamento()
        {
            Municipios = new List<Municipio?>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int IdDepartamento { get; set; }

        [RequiredCustom]
        [MaxLengthCustomAttribute(100)]
        [Display(Name = "Nombre departamento")]
        [TextOnlyCustom]
        public string? Nombre { get; set; }

        [RequiredCustom]
        [MaxLengthCustomAttribute(2)]
        [Display(Name = "Código departamento")]
        [NumbersOnlyCustom]
        public string? Codigo { get; set; }

        public List<Municipio?> Municipios { get; set; }
    }
}
