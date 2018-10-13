using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Lomtseu
{
    public abstract class BaseLogger: ILogger
    {
        private Boolean _isSavingFilesValue;

        protected Boolean IsSavingFiles {
            get => this._isSavingFilesValue;
        }

        public BaseLogger(Boolean isSavingFiles = false)
        {
            this._isSavingFilesValue = isSavingFiles;
        }

        public void Log(String index, HttpRequestMessage request)
        {
            this.WriteStrings(
                String.Format("{0}.request", index),
                new List<String>()
                {
                    $"HTTP/{request.Version}",
                    $"Request URL: {request.RequestUri}",
                    $"Request Method: {request.Method}",
                    $"Accept: {request.Headers.Accept}",
                    $"Accept-Encoding: {request.Headers.AcceptEncoding}",
                    $"Accept-Language: {request.Headers.AcceptLanguage}",
                    $"Connection: {request.Headers.Connection}",
                    //$"Content-Length: {request?.Content.Headers.ContentLength.ToString() ?? ""}",
                    $"Host: {request.Headers.Host}",
                    $"Origin: {request.RequestUri.OriginalString}",
                    $"Refer: {request.RequestUri.LocalPath}",
                    $"User-Agent: {request.Headers.UserAgent}",
                    // X-Requested-With
                    Environment.NewLine,
                    //request?.Content.ToString() ?? ""
                }
            );
        }

        public void Log(String index, HttpResponseMessage response)
        {
            this.WriteStrings(
                String.Format("{0}.response", index),
                new List<String>()
                {
                    $"HTTP/{response.Version} {response.StatusCode} {response.ReasonPhrase}",
                    $"Connection: {response.Headers.Connection}",
                    $"Content-Length: {response.Content.Headers.ContentLength}",
                    $"Data: {response.Headers.Date}",
                    $"ETag: {response.Headers.ETag}",
                    $"Server: {response.Headers.Server}",
                    $"Var: {response.Headers.Vary}",
                    // X-Powered-By
                    Environment.NewLine,
                    response.Content?.ReadAsStringAsync().Result ?? ""
                }
            );
        }

        protected abstract void WriteStrings(String fileName, IEnumerable<String> contentStrings);
    }
}