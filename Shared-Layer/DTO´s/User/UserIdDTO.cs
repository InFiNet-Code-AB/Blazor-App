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
        [Required (ErrorMessage ="User ID is Required")]
        [StringLength(36, MinimumLength = 36, ErrorMessage ="User ID must be exacly 36 characters long.")]
        //Reguljära uttryck, 8-4-4-4-12 hexadecimal digits
        [RegularExpression("^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$",
            ErrorMessage = "User ID must be a valid UUID (e.x = bc3b4aa2-XXXX-XXXX-XXXX-58s42t102938).")]
        public string Id { get; set; } = string.Empty;
    }
}
