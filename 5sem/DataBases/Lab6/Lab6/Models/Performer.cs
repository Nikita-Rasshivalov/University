using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#nullable disable

namespace Lab6.Models
{
    public partial class Performer
    {
        public Performer()
        {
            Records = new HashSet<Record>();
        }
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? GroupId { get; set; }

        public virtual Group Group { get; set; }
        public virtual ICollection<Record> Records { get; set; }
    }
}
