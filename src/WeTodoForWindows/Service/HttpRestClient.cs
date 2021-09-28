using RestSharp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeTodoForWindows.Service
{
    public class HttpRestClient
    {
        private readonly string apiUrl;
        private readonly RestClient client;

        public HttpRestClient(string apiUrl)
        {
            this.apiUrl = apiUrl;
            client = new RestClient();
        }
    }
}
