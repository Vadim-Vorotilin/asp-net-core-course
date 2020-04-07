using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsApi;
using StudentsApi.Entities;
using StudentsApi.Models;

namespace Gateway
{
    [ApiController]
    //[Authorize]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly StudentsDbContext _dbContext;

        public StudentController(StudentsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var students = await _dbContext.Students
                                           .OrderBy(s => s.Id)
                                           .ToArrayAsync();

            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var student = await _dbContext.Students.FindAsync(id);

            if (student == null)
                return NotFound();

            return Ok(student);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(StudentModel model)
        {
            TeacherEntity teacher = null;

            if (!string.IsNullOrEmpty(model.TeacherName))
            {
                teacher = await _dbContext.Teachers.FindAsync(model.TeacherName);

                if (teacher == null)
                    return NotFound();
            }

            var entity = _dbContext.Students.Add(new StudentEntity { Name = model.Name, Score = model.Score });//, Teacher = teacher });

            await _dbContext.SaveChangesAsync();
            
            return Ok(entity.Entity);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateScore([FromRoute] int id,
                                                     [FromQuery] int score)
        {
            var student = await _dbContext.Students.FindAsync(id);

            if (student == null)
                return NotFound();

            student.Score = score;

            await _dbContext.SaveChangesAsync();

            return Ok(student);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var student = await _dbContext.Students.FindAsync(id);

            if (student == null)
                return NotFound();

            _dbContext.Students.Remove(student);

            await _dbContext.SaveChangesAsync();

            return Ok(student.Id);
        }
    }
}