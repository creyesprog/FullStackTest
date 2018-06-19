using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Diagnostics;

namespace FullStackTest.DAL
{
    //Singleton logging class
    public sealed class Logger
    {
        private static volatile Logger instance;
        private static readonly object syncLock = new object();

        private Logger()
        {
        }

        public static Logger Instance
        {
            get
            {
                if (instance != null) return instance;
                lock (syncLock)
                {
                    if (instance == null)
                    {
                        instance = new Logger();
                    }
                }
                return instance;
            }
        }

        //Wouldn't add to public folder, but another with more restrictions
        public void LogError(string error)
        {
            DateTime today = DateTime.UtcNow.Date;
            string fileName = today.ToString("dd-MM-yyyy") + ".err";
            string directory = @"C:\Users\Public\Logs";

            try
            {
                if (Directory.Exists(directory))
                { 
                }
                else
                {
                    Directory.CreateDirectory(@"C:\Users\Public\Logs");
                }
                using (StreamWriter file = new StreamWriter(@"C:\Users\Public\Logs\" + fileName))
                {
                    file.WriteLine(error);
                }
            }
            catch (Exception ex)
            {
                WriteToEventLog("Failed logging error: " + ex.ToString());
            }
        }

        private void WriteToEventLog(string message)
        {
            try
            {
                EventLog.WriteEntry("Application", message, EventLogEntryType.Error);
            }
            catch (Exception ex)
            {
                //nothing else to do
            }
        }
    }
}