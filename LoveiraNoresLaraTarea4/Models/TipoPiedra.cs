using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveiraNoresLaraTarea4.Models
{
    [Table("tipo_piedra")]
    public class TipoPiedra
    {
        [Key]
        public int id_tipo_piedra { get; set; }
        public required string nombre_piedra { get; set; }

        public ICollection<Piedra> Piedra { get; set; } = new List<Piedra>();
    }
}