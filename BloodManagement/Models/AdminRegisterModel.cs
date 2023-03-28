using System.ComponentModel.DataAnnotations;

namespace BloodManagement.Models
{
    public class AdminRegisterModel
    {
        public int admin_id { get; set; }

        [Required(ErrorMessage = "Required.")]
        public string fullname { get; set; }


        [Required(ErrorMessage = "Required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string mail { get; set; }

        [Required(ErrorMessage = "Required.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [StringLength(150, MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Required.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [StringLength(150, MinimumLength = 6)]
        public string ConfirmPassword { get; set; }

    }
}
