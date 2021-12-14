using System;
using System.Collections.Generic;

#nullable disable

namespace RadiostationApp.Models
{
    public partial class Employee
    {
        public Employee()
        {
            BroadcastSchedules = new HashSet<BroadcastSchedule>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Middlename { get; set; }

        public virtual ICollection<BroadcastSchedule> BroadcastSchedules { get; set; }
    }
}
