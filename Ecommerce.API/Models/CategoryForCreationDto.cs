using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.Models
{
    public class CategoryForCreationDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}