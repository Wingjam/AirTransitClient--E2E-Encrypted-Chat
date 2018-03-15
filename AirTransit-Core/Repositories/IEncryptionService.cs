using AirTransit_Core.Models;

namespace AirTransit_Core.Repositories
{
    public interface IEncryptionService
    {
        string Encrypt(string message, Contact contact);
        string Decrypt(string encryptedMessage);
    }
}