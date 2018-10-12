using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Lomtseu
{
    public class Requester : IRequester
    {
        private HttpClient _httpClient;

        protected HttpClient Client {
            get => this._httpClient;
        }

        public Requester(DelegatingHandler handler)
        {
            this._httpClient = new HttpClient(handler);
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