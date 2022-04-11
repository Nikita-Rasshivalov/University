using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RadiostationWeb.Models
{
    public class EditPerformerViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public int? GroupId { get; set; }
        public IEnumerable<SelectListItem> GroupsList { get; set; }
    }
}
