using System.ComponentModel.DataAnnotations;


namespace RadiostationWeb.Models
{
    public class RegistrationViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Username is requred")]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$", ErrorMessage = "Easy password!")]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Required repeat password")]
        [Compare("Password", ErrorMessage = "Repeat must match")]
        public string RepeatPassword { get; set; } = "";

        [Required(ErrorMessage = "Email")]
        [EmailAddress(ErrorMessage = "Incorrect email")]
        public string Email { get; set; } = "";
    }
}
