using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveiraNoresLaraTarea4.Models
{
    [Table("pokemon_forma_evolucion")]
    public class PokemonFormaEvolucion
    {
        public int PokemonId { get; set; }
        [Key]
        public int id_forma_evolucion { get; set; }

        public required Pokemon Pokemon { get; set; }
        public required FormaEvolucion FormaEvolucion { get; set; }
    }
}