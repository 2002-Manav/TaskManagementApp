using TaskManagement.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Infrastructure.DatabaseContext;
using TaskManagement.Core.Domain.Entities;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get a user by UserId
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<User> GetUserByIdAsync(Guid userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    /// <summary>
    /// get a user by UserEmail
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == email);
    }

    /// <summary>
    /// Adds a new user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<bool> AddUserAsync(User user)
    {
        _context.Users.Add(user);
        return await _context.SaveChangesAsync() > 0;
    }
}
