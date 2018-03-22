using System;
using AirTransit_Core.Models;
using AirTransit_Core.Repositories;
using System.Security.Cryptography;

namespace AirTransit_Core.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IKeySetRepository _keySetRepository;
        private readonly string _phoneNumber;

        public AuthenticationService(IKeySetRepository keySetRepository, String phoneNumber)
        {
            this._keySetRepository = keySetRepository;
            this._phoneNumber = phoneNumber;
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
            Registry registry = new Registry()
            {
                PublicKey = publicKey,
                PhoneNumber = _phoneNumber
            };
            return ServerCommunication.CreateRegistry(registry);
        }
    }
}