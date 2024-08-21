﻿using IdeoPrivateRoom.WebApi.Models.Dtos;
using IdeoPrivateRoom.WebApi.Models.Requests;
using IdeoPrivateRoom.WebApi.Models.Responses;

namespace IdeoPrivateRoom.WebApi.Services.Interfaces;
public interface IVocationService
{
    Task<List<VocationResponse>> GetAll();
    Task<List<VocationResponse>> GetByUserId(Guid id);
    Task<Guid> Create(CreateVocationRequest vocation);
    Task<Guid?> Delete(Guid id);
    Task<Guid?> Update(Guid id, VocationRequestDto vocation);
}