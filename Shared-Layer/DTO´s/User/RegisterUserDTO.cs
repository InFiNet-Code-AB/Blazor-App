using System.ComponentModel.DataAnnotations;

namespace Shared_Layer.DTO_s.User
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessage = "Role is required.")]
        [RegularExpression(@"^(Teacher|Student)$", ErrorMessage = "Invalid role, you can choose between Teacher or Student.")]
        public string Role { get; set; } = string.Empty;

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50,ErrorMessage ="First can't be longer than 50 characters" )]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
