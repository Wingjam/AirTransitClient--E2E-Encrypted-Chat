using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AirTransit_Core.Models;
using Newtonsoft.Json;

namespace AirTransit_Core
{
    public class MessageFetcher
    {
        private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private readonly Task _fetchTask;
        private string PhoneNumber;
        private string AuthSignature;

        public MessageFetcher(Action<IEnumerable<EncryptedMessage>> onNewMessageAction, TimeSpan pollingRate, string phoneNumber, string authSignature)
        {
            var cancellationToken = this._tokenSource.Token;
            PhoneNumber = phoneNumber;
            AuthSignature = authSignature;

            this._fetchTask = new Task(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var messages = Fetch();
                    if (messages.Any())
                    {
                        onNewMessageAction(messages);
                    }
                    Thread.Sleep(pollingRate);
                }
            }, cancellationToken);
            this._fetchTask.Start();
        }

        public void Stop()
        {
            this._tokenSource.Cancel();
        }

        private IEnumerable<EncryptedMessage> Fetch()
        {
            return ServerCommunication.GetMessages(PhoneNumber, AuthSignature);
        }
    }
}