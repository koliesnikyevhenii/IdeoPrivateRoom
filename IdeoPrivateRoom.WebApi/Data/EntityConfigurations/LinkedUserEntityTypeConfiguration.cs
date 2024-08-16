using IdeoPrivateRoom.WebApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdeoPrivateRoom.WebApi.Data.EntityConfigurations;

public class LinkedUserEntityTypeConfiguration : IEntityTypeConfiguration<LinkedUser>
{
    public void Configure(EntityTypeBuilder<LinkedUser> builder)
    {
        builder.ToTable("LinkedUser");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.CreatedDate)
            .IsRequired();

        builder.HasOne(l => l.User)
            .WithMany(u => u.LinkedUsers)
            .HasForeignKey(u => u.UserId);

        builder.HasOne(l => l.AssociatedUser)
            .WithMany(u => u.AssociatedUsers)
            .HasForeignKey(l => l.LinkedUserId);
    }
}
