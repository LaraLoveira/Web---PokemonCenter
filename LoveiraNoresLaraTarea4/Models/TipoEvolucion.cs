using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveiraNoresLaraTarea4.Models
{
    [Table("tipo_evolucion")]
    public class TipoEvolucion
    {
        [Key]
        public int id_tipo_evolucion { get; set; }
        public required string tipo_evolucion { get; set; }
    }
}