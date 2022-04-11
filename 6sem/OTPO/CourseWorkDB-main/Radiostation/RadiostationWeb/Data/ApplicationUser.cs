using Microsoft.AspNetCore.Identity;
namespace RadiostationWeb.Data
{
    public class ApplicationUser:IdentityUser
    {
        [PersonalData]
        public string Name { get; set; }
        [PersonalData]
        public string Surname { get; set; }
        [PersonalData]
        public string MiddleName { get; set; }

    }
}
