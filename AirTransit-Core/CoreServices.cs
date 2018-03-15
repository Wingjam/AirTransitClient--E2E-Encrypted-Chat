using AirTransit_Core.Models;
using AirTransit_Core.Repositories;
using AirTransit_Core.Services;
using Microsoft.EntityFrameworkCore;
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
        private readonly BlockingCollection<string> _blockingCollection;
        
        private IAuthenticationService _authenticationService;
        private IKeySetRepository _keySetRepository;
        private MessagingContext _messagingContext;
        
        public static string SERVER_ADDRESS = "servermartineau.com";
        
        public CoreServices()
        {
            _blockingCollection = new BlockingCollection<string>();
            this._encryptionService = new RSAEncryptionService();
        }

        public bool Init(string phoneNumber)
        {
            this._messagingContext = new DesignTimeDbContextFactory().CreateDbContext(new string[] { });
            InitializeRepositories(phoneNumber, this._messagingContext);
            this._authenticationService = new AuthenticationService(this._keySetRepository);

            KeySet keySet = _authenticationService.SignUp(phoneNumber);
            if (keySet != null)
            {
                InitializeServices(keySet);
                return true;
            }

            return false;
        }

        public BlockingCollection<string> GetBlockingCollection()
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
