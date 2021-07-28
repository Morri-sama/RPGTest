using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return await SendRequestAsync<T>(request);
        }

        public async Task<Response> GetAsync(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return await SendRequestAsync(request, string.Empty);
        }



        public async Task<T> PostAsync<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return await SendRequestAsync<T>(request);
        }

        public async Task PostAsync(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            await SendRequestAsync(request);
        }

        public async Task<T> PutAsync<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return await SendRequestAsync<T>(request);
        }

        public async Task PutAsync(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            await SendRequestAsync(request);
        }

        public async Task DeleteAsync(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            await SendRequestAsync(request);
        }

        private async Task<T> SendRequestAsync<T>(HttpRequestMessage request, bool sendAuthHeader = true)
        {
            using var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                throw new Exception(error["message"]);
            }

            return await response.Content.ReadFromJsonAsync<T>();
        }

        private async Task SendRequestAsync(HttpRequestMessage request)
        {
            using var response = await _httpClient.SendAsync(request);
        }

        private async Task<Response<T>> SendRequestAsync<T>(HttpRequestMessage request, string xdd)
        {
            using var response = await _httpClient.SendAsync(request);

            Response<T> response2 = new Response<T>()
            {
                IsSuccess = response.IsSuccessStatusCode,
                StatusCode = (int)response.StatusCode,
                Message = response.ReasonPhrase,
                ResponseData = await response.Content.ReadFromJsonAsync<T>()
            };

            return response2;
        }


        private async Task<Response> SendRequestAsync(HttpRequestMessage request, string xdd)
        {
            using var response = await _httpClient.SendAsync(request);

            Response response2 = new Response()
            {
                IsSuccess = response.IsSuccessStatusCode,
                StatusCode = (int)response.StatusCode,
                Message = response.ReasonPhrase
            };

            return response2;
        }

        public async Task<Response<T>> GetAsync<T>(string uri, string xdd)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return await SendRequestAsync<T>(request, string.Empty);
        }
    }
}

