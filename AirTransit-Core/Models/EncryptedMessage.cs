using Newtonsoft.Json;

namespace AirTransit_Core.Models
{
    public class EncryptedMessage
    {
        [JsonProperty("id")]
        public string Guid;
        [JsonProperty("PhoneNumber")]
        public string DestinationPhoneNumber;
        public string Content;
    }
}
