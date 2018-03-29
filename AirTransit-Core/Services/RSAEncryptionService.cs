using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using AirTransit_Core.Models;
using AirTransit_Core.Repositories;
using AirTransit_Core.Utilities;
using System.Linq;

[assembly: InternalsVisibleTo("AirTransit_Core_Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace AirTransit_Core.Services
{
    internal class RSAEncryptionService : IEncryptionService
    {
        private readonly IKeySetRepository _keySetRepository;
        private readonly Encoding _encoding;
        internal static readonly int KEY_SIZE = 2048;
        internal static readonly int ENCRYPTED_CHUNK_SIZE = 256;
            
        public RSAEncryptionService(IKeySetRepository keySetRepository, Encoding encoding)
        {
            this._keySetRepository = keySetRepository;
            this._encoding = encoding;
        }
        
        public string GenerateSignature(string content)
        {
            try
            {
                using (var rsa = new RSACryptoServiceProvider(KEY_SIZE))
                {
                    var contentBytes = this._encoding.GetBytes(content);
                    var clientKey = this._keySetRepository.GetKeySet();
                    rsa.FromXmlStringNetCore(clientKey.PrivateKey);
                    var signature = rsa.SignData(contentBytes, CryptoConfig.MapNameToOID("SHA256"));
                    return Convert.ToBase64String(signature);
                }
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        
        public bool VerifySignature(string signature, string signedData, Contact contact)
        {
            try
            {
                using (var rsa = new RSACryptoServiceProvider(KEY_SIZE))
                {
                    var clientKey = contact.PublicKey;
                    rsa.FromXmlStringNetCore(clientKey);
                    var signatureBytes = this._encoding.GetBytes(signature);
                    var signedDataBytes = Convert.FromBase64String(signedData);
                    
                    var hash = new SHA256Managed();

                    bool dataOK = rsa.VerifyData(signedDataBytes, CryptoConfig.MapNameToOID("SHA256"), signatureBytes);
                    var hashedData = hash.ComputeHash(signedDataBytes);
                    return dataOK && rsa.VerifyHash(hashedData, CryptoConfig.MapNameToOID("SHA256"), signatureBytes);
                }
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        
        internal IEnumerable<byte[]> SplitMessage(byte[] message, int chunkSize)
        {
            int chunkCount = (int)Math.Ceiling((double)message.Length / chunkSize);
            for (int i = 0; i < chunkCount; ++i)
                yield return message.Skip(i * chunkSize).Take(chunkSize).ToArray();
        }
        
        public string Encrypt(string message, Contact contact)
        {
            byte[] messageBytes = _encoding.GetBytes(message);
            
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(KEY_SIZE))
                {
                    rsa.FromXmlStringNetCore(contact.PublicKey);
                    var encryptedData = rsa.Encrypt(messageBytes, RSAEncryptionPadding.Pkcs1);
                    return Convert.ToBase64String(encryptedData.ToArray());
                }
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    
        public string Decrypt(string encryptedMessage)
        {
            var key = this._keySetRepository.GetKeySet();
            var encryptedMessageBytes = Convert.FromBase64String(encryptedMessage);
            
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(KEY_SIZE))
                {
                    rsa.FromXmlStringNetCore(key.PrivateKey);
                    var decryptedData = SplitMessage(encryptedMessageBytes, ENCRYPTED_CHUNK_SIZE)
                        .SelectMany(chunk => rsa.Decrypt(chunk, RSAEncryptionPadding.Pkcs1));
                    return _encoding.GetString(decryptedData.ToArray());
                }
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
    
                return null;
            }
        }
    }
}