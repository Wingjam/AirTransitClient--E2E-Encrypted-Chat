namespace AirTransit_Core.Models
{
    class KeySet
    {
        int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }
}