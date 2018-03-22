using Newtonsoft.Json;

namespace AirTransit_Standard.Models
{
    public class EncryptedMessage
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Guid;
        [JsonProperty("PhoneNumber")]
        public string DestinationPhoneNumber;
        public string Content;
    }
}
