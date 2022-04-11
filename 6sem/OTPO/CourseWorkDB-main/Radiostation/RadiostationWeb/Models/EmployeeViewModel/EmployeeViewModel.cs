namespace RadiostationWeb.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string Username { get; set; }

        public string AspNetUserId { get; set; }

        public string PositionName { get; set; }

        public string Education { get; set; }
        public int? WorkTime { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
    }
}
