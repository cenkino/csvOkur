using System.ComponentModel.DataAnnotations;

namespace WebApplicationCenkY.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Username is required")]
        [StringLength(30,ErrorMessage ="Username max 30 characters")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Password required")]
        [MinLength(6,ErrorMessage ="Min 6 characters")]
        [MaxLength(16,ErrorMessage ="Max 30 characters")]
        public string Password { get; set; }
    }
}
