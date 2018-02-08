using System;
using AirTransit_Core.Models;

namespace AirTransit_Core.Services
{
    interface IAuthenticationService
    {
        KeySet SignUp(String phoneNumber);
    }
}