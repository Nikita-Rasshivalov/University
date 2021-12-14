using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ComputerShop.WebUI.Models
{
    public class Manufacturer
    {
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }

        public virtual ICollection<Component> Components { get; set; }
    }
}
