using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AirTransit_Core.Models;

namespace AirTransit_Core
{
    public static class ServerCommunication
    {
        static HttpClient client = new HttpClient();

        // ===================
        // ===== Message =====
        // ===================
        public static async Task<string> CreateMessageAsync(EncryptedMessage message)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/message", message);
            response.EnsureSuccessStatusCode();

            // Deserialize the added message from the response body.
            message = await response.Content.ReadAsAsync<EncryptedMessage>();
            return message.Guid;
        }

        public static async Task<List<EncryptedMessage>> GetMessagesAsync(string phoneNumber, string auth)
        {
            List<EncryptedMessage> messages = null;
            HttpResponseMessage response = await client.GetAsync($"api/message/{phoneNumber}?authSignature={auth}");
            if (response.IsSuccessStatusCode)
            {
                messages = await response.Content.ReadAsAsync<List<EncryptedMessage>>();
            }
            return messages;
        }

        static async Task<bool> DeleteMessageAsync(string id, string auth)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/message/{id}?authSignature={auth}");
            return response.StatusCode == HttpStatusCode.NoContent;
        }

        // ====================
        // ===== Registry =====
        // ====================
        public static async Task<bool> CreateRegistryAsync(Registry registry)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/registry", registry);
            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode;
        }

        public static async Task<Registry> GetRegistryAsync(string phoneNumber)
        {
            Registry registry = null;
            HttpResponseMessage response = await client.GetAsync($"api/registry/{phoneNumber}");
            if (response.IsSuccessStatusCode)
            {
                registry = await response.Content.ReadAsAsync<Registry>();
            }
            return registry;
        }

        static async Task<Registry> UpdateRegistryAsync(Registry registry)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/registry/{registry.PhoneNumber}", registry);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated registry from the response body.
            registry = await response.Content.ReadAsAsync<Registry>();
            return registry;
        }

        static async Task<bool> DeleteRegistryAsync(string phoneNumber)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/registry/{phoneNumber}");
            return response.StatusCode == HttpStatusCode.NoContent;
        }
    }
}
