using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace Nimbi.Framework
{
    public static class Logger
    {

        /// <summary>
        /// Logs a message to the console and the log file
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void Log(string message)
        {
            Log(message, LogType.Log);
        }

        /// <summary>
        /// Logs a message to the console and the log file
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="logType">Type of message to log</param>
        public static void Log(string message, LogType logType)
        {
            switch (logType)
            {
                case LogType.Assert:
                    message = "[Assert] " + message;
                    Debug.Log(message);
                    break;
                case LogType.Error:
                    message = "[Error] " + message;
                    Debug.LogError(message);
                    break;
                case LogType.Exception:
                    message = "[Exception] " + message;
                    Debug.LogError(message);
                    break;
                case LogType.Log:
                    message = "[Log] " + message;
                    Debug.Log(message);
                    break;
                case LogType.Warning:
                    message = "[Warning] " + message;
                    Debug.LogWarning(message);
                    break;
                default:
                    message = "[Log] " + message;
                    Debug.Log(message);
                    break;
            }
            using (StreamWriter w = File.AppendText("log.txt"))
            {
                WriteLog(message, w);
            }
        }

        /// <summary>
        /// Logs an exception to the log file and the console
        /// </summary>
        /// <param name="e">The exception</param>
        public static void Log(Exception e)
        {
            Debug.LogException(e);
            string message = "[Exception] " + e.ToString();
            using (StreamWriter w = File.AppendText("log.txt"))
            {
                WriteLog(message, w);
            }
        }

        private static void WriteLog(string logMessage, TextWriter w)
        {
            w.WriteLine("{0} {1} : {2}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), logMessage);
        }

        public static void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Debug.Log(line);
            }
        }

    }
}