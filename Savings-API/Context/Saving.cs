namespace Savings_API.Context;

public class Saving
{
    public int Id { get; set; }
    public DateTime InsertDate { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
    public DateTime? Date { get; set; }
    public Guid? UserId { get; set; }
    public int? GoalId { get; set; }

    public virtual ApplicationUser User { get; set; }

    public virtual Goal Goal { get; set; }
}
