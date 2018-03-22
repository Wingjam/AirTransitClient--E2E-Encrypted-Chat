using AirTransit_Standard.Models;

namespace AirTransit_Standard.Services
{
    public interface IEncryptionService
    {
        string GenerateSignature(string content);
        bool VerifySignature(string dataToVerify, string signedData, Contact contact);

        string Encrypt(string message, Contact contact);
        string Decrypt(string encryptedMessage);
    }
}