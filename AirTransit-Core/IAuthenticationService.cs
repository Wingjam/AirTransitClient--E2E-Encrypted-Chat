using System;

namespace AirTransit_Core
{
    public interface IAuthenticationService
    {
        bool SignUp(String phoneNumber);
    }
}