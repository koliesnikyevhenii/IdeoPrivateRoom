using IdeoPrivateRoom.DAL.Data.Entities;
using IdeoPrivateRoom.DAL.Data;
using IdeoPrivateRoom.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdeoPrivateRoom.DAL.Repositories;
public class UserRepository(ApplicationDbContext _dbContext) : IUserRepository
{
    public async Task<Guid> Create(UserEntity user)
    {
        var createdUser = await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return createdUser.Entity.Id;
    }

    public async Task<List<UserEntity>> Get()
    {
        return await _dbContext.Users
            .AsNoTracking()
            .Include(u => u.RoleMappings)
                .ThenInclude(rm => rm.Role)
            .ToListAsync();
    }

    public async Task<UserEntity?> Get(Guid id)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .Where(u => u.Id == id)
            .Include(u => u.RoleMappings)
                .ThenInclude(rm => rm.Role)
            .FirstOrDefaultAsync();
    }

    public async Task<Guid?> Update(Guid id, UserEntity user)
    {
        await _dbContext.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(u => u
                .SetProperty(p => p.Email, p => user.Email)
                .SetProperty(p => p.FirstName, p => user.FirstName)
                .SetProperty(p => p.LastName, p => user.LastName)
                .SetProperty(p => p.UpdatedDate, p => user.UpdatedDate)
                .SetProperty(p => p.CreatedDate, p => user.CreatedDate)
                .SetProperty(p => p.PasswordHash, p => user.PasswordHash)
                .SetProperty(p => p.IsEmailConfirmed, p => user.IsEmailConfirmed));

        return user.Id;
    }

    public async Task<Guid?> Delete(Guid id)
    {
        await _dbContext.Users
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
