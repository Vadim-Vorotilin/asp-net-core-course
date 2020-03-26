using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Lection7
{
    public class StudentsMiddleware
    {
        private readonly RequestDelegate _next;

        public StudentsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, Lection7DbContext dbContext)
        {
            // /student/add?name=Alex&score=100
            if (context.Request.Path.Value.Substring(9).Equals("add") &&
                context.Request.QueryString.HasValue)
            {
                var query = context.Request.QueryString.Value.Substring(1);    // name=Alex&score=100

                var dict = query.Split('&') // [ "name=Alex", "score=100" ]
                                .Select(s => s.Split('=')) // [ ["name", "Alex" ], [ "score", "100" ] ]
                                .ToDictionary(s => s[0], s => s[1]); // { { "name" : "Alex" }, { "score" : "100" } }

                if (!dict.ContainsKey("name") ||
                    !dict.ContainsKey("score"))
                    throw new ValidationException("Name or score not provided");
                
                var student = new Student { Name = dict["name"], Score = int.Parse(dict["score"]) };
                
                if (dict.ContainsKey("teacherName"))
                    student.Teacher = new Teacher
                    {
                        Name = dict["teacherName"],
                        Discipline = dict.ContainsKey("discipline") ? dict["discipline"] : null
                    };

                var entity = dbContext.Students.Add(student);

                await dbContext.SaveChangesAsync();

                await context.Response.WriteAsync(entity.Entity.Id.ToString());
            }
            else
            {
                await _next(context);
            }
        }
    }
}