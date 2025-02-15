using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Savings_API.DTOs
{
    public class AddOrEditSavingDto
    {
        [Required(ErrorMessage = "Goal is required.")]
        public required int GoalId { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(250, ErrorMessage = "Max length of goal name is 250.")]
        public required string Description { get; set; }
        [Required(ErrorMessage = "Amount field is required.")]
        [Min(0, ErrorMessage = "Amount cannot be less than 0.")]
        public required int Amount { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly Date { get; set; }
        public int? UserId { get; set; }
    }
}
