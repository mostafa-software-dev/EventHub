using System;
using System.Collections.Generic;
using System.Text;

namespace EventHub.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public string Title { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int MaxAttendes { get; set; }
        public int OrganizerId { get; set; }
        public Organizer Organizer { get; set; } = null!;
        public int? ParentEventId { get; set; }
        public Event? ParentEvent { get; set; }
        public ICollection<Event> Sessions { get; set; } = new List<Event>();

        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();


    }
}
