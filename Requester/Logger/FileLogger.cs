using System;
using System.Collections.Generic;
using System.IO;

namespace Lomtseu
{
    public class FileLogger : BaseLogger, IDisposable
    {
        private ICollection<String> _fileNamesStringsCollection;

        public FileLogger(Boolean isSavingFile = false) : base(isSavingFile)
        {
            this._fileNamesStringsCollection = new List<String>();
        }

        protected override void WriteStrings(String fileName, IEnumerable<String> contentStrings)
        {
            using (var fileStream = new FileStream(fileName, FileMode.CreateNew))
            {
                StreamWriter streamWriter = new StreamWriter(fileStream);

                foreach (var currString in contentStrings)
                {
                    streamWriter.Write(currString);
                }
                streamWriter.Flush();
            }
        }

        public void Dispose()
        {
            if (!this.IsSavingFiles)
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
}