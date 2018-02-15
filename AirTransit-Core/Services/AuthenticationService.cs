using System;
using AirTransit_Core.Models;
using AirTransit_Core.Repositories;
using System.Security.Cryptography;

namespace AirTransit_Core.Services
{
    class AuthenticationService : IAuthenticationService
    {
        private readonly IKeySetRepository _keySetRepository;

        public AuthenticationService(IKeySetRepository keySetRepository)
        {
            this._keySetRepository = keySetRepository;
        }

        public KeySet SignUp(string phoneNumber)
        {
            var keySet = this._keySetRepository.GetOrCreateKeySet(phoneNumber);
            SendToServer(keySet.PublicKey);
            return keySet;
        }

        private bool SendToServer(string publicKey)
        {
            throw new NotImplementedException();
        }
    }
}