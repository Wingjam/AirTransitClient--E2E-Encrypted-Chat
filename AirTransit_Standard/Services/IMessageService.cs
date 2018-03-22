using System;
using System.Collections.Generic;
using AirTransit_Standard.Models;

namespace AirTransit_Standard.Services
{
    public interface IMessageService
    {
        bool SendMessage(Contact destination, String message);
        Message ReceiveNewMessages(EncryptedMessage encryptedMessages);
    }
}