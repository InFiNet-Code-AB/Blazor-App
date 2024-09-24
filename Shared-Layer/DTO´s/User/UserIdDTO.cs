using System.ComponentModel.DataAnnotations;

namespace Shared_Layer.DTO_s.User
{
    public class UserIdDTO
    {
        [Required(ErrorMessage = "User ID is required.")]
        [RegularExpression(@"^[A-Fa-f0-9\-]{36}$", ErrorMessage = "The ID format is incorrect.")]
        public string Id { get; set; }
    }
}
