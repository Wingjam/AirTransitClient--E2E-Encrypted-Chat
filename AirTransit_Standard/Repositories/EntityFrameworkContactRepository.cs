using System.Collections.Generic;
using System.Linq;
using AirTransit_Standard.Models;

namespace AirTransit_Standard.Repositories
{
    internal class EntityFrameworkContactRepository : EntityFrameworkRepository, IContactRepository
    {
        private string _phoneNumber;

        public EntityFrameworkContactRepository(string phoneNumber, MessagingContext messagingContext) : base(
            messagingContext)
        {
            this._phoneNumber = phoneNumber;

            if (GetContact(this._phoneNumber) == null) return;
            AddContact(new Contact(this._phoneNumber, "You"));
            Commit();
        }

        public IEnumerable<Contact> GetContacts()
        {
            return MessagingContext.Contacts;
        }

        public Contact GetSelf()
        {
            return MessagingContext.Contacts?
                .Where(c => c.PhoneNumber == this._phoneNumber)
                .SingleOrDefault();
        }

        public Contact GetContact(string phoneNumber)
        {
            return this.MessagingContext.Contacts?
                .SingleOrDefault(c => c.PhoneNumber == phoneNumber);
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