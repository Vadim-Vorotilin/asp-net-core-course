using System.Collections.Generic;

namespace Lection7.Entities
{
    public class StudentEntity
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public int Score { get; set; }
        
        public IEnumerable<StudentTeacherEntity> Teachers { get; set; }
    }
}