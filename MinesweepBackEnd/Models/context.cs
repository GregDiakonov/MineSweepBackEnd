using Microsoft.EntityFrameworkCore;

namespace MinesweepBackEnd.Models
{
    public class MineContext : DbContext
    {
        public DbSet<Mines> Mines => Set<Mines>();

        public MineContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server = GREGBOOK\SQLEXPRESS;Database=Minesweeper;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
