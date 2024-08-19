using IdeoPrivateRoom.WebApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdeoPrivateRoom.WebApi.Data.EntityConfigurations;

public class UserApprovalResponseEntityTypeConfiguration : IEntityTypeConfiguration<UserApprovalResponse>
{
    public void Configure(EntityTypeBuilder<UserApprovalResponse> builder)
    {
        builder.ToTable("UserApprovalResponse");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.ApprovalStatus)
            .HasConversion<string>()
            .HasMaxLength(15);

        builder.Property(u => u.CreatedDate)
            .IsRequired();

        builder.HasOne(u => u.User)
            .WithMany(u => u.UserApprovalResponses)
            .HasForeignKey(u => u.UserId);

        builder.HasOne(u => u.VocationRequest)
            .WithMany(u => u.UserApprovalResponses)
            .HasForeignKey(u => u.VocationRequestId);


    }
}
