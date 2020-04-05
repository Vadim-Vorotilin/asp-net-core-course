using Lection7.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lection7
{
    public class Lection7DbContext : IdentityDbContext<MyUser>
    {
        public Lection7DbContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<TeacherEntity> Teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<StudentTeacherEntity>()
                   .HasKey(st => new { st.StudentId, st.TeacherName });

            builder.Entity<StudentTeacherEntity>()
                   .HasOne(st => st.Student)
                   .WithMany(s => s.Teachers)
                   .HasForeignKey(st => st.StudentId);

            builder.Entity<StudentTeacherEntity>()
                   .HasOne(st => st.Teacher)
                   .WithMany(s => s.Students)
                   .HasForeignKey(st => st.TeacherName);
        }
    }
}