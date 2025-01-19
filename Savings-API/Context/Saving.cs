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
    public DateTime Date { get; set; }
    public Guid? UserId { get; set; }
    public required int GoalId { get; set; }

    public required virtual ApplicationUser User { get; set; }

    public required virtual Goal Goal { get; set; }
}
