using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EventEase.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }

        [ForeignKey("Venue")]
        public int VenueId { get; set; }

        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }

        // Navigation properties
        public Event? Event { get; set; }
        public Venue? Venue { get; set; }
    }
}
