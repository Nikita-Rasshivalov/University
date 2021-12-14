using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ComputerShop.WebUI.Models
{
    public class Component
    {
        public int ComponentId { get; set; }
        public int ComponentTypeId { get; set; }
        public string ComponentModel { get; set; }
        
        
        public int ComponentManufacturerId { get; set; }
        
        
        public int ComponentCountryId { get; set; }
        
        
        
        public DateTime ComponentReleaseDate { get; set; }
        
        
        
        public string ComponentCharacteristics { get; set; }
        
        
        
        public int ComponentWarrantyInMonths { get; set; }
        
        
        
        public string ComponentDescription { get; set; }
        
        
        
        public decimal ComponentPrice { get; set; }

        public virtual Country Country { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual ComponentType ComponentType { get; set; }
    }
}
