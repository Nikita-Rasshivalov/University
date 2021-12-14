using System;
using System.Collections.Generic;

#nullable disable

namespace Lab6.Models
{
    public partial class Group
    {
        public Group()
        {
            Performers = new HashSet<Performer>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Performer> Performers { get; set; }
    }
}
