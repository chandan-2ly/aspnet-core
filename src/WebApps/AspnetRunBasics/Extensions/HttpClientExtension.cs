using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AspnetRunBasics.Extensions
{
    public static class HttpClientExtension
    {
        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Something went wrong while calling API: {response.ReasonPhrase}");
            }

            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result =  JsonConvert.DeserializeObject<T>(dataAsString);
            return result;
        }

        public static Task<HttpResponseMessage> PostAsJson<T>(this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return httpClient.PostAsync(url, content);
        }

        public static Task<HttpResponseMessage> PutAsJson<T>(this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return httpClient.PutAsync(url, content);
        }

    }
}
