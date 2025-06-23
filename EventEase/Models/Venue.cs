using System.ComponentModel.DataAnnotations;

namespace EventEase.Models
{
    public class Venue
    {
        [Key]
        public int VenueId { get; set; }

        [Required]
        [StringLength(100)]
        public string VenueName { get; set; }

        [Required]
        public string Location { get; set; }

        [Range(1, int.MaxValue)]
        public int Capacity { get; set; }

        public string? ImageUrl { get; set; }

        // Navigation property
        public ICollection<Event>? Events { get; set; }
        public ICollection<Booking>? Bookings { get; set; }

        public bool IsAvailable { get; set; }

    }
}
