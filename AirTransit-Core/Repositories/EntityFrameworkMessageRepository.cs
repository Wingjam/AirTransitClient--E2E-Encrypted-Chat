using System;
using System.Collections.Generic;
using AirTransit_Core.Models;

namespace AirTransit_Core.Repositories
{
    public class EntityFrameworkMessageRepository : EntityFrameworkRepository, IMessageRepository
    {
        public Message SendMessage(string phoneNumber, string message)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> GetMessages()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> GetMessages(DateTime since)
        {
            throw new NotImplementedException();
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