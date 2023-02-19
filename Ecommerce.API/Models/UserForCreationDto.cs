using System;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.Models
{
    public class UserForCreationDto
    {
        
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}