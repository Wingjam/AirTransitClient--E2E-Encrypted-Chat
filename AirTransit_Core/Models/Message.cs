using System;

namespace AirTransit_Core.Models
{
    public class Message
    {
        public string Id { get; set; }
        public Contact Sender { get; set; }
        public string DestinationPhoneNumber { get; set; }
        public DateTime Timestamp { get; set; }
        public string Content { get; set; }
    }
}
