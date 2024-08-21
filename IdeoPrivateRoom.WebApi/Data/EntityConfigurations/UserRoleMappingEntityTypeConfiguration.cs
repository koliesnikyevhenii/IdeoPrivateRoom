using IdeoPrivateRoom.WebApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdeoPrivateRoom.WebApi.Data.EntityConfigurations;

public class UserRoleMappingEntityTypeConfiguration : IEntityTypeConfiguration<UserRoleMappingEntity>
{
    public void Configure(EntityTypeBuilder<UserRoleMappingEntity> builder)
    {
        builder.ToTable("UserRoleMapping");

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
            .WithMany(u => u.RoleMappings)
            .HasForeignKey(u => u.UserId);

        builder.HasOne(u => u.Role)
            .WithMany(u => u.RoleMappings)
            .HasForeignKey(u => u.RoleId);
    }
}
