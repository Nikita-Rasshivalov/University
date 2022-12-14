namespace PersonalAPI.Models
{
    public class Personal
    {
        public int PersonId { get; set; }

        public required string PersonName { get; set; }

        public required string PersonSurname { get; set; }

        public required int DepartmentNumber { get; set; }

        public required string Speciality { get; set; }
 
    }
}