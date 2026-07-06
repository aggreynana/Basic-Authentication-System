using BasicAuth.Context;
using BasicAuth.Entities;
using BasicAuth.Storage.Repository.Interfaces;

namespace BasicAuth.Storage.Repository.Providers;

public class UserEntityRepository : IUserEntityRepository
{
    private readonly ApplicationDbContext _context;

    public UserEntityRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<bool> AddUserAsync(UserEntity user)
    {
        await _context.AddAsync(user);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<UserEntity?> GetUserByIdAsync(string id)
    {
        return await _context.Users.FindAsync(id);
        
    }
}
