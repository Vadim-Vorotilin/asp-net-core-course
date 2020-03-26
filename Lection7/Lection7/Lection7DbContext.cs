using Microsoft.EntityFrameworkCore;

namespace Lection7
{
    public class Lection7DbContext : DbContext
    {
        public Lection7DbContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Student> Students { get; set; }
    }
}