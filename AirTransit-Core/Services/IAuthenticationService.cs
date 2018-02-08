using System;

namespace AirTransit_Core.Services
{
    public interface IAuthenticationService
    {
        bool SignUp(String phoneNumber);
    }
}