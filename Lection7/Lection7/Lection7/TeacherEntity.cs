using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lection7
{
    [Table("Teacher")]
    public class TeacherEntity
    {
        [Key]
        public string Name { get; set; }
        
        public string Discipline { get; set; }
    }
}