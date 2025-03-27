using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveiraNoresLaraTarea4.Models
{
    [Table("movimiento_efecto_secundario")]
    public class MovimientoEfectoSecundario
    {
        public int id_movimiento { get; set; }
        public int id_efecto_secundario { get; set; }
        public double Probabilidad { get; set; }


        [ForeignKey("id_movimiento")]
        public required Movimiento Movimiento { get; set; }
        [ForeignKey("id_efecto_secundario")]
        public required EfectoSecundario EfectoSecundario { get; set; }
    }
}