using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MinesweepBackEnd.Models
{
    public class Board
    {
        [Key]
        public long Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string? Mines { get; set; }
    }
}
