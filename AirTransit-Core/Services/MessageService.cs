using System.Security.Cryptography;
using System.Text;
﻿using System;
using AirTransit_Core.Models;
using AirTransit_Core.Repositories;
using Newtonsoft.Json;

namespace AirTransit_Core.Services
{
    internal class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly Encoding _encoding;
        private string _phoneNumber;

        public MessageService(
            IMessageRepository messageRepository, 
            IEncryptionService encryptionService, 
            Encoding encoding,
            string phoneNumber)
        {
            this._messageRepository = messageRepository;
            this._encryptionService = encryptionService;
            this._encoding = encoding;
            this._phoneNumber = phoneNumber;
        }
        
        public void PersistMessageLocally(Contact destination, string content)
        {
            this._messageRepository.AddMessage(new Message
            {
                Content = content,
                DestinationPhoneNumber = destination.PhoneNumber,
            });
        }
        
        public bool SendMessage(Contact destination, string content)
        {
            MessageDTO messageDTO = new MessageDTO()
            {
                SenderPhoneNumber = _phoneNumber,
                Content = content,
                Timestamp = DateTime.Now
            };

            string standard_message = JsonConvert.SerializeObject(messageDTO);
            string encrypted_standard_message = _encryptionService.Encrypt(standard_message, destination);

            EncryptedMessage encryptedMessage = new EncryptedMessage()
            {
                Content = encrypted_standard_message,
                DestinationPhoneNumber = destination.PhoneNumber
            };

            // Task task = ServerCommunication.CreateMessageAsync(encryptedMessage);
            // task.Wait();
            // string guid = task.Result;
            string guid = "";

            //Message message = new Message()
            //{
            //    Id = guid,

            //}

            return true;
        }
    }
}