using System;
using System.Collections.Generic;
using AirTransit_Core.Models;

namespace AirTransit_Core.Repositories
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetContacts();
        Contact GetSelf();
        Contact GetContact(String phoneNumber);
        void AddContact(Contact contact);
        void DeleteContact(Contact contact);
    }
}