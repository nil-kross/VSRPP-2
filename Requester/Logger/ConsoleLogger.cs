using System;
using System.Collections.Generic;

namespace Lomtseu.Logger
{
    public class ConsoleLogger : BaseLogger
    {
        public ConsoleLogger(Boolean isSavingFile = false) : base(isSavingFile) { }

        protected override void WriteStrings(string fileName, IEnumerable<string> contentStrings)
        {
            foreach (var currString in contentStrings)
            {
                Console.WriteLine(currString);
            }
        }
    }
}