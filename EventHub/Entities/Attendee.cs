using System;
using System.Collections.Generic;
using System.Text;

namespace EventHub.Entities
{
    public class Attendee
    {
        public int ID { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public Adderss Adderss { get; set; } = null!;
        public Badge? Badge { get; set; }
        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();

    }
}
