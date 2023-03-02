using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.Models
{
    public class CategoryForUpdateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}