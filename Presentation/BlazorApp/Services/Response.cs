using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public class Response<T>
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T ResponseData { get; set; }
    }

    public class Response
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
