using System;
using System.Linq;
using System.Text;
using Lection7.Entities;
using Lection7.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Lection7
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Lection7DbContext>(options =>
             {
                 options.UseNpgsql(_configuration.GetConnectionString("MyConnectionString"));
             });

            services.AddMvc();
            services.AddControllers();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 10;
            });
            
            services.Configure<PasswordHasherOptions>(option =>
            {
                option.IterationCount = 120000;
            });

            services.AddIdentity<MyUser, IdentityRole>()
                    .AddEntityFrameworkStores<Lection7DbContext>()
                    .AddDefaultTokenProviders();

            services.AddSingleton<JwtService>();
            
            var secret = _configuration.GetSection("JwtConfig" +
                                                   ":Secret").Value;  
            var key = Encoding.ASCII.GetBytes(secret);
            
            services.AddAuthentication(x =>
                                       {
                                           x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                           x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                                       })
                    .AddJwtBearer(x =>
                                  {
                                      x.TokenValidationParameters = new TokenValidationParameters
                                      {
                                          IssuerSigningKey = new SymmetricSecurityKey(key),
                                          ValidateIssuer = false,
                                          ValidateAudience = false
                                      };
                                  });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Lection7DbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.MapWhen(context => context.Request.Path.HasValue &&
                                   context.Request.Path.Value.StartsWith("/student-mw/"),
                        a => a.UseMiddleware<StudentsMiddleware>());

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Lection 8!");
                });

                endpoints.MapControllers();
            });
            
            Examples(dbContext);
        }

        private void Examples(Lection7DbContext dbContext)
        {
            var mathTeacher = new TeacherEntity
            {
                Name = "Dawson",
                Discipline = "Math"
            };
            
            var alex = new StudentEntity
            {
                Name = "Alex",
                Score = 100
            };
            
            var oleg = new StudentEntity
            {
                Name = "Oleg",
                Score = 90
            };
            
            dbContext.AddRange(alex, oleg, mathTeacher);
            
            mathTeacher.Students = new[]
            {
                new StudentTeacherEntity { Teacher = mathTeacher, Student = alex },
                new StudentTeacherEntity { Teacher = mathTeacher, Student = oleg }
            };
            
            var biologyTeacher = new TeacherEntity
            {
                Name = "Alla Pallna",
                Discipline = "Biology"
            };
            
            var geographyTeacher = new TeacherEntity
            {
                Name = "Viktor Petrovich",
                Discipline = "Geography"
            };
            
            var vadim = new StudentEntity { Name = "Vadim" };
            var ira = new StudentEntity { Name = "Ira" };
            
            dbContext.AddRange(vadim, ira, geographyTeacher, biologyTeacher);
            
            vadim.Teachers = new[]
            {
                new StudentTeacherEntity { TeacherName = "Dawson" },
                new StudentTeacherEntity { Teacher = biologyTeacher },
                new StudentTeacherEntity { Teacher = geographyTeacher }
            };
            
            ira.Teachers = new[]
            {
                new StudentTeacherEntity { Teacher = biologyTeacher }
            };
            
            dbContext.SaveChanges();
            
            Console.WriteLine(dbContext.Students
                                       .AsNoTracking()
                                       .Where(s => s.Name == "Vadim")
                                       .Include(s => s.Teachers)
                                       .Select(s => s.Teachers.Select(st => st.TeacherName))
                                       .First()
                                       .Aggregate((s1, s2) => $"{s1}, {s2}"));

            Console.WriteLine("===================================");
            Console.WriteLine(dbContext.Students
                                       .AsNoTracking()
                                       .Include(s => s.Teachers)
                                       .Select(s => new { student = s, teachers = s.Teachers.Select(t => t.TeacherName) })
                                       .ToArray()
                                       .Select(s => $"{s.student.Name} : {s.teachers.Aggregate((s1, s2) => $"{s1}, {s2}")}")
                                       .Aggregate((s1, s2) => $"{s1}\n{s2}"));
            
            Console.WriteLine(dbContext.Teachers
                                       .AsNoTracking()
                                       .Include(t => t.Students)
                                       .ThenInclude(st => st.Student)
                                       .Select(t => new { teacher = t, students = t.Students.Select(s => s.Student.Name) })
                                       .ToArray()
                                       .Select(s => $"{s.teacher.Name} : {s.students.Aggregate((s1, s2) => $"{s1}, {s2}")}")
                                       .Aggregate((s1, s2) => $"{s1}\n{s2}"));
        }
    }
}
