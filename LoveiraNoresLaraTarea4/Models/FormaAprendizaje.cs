using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveiraNoresLaraTarea4.Models
{
    [Table("forma_aprendizaje")]
    public class FormaAprendizaje
    {
        [Key]
        public int id_forma_aprendizaje { get; set; }
        public int id_tipo_aprendizaje { get; set; }


        [ForeignKey("id_tipo_aprendizaje")]
        public TipoFormaAprendizaje TipoFormaAprendizaje { get; set; } = null!;
        public ICollection<PokemonMovimientoForma> PokemonMovimientoForma { get; set; } = new List<PokemonMovimientoForma>();
    }
}