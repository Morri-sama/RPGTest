using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public interface IHttpService
    {
        Task<Response<T>> GetAsync<T>(string uri, string xdd);
        Task<T> GetAsync<T>(string uri);
        Task<Response> GetAsync(string uri);
        Task<T> PostAsync<T>(string uri, object value);
        Task PostAsync(string uri);
        Task<T> PutAsync<T>(string uri, object value);
        Task PutAsync(string uri);
        Task DeleteAsync(string uri);
    }
}
