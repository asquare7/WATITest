

using Azure;
using CodeTest02.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeTest02.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
        
        }

        public DbSet<Activity> Activities { get; set; }
    }
}
