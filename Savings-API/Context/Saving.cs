namespace Savings_API.Context
{
    public class Saving
    {
        public int Id { get; set; }
        public required DateTime InsertDate { get; set; }
        public required string Goal { get; set; }
        public required string Description { get; set; }
        public required int Amount { get; set; }
        public DateOnly? Date { get; set; }
    }
}
