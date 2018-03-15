using System.Security.Cryptography;
using System.Text;
using AirTransit_Core.Models;
using AirTransit_Core.Repositories;

namespace AirTransit_Core.Services
{
    internal class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly KeySet _keySet;
        private readonly Encoding _encoding;

        public MessageService(
            IMessageRepository messageRepository, 
            IEncryptionService encryptionService, 
            KeySet keySet,
            Encoding encoding)
        {
            this._messageRepository = messageRepository;
            this._encryptionService = encryptionService;
            this._keySet = keySet;
            this._encoding = encoding;
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
            var encryptedContent = this._encryptionService.Encrypt(content, destination);

            // Send message to server
            throw new System.NotImplementedException();
        }
    }
}