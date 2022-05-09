using System.ComponentModel.DataAnnotations;

namespace WarehouseReport.Api.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{4,}$",
            ErrorMessage = "Minimum four characters, at least one uppercase letter, one lowercase letter, " +
                           "one number and one special character")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}