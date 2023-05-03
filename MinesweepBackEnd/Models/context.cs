using Microsoft.EntityFrameworkCore;

namespace MinesweepBackEnd.Models
{
    public class MinesweeperContext : DbContext
    {
        public MinesweeperContext(DbContextOptions<MinesweeperContext> options) : base(options) { }

        public DbSet<Board> Boards { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
