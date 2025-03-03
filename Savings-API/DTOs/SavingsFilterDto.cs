public class SavingsFilterDto
{
    public DateOnly? DateFrom { get; set; } = DateOnly.FromDateTime(DateTime.Today.AddMonths(-3));
    public DateOnly? DateTo { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    public int? UserId { get; set; }
    public int? GoalId { get; set; }
    public string? Description { get; set; }
}