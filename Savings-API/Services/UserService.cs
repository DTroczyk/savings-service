using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Savings_API.Context;
using Savings_API.VMs;

namespace Savings_API.Services
{
    public interface IUserService
    {
        public IList<UserVm> GetUsers();
    }

    public class UserService(AppDbContext _dbContext, IMapper _mapper) : BaseService(_dbContext), IUserService
    {

        public IList<UserVm> GetUsers()
        {
            var users = _dbContext.ApplicationUsers.AsNoTracking().ToList();
            var userVms = _mapper.Map<List<UserVm>>(users);

            return userVms;
        }
    }
}
