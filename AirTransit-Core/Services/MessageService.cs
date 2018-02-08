using AirTransit_Core.Models;
using AirTransit_Core.Repositories;

namespace AirTransit_Core.Services
{
    class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly KeySet _keySet;

        public MessageService(IMessageRepository messageRepository, KeySet keySet)
        {
            this._messageRepository = messageRepository;
            this._keySet = keySet;
        }
        
        public void PersistMessageLocally(Contact destination, string content)
        {
            this._messageRepository.AddMessage(new Message
            {
                Content = content,
                DestinationPhoneNumber = destination.PhoneNumber,
            });
        }
        
        public Message SendMessage(Contact destination, string content)
        {
            // Encrypt message
            // Send message to server
            throw new System.NotImplementedException();
        }
    }
}