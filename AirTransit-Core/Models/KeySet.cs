namespace AirTransit_Core.Models
{
    class KeySet
    {
        public KeySet(string phoneNumber, string publicKey, string privateKey)
        {
            this.PhoneNumber = phoneNumber;
            this.PublicKey = publicKey;
            this.PrivateKey = privateKey;
        }
        
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }
}