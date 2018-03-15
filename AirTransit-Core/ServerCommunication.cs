using AirTransit_Core.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AirTransit_Core
{
    public static class ServerCommunication
    {
        public static string ServerAddress = "http://jo2server.ddns.net:5000/";
        private static readonly HttpClient Client = new HttpClient
        {
            BaseAddress = new Uri(ServerAddress),
            DefaultRequestHeaders = {Accept = {new MediaTypeWithQualityHeaderValue("application/json")}}
        };

        // ===================
        // ===== Message =====
        // ===================
        public static string CreateMessage(EncryptedMessage message)
        {
            HttpResponseMessage response = Client.PostAsJsonAsync("api/message", message).Result;

            response.EnsureSuccessStatusCode();

            // Deserialize the added message from the response body.
            message = response.Content.ReadAsAsync<EncryptedMessage>().Result;
            return message.Guid;
        }

        public static List<EncryptedMessage> GetMessages(string phoneNumber, string auth)
        {
            List<EncryptedMessage> messages = null;
            HttpResponseMessage response = Client.GetAsync($"api/message/{phoneNumber}?authSignature={auth}").Result;
            if (response.IsSuccessStatusCode)
            {
                messages = response.Content.ReadAsAsync<List<EncryptedMessage>>().Result;
            }
            return messages;
        }

        public static bool DeleteMessage(string id, string auth)
        {
            HttpResponseMessage response = Client.DeleteAsync(
                $"api/message/{id}?authSignature={auth}").Result;
            return response.StatusCode == HttpStatusCode.NoContent;
        }

        // ====================
        // ===== Registry =====
        // ====================
        public static bool CreateRegistry(Registry registry)
        {
            HttpResponseMessage response = Client.PostAsJsonAsync(
                "api/registry", registry).Result;
            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode;
        }

        public static Registry GetRegistry(string phoneNumber)
        {
            Registry registry = null;
            HttpResponseMessage response = Client.GetAsync($"api/registry/{phoneNumber}").Result;
            if (response.IsSuccessStatusCode)
            {
                registry = response.Content.ReadAsAsync<Registry>().Result;
            }
            return registry;
        }

        public static Registry UpdateRegistry(Registry registry)
        {
            HttpResponseMessage response = Client.PutAsJsonAsync(
                $"api/registry/{registry.PhoneNumber}", registry).Result;
            response.EnsureSuccessStatusCode();

            // Deserialize the updated registry from the response body.
            registry = response.Content.ReadAsAsync<Registry>().Result;
            return registry;
        }

        public static bool DeleteRegistry(string phoneNumber)
        {
            HttpResponseMessage response = Client.DeleteAsync(
                $"api/registry/{phoneNumber}").Result;
            return response.StatusCode == HttpStatusCode.NoContent;
        }
    }
}
