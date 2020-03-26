using System.ComponentModel.DataAnnotations;

namespace Lection7
{
    public class Teacher
    {
        [Key]
        public string Name { get; set; }
        
        public string Discipline { get; set; }
    }
}