using System;
using System.ComponentModel.DataAnnotations;

namespace RadiostationWeb.Models
{
    public partial class Broadcast
    {
        public int Id { get; set; }
        [Required]
        public DateTime? DateAndTime { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int RecordId { get; set; }

    }
}
