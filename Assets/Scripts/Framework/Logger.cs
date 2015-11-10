using System;
using System.Collections;
using System.IO;
using UnityEngine;

public static class Logger {

	public static void Log(string message){
		Log (message, LogType.Log);
	}
	
	public static void Log(string message, LogType logType)
	{
		switch (logType) {
		case LogType.Assert:
			message = "[Assert] " + message;
			Debug.Log (message);
			break;
		case LogType.Error:
			message = "[Error] " + message;
			Debug.LogError (message);
			break;
		case LogType.Exception:
			message = "[Exception] " + message;
			Debug.LogError (message);
			break;
		case LogType.Log:
			message = "[Log] " + message;
			Debug.Log (message);
			break;
		case LogType.Warning:
			message = "[Warning] " + message;
			Debug.LogWarning (message);
			break;
		default:
			message = "[Log] " + message;
			Debug.Log (message);
			break;
		}
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
