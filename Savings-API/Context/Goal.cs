namespace Savings_API.Context
{
    public class Goal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        // Nawigacja do Savings (relacja jeden-do-wielu)
        public virtual ICollection<Saving> Savings { get; set; }
    }
}
