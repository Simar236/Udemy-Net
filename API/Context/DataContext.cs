using API.Entity;
using Microsoft.EntityFrameworkCore;

namespace API.Context
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<AppUser> Users { get; set; }
    }
}