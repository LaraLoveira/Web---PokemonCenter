using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveiraNoresLaraTarea4.Models
{
    [Table("pokemon_movimiento_forma")]
    public class PokemonMovimientoForma
    {
        public int PokemonId { get; set; }
        public int id_movimiento { get; set; }
        public int id_forma_aprendizaje { get; set; }


        [ForeignKey("PokemonId")]
        public Pokemon Pokemon { get; set; } = null!;

        [ForeignKey("id_movimiento")]
        public Movimiento Movimiento { get; set; } = null!;

        [ForeignKey("id_forma_aprendizaje")]
        public FormaAprendizaje FormaAprendizaje { get; set; } = null!;
    }
}