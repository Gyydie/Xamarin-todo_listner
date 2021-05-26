using App1.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace App1.Services
{
    public class DataStoreWebItems : IDataStore<Item, string>
    {
        const string Url = "https://gyydieservice.azurewebsites.net/api/items"; // обращайте внимание на конечный слеш
       
        // настройки для десериализации для нечувствительности к регистру символов
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        // настройка клиента
        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        // получаем всех друзей
        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url);
            return JsonSerializer.Deserialize<IEnumerable<Item>>(result, options);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url + id);

            var Item = JsonSerializer.Deserialize<Item>(result, options);

            return await Task.FromResult(Item);
        }

        // добавляем одного друга
        public async Task<bool> AddItemAsync(Item item)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(Url,
                new StringContent(
                    JsonSerializer.Serialize(item),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return false;

            var answer = JsonSerializer.Deserialize<Item>(
                await response.Content.ReadAsStringAsync(), options);

            bool result = answer != null;
            return await Task.FromResult(result);
        }
        // обновляем друга
        public async Task<bool> UpdateItemAsync(Item item)
        {
            HttpClient client = GetClient();
            var response = await client.PutAsync(Url,
                new StringContent(
                    JsonSerializer.Serialize(item),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return false;

            var answer = JsonSerializer.Deserialize<Item>(
                await response.Content.ReadAsStringAsync(), options);

            bool result = answer != null;
            return await Task.FromResult(result);
        }
        // удаляем друга
        public async Task<bool> DeleteItemAsync(string id)
        {
            HttpClient client = GetClient();
            var response = await client.DeleteAsync(Url + id);
            if (response.StatusCode != HttpStatusCode.OK)
                return false;

            var answer = JsonSerializer.Deserialize<Item>(
               await response.Content.ReadAsStringAsync(), options);

            bool result = answer != null;
            return await Task.FromResult(result);
        }

    }
}
