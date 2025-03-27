using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveiraNoresLaraTarea4.Models
{
    [Table("movimiento")]
    public class Movimiento
    {
        [Key]
        public int id_movimiento { get; set; }
        public required string nombre { get; set; }
        public int potencia { get; set; }
        public int precision_mov { get; set; }
        public required string descripcion { get; set; }
        public int pp { get; set; }
        public int id_tipo { get; set; }
        public int prioridad { get; set; }


        [ForeignKey("id_tipo")]
        public required Tipo Tipo { get; set; }
        public required ICollection<PokemonMovimientoForma> PokemonMovimientoForma { get; set; }
        public ICollection<MovimientoEfectoSecundario>? MovimientoEfectoSecundario { get; set; }
    }
}