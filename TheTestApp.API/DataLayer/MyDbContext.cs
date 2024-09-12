using Microsoft.EntityFrameworkCore;
using TheTestApp.API.Models;

namespace TheTestApp.API.DataLayer
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) 
            : base(options) 
        { 
        }

        public DbSet<StudyGroup> StudyGroups { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
