using System.ComponentModel.DataAnnotations;

namespace server.Data
{
    public class CreateUserRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string email { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string fname { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string lname { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string password { get; set; }
    }
}