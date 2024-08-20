﻿using IdeoPrivateRoom.WebApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdeoPrivateRoom.WebApi.Data.EntityConfigurations;

public class VocationRequestEntityTypeConfiguration : IEntityTypeConfiguration<VocationRequestEntity>
{
    public void Configure(EntityTypeBuilder<VocationRequestEntity> builder)
    {
        builder.ToTable("VocationRequest");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.UserId)
            .IsRequired();

        builder.Property(v => v.StartDate)
            .IsRequired();

        builder.Property(v => v.EndDate)
            .IsRequired();

        builder.Property(v => v.Title)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(v => v.CreatedDate)
            .IsRequired();

        builder.Property(v => v.UpdatedDate)
            .IsRequired();

        builder.Property(v => v.VocationStatus)
            .HasConversion<string>()
            .HasMaxLength(15);

        builder.HasOne(v => v.User)
            .WithMany(u => u.VocationRequests)
            .HasForeignKey(v => v.UserId);

    }
}
