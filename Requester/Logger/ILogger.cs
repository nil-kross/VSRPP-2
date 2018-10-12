using System;
using System.Net.Http;

namespace Lomtseu
{
    public interface ILogger
    {
        void Log(String index, HttpRequestMessage request);
        void Log(String index, HttpResponseMessage response);
    }
}