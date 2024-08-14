﻿using IdeoPrivateRoom.WebApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdeoPrivateRoom.WebApi.Data.EntityConfigurations
{
    public class UserHasRoleEntityTypeConfiguration : IEntityTypeConfiguration<UserHasRole>
    {
        public void Configure(EntityTypeBuilder<UserHasRole> builder)
        {
            builder.ToTable("UserHasRoles");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.UserId)
                .IsRequired();

            builder.Property(u => u.RoleId)
                .IsRequired();  

            builder.Property(u => u.CreatedDate)
                .IsRequired();

            builder.Property(u => u.UpdatedDate)
                .IsRequired();

            builder.HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(u => u.UserId);

            builder.HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId);
        }
    }
}
