using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveiraNoresLaraTarea4.Models
{
    [Table("tipo_ataque")]
    public class TipoAtaque
    {
        [Key]
        public int id_tipo_ataque { get; set; }
        public required string tipo { get; set; }

        public required ICollection<Tipo> Tipo { get; set; }
    }
}