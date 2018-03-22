using System;
using System.Collections.Generic;
using System.Linq;
using AirTransit_Standard.Models;

namespace AirTransit_Standard.Repositories
{
    internal class EntityFrameworkMessageRepository : EntityFrameworkRepository, IMessageRepository
    {
        public EntityFrameworkMessageRepository(MessagingContext messagingContext) : base(messagingContext) { }

        public Message GetMessage(string id)
        {
            return this.MessagingContext.Messages?.Where(m => m.Id == id).FirstOrDefault();
        }

        public IEnumerable<Message> GetMessages()
        {
            return this.MessagingContext.Messages;
        }

        public IEnumerable<Message> GetMessages(DateTime since)
        {
            return this.MessagingContext.Messages?
                .Where(m => m.Timestamp > since);
        }

        public IEnumerable<Message> GetMessages(int maximumNumberOfMessages)
        {
            return this.MessagingContext.Messages?
                .OrderBy(m => m.Timestamp)
                .Take(maximumNumberOfMessages);
        }

        private IEnumerable<Message> GetMessagesExchangeWithContact(Contact contact)
        {
            return this.MessagingContext.Messages?
                .Where(m => m.DestinationPhoneNumber == contact.PhoneNumber
                             || m.Sender.PhoneNumber == contact.PhoneNumber);
        }

        public IEnumerable<Message> GetMessages(Contact contact)
        {
            return GetMessagesExchangeWithContact(contact);
        }

        public IEnumerable<Message> GetMessages(Contact contact, DateTime since)
        {
            return GetMessagesExchangeWithContact(contact)?
                .Where(m => m.Timestamp > since);
        }

        public IEnumerable<Message> GetMessages(Contact contact, int maximumNumberOfMessages)
        {
            return GetMessagesExchangeWithContact(contact)?
                .OrderBy(m => m.Timestamp)
                .Take(maximumNumberOfMessages);
        }

        public Message GetLastMessage(Contact contact)
        {
            return GetMessagesExchangeWithContact(contact)?
                .OrderBy(m => m.Timestamp)
                .FirstOrDefault();
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