using Newtonsoft.Json;
using System;

namespace AirTransit_Core.Models
{
    internal class MessageDTO
    {
        [JsonProperty("Content")]
        public string Content { get; set; }

        [JsonProperty("SenderPhoneNumber")]
        /// <summary>
        /// Is in the signature, that is in the content of the encrypted message.
        /// </summary>
        public string SenderPhoneNumber { get; set; }

        [JsonProperty("Signature")]
        public string Signature { get; set; }

        [JsonProperty("Timestamp")]
        /// <summary>
        /// Is in the content
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
