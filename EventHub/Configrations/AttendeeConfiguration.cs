using EventHub.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventHub.Configrations
{
    public class AttendeeConfiguration : IEntityTypeConfiguration<Attendee>
    {
        public void Configure(EntityTypeBuilder<Attendee> builder)
        {
            builder.ToTable("Attendee");
            builder.HasKey(t => t.ID);

            builder.Property(t => t.FullName).IsRequired().HasMaxLength(150);
            builder.Property(t => t.Email).IsRequired().HasMaxLength(200);
            builder.HasIndex(t => t.Email).IsUnique();

            builder.OwnsOne(t => t.Adderss, addr =>
            {
                addr.Property(x => x.Streat).IsRequired().HasMaxLength(200);
                addr.Property(x => x.City).IsRequired().HasMaxLength(100);
                addr.Property(x => x.Country).IsRequired().HasMaxLength(100);
                addr.Property(x => x.Posta1Code).IsRequired().HasMaxLength(30);
            });
            builder.HasOne (t => t.Badge)
                   .WithOne(b => b.Attendee)
                   .HasForeignKey<Badge>(b => b.AttendeeId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
