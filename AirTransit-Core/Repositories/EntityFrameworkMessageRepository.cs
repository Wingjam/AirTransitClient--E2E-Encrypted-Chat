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
            return this.MessagingContext.Messages
                .Where(m => m.Timestamp > since);
        }

        public IEnumerable<Message> GetMessages(int maximumNumberOfMessages)
        {
            return this.MessagingContext.Messages
                .OrderBy(m => m.Timestamp)
                .Take(maximumNumberOfMessages);
        }

        public IEnumerable<Message> GetMessages(Contact contact)
        {
            return contact.Messages
                .Where(m => m.DestinationPhoneNumber == contact.PhoneNumber);
        }

        public IEnumerable<Message> GetMessages(Contact contact, DateTime since)
        {
            return contact.Messages
                .Where(m => m.DestinationPhoneNumber == contact.PhoneNumber && m.Timestamp > since);
        }

        public IEnumerable<Message> GetMessages(Contact contact, int maximumNumberOfMessages)
        {
            return contact.Messages
                .Where(m => m.DestinationPhoneNumber == contact.PhoneNumber)
                .OrderBy(m => m.Timestamp)
                .Take(maximumNumberOfMessages);
        }

        public Message GetLastMessage(Contact contact)
        {
            return contact.Messages.OrderBy(m => m.Timestamp).First();
        }

        public void DeleteMessages(IEnumerable<Message> messages)
        {
            this.MessagingContext.RemoveRange(messages);
            Commit();
        }

        public void AddMessage(Message message)
        {
            this.MessagingContext.Messages.Add(message);
            Commit();
        }
    }
}