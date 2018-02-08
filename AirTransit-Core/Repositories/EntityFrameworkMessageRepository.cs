using System;
using System.Collections.Generic;
using System.Linq;
using AirTransit_Core.Models;

namespace AirTransit_Core.Repositories
{
    public class EntityFrameworkMessageRepository : EntityFrameworkRepository, IMessageRepository
    {
        public IEnumerable<Message> GetMessages()
        {
            return this.MessagingContext.Messages;
        }

        public IEnumerable<Message> GetMessages(DateTime since)
        {
            return this.MessagingContext.Messages.Where(m => m.Timestamp > since);
        }

        public IEnumerable<Message> GetMessages(int maximumNumberOfMessages)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> GetMessages(Contact contact)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> GetMessages(Contact contact, DateTime since)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> GetMessages(Contact contact, int maximumNumberOfMessages)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> GetLastMessagesOfContacts(IEnumerable<Contact> contacts)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMessages(List<string> messageIDs)
        {
            throw new NotImplementedException();
        }
    }
}