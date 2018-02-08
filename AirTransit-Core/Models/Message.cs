using System;
using System.Collections.Generic;
using System.Text;

namespace AirTransit_Core.Models
{
    public class Message
    {
        String Id { get; set; }
        Contact Sender { get; set; }
        String DestinationPhoneNumber { get; set; }
        DateTime Timestamp { get; set; }
        String Content { get; set; }
    }
}
