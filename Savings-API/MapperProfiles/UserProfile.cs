namespace Savings_API.MapperProfiles
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Savings_API.VMs;

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<IdentityUser, UserVm>();
        }
    }
}
