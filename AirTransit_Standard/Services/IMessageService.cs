using System;
using AirTransit_Core.Models;

namespace AirTransit_Core.Services
{
    public interface IMessageService
    {
        bool SendMessage(Contact destination, String message);
        Message ReceiveNewMessages(EncryptedMessage encryptedMessages);
    }
}