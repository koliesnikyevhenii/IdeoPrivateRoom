﻿namespace IdeoPrivateRoom.DAL.Data.Entities;

public class RoleEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public ICollection<UserRoleMappingEntity> RoleMappings { get; set; } = [];
}
