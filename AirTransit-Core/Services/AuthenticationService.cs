using System;
using AirTransit_Core.Models;
using AirTransit_Core.Repositories;

namespace AirTransit_Core.Services
{
    class AuthenticationService : IAuthenticationService
    {
        private readonly IContactRepository _contactRepository;

        public AuthenticationService(IContactRepository contactRepository)
        {
            this._contactRepository = contactRepository;
        }

        public KeySet SignUp(string phoneNumber)
        {
            //this._contactRepository.GetContact(phoneNumber);
            // Check if a set of private and public key for the phone number exists
            // If it exists, we don't have to create new keys
            // If it doesn't, we have to create a new set of keys and then send the public key to the server

            return null;
        }
    }
}