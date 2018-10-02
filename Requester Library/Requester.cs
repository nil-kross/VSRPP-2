using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Lomtseu
{
    public class ResponseHandler : DelegatingHandler
    {
        public ResponseHandler()
        {
            this.InnerHandler = new HttpClientHandler();
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // work on the request 
            //Trace.WriteLine(request.RequestUri.ToString());

            return base.SendAsync(request, cancellationToken)
             .ContinueWith(task =>
             {
             // work on the response
             var response = task.Result;
                 response.Headers.Add("X-Dummy-Header", Guid.NewGuid().ToString());
                 return response;
             });
        }
    }

    public class Requester
    {
        private HttpClient _httpClient = new HttpClient(new ResponseHandler());

        protected HttpClient Client {
            get => this._httpClient;
        }

        protected HttpResponseMessage GetResponseMessage(Uri uri)
        {
            return this.Client.GetAsync(uri, HttpCompletionOption.ResponseContentRead).Result;
        }

        public String GetResponseString(Uri uri)
        {
            String responseString = null;
            var response = this.GetResponseMessage(uri);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                responseString = response.Content.ReadAsStringAsync().Result;
            }

            return responseString;
        }

        public T GetResponseItem<T>(Uri uri)
        {
            T item = default(T);
            var response = this.GetResponseMessage(uri);

            if (response.IsSuccessStatusCode)
            {
                item = response.Content.ReadAsAsync<T>().Result;
            }

            return item;
        }

        public IEnumerable<T> GetResponseItems<T>(Uri uri)
        {
            return this.GetResponseItem<IEnumerable<T>>(uri);
        }
    }
}