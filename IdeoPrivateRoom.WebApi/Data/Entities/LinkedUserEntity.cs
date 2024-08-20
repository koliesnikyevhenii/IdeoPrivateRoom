﻿namespace IdeoPrivateRoom.WebApi.Data.Entities;

public class LinkedUserEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserEntity User { get; set; } = null!;
    public Guid LinkedUserId { get; set; }
    public UserEntity AssociatedUser { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
