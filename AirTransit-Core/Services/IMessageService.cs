using System;
using AirTransit_Core.Models;

namespace AirTransit_Core.Services
{
    public interface IMessageService
    {
        Message SendMessage(String phoneNumber, String message);
    }
}