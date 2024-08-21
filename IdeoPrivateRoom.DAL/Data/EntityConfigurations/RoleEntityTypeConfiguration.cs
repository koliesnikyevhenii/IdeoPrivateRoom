using IdeoPrivateRoom.DAL.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdeoPrivateRoom.DAL.Data.EntityConfigurations;

public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.ToTable("Role");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(r => r.CreatedDate)
            .IsRequired();
    }
}
