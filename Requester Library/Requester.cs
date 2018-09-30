using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Lomtseu
{
    public class Requester
    {
        private HttpClient _httpClient = new HttpClient();

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