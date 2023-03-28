using System.ComponentModel.DataAnnotations;

namespace BloodManagement.Models
{
    public class AddmoreModel
    {
        [Required(ErrorMessage = "Required.")]
        public string DoB { get; set; }
        [Required(ErrorMessage = "Required.")]
        public string Bloodgroup { get; set; }
        [Required(ErrorMessage = "Required.")]
        public long mobile { get; set; }
        [Required(ErrorMessage = "Required.")]
        public string address1 { get; set; }
        [Required(ErrorMessage = "Required.")]
        public string district { get; set; }
        [Required(ErrorMessage = "Required.")]
        public string states { get; set; }

        [Required(ErrorMessage = "Required.")]
        public int pincode { get; set; }
    }
}
