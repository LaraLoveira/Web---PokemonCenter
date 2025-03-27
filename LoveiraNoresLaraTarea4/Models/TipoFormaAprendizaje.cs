using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveiraNoresLaraTarea4.Models
{
    [Table("tipo_forma_aprendizaje")]
    public class TipoFormaAprendizaje
    {
        [Key]
        public int id_tipo_aprendizaje { get; set; }
        public required string tipo_aprendizaje { get; set; }
    }
}