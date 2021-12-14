using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ComputerShop.WebUI.Models
{
    public class ComponentType
    {

        public int ComponentTypeId { get; set; }
        public string ComponentTypeName { get; set; }
        public string ComponentTypeDescription { get; set; }

        public virtual ICollection<Component> Components { get; set; }
    }
}
