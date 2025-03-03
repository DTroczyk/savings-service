using DataAnnotationsExtensions;
using Savings_API.Context;
using System.ComponentModel.DataAnnotations;

namespace Savings_API.VMs
{
    public class SavingVm
    {
        public int Id { get; set; }
        public DateTime InsertDate { get; set; }
        public required string Description { get; set; }
        public required int Amount { get; set; }
        public DateOnly Date { get; set; }
        public int? UserId { get; set; }
        public required int GoalId { get; set; }

        public string? UserName { get; set; }
        public required string GoalName { get; set; }
    }
}
