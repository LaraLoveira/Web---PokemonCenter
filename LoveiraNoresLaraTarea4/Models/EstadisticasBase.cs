using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveiraNoresLaraTarea4.Models
{
    [Table("estadisticas_base")]
    public class EstadisticasBase
    {
        [Key]
        public int PokemonId { get; set; }
        public int Ps { get; set; }
        public int Ataque { get; set; }
        public int Defensa { get; set; }
        public int Especial { get; set; }
        public int Velocidad { get; set; }


        public required Pokemon Pokemon { get; set; } 
    }
}