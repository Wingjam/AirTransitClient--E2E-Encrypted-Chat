using System;
using AirTransit_Core.Models;
using AirTransit_Core.Repositories;
using System.Security.Cryptography;

namespace AirTransit_Core.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IKeySetRepository _keySetRepository;

        public AuthenticationService(IKeySetRepository keySetRepository)
        {
            this._keySetRepository = keySetRepository;
        }

        public bool CheckIfKeysExist()
        {
            return this._keySetRepository.GetKeySet() != null;
        }

        public bool SignUp()
        {
            var keySet = this._keySetRepository.CreateKeySet();
            return SendToServer(keySet.PublicKey);
        }

        private bool SendToServer(string publicKey)
        {
            // TODO
            return true;
        }
    }
}