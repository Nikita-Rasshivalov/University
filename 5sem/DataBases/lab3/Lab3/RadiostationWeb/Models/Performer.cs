using System;
using System.Collections.Generic;

#nullable disable

namespace RadiostationWeb.Models
{
    public partial class Performer
    {
        public Performer()
        {
            Records = new HashSet<Record>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? GroupId { get; set; }

        public virtual Group Group { get; set; }
        public virtual ICollection<Record> Records { get; set; }
    }
}
