using System.ComponentModel.DataAnnotations;

namespace RadiostationWeb.Models
{
    public  class Group
    {

        public int Id { get; set; }
        [Required]
        public string Description { get; set; }

    }
}
