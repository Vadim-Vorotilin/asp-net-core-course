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
    }
}