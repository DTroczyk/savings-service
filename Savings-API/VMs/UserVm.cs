using System.ComponentModel.DataAnnotations;

namespace Savings_API.VMs
{
    public class UserVm
    {
        public required int Id { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
    }
}
