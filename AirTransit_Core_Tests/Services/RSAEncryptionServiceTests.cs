using System.Linq;
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
        private readonly Encoding _encoding;

        public RSAEncryptionServiceTests()
        {
            this._keySetRepository = A.Fake<IKeySetRepository>();
            this._encoding = Encoding.UTF8;
            this._rsaEncryptionService = new RSAEncryptionService(this._keySetRepository, this._encoding);
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
        public void EncryptMessage_WithShortMessage_CanBeDecryptedSuccessfully()
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
        public void Encrypt_WithLongMessage_CanBeDecryptedSuccessfully()
        {
            var keySet = GenerateValidRsaKeySet();
            A.CallTo(() => this._keySetRepository.GetKeySet()).Returns(keySet);
            var longMessage = "{\"Content\":\"allo\",\"SenderPhoneNumber\":\"8198888888\",\"Signature\":\"imB0iiYKTr4lxaXdSRp/Wyuyqk2jjT6EdWlRaSCjPYrPZwsy7JT8VuixqLSyTsbehyW9nT3bARxVpadl0VZ9qfBfyOojyM75qkJRY0a+8oyaRXM7ahFreZR3jrGVbGV+ZhvmbvabQnLdmSZ2LAEhbWUfaZBx1uQfB5/I48zRMNZ7NUARK9f1etdRzPWWO3ApvdgrSmasaZ5N5xPtp6oU0jWOF9EF9q4kDQhj4rnYSBmS7tOwkhZ7Mj6ywe+HkSM2hGSCGSvShXPqtFB3E3Mp/MyiqVHsV2fzqGOkBwtM6zf7S5lWqBl5PGPx4SFqk5uSGVuLKOVOTXv65pIPmpUprA==\",\"Timestamp\":\"2018-03-22T16:29:14.317983-04:00\"}";

            var contact = new Contact
            {
                PublicKey = keySet.PublicKey
            };

            var encryptedMessage = this._rsaEncryptionService.Encrypt(longMessage, contact);
            var decryptedMessage = this._rsaEncryptionService.Decrypt(encryptedMessage);
            Assert.Equal(longMessage, decryptedMessage);

        }

        #region SplitMessage
        [Fact]
        public void SplitMessage_WithStringLongerThanChunkSize_ShouldReturnChunksNotGreaterThanMaxSize()
        {
            var longMessage = "{\"Content\":\"allo\",\"SenderPhoneNumber\":\"8198888888\",\"Signature\":\"imB0iiYKTr4lxaXdSRp/Wyuyqk2jjT6EdWlRaSCjPYrPZwsy7JT8VuixqLSyTsbehyW9nT3bARxVpadl0VZ9qfBfyOojyM75qkJRY0a+8oyaRXM7ahFreZR3jrGVbGV+ZhvmbvabQnLdmSZ2LAEhbWUfaZBx1uQfB5/I48zRMNZ7NUARK9f1etdRzPWWO3ApvdgrSmasaZ5N5xPtp6oU0jWOF9EF9q4kDQhj4rnYSBmS7tOwkhZ7Mj6ywe+HkSM2hGSCGSvShXPqtFB3E3Mp/MyiqVHsV2fzqGOkBwtM6zf7S5lWqBl5PGPx4SFqk5uSGVuLKOVOTXv65pIPmpUprA==\",\"Timestamp\":\"2018-03-22T16:29:14.317983-04:00\"}";
            var chunkSize = 123;
            var splittedMessage = this._rsaEncryptionService.SplitMessage(this._encoding.GetBytes(longMessage), chunkSize).ToArray();
            Assert.True(splittedMessage.Count() > 1);
            foreach (var chunk in splittedMessage)
            {
                Assert.True(chunk.Count() <= chunkSize);
            }
        }
        
        [Fact]
        public void SplitMessage_WithStringShorterThanChunkSize_ShouldReturnOneChunk()
        {
            var longMessage = "1234pouet";
            var splittedMessage = this._rsaEncryptionService.SplitMessage(this._encoding.GetBytes(longMessage), 1000);
            Assert.Equal(splittedMessage.Count(), 1);
        }
        #endregion
        
        
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