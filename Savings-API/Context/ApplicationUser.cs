using Microsoft.AspNetCore.Identity;

namespace Savings_API.Context;

public class ApplicationUser : IdentityUser<Guid>
{
    public virtual ICollection<Saving> Savings { get; set; }
    public virtual ICollection<Goal> Goals { get; set; }
}
