using Savings_API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Savings_API.Context
{
    public class Goal
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public required string Name { get; set; }
        [MaxLength(250)]
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? OwnerId { get; set; }
        public EntityStatusEnum Status { get; set; }

        public virtual ICollection<Saving> Savings { get; set; }
        public virtual ApplicationUser Owner { get; set; }
    }
}
