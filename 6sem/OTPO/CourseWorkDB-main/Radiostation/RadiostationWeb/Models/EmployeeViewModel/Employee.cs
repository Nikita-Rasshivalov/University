namespace RadiostationWeb.Models
{
    public partial class Employee
    {
        public int Id { get; set; }

        public string AspNetUserId { get; set; }
        public int? PositionId { get; set; }

        public string Education { get; set; }
        public int? WorkTime { get; set; }


    }
}
