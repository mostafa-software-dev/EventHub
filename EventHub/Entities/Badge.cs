using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EventHub.Entities
{
    [Table("Badges")]
    [Index(nameof(AttendeeId), IsUnique = true)]
    public class Badge
    {
        [Key]
        public int Id { get; set; }
        [Required, Column(TypeName = "data")]
        public DateTime IssusedData { get; set; }

        [Required]
        public  BadgeTier Tier { get; set; }
        public int AttendeeId { get; set; }
        public Attendee Attendee { get; set; } = null!;

    }

    public enum BadgeTier
    {
        Standard = 0,
        VIP = 1
    }
}
