using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace Savings_API.DTOs
{
    public class AddSavingDto
    {
        [Required]
        public required string Goal { get; set; }
        [Required]
        public required string Description { get; set; }
        [Required]
        [Min(0)]
        public required int Amount { get; set; }
        public DateTime? Date { get; set; }
    }
}
