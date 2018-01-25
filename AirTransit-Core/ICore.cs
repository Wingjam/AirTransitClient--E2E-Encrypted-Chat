using System;
using System.Collections.Generic;
using System.Text;

namespace AirTransit_Core
{
    interface ICore
    {
        Message SendMessage(String phoneNumber, String message);
        List<Message> GetMessages();
        List<Message> GetMessages(DateTime since);
        List<Message> GetMessages(int maximumNumberOfMessages);
        List<Message> GetMessages(String phoneNumber);
        List<Message> GetMessages(String phoneNumber, DateTime since);
        List<Message> GetMessages(String phoneNumber, int maximumNumberOfMessages);
        bool DeleteMessages(List<String> messageIDs);
    }
}
