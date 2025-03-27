using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveiraNoresLaraTarea4.Models
{
    [Table("pokemon_tipo")]
    public class PokemonTipo
    {

        [ForeignKey("PokemonId")]
        public int PokemonId { get; set; }

        [ForeignKey("id_tipo")]
        public int id_tipo { get; set; }


        [ForeignKey("PokemonId")]
        public required Pokemon Pokemon { get; set; }
        [ForeignKey("id_tipo")]
        public required Tipo Tipo { get; set; }
    }
}