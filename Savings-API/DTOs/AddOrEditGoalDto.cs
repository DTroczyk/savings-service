using System.ComponentModel.DataAnnotations;

namespace Savings_API.DTOs
{
    public class AddOrEditGoalDto
    {
        [MaxLength(50, ErrorMessage = "Maximum goal name length is 50 characters.")]
        [MinLength(3, ErrorMessage = "Minimal goal name length is 3 characters.")]
        [Required(ErrorMessage = "Goal name is required.")]
        public required string Name { get; set; }
        [MaxLength(250, ErrorMessage = "Maximum goal description length is 250 characters.")]
        public string? Description { get; set; }
    }
}
