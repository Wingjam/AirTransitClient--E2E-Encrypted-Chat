using System;
using System.Collections.Generic;
using AirTransit_Core.Models;

namespace AirTransit_Core.Repositories
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetContacts();
        Contact GetContact(String phoneNumber);
        void AddContact(Contact contact);
        bool DeleteContact(Contact contact);
    }
}