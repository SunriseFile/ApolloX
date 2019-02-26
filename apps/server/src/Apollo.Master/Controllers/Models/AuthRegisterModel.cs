using System.ComponentModel.DataAnnotations;

namespace Apollo.Master.Controllers.Models
{
    public class AuthRegisterModel
    {
        [Required(ErrorMessage = "UserName is required")]
        [StringLength(64, ErrorMessage = "UserName cannot be longer than 64 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password cannot be less than 6 characters")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(64, ErrorMessage = "Full name cannot be longer than 64 characters")]
        public string FullName { get; set; }
    }
}
