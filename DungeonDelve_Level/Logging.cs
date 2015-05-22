using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DungeonDelve_Level
{
    public class Logging
    {
        string sLoggingFile = string.Empty;

        public Logging(string sLogFile)
        {
            if (File.Exists(sLogFile))
                File.Delete(sLogFile);
            sLoggingFile = sLogFile;
        }

        public void LogInfo(string sMsg)
        {
            File.AppendAllText(sLoggingFile, sMsg + Environment.NewLine);
        }
    }
}
