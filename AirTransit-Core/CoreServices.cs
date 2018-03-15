using AirTransit_Core.Models;
using AirTransit_Core.Repositories;
using AirTransit_Core.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirTransit_Core
{
    public class CoreServices
    {
        public IContactRepository ContactRepository { get; private set; }
        public IMessageRepository MessageRepository { get; private set; }
        public IMessageService MessageService { get; private set; }
        public Encoding Encoding { get; } = Encoding.UTF8;
        
        private readonly IEncryptionService _encryptionService;
        private readonly BlockingCollection<Message> _blockingCollection;
        
        private IAuthenticationService _authenticationService;
        private IKeySetRepository _keySetRepository;
        private MessagingContext _messagingContext;
        private MessageFetcher _messageFetcher;
        
        public static string SERVER_ADDRESS = "jo2server.ddns.net:5000";
        
        public CoreServices()
        {
            _blockingCollection = new BlockingCollection<Message>();
            this._encryptionService = new RSAEncryptionService();
        }

        public bool Init(string phoneNumber)
        {
            this._messagingContext = new DesignTimeDbContextFactory().CreateDbContext(new string[] { });
            InitializeRepositories(phoneNumber, this._messagingContext);
            this._authenticationService = new AuthenticationService(this._keySetRepository);

            var keySet = _authenticationService.SignUp(phoneNumber);
            if (keySet == null) return false;
            InitializeServices(keySet);
            _messageFetcher = new MessageFetcher(ReceiveNewMessages, TimeSpan.FromMilliseconds(1000), phoneNumber, "TODO la authSignature de hugo");
            return true;

        }

        private void ReceiveNewMessages(IEnumerable<EncryptedMessage> encryptedMessages)
        {
            foreach (EncryptedMessage encryptedMessage in encryptedMessages)
            {
                // 1. decrypt message
                // TODO : utiliser le vrai decrypt. et avoir une fonction qui prend le raw message decrypt et qui le tranforme en un vrai objet message.
                encryptedMessage.EncryptedMessageContent = ""; //= RSA.Decrypt(encryptMessage);
                MessageDTO decryptedMessage = StringToMessageDTO(encryptedMessage.EncryptedMessageContent);
                // 1.5 Validate that message with its signature
                // TODO decrypt la signature avec la clef publique du sender.
                if (decryptedMessage.Signature != encryptedMessage.DestinationPhoneNumber)
                {
                    // If the signature is invalid, we skip this message.
                    continue;
                }

                // 2. Add the contact if he do not exist
                Contact senderContact = ContactRepository.GetContact(decryptedMessage.SenderPhoneNumber);
                if (senderContact == null)
                {
                    senderContact = new Contact(decryptedMessage.SenderPhoneNumber, decryptedMessage.SenderPhoneNumber);
                    ContactRepository.AddContact(senderContact);
                }
                // 3. Add the message in the BD
                Message message = new Message()
                {
                    Id = encryptedMessage.Guid,
                    Sender = senderContact,
                    Content = decryptedMessage.Content,
                    DestinationPhoneNumber = encryptedMessage.DestinationPhoneNumber,
                    Timestamp = decryptedMessage.Timestamp
                };
                MessageRepository.AddMessage(message);

                // 4. Push the new message in the blocking collection
                _blockingCollection.Add(message);
            }

        }

        private MessageDTO StringToMessageDTO(string decryptedMessage)
        {
            MessageDTO messageDTO = JsonConvert.DeserializeObject<MessageDTO>(decryptedMessage);
            return messageDTO;
        }

        private string MessageDTOToString(MessageDTO message)
        {
            return JsonConvert.SerializeObject(message);
        }

        public BlockingCollection<Message> GetBlockingCollection()
        {
            return _blockingCollection;
        }

        private void InitializeRepositories(string phoneNumber, MessagingContext messagingContext)
        {
            ContactRepository = new EntityFrameworkContactRepository(phoneNumber, messagingContext);
            MessageRepository = new EntityFrameworkMessageRepository(messagingContext);
            this._keySetRepository = new EntityFrameworkKeySetRepository(this._messagingContext);
        }

        private void InitializeServices(KeySet keySet)
        {
            MessageService = new MessageService(MessageRepository, this._encryptionService, keySet, Encoding);
        }
    }
}
