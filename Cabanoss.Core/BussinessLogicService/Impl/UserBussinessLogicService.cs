using Cabanoss.Core.Data.Entities;
using Cabanoss.Core.Repositories;

namespace Cabanoss.Core.BussinessLogicService.Impl
{
    public class UserBussinessLogicService : IUserBussinessLogicService
    {
        private IUserBaseRepository _userBase;

        public UserBussinessLogicService(IUserBaseRepository userBase)
        {
            _userBase = userBase;
        }

        public async System.Threading.Tasks.Task AddUser(User user)
        {
            await _userBase.AddAsync(user);
        }
    }
}
