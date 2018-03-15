using AirTransit_Core.Models;

namespace AirTransit_Core.Repositories
{
    public interface IEncryptionService
    {
        string GenerateSignature(string content);
        bool VerifySignature(string dataToVerify, string signedData, Contact contact);

        string Encrypt(string message, Contact contact);
        string Decrypt(string encryptedMessage);
    }
}