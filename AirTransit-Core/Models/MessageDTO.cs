using System;

namespace AirTransit_Core.Models
{
    internal class MessageDTO
    {
        public string Id { get; set; }

        /// <summary>
        /// Is in the signature, that is in the content of the encrypted message.
        /// </summary>
        public string SenderPhoneNumber { get; set; }

        public string DestinationPhoneNumber { get; set; }

        /// <summary>
        /// Is in the content
        /// </summary>
        public DateTime Timestamp { get; set; }

        public string Content { get; set; }
    }
}
