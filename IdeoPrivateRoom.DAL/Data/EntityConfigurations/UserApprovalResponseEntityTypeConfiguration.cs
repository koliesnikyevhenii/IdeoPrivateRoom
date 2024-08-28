using IdeoPrivateRoom.DAL.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdeoPrivateRoom.DAL.Data.EntityConfigurations;

public class UserApprovalResponseEntityTypeConfiguration : IEntityTypeConfiguration<UserApprovalResponseEntity>
{
    public void Configure(EntityTypeBuilder<UserApprovalResponseEntity> builder)
    {
        builder.ToTable("UserApprovalResponse");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.ApprovalStatus)
            .HasMaxLength(15);

        builder.Property(u => u.CreatedDate)
            .IsRequired();

        builder.HasOne(u => u.User)
            .WithMany(u => u.UserApprovalResponses)
            .HasForeignKey(u => u.UserId);

        builder.HasOne(u => u.VacationRequest)
            .WithMany(u => u.UserApprovalResponses)
            .HasForeignKey(u => u.VacationRequestId);


    }
}
