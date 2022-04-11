using System.ComponentModel.DataAnnotations;

namespace RadiostationWeb.Models
{
    public partial class Genre
    {
        public int Id { get; set; }
        [Required]
        public string GenreName { get; set; }
        [Required]
        public string Description { get; set; }

    }
}
