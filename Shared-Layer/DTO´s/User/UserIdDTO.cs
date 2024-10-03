using System.ComponentModel.DataAnnotations;

namespace Shared_Layer.DTO_s.User
{
    public class UserIdDTO
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

    }
}
