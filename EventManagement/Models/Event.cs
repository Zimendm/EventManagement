using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Models
{
    public class Event
    {
        public int EventId { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }


        // Navigation properties
        public int EventTypeId { get; set; }
        public EventType EventType { get; set; }

        public List<EventDocument> EventDocuments { get; set; }
    }
}
