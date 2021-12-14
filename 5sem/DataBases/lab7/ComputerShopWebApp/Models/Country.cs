using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ComputerShop.WebUI.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public virtual ICollection<Component> Components { get; set; }
    }
}
