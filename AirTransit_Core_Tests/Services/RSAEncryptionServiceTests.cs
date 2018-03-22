using System.Security.Cryptography;
using System.Text;
using AirTransit_Core.Models;
using FakeItEasy;
using Xunit;
using AirTransit_Core.Repositories;
using AirTransit_Core.Services;
using AirTransit_Core.Utilities;

namespace AirTransit_Core_Tests.Services
{
    public class RSAEncryptionServiceTests
    {
        private readonly RSAEncryptionService _rsaEncryptionService;
        private readonly IKeySetRepository _keySetRepository;
        
        public RSAEncryptionServiceTests()
        {
            this._keySetRepository = A.Fake<IKeySetRepository>();
            this._rsaEncryptionService = new RSAEncryptionService(this._keySetRepository, Encoding.UTF8);
        }
        
        [Fact]
        public void GenerateSignature_WithGivenPrivateKey_ShouldBeVerifiableWithAssociatedPublicKey()
        {
            var keySet = GenerateValidRsaKeySet();
            A.CallTo(() => this._keySetRepository.GetKeySet()).Returns(keySet);
            var signature = "1234pouet";
            var encryptedSignature = this._rsaEncryptionService.GenerateSignature(signature);
            
            var contact = new Contact
            {
                PublicKey = keySet.PublicKey
            };
            
            Assert.True(this._rsaEncryptionService.VerifySignature(signature, encryptedSignature, contact));

        }


        #region Helpers
        private static KeySet GenerateValidRsaKeySet()
        {
            using (var rsa = RSA.Create())
            {
                var publicKey = rsa.ToXmlStringNetCore(false);
                var privateKey = rsa.ToXmlStringNetCore(true);
                var keySet = new KeySet("123", publicKey, privateKey);

                return keySet; 
            }
        }
        #endregion
    }
}