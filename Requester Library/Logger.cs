using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace Lomtseu
{
    public interface ILogger
    {
        void Log(String index, HttpRequestMessage request);
        void Log(String index, HttpResponseMessage response);
    }

    public class Logger: ILogger, IDisposable
    {
        private Boolean _isSavingFile;
        private ICollection<String> _fileNamesStringsCollection;

        public Logger(Boolean isSavingFile = false)
        {
            this._isSavingFile = isSavingFile;
            this._fileNamesStringsCollection = new List<String>();
        }

        protected void LogToFile(String fileName, IEnumerable<String> contentStrings)
        {
            using (var fileStream = new FileStream(fileName, FileMode.CreateNew))
            {
                StreamWriter streamWriter = new StreamWriter(fileStream);

                foreach (var currString in contentStrings)
                {
                    streamWriter.Write(currString);
                }
            }
        }

        public void Log(String index, HttpRequestMessage request)
        {
            this.LogToFile(
                String.Format("{0}.request", index),
                new List<String>()
                {
                    String.Format("HTTP/{0}", request.Version.ToString()),
                    request.Headers.Host
                }
            );
        }

        public void Log(String index, HttpResponseMessage response)
        {
            this.LogToFile(
                String.Format("{0}.response", index),
                new List<String>()
                {
                    String.Format("HTTP/{0} {1} {2}", response.Version.ToString(), response.StatusCode, response.ReasonPhrase),
                    String.Format("Server: {0}", response.Headers.Server),
                    String.Format("Content-Type: {0}", response.Content.Headers.ContentType),
                    String.Format("Date: {0}", response.Headers.Date),
                    String.Format("Connection: {0}", response.Headers.Connection),
                    String.Format("Content-Length: {0}", response.Content.Headers.ContentLength),
                    Environment.NewLine
                }
            );
        }

        public void Dispose()
        {
            if (!this._isSavingFile)
            {
                foreach (var fileNameString in this._fileNamesStringsCollection)
                {
                    if (File.Exists(fileNameString))
                    {
                        File.Delete(fileNameString);
                    }
                }
            }
        }
    }

    public interface IFactory<T>
    {
        T Produce();
    }

    public class LoggerFactory: IFactory<Logger>
    {
        public Logger Produce()
        {
            Boolean isSavingFileValue = false;
            object obj = Environment.Version;

            return new Logger(isSavingFileValue);
        }
    }

    public static class Loggers {
        private static IFactory<Logger> factory = new LoggerFactory(); 

        public static Logger GetNew()
        {
            return factory.Produce();
        }
    }
}