using System;
using System.ComponentModel.DataAnnotations;

namespace RadiostationWeb.Models
{
    public partial class Record
    {
        public int Id { get; set; }
        [Required]
        public int PerformerId { get; set; }
        [Required]
        public int GenreId { get; set; }
        [Required]
        public string Album { get; set; }
        [Required]
        public DateTime RecordDate { get; set; }
        [Required]
        public int Lasting { get; set; }
        [Required]
        public decimal Rating { get; set; }
        [Required]
        public string СompositionName { get; set; }

    }
}
