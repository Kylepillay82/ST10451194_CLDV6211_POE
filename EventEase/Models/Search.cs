using System;
using System.Collections.Generic;
using EventEase.Models;

namespace EventEase.Models
{
    public class Search
    {
        public string EventName { get; set; }

        public int? EventTypeId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string VenueName { get; set; }

        public bool? VenueAvailable { get; set; }  // True = available, False = unavailable, Null = any

        public List<Event> Results { get; set; }

        public List<EventType> EventTypes { get; set; }  // For dropdown population
    }
}
