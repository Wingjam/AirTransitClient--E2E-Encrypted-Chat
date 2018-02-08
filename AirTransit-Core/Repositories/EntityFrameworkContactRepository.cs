using System.Collections.Generic;
using System.Linq;
using AirTransit_Core.Models;

namespace AirTransit_Core.Repositories
{
    class EntityFrameworkContactRepository : EntityFrameworkRepository, IContactRepository
    {
        public EntityFrameworkContactRepository(MessagingContext messagingContext) : base(messagingContext) { }

        public IEnumerable<Contact> GetContacts()
        {
            return MessagingContext.Contacts;
        }

        public Contact GetContact(string phoneNumber)
        {
            return this.MessagingContext.Contacts.Single(c => c.PhoneNumber == phoneNumber);
        }

        public void AddContact(Contact contact)
        {
            this.MessagingContext.Contacts.Add(contact);
            Commit();
        }

        public void DeleteContact(Contact contact)
        {
            this.MessagingContext.Remove(contact);
            Commit();
        }
    }
}