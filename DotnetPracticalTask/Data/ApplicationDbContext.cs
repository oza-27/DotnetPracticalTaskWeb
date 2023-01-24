using DotnetPracticalTask.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetPracticalTask.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> category { get; set; }

    }
}
