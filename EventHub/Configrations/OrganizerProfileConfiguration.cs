using EventHub.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventHub.Configrations
{
    public class OrganizerProfileConfiguration : IEntityTypeConfiguration<OrganizerProfile>
    {
        public void Configure(EntityTypeBuilder<OrganizerProfile> builder)
        {
            builder.ToTable("OrganaizerProfiles");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Biography).HasMaxLength(1000);
            builder.Property(p => p.WebsiteUr1).HasMaxLength(300);
            builder.Property(p => p.LogoUr1).HasMaxLength(300);

            builder.HasOne(p => p.Organizer)
                   .WithOne(o => o.Profile)
                   .HasForeignKey<OrganizerProfile>(p => p.OrganizerId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
