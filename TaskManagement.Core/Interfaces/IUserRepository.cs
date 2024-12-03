using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Domain.Entities;

namespace TaskManagement.Core.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// implemented in service to get a user by UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<User> GetUserByIdAsync(Guid userId);

        /// <summary>
        ///  implemented in service to get a user by UserEmail
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<User> GetUserByEmailAsync(string email);

        /// <summary>
        ///  implemented in service for adding a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> AddUserAsync(User user);
    }
}
