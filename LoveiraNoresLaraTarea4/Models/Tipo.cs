using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveiraNoresLaraTarea4.Models
{
    [Table("tipo")]
    public class Tipo
    {
        [Key]
        public int id_tipo { get; set; } 
        public required string nombre { get; set; }

        [ForeignKey("id_tipo_ataque")]
        public int id_tipo_ataque { get; set; }


        public TipoAtaque? TipoAtaque { get; set; }
        public ICollection<PokemonTipo> PokemonTipo { get; set; } = new List<PokemonTipo>();
    }
}