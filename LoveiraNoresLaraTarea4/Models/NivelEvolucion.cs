using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveiraNoresLaraTarea4.Models
{
    [Table("nivel_evolucion")]
    public class NivelEvolucion
    {
        [Key]
        public int id_forma_evolucion { get; set; }
        public int nivel { get; set; }

        public required FormaEvolucion FormaEvolucion { get; set; }
    }
}