using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RadiostationWeb.Models
{
    public  class Performer
    {

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public int? GroupId { get; set; }

    }
}
