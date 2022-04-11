using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RadiostationWeb.Models
{
    public class CreateBroadcastViewModel
    {
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int RecordId { get; set; }
        [Required(ErrorMessage = "Date and time is requred")]
        public DateTime? DateAndTime { get; set; }

        public IEnumerable<SelectListItem> EmployeeList { get; set; }
        public IEnumerable<SelectListItem> RecordList { get; set; }
    }
}
