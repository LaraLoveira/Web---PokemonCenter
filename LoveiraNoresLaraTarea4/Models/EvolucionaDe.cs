using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveiraNoresLaraTarea4.Models
{
    [Table("evoluciona_de")]
    public class EvolucionaDe
    {
        [ForeignKey("pokemon_evolucionado")]
        public int pokemon_evolucionado { get; set; }
        [ForeignKey("pokemon_origen")]
        public int pokemon_origen { get; set; }


        public required Pokemon PokemonEvolucionado { get; set; }
        public required Pokemon PokemonOrigen { get; set; }
    }
}