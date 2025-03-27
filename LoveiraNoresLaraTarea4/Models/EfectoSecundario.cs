using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveiraNoresLaraTarea4.Models
{
    [Table("efecto_secundario")]
    public class EfectoSecundario
    {
        [Key]
        public int id_efecto_secundario { get; set; }
        public required string efecto_secundario { get; set; }


        public ICollection<MovimientoEfectoSecundario>? MovimientoEfectoSecundario { get; set; }
    }
}