using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveiraNoresLaraTarea4.Models
{
    public class MO
    {
        [Key]
        public int id_forma_aprendizaje { get; set; }
        public required string Mo { get; set; }


        [ForeignKey("id_forma_aprendizaje")]
        public required FormaAprendizaje FormaAprendizaje { get; set; }
    }
}