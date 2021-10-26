using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guides
{
    public class Guide
    {
        public Guide()
        {
        }
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public Organization Organization {get;set;}
        public List<Equipment> EquipmentList { get; set; } = new List<Equipment>();
    }
}
