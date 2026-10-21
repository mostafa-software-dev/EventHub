using EventHub.Configrations;
using EventHub.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventHub
{
    public class EventHubContext : DbContext
    {
        public DbSet<Organizer> Organizers => Set<Organizer>();
        public DbSet<OrganizerProfile> organizerProfiles => Set<OrganizerProfile>();
        public DbSet<Event> Events => Set<Event>();
        public DbSet<Attendee> Attendees => Set<Attendee>();
        public DbSet<Badge> Badges => Set<Badge>();
        public DbSet<Registration> Registrations => Set<Registration>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=.;Database=EventHubDB;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrganizerProfileConfiguration());
            modelBuilder.ApplyConfiguration(new AttendeeConfiguration());

            modelBuilder.Entity<Event>(e =>
            {
                e.ToTable("Event");
                e.HasKey(x => x.Id);

                e.Property(x => x.Title).IsRequired().HasMaxLength(200);
                e.Property(x => x.Description).IsRequired().HasMaxLength(200);
                e.Property(x => x.StartDate).IsRequired();
                e.Property(x => x.EndDate);
                e.Property(x => x.MaxAttendes).IsRequired();

                e.Property<DateTime>("CreatedAT");
                e.Property<DateTime>("UpdatedAT");

                // Organizer 1 : N Event
                e.HasOne(x => x.Organizer )
                 .WithMany(o => o.Events)
                 .HasForeignKey(x => x.OrganizerId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade);

                // Self-reference: Event 1 : N Sessions
                e.HasOne(x => x.ParentEvent)
                 .WithMany(x => x.Sessions)
                 .HasForeignKey(x => x.ParentEventId)
                 .IsRequired(false)
                 .OnDelete(DeleteBehavior.Restrict);


            });

            modelBuilder.Entity<Registration>(r =>
            {
                r.ToTable("Registrations");
                r.HasKey(x => new { x.AttendeeId, x.EventId });   // composite key

                r.Property(x => x.Note).HasMaxLength(500);
                r.Property(x => x.RegisteredAt)
                 .HasDefaultValueSql("GETUTCDATE()")
                 .ValueGeneratedOnAdd();

                r.HasOne(x => x.Attendee)
                 .WithMany(a => a.Registrations)
                 .HasForeignKey(x => x.AttendeeId)
                 .OnDelete(DeleteBehavior.Cascade);

                r.HasOne(x => x.Event)
                 .WithMany(ev => ev.Registrations)
                 .HasForeignKey(x => x.EventId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

        modelBuilder.Entity<Registration>(r =>
        {
            r.ToTable("Registrations");
            r.HasKey(x => new { x.AttendeeId, x.EventId });   // composite key

            r.Property(x => x.Note).HasMaxLength(500);
            r.Property(x => x.RegisteredAt)
             .HasDefaultValueSql("GETUTCDATE()")
             .ValueGeneratedOnAdd();

            r.HasOne(x => x.Attendee)
             .WithMany(a => a.Registrations)
             .HasForeignKey(x => x.AttendeeId)
             .OnDelete(DeleteBehavior.Cascade);

            r.HasOne(x => x.Event)
             .WithMany(ev => ev.Registrations)
             .HasForeignKey(x => x.EventId)
             .OnDelete(DeleteBehavior.Cascade);
        });
        }


        
    }
}
