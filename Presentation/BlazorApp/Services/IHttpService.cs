using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public interface IHttpService
    {
        Task<T> GetAsync<T>(string uri);
        Task<T> PostAsync<T>(string uri, object value);
        Task<T> PutAsync<T>(string uri, object value);
        Task DeleteAsync(string uri);
    }
}
