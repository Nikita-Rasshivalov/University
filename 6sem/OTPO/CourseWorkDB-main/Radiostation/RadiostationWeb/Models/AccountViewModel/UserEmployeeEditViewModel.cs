using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace RadiostationWeb.Models
{
    public class UserEmployeeEditViewModel
    {
        public string Id { get; set; }
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Username is requred")]
        public string UserName { get; set; } = "";

        [Required(ErrorMessage = "Name is requred")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Surname is requred")]
        public string Surname { get; set; } = "";

        public string MiddleName { get; set; } = "";

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Incorrect email")]
        public string Email { get; set; } = "";
        [Required(ErrorMessage = "Position is required")]
        public int? PositionId { get; set; }
        public string Education { get; set; }
        public bool EmployeeRole { get; set; }
        [Range(1, 12, ErrorMessage = "The value must be in the limit from 1 to 12")]
        public int? WorkTime { get; set; }
        public IEnumerable<SelectListItem> PositionList { get; set; }
        public IEnumerable<SelectListItem> EducationList { get; set; }
    }
}
