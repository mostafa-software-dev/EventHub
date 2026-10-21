using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventHub.Entities
{
    public class Organizer
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(150)]
        public string CompanyName { get; set; } 
        public bool IsVerified { get; set; }

        // navigathion
        public OrganizerProfile? Profile { get; set; } // 1 ; 1
        public ICollection<Event> Events { get; set; } = new List<Event>(); // 1 ; n
    }
}
