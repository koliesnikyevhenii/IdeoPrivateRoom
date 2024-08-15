using IdeoPrivateRoom.WebApi.Data;
using IdeoPrivateRoom.WebApi.Data.Entities;
using IdeoPrivateRoom.WebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdeoPrivateRoom.WebApi.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;
    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Add(User user)
    {
        var createdUser = await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return createdUser.Entity.Id;
    }

    public async Task<List<User>> Get()
    {
        var users = await _dbContext.Users.ToListAsync();

        return users;
    }
}
