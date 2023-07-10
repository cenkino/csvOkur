using System.ComponentModel.DataAnnotations;

namespace WebApplicationCenkY.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(30, ErrorMessage = "Username max 30 characters")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password required")]
        [MinLength(6, ErrorMessage = "Min 6 characters")]
        [MaxLength(16, ErrorMessage = "Max 30 characters")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "RePassword required")]
        [MinLength(6, ErrorMessage = "Min 6 characters")]
        [MaxLength(16, ErrorMessage = "Max 30 characters")]
        [Compare(nameof(Password))]
        public string RePassword { get; set; }
    }
}
