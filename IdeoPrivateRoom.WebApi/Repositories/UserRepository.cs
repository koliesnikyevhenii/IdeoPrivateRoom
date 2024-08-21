using AutoMapper;
using IdeoPrivateRoom.DAL.Data;
using IdeoPrivateRoom.DAL.Data.Entities;
using IdeoPrivateRoom.WebApi.Models.Dtos;
using IdeoPrivateRoom.WebApi.Models.Enums;
using IdeoPrivateRoom.WebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdeoPrivateRoom.WebApi.Repositories;

public class UserRepository(ApplicationDbContext _dbContext, IMapper _mapper) : IUserRepository
{
    public async Task<Guid> Create(UserDto user)
    {
        var createdUser = await _dbContext.Users.AddAsync(_mapper.Map<UserEntity>(user));
        await _dbContext.SaveChangesAsync();

        return createdUser.Entity.Id;
    }

    public async Task<List<UserDto>> Get()
    {
        return await _dbContext.Users
            .AsNoTracking()
            .Include(u => u.RoleMappings)
                .ThenInclude(rm => rm.Role)
            .Select(u => new UserDto
            {
                Id = u.Id,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                UserIcon = u.UserIcon,
                IsEmailConfirmed = u.IsEmailConfirmed,
                CreatedDate = u.CreatedDate,
                UpdatedDate = u.UpdatedDate,
                Roles = u.RoleMappings.Select(r => new RoleDto
                {
                    Id = r.Role.Id,
                    Name = r.Role.Name,
                    CreatedDate = r.Role.CreatedDate,
                    UpdatedDate = r.Role.UpdatedDate
                }),
            }).ToListAsync();
    }

    public async Task<UserDto> Get(Guid id)
    {
        var user = await _dbContext.Users
            .AsNoTracking()
            .Where(u => u.Id == id)
            .Include(u => u.RoleMappings)
                .ThenInclude(rm => rm.Role)
            .FirstOrDefaultAsync();

        var result = new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserIcon = user.UserIcon,
            IsEmailConfirmed = user.IsEmailConfirmed,
            CreatedDate = user.CreatedDate,
            UpdatedDate = user.UpdatedDate,
            Roles = user.RoleMappings.Select(r => new RoleDto
            {
                Id = r.Role.Id,
                Name = r.Role.Name,
                CreatedDate = r.Role.CreatedDate,
                UpdatedDate = r.Role.UpdatedDate
            })
        };

        return result;
    }

    public async Task<Guid?> Update(Guid id, UserDto user)
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

    public async Task<List<UserDtoTest>> GetAll()
    {
        return await _dbContext.Users
            .Include(u => u.RoleMappings)
                .ThenInclude(urm => urm.Role)
            .Include(u => u.VocationRequests)
            .Include(u => u.LinkedUsers)
                .ThenInclude(lu => lu.AssociatedUser)
            .Select(u => new UserDtoTest
            {
                Id = u.Id,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                UserIcon = u.UserIcon,
                IsEmailConfirmed = u.IsEmailConfirmed,
                CreatedDate = u.CreatedDate,
                UpdatedDate = u.UpdatedDate,
                Roles = u.RoleMappings.Select(urm => new RoleDtoTest
                {
                    Id = urm.Role.Id,
                    Name = urm.Role.Name
                }),
                Vocations = u.VocationRequests.Select(v => new VocationRequestDtoTest
                {
                    Id = v.Id,
                    StartDate = v.StartDate,
                    EndDate = v.EndDate,
                    VocationStatus = (ApprovalStatus)int.Parse(v.VocationStatus)
                }),
                LinkedUsers = u.LinkedUsers.Select(lu => new LinkedUserDtoTest
                {
                    LinkedUserId = lu.AssociatedUser.Id,
                    LinkedUserName = $"{lu.AssociatedUser.FirstName} {lu.AssociatedUser.LastName}"
                })
            })
            .ToListAsync();
    }
}

public class UserDtoTest
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserIcon { get; set; }
    public bool IsEmailConfirmed { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

    public IEnumerable<RoleDtoTest> Roles { get; set; }
    public IEnumerable<VocationRequestDtoTest> Vocations { get; set; }
    public IEnumerable<LinkedUserDtoTest> LinkedUsers { get; set; }
}

public class RoleDtoTest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

public class VocationRequestDtoTest
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ApprovalStatus VocationStatus { get; set; }
}

public class LinkedUserDtoTest
{
    public Guid LinkedUserId { get; set; }
    public string LinkedUserName { get; set; }
}
