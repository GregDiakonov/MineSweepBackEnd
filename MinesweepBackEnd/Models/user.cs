using System.ComponentModel.DataAnnotations;

namespace MinesweepBackEnd.Models
{
    public class User
    {
        [Key]
        public string Login { get; set; }
        public string Password { get; set; }
        public long BoardId { get; set; }
    }
}
