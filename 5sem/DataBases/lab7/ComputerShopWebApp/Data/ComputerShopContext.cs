using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerShop.WebUI.Data
{
    public class ComputerShopContext : DbContext
    {
        public ComputerShopContext()
            : base("name=ComputerShopConnectionString")
        { }

        public virtual DbSet<Models.ComponentType> ComponentTypes { get; set; }
        public virtual DbSet<Models.Component> Components { get; set; }
        public virtual DbSet<Models.Country> Countries { get; set; }
        public virtual DbSet<Models.Manufacturer> Manufacturers { get; set; }
    }
}
