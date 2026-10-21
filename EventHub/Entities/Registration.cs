using System;
using System.Collections.Generic;
using System.Text;

namespace EventHub.Entities
{
    public class Registration
    {
        public int AttendeeId { get; set; }
        public Attendee Attendee { get; set; } = null!;
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;
        public string? Note { get; set; }
        public DateTime RegisteredAt { get; set; }
    }
}
