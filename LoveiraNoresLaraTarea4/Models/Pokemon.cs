using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveiraNoresLaraTarea4.Models
{
    [Table("pokemon")]
    public class Pokemon
    {
        [Key]
        public int PokemonId { get; set; }
        public required string nombre { get; set; }
        public double peso { get; set; }
        public double altura { get; set; }


        public required EstadisticasBase EstadisticasBase { get; set; }
        public required ICollection<PokemonTipo> PokemonTipo { get; set; }
        public required ICollection<PokemonMovimientoForma> PokemonMovimientoForma { get; set; }
        public required ICollection<EvolucionaDe> Evoluciones { get; set; }
        public ICollection<EvolucionaDe>? EvolucionesOrigen { get; set; }
    }
}