using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsApi.Entities
{
    [Table("Teacher")]
    public class TeacherEntity
    {
        [Key]
        public string Name { get; set; }
        
        public string Discipline { get; set; }
        
        public IEnumerable<StudentTeacherEntity> Students { get; set; }
    }
}