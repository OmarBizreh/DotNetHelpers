using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetHelpers.Logger
{
    public class Logger
    {
        public FileInfo LogFile { get; }

        public Logger(string _logPath)
        {
            this.LogFile = new FileInfo(_logPath);
        }

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
