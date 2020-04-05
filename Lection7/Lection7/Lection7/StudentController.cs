using System.Linq;
using System.Threading.Tasks;
using Lection7.Entities;
using Lection7.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lection7
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly Lection7DbContext _dbContext;

        public StudentController(Lection7DbContext dbContext)
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