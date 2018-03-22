using AirTransit_Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace AirTransit_Core
{
    public static class ServerCommunication
    {
        public static string ServerAddress = "http://jo2server.ddns.net:5000/";
        private static readonly HttpClient Client = new HttpClient();

        // ===================
        // ===== Message =====
        // ===================
        public static string CreateMessage(EncryptedMessage message)
        {
            return PostAsJsonAsync<EncryptedMessage, EncryptedMessage>(Client, "api/message", message).Guid;
        }

        public static List<EncryptedMessage> GetMessages(string phoneNumber, string auth)
        {
            return GetFromJsonAsync<List<EncryptedMessage>>(Client, $"api/message/{phoneNumber}?authSignature={auth}");
        }

        public static bool DeleteMessage(string id, string auth)
        {
            return DeleteAsync(Client, $"api/message/{id}?authSignature={auth}");
        }

        // ====================
        // ===== Registry =====
        // ====================
        public static void CreateRegistry(Registry registry)
        {
            // Will throw if server returns error
            PostAsJsonAsync<Registry, Registry>(Client, "api/registry", registry);
        }

        public static Registry GetRegistry(string phoneNumber)
        {
            return GetFromJsonAsync<Registry>(Client, $"api/registry/{phoneNumber}");
        }

        public static Registry UpdateRegistry(Registry registry)
        {
            return PutAsJsonAsync<Registry, Registry>(Client, $"api/registry/{registry.PhoneNumber}", registry);
        }

        public static bool DeleteRegistry(string phoneNumber)
        {
            return DeleteAsync(Client, $"api/registry/{phoneNumber}");
        }

        public static R GetFromJsonAsync<R>(HttpClient client, String uri)
        {
            var response = client.GetAsync(BuildUri(uri), HttpCompletionOption.ResponseHeadersRead).Result;

            response.EnsureSuccessStatusCode();

            return DeserializeAsyncHttpReponse<R>(response);
        }

        public static R PostAsJsonAsync<T, R>(HttpClient client, string uri, T message)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");
            var response = client.PostAsync(BuildUri(uri), stringContent).Result;

            response.EnsureSuccessStatusCode();

            return DeserializeAsyncHttpReponse<R>(response);
        }

        public static R PutAsJsonAsync<T, R>(HttpClient client, string uri, T message)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");
            var response = client.PutAsync(BuildUri(uri), stringContent).Result;

            response.EnsureSuccessStatusCode();

            return DeserializeAsyncHttpReponse<R>(response);
        }

        public static bool DeleteAsync(HttpClient client, string uri)
        {
            HttpResponseMessage response = client.DeleteAsync(
                BuildUri(uri)).Result;
            return response.StatusCode == HttpStatusCode.NoContent;
        }

        public static R DeserializeAsyncHttpReponse<R>(HttpResponseMessage response)
        {
            using (var stream = response.Content.ReadAsStreamAsync().Result)
            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                var serializer = new JsonSerializer();
                return serializer.Deserialize<R>(jsonReader);
            }
        }

        private static string BuildUri(string uri)
        {
            return ServerAddress + uri;
        }
    }
}
