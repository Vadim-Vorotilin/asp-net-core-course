using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gateway
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("This is Gateway!");
                });

                endpoints.MapControllers();
            });
        }

        // private void Examples(Lection7DbContext dbContext)
        // {
        //     var mathTeacher = new TeacherEntity
        //     {
        //         Name = "Dawson",
        //         Discipline = "Math"
        //     };
        //     
        //     var alex = new StudentEntity
        //     {
        //         Name = "Alex",
        //         Score = 100
        //     };
        //     
        //     var oleg = new StudentEntity
        //     {
        //         Name = "Oleg",
        //         Score = 90
        //     };
        //     
        //     dbContext.AddRange(alex, oleg, mathTeacher);
        //     
        //     mathTeacher.Students = new[]
        //     {
        //         new StudentTeacherEntity { Teacher = mathTeacher, Student = alex },
        //         new StudentTeacherEntity { Teacher = mathTeacher, Student = oleg }
        //     };
        //     
        //     var biologyTeacher = new TeacherEntity
        //     {
        //         Name = "Alla Pallna",
        //         Discipline = "Biology"
        //     };
        //     
        //     var geographyTeacher = new TeacherEntity
        //     {
        //         Name = "Viktor Petrovich",
        //         Discipline = "Geography"
        //     };
        //     
        //     var vadim = new StudentEntity { Name = "Vadim" };
        //     var ira = new StudentEntity { Name = "Ira" };
        //     
        //     dbContext.AddRange(vadim, ira, geographyTeacher, biologyTeacher);
        //     
        //     vadim.Teachers = new[]
        //     {
        //         new StudentTeacherEntity { TeacherName = "Dawson" },
        //         new StudentTeacherEntity { Teacher = biologyTeacher },
        //         new StudentTeacherEntity { Teacher = geographyTeacher }
        //     };
        //     
        //     ira.Teachers = new[]
        //     {
        //         new StudentTeacherEntity { Teacher = biologyTeacher }
        //     };
        //     
        //     dbContext.SaveChanges();
        //     
        //     Console.WriteLine(dbContext.Students
        //                                .AsNoTracking()
        //                                .Where(s => s.Name == "Vadim")
        //                                .Include(s => s.Teachers)
        //                                .Select(s => s.Teachers.Select(st => st.TeacherName))
        //                                .First()
        //                                .Aggregate((s1, s2) => $"{s1}, {s2}"));
        //
        //     Console.WriteLine("===================================");
        //     Console.WriteLine(dbContext.Students
        //                                .AsNoTracking()
        //                                .Include(s => s.Teachers)
        //                                .Select(s => new { student = s, teachers = s.Teachers.Select(t => t.TeacherName) })
        //                                .ToArray()
        //                                .Select(s => $"{s.student.Name} : {s.teachers.Aggregate((s1, s2) => $"{s1}, {s2}")}")
        //                                .Aggregate((s1, s2) => $"{s1}\n{s2}"));
        //     
        //     Console.WriteLine(dbContext.Teachers
        //                                .AsNoTracking()
        //                                .Include(t => t.Students)
        //                                .ThenInclude(st => st.Student)
        //                                .Select(t => new { teacher = t, students = t.Students.Select(s => s.Student.Name) })
        //                                .ToArray()
        //                                .Select(s => $"{s.teacher.Name} : {s.students.Aggregate((s1, s2) => $"{s1}, {s2}")}")
        //                                .Aggregate((s1, s2) => $"{s1}\n{s2}"));
        // }
    }
}
