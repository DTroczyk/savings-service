using Savings_API.Context;
using Savings_API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Savings_API.VMs
{
    public class GoalVm
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public EntityStatusEnum Status { get; set; }
    }
}
