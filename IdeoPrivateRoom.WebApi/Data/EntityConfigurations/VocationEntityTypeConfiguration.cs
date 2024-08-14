using IdeoPrivateRoom.WebApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography.X509Certificates;

namespace IdeoPrivateRoom.WebApi.Data.EntityConfigurations
{
    public class VocationEntityTypeConfiguration : IEntityTypeConfiguration<Vocation>
    {
        public void Configure(EntityTypeBuilder<Vocation> builder)
        {
            builder.ToTable("Vocations");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.UserId)
                .IsRequired();

            builder.Property(v => v.StartDate)
                .IsRequired();

            builder.Property(v => v.EndDate)
                .IsRequired();

            builder.Property(v => v.CreatedDate)
                .IsRequired();

            builder.Property(v => v.UpdatedDate)
                .IsRequired();

            builder.HasOne(v => v.User)
                .WithMany()
                .HasForeignKey(v => v.UserId);

        }
    }
}
