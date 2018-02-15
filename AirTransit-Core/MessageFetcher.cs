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

        private IEnumerable<EncryptedMessage> Fetch()
        {
            // TODO This method need to be completely tested.
            // Fetch from server
            string jsonInformation =
                GET($"{ CoreServices.SERVER_ADDRESS }/Messages?{ PhoneNumber }&{ AuthSignature }");

            // Fill a list of messages
            List<EncryptedMessage> deserializedProduct = JsonConvert.DeserializeObject<List<EncryptedMessage>>(jsonInformation);
            
            return deserializedProduct;
        }

        // Based on : https://stackoverflow.com/questions/8270464/best-way-to-call-a-json-webservice-from-a-net-console
        // Returns JSON string
        string GET(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }
    }
}