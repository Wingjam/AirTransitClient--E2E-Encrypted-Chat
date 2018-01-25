using System;
using System.Collections.Generic;
using System.Text;

namespace AirTransit_Core
{
    class Message
    {
        String ID { get; set; }
        String SenderPhoneNumber { get; set; }
        String DestinationPhoneNumber { get; set; }
        DateTime Timestamp { get; set; }
        String Content { get; set; }
    }
}
