using System;
using System.IO;

namespace DotNetHelpers.Logger
{
    public class Logger
    {
        public FileInfo LogFile { get; }

        /// <summary>
        /// Create a new instance of <see cref="Logger"/> class
        /// </summary>
        /// <param name="_logPath">Path to create a log file in</param>
        public Logger(string _logPath)
        {
            this.LogFile = new FileInfo(_logPath);
        }

        /// <summary>
        /// Write log message to file
        /// </summary>
        /// <param name="Message">Message to write</param>
        /// <param name="IncludeTimeStamp">If to include timestamp</param>
        /// <param name="IncludeLineSeperator">if to seperate messages with a string line seperator</param>
        public void WriteLog(string Message, bool IncludeTimeStamp = true, bool IncludeLineSeperator = true)
        {
            using (FileStream fs = new FileStream(this.LogFile.FullName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                if (IncludeTimeStamp) sw.WriteLine(DateTime.Now.ToString());
                sw.WriteLine(Message);
                if (IncludeLineSeperator) sw.WriteLine("-------------------------------------------------------------");
            }
        }
    }
}