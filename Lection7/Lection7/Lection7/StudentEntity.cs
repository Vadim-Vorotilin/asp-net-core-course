namespace Lection7
{
    public class StudentEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        
        public TeacherEntity Teacher { get; set; }
    }
}