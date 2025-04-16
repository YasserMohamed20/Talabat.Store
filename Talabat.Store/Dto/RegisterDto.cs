using System.ComponentModel.DataAnnotations;

namespace Talabat.Store.Dto
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email {  get; set; }
        [Required]
        public string Name {  get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=^.*[!@#$%^&amp;*()_+]).*$"
            , ErrorMessage ="Password must be contains 1 Uppercase ,1 Lowercase ,1 Digit ,1 Special Characters")]
        public string Password { get; set; }
    }
}