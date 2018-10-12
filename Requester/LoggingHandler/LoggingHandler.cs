using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Lomtseu
{
    public class LoggingHandler : DelegatingHandler
    {
        private ILogger _logger;

        public LoggingHandler(ILogger logger)
        {
            this._logger = logger;
            this.InnerHandler = new HttpClientHandler();
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            String fetchNameString = String.Format(
                "{0}",
                DateTime.Now.ToString("yy_MM_dd hh_mm_fff")
            );

            this._logger.Log(fetchNameString, request);

            return base.SendAsync(request, cancellationToken)
                .ContinueWith(task =>
                {
                    var response = task.Result;

                    this._logger.Log(fetchNameString, response);

                    return response;
                });
        }
    }
}