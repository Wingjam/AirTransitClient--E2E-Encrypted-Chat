namespace AirTransit_Core.Repositories
{
    public interface IEncryptionService
    {
        byte[] Encrypt(byte[] payload, string privateKey);
        byte[] Decrypt(byte[] payload, string destinationPublicKey);
    }
}