using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoveiraNoresLaraTarea4.Models
{
    public class MT
    {
        [Key]
        public int id_forma_aprendizaje { get; set; }
        public string? Mt { get; set; }


        [ForeignKey("id_forma_aprendizaje")]
        public FormaAprendizaje? FormaAprendizaje { get; set; }
    }
}