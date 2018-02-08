using System;
using AirTransit_Core.Models;

namespace AirTransit_Core.Services
{
    public interface IMessageService
    {
        Message SendMessage(Contact destination, String message);
    }
}