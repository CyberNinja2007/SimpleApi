using Microsoft.EntityFrameworkCore;
using SimpleApi.Models;

namespace SimpleApi.Data
{
    public class CitizenContext : DbContext
    {
        public DbSet<Citizen> Citizens { get; set; }
        public CitizenContext(DbContextOptions<CitizenContext> options)
            : base(options)
        { 
            Database.EnsureCreated();
        }
    }
}