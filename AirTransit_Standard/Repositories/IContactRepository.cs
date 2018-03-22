using System;
using System.Collections.Generic;
using AirTransit_Standard.Models;

namespace AirTransit_Standard.Repositories
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetContacts();
        Contact GetSelf();
        Contact GetContact(String phoneNumber);
        void AddContact(Contact contact);
        void UpdateContact(Contact contact);
        void DeleteContact(Contact contact);
    }
}