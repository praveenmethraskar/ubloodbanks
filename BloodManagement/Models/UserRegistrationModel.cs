using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web;
namespace BloodManagement.Models
{
    public class UserRegistrationModel
    {
        public int users_id { get; set; } 
        public string fullname { get; set; }

        //[Required(ErrorMessage = "Required.")]
        //[EmailAddress(ErrorMessage = "Invalid email address.")]
        public string mail { get; set; }

        //[Required(ErrorMessage = "Required.")]
        //[Compare("Password", ErrorMessage = "Passwords do not match.")]
        //[StringLength(150, MinimumLength = 6)]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Required.")]
        //[Compare("Password", ErrorMessage = "Passwords do not match.")]
        //[StringLength(150, MinimumLength = 6)]
        public string ConfirmPassword { get; set; }



    }
}
