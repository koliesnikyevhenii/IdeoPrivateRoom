using AutoMapper;
using IdeoPrivateRoom.WebApi.Data;
using IdeoPrivateRoom.WebApi.Data.Entities;
using IdeoPrivateRoom.WebApi.Models.Dtos;
using IdeoPrivateRoom.WebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdeoPrivateRoom.WebApi.Repositories;

public class VocationRepository(ApplicationDbContext _dbContext, IMapper _mapper) : IVocationRepository
{
    public async Task<Guid> Create(VocationRequestDto vocationRequest)
    {
        var createdVocation = await _dbContext.VocationRequests
            .AddAsync(_mapper.Map<VocationRequest>(vocationRequest));

        await _dbContext.SaveChangesAsync();

        return createdVocation.Entity.Id;
    }

    public async Task<List<VocationRequestDto>> Get()
    {
        return await _dbContext.VocationRequests
            .AsNoTracking()
            .Include(v => v.UserApprovalResponses)
                .ThenInclude(u => u.User)
            .Select(v => new VocationRequestDto
            {
                Id = v.Id,
                StartDate = v.StartDate,
                EndDate = v.EndDate,
                Title = v.Title,
                VocationStatus = v.VocationStatus,
                User = new VocationUserDto
                {
                    Id = v.User.Id,
                    Name = $"{v.User.FirstName} {v.User.LastName}",
                    Icon = v.User.UserIcon
                },
                UserApprovalResponses = v.UserApprovalResponses.Select(u => new VocationUserApprovalDto
                {
                    Id = u.Id,
                    User = new VocationUserDto
                    {
                        Id = u.User.Id,
                        Name = $"{u.User.FirstName} {u.User.LastName}",
                        Icon = u.User.UserIcon
                    },
                    ApprovalStatus = u.ApprovalStatus
                })
            }).ToListAsync();
    }

    public async Task<List<VocationRequestDto>> Get(Guid userId)
    {
        return await _dbContext.VocationRequests
            .AsNoTracking()
            .Where(v => v.UserId == userId)
            .Select(v => _mapper.Map<VocationRequestDto>(v))
            .ToListAsync();
    }

    public async Task<Guid?> Update(Guid id, VocationRequestDto vocation)
    {
        await _dbContext.VocationRequests
            .Where(v => v.Id == id)
            .ExecuteUpdateAsync(v => v
                .SetProperty(p => p.StartDate, p => vocation.StartDate)
                .SetProperty(p => p.EndDate, p => vocation.EndDate)
                .SetProperty(p => p.UpdatedDate, p => vocation.UpdatedDate)
                .SetProperty(p => p.CreatedDate, p => vocation.CreatedDate)
                .SetProperty(p => p.VocationStatus, p => vocation.VocationStatus));

        return id;
    }

    public async Task<Guid?> Delete(Guid id)
    {
        await _dbContext.VocationRequests
            .Where(v => v.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
