using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace Savings_API.Context;

public class Saving
{
    public int Id { get; set; }
    public DateTime InsertDate { get; set; }
    [MaxLength(250)]
    public required string Description { get; set; }
    [Min(1)]
    public required int Amount { get; set; }
    public DateOnly Date { get; set; }
    public int? UserId { get; set; }
    public required int GoalId { get; set; }

    public virtual ApplicationUser User { get; set; }
    public virtual Goal Goal { get; set; }
}
