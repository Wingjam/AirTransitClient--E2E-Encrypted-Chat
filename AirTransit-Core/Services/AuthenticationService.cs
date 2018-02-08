using System;

namespace AirTransit_Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationService()
        {
        }

        public bool SignUp(string phoneNumber)
        {
            // Check if a set of private and public key exists
            // If it exists, we don't have to create new keys
            // If it doesn't, we have to create a new set of keys and then send the public key to the server
            throw new NotImplementedException();
        }
    }
}