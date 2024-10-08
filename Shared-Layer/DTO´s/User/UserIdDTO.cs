using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Layer.DTO_s.User
{
    public class UserIdDTO
    {
        [Required]
        [StringLength(36, MinimumLength = 36)]
        public string Id { get; set; } = string.Empty;
    }
}
