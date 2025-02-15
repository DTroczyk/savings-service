using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Savings_API.Context;

public class ApplicationUser
{
    public required int Id { get; set; }
    [MaxLength(50)]
    public required string Firstname { get; set; }
    [MaxLength(50)]
    public required string Lastname { get; set; }
    public required DateTime InsertDate { get; set; }

    public virtual ICollection<Saving> Savings { get; set; }
}
