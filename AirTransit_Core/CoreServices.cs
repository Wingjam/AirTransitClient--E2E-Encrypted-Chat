using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using AirTransit_Core.Models;
using AirTransit_Core.Repositories;
using AirTransit_Core.Services;

namespace AirTransit_Core
{
    public class CoreServices
    {
        public IContactRepository ContactRepository { get; private set; }
        public IMessageRepository MessageRepository { get; private set; }
        public IMessageService MessageService { get; private set; }
        public Encoding Encoding { get; } = Encoding.UTF8;
        
        private readonly BlockingCollection<Message> _blockingCollection;
        
        private IAuthenticationService AuthenticationService;
        private IKeySetRepository KeySetRepository;
        private MessagingContext MessagingContext;
        private MessageFetcher _messageFetcher;
        private IEncryptionService EncryptionService;

        private IMessageService FetcherMessageService;

        public CoreServices()
        {
            _blockingCollection = new BlockingCollection<Message>();
        }

        public bool Init(string phoneNumber)
        {
            InitializeRepositories(phoneNumber);
            InitializeFetcherDependencies(phoneNumber);

            if (!AuthenticationService.CheckIfKeysExist())
            {
                if (!AuthenticationService.SignUp())
                {
                    // TODO This means a communication to the server failed, maybe send an exception instead?
                    return false;
                }
            }

            String signature = EncryptionService.GenerateSignature(phoneNumber);

            _messageFetcher = new MessageFetcher(ReceiveNewMessages, TimeSpan.FromMilliseconds(1000), phoneNumber, signature);

            return true;
        }

        private void ReceiveNewMessages(IEnumerable<EncryptedMessage> encryptedMessages)
        {
            foreach (EncryptedMessage encryptedMessage in encryptedMessages)
            {
                Message message = FetcherMessageService.ReceiveNewMessages(encryptedMessage);
                if (message != null)
                {
                    _blockingCollection.Add(message);
                }
            }
        }

        public BlockingCollection<Message> GetBlockingCollection()
        {
            return _blockingCollection;
        }

        private void InitializeRepositories(string phoneNumber)
        {
            MessagingContext = new DesignTimeDbContextFactory().CreateDbContext(new string[] { });
            ContactRepository = new EntityFrameworkContactRepository(phoneNumber, MessagingContext);
            MessageRepository = new EntityFrameworkMessageRepository(MessagingContext);
            KeySetRepository = new EntityFrameworkKeySetRepository(phoneNumber, this.MessagingContext);
            EncryptionService = new RSAEncryptionService(this.KeySetRepository, this.Encoding);
            AuthenticationService = new AuthenticationService(this.KeySetRepository, phoneNumber);
            MessageService = new MessageService(ContactRepository, MessageRepository, this.EncryptionService, Encoding, phoneNumber);
        }

        private void InitializeFetcherDependencies(string phoneNumber)
        {
            MessagingContext fetcherMessagingContext = new DesignTimeDbContextFactory().CreateDbContext(new string[] { });
            IMessageRepository fetcherMessageRepository = new EntityFrameworkMessageRepository(fetcherMessagingContext);
            IContactRepository fetcherContactRepository = new EntityFrameworkContactRepository(phoneNumber, fetcherMessagingContext);
            IKeySetRepository fetcherKeySetRepository = new EntityFrameworkKeySetRepository(phoneNumber, fetcherMessagingContext);
            IEncryptionService fetcherEncryptionService = new RSAEncryptionService(fetcherKeySetRepository, Encoding);

            FetcherMessageService = new MessageService(fetcherContactRepository, fetcherMessageRepository, fetcherEncryptionService, Encoding, phoneNumber);
        }
    }
}
