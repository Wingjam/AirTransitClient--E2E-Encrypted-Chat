using AirTransit_Core.Models;
using AirTransit_Core.Repositories;
using AirTransit_Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirTransit_Core
{
    public class CoreServices
    {
        IContactRepository ContactRepository { get; set; }
        IMessageRepository MessageRepository { get; set; }

        IMessageService MessageService { get; set; }

        private IAuthenticationService AuthenticationService { get; set; }

        public CoreServices()
        {
            
        }

        public bool Init(string phoneNumber)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MessagingContext>();
            optionsBuilder.UseSqlite("Data Source=blog.db");
            MessagingContext messagingContext = new MessagingContext(optionsBuilder.Options);

            KeySet keySet = AuthenticationService.SignUp(phoneNumber);

            if (keySet != null)
            {
                InitializeRepositories(messagingContext);
                InitializeServices(keySet);
            }

            return keySet != null;
        }

        private void InitializeRepositories(MessagingContext messagingContext)
        {
            ContactRepository = new EntityFrameworkContactRepository(messagingContext);
            MessageRepository = new EntityFrameworkMessageRepository(messagingContext);
        }

        private void InitializeServices(KeySet keySet)
        {
            MessageService = new MessageService(MessageRepository, keySet);
        }
    }
}
