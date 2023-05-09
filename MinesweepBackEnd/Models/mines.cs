using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinesweepBackEnd.Models
{
    [Table("Mines")]
    public class Mines
    {
        [Key]
        public int Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int StartHeight { get; set; }

        public int StartWidth { get; set; }

        public string Layout { get; set; }

        public Mines(int id, int width, int height, int startHeight, int startWidth, string layout)
        {
            Id = id;
            Width = width;
            Height = height;
            StartHeight = startHeight;
            StartWidth = startWidth;
            Layout = layout;
        }
    }
}
