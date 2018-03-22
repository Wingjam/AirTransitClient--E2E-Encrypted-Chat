using System.Security.Cryptography;
using System.Text;
using AirTransit_Core.Models;
using AirTransit_Core.Repositories;
using AirTransit_Core.Services;
using AirTransit_Core.Utilities;
using FakeItEasy;
using Xunit;

namespace AirTransit_Standard_Tests.Services
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
        
        [Fact]
        public void EncryptMessage_CanBeDecryptedSuccessfully()
        {
            var keySet = GenerateValidRsaKeySet();
            A.CallTo(() => this._keySetRepository.GetKeySet()).Returns(keySet);
            var message = "1234pouet";
            
            var contact = new Contact
            {
                PublicKey = keySet.PublicKey
            };
            
            var encryptedMessage = this._rsaEncryptionService.Encrypt(message, contact);
            Assert.Equal(message, this._rsaEncryptionService.Decrypt(encryptedMessage));

        }

        [Fact]
        public void EncryptLongMessage_CanBeDecryptedSuccessfully()
        {
            var keySet = GenerateValidRsaKeySet();
            A.CallTo(() => this._keySetRepository.GetKeySet()).Returns(keySet);
            var longMessage = "{\"Content\":\"allo\",\"SenderPhoneNumber\":\"8198888888\",\"Signature\":\"imB0iiYKTr4lxaXdSRp/Wyuyqk2jjT6EdWlRaSCjPYrPZwsy7JT8VuixqLSyTsbehyW9nT3bARxVpadl0VZ9qfBfyOojyM75qkJRY0a+8oyaRXM7ahFreZR3jrGVbGV+ZhvmbvabQnLdmSZ2LAEhbWUfaZBx1uQfB5/I48zRMNZ7NUARK9f1etdRzPWWO3ApvdgrSmasaZ5N5xPtp6oU0jWOF9EF9q4kDQhj4rnYSBmS7tOwkhZ7Mj6ywe+HkSM2hGSCGSvShXPqtFB3E3Mp/MyiqVHsV2fzqGOkBwtM6zf7S5lWqBl5PGPx4SFqk5uSGVuLKOVOTXv65pIPmpUprA==\",\"Timestamp\":\"2018-03-22T16:29:14.317983-04:00\"}";

            var contact = new Contact
            {
                PublicKey = keySet.PublicKey
            };

            var encryptedMessage = this._rsaEncryptionService.Encrypt(longMessage, contact);
            Assert.Equal(longMessage, this._rsaEncryptionService.Decrypt(encryptedMessage));

        }

        #region Helpers
        private static KeySet GenerateValidRsaKeySet()
        {
            using (var rsa = new RSACryptoServiceProvider(RSAEncryptionService.KEY_SIZE))
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