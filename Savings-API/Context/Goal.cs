using Savings_API.Enums;

namespace Savings_API.Context
{
    public class Goal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid OwnerId { get; set; }
        public EntityStatusEnum Status { get; set; }

        public virtual ICollection<Saving> Savings { get; set; }
        public virtual ApplicationUser Owner { get; set; }
    }
}
