using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveiraNoresLaraTarea4.Models
{
    [Table("nivel_aprendizaje")]
    public class NivelAprendizaje
    {
        [Key]
        public int id_forma_aprendizaje { get; set; }
        public int nivel { get; set; }
        [ForeignKey("id_forma_aprendizaje")]
        public FormaAprendizaje FormaAprendizaje { get; set; } = null!;
    }
}