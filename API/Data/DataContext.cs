using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        //dbSet represents a table inside my database
        public DbSet<AppUser> Users { get; set; }
    }
}