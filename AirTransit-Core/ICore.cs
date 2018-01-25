using System;
using System.Collections.Generic;
using System.Text;

namespace AirTransit_Core
{
    interface ICore
    {
        bool SignUp();
        Message SendMessage(String phoneNumber, String message);
        List<Message> GetMessages();
        List<Message> GetMessages(DateTime since);
        List<Message> GetMessages(int maximumNumberOfMessages);
        List<Message> GetMessages(Contact contact);
        List<Message> GetMessages(Contact contact, DateTime since);
        List<Message> GetMessages(Contact contact, int maximumNumberOfMessages);
        List<Message> GetLastMessagesOfContacts(List<Contact> contacts);
        List<Contact> GetContacts();
        Contact GetContact(String phoneNumber);
        void AddContact(Contact contact);
        bool DeleteContact(Contact contact);
        bool DeleteMessages(List<String> messageIDs);
    }
}
