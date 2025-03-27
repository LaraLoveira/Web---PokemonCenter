using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveiraNoresLaraTarea4.Models
{
    [Table("piedra")]
    public class Piedra
    {
        [ForeignKey("id_forma_evolucion")]
        public int id_forma_evolucion { get; set; }
        [ForeignKey("id_tipo_piedra")]
        public int id_tipo_piedra { get; set; }


        [ForeignKey("id_forma_evolucion")]
        public required FormaEvolucion FormaEvolucion { get; set; }
        [ForeignKey("id_tipo_piedra")]
        public required TipoPiedra TipoPiedra { get; set; }
    }
}