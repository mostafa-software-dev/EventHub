using System;
using System.Collections.Generic;
using System.Text;

namespace EventHub.Entities
{
    public class OrganizerProfile
    {
        public int Id { get; set; }
        public string Biography { get; set; } = string.Empty;
        public string WebsiteUr1 { get; set; }
        public int OrganizerId { get; set; }
        public string LogoUr1 { get; set; }
        public Organizer Organizer { get; set; } = null!; 


    }
}
