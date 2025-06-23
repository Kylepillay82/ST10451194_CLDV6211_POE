using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EventEase.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        [StringLength(100)]
        public string EventName { get; set; }

        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        public string? Description { get; set; }

        // Foreign Keys
        [ForeignKey("Venue")]
        public int VenueId { get; set; }

        [ForeignKey("EventType")]
        public int EventTypeId { get; set; }  // <-- Ensure this is added

        // Navigation Properties
        public Venue? Venue { get; set; }
        public EventType? EventType { get; set; } // <-- Add this too
        public ICollection<Booking>? Bookings { get; set; }
    }
}
