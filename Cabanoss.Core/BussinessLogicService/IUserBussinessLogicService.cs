using Cabanoss.Core.Data.Entities;

namespace Cabanoss.Core.BussinessLogicService
{
    public interface IUserBussinessLogicService
    {
        System.Threading.Tasks.Task AddUser(User user);
    }
}