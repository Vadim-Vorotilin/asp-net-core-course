using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsApi.Entities
{
    [Table("StudentTeacher")]
    public class StudentTeacherEntity
    {
        public int StudentId { get; set; }
        public string TeacherName { get; set; }
        
        public StudentEntity Student { get; set; }
        public TeacherEntity Teacher { get; set; }
    }
}