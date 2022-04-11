using System;
namespace RadiostationWeb.Models
{
    public class BroadcastViewModel
    {
        public int Id { get; set; }
        public DateTime? DateAndTime { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public string RecordName { get; set; }
    }
}
