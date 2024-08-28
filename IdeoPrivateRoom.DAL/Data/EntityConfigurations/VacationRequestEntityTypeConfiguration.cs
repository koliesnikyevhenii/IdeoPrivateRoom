using IdeoPrivateRoom.DAL.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdeoPrivateRoom.DAL.Data.EntityConfigurations;

public class VacationRequestEntityTypeConfiguration : IEntityTypeConfiguration<VacationRequestEntity>
{
    public void Configure(EntityTypeBuilder<VacationRequestEntity> builder)
    {
        builder.ToTable("VacationRequest");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.UserId)
            .IsRequired();

        builder.Property(v => v.StartDate)
            .IsRequired();

        builder.Property(v => v.EndDate)
            .IsRequired();

        builder.Property(v => v.Comment)
            .HasMaxLength(50);

        builder.Property(v => v.CreatedDate)
            .IsRequired();

        builder.Property(v => v.UpdatedDate)
            .IsRequired();

        builder.Property(v => v.VacationStatus)
            .HasMaxLength(15);

        builder.HasOne(v => v.User)
            .WithMany(u => u.VacationRequests)
            .HasForeignKey(v => v.UserId);

    }
}
