using System;
using System.Collections.Generic;

namespace AirTransit_Core
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetContacts();
        Contact GetContact(String phoneNumber);
        void AddContact(Contact contact);
        bool DeleteContact(Contact contact);
    }
}