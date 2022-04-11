using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RadiostationWeb.Models
{
    public class EditBroadcastViewModel
    {
        public int Id { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int RecordId { get; set; }
        [Required(ErrorMessage = "Data and time is required")]
        public DateTime? DateAndTime { get; set; }
        public IEnumerable<SelectListItem> EmployeeList { get; set; }
        public IEnumerable<SelectListItem> RecordList { get; set; }
    }
}
