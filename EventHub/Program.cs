using EventHub.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventHub
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var db = new EventHubContext();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            // Organizer + Profile (1:1)
            var organizer = new Organizer
            {
                Name = "Ahmed Ali",
                CompanyName = "TechCorp",
                IsVerified = true,
                Profile = new OrganizerProfile
                {
                    Biography = "We organize tech events.",
                    WebsiteUr1 = "https://techcorp.example",
                    LogoUr1 = "https://techcorp.example/logo.png"
                }
            };

            // Parent event + sessions (self reference)
            var conference = new Event
            {
                Title = "Tech Conference 2026",
                Description = "Annual technology conference.",
                StartDate = new DateTime(2026, 11, 20),
                EndDate = new DateTime(2026, 11, 22),
                MaxAttendes = 500,
                Organizer = organizer
            };

            conference.Sessions.Add(new Event
            {
                Title = "EF Core Workshop",
                Description = "Hands-on EF Core.",
                StartDate = new DateTime(2026, 11, 20, 10, 0, 0),
                MaxAttendes = 40,
                Organizer = organizer
            });

            // Attendee + Address (owned)
            var attendee = new Attendee
            {
                FullName = "Mona Hassan",
                Email = "mona@example.com",
                Adderss = new Adderss
                {
                    Streat = "10 Nile St",
                    City = "Cairo",
                    Country = "Egypt",
                    Posta1Code = "11511"
                }
            };

            // Registration (M:N) -> then issue badge (1:0..1)
            attendee.Registrations.Add(new Registration
            {
                Event = conference,
                Note = "Looking forward to it!"
            });
            attendee.Badge = new Badge
            {
                IssusedData = DateTime.UtcNow.Date,
                Tier = BadgeTier.Standard
            };

            db.Organizers.Add(organizer);
            db.Attendees.Add(attendee);
            db.SaveChanges();

            // ---- Read back ----
            var ev = db.Events.Include(e => e.Sessions).First(e => e.ParentEventId == null);
            var created = db.Entry(ev).Property<DateTime>("CreatedAt").CurrentValue;   // shadow property

            Console.WriteLine($"Event: {ev.Title}  | Sessions: {ev.Sessions.Count}  | CreatedAt (shadow): {created:u}");

            var a = db.Attendees.Include(x => x.Badge).Include(x => x.Registrations).First();
            Console.WriteLine($"Attendee: {a.FullName} | {a.Adderss.City}, {a.Adderss.Country} | Badge #{a.Badge!.Id} ({a.Badge.Tier}) | Registrations: {a.Registrations.Count}");
        }
    }
}
