using System.Security.Cryptography;
using System.Text;
﻿using System;
using AirTransit_Core.Models;
using AirTransit_Core.Repositories;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AirTransit_Core.Services
{
    internal class MessageService : IMessageService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly Encoding _encoding;
        private string _phoneNumber;

        public MessageService(
            IContactRepository contactRepository,
            IMessageRepository messageRepository, 
            IEncryptionService encryptionService, 
            Encoding encoding,
            string phoneNumber)
        {
            this._contactRepository = contactRepository;
            this._messageRepository = messageRepository;
            this._encryptionService = encryptionService;
            this._encoding = encoding;
            this._phoneNumber = phoneNumber;
        }

        public bool SendMessage(Contact destination, string content)
        {
            if (destination.PublicKey == null)
            {
                // Get the public key of the contact through a server call
                Task<Registry> taskRegistry = ServerCommunication.GetRegistryAsync(destination.PhoneNumber);
                taskRegistry.Wait();
                destination.PublicKey = taskRegistry.Result.PublicKey;
                _contactRepository.UpdateContact(destination);
            }

            MessageDTO messageDTO = new MessageDTO()
            {
                SenderPhoneNumber = _phoneNumber,
                Content = content,
                Timestamp = DateTime.Now,
                Signature = _encryptionService.GenerateSignature(destination.PhoneNumber)
            };

            string standard_message = JsonConvert.SerializeObject(messageDTO);
            string encrypted_standard_message = _encryptionService.Encrypt(standard_message, destination);

            EncryptedMessage encryptedMessage = new EncryptedMessage()
            {
                Content = encrypted_standard_message,
                DestinationPhoneNumber = destination.PhoneNumber,
            };

            Task<string> task = ServerCommunication.CreateMessageAsync(encryptedMessage);
            task.Wait();
            string guid = task.Result;
            
            Message message = new Message()
            {
                Id = guid,
                Content = content,
                DestinationPhoneNumber = destination.PhoneNumber,
                // TODO
                //Sender = _contactRepository.GetSelf(),
                Timestamp = messageDTO.Timestamp
            };

            _messageRepository.AddMessage(message);

            return true;
        }

        public Message ReceiveNewMessages(EncryptedMessage encryptedMessage)
        {
            // 1. decrypt message
            MessageDTO decryptedMessage = StringToMessageDTO(_encryptionService.Decrypt(encryptedMessage.Content));

            // 2. Add the contact if he do not exist
            Contact senderContact = _contactRepository.GetContact(decryptedMessage.SenderPhoneNumber);
            if (senderContact == null)
            {
                // TODO aller fetch la public key du contact et creer le contact avec cette clef
                senderContact = new Contact(decryptedMessage.SenderPhoneNumber, decryptedMessage.SenderPhoneNumber);
                _contactRepository.AddContact(senderContact);
            }

            // 2.5 Validate that message with its signature
            if (!_encryptionService.VerifySignature(encryptedMessage.DestinationPhoneNumber, decryptedMessage.Signature, senderContact))
            {
                // If the signature is invalid, we skip this message.
                return null;
            }
            // 3. Add the message in the BD
            Message message = new Message()
            {
                Id = encryptedMessage.Guid,
                Sender = senderContact,
                Content = decryptedMessage.Content,
                DestinationPhoneNumber = encryptedMessage.DestinationPhoneNumber,
                Timestamp = decryptedMessage.Timestamp
            };
            _messageRepository.AddMessage(message);

            // 4. Push the new message in the blocking collection
            return message;
        }

        private MessageDTO StringToMessageDTO(string decryptedMessage)
        {
            MessageDTO messageDTO = JsonConvert.DeserializeObject<MessageDTO>(decryptedMessage);
            return messageDTO;
        }

        private string MessageDTOToString(MessageDTO message)
        {
            return JsonConvert.SerializeObject(message);
        }

    }
}