using AirTransit_Core.Models;
using AirTransit_Core.Repositories;

namespace AirTransit_Core.Services
{
    public class MessageService : IMessageService
    {
        public void PersistMessageLocally(Contact contact)
        {
        }
        
        public Message SendMessage(Contact destination, string message)
        {
            // Encrypt message
            // Send message to server
            throw new System.NotImplementedException();
        }
    }
}