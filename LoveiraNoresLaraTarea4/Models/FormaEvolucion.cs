using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveiraNoresLaraTarea4.Models
{
    [Table("forma_evolucion")]
    public class FormaEvolucion
    {
        [Key]
        public int id_forma_evolucion { get; set; }
        public int tipo_evolucion { get; set; }


        public TipoEvolucion? TipoEvolucion { get; set; }
        public ICollection<PokemonFormaEvolucion>? PokemonFormaEvolucion { get; set; }
    }
}