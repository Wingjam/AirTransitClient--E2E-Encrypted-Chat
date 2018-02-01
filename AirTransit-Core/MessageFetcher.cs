using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AirTransit_Core
{
    public class MessageFetcher
    {
        private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private readonly Task _fetchTask;
        
        public MessageFetcher(Action<IEnumerable<String>> onNewMessageAction, TimeSpan pollingRate)
        {
            var cancellationToken = this._tokenSource.Token;

            this._fetchTask = new Task(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var messages = Fetch();
                    Console.WriteLine("###");
                    if (messages.Any())
                    {
                        onNewMessageAction(messages);
                    }
                    Console.WriteLine("...");
                    Thread.Sleep(pollingRate);
                }
            }, cancellationToken);
            this._fetchTask.Start();
        }

        public void Stop()
        {
            this._tokenSource.Cancel();
        }

        private IEnumerable<String> Fetch()
        {
            // Fetch from server
            // Fill a list of messages
            return new List<string>{"123", "456"};
        }
    }
}