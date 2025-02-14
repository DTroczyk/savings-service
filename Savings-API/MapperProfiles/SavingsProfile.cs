namespace Savings_API.MapperProfiles
{
    using AutoMapper;
    using Savings_API.Context;
    using Savings_API.VMs;

    public class SavingsProfile : Profile
    {
        public SavingsProfile()
        {
            CreateMap<Saving, SavingVm>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Firstname + " " + src.User.Lastname))
                .ForMember(dest => dest.GoalName, opt => opt.MapFrom(src => src.Goal.Name));

        }
    }
}
