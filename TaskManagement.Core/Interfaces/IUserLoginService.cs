using System.Threading.Tasks;
using TaskManagement.Core.DTOs;

namespace TaskManagement.Core.Interfaces
{
    public interface IUserLoginService
    {
        //userlogin
        Task<string> LoginAsync(UserLoginRequest request); 
    }
}
