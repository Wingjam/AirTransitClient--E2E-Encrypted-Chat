using System.Collections.Generic;
using System.Linq;
using AirTransit_Core.Models;

namespace AirTransit_Core.Repositories
{
    internal class EntityFrameworkContactRepository : EntityFrameworkRepository, IContactRepository
    {
        private string _phoneNumber;

        public EntityFrameworkContactRepository(string phoneNumber, MessagingContext messagingContext) : base(
            messagingContext)
        {
            this._phoneNumber = phoneNumber;

            if (GetSelf() == null)
            {
                AddContact(new Contact(this._phoneNumber, "You"));
            }
        }

        public IEnumerable<Contact> GetContacts()
        {
            return MessagingContext.Contacts;
        }

        public Contact GetSelf()
        {
            return GetContact(this._phoneNumber);
        }

        public Contact GetContact(string phoneNumber)
        {
            return this.MessagingContext.Contacts
                .FirstOrDefault(c => c.PhoneNumber == phoneNumber);
        }

        public void AddContact(Contact contact)
        {
            this.MessagingContext.Contacts.Add(contact);
            Commit();
        }

        public void UpdateContact(Contact contact)
        {
            this.MessagingContext.Contacts.Update(contact);
            Commit();
        }

        public void DeleteContact(Contact contact)
        {
            this.MessagingContext.Remove(contact);
            Commit();
        }
    }
}