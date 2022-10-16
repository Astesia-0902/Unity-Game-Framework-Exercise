using System;
using System.IO;
using System.Text;
using UnityGameFramework.Runtime;
using UnityEngine;
using Utility = GameFramework.Utility;

namespace Tutorial
{
    internal class FileLogHelper : DefaultLogHelper
    {
        private readonly string m_CurrentLogPath =
            Utility.Path.GetRegularPath(Path.Combine(Application.persistentDataPath, "current.log"));
        private readonly string m_PreviousLogPath =
            Utility.Path.GetRegularPath(Path.Combine(Application.persistentDataPath, "previous.log"));

        public FileLogHelper()
        {
            Application.logMessageReceived += OnLogMessageReceived;

            try
            {
                if (File.Exists(m_PreviousLogPath))
                {
                    File.Delete(m_PreviousLogPath);
                }

                if (File.Exists(m_CurrentLogPath))
                {
                    File.Move(m_CurrentLogPath, m_PreviousLogPath);
                }
            }
            catch
            {
                // ignored
            }
        }

        private void OnLogMessageReceived(string logMessage, string stackTrace, LogType logType)
        {
            string log = Utility.Text.Format("[{0}][{1}] {2}{4}{3}{4}",                                 //当logMessage为null时，默认赋值<Empty Message>
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), logType.ToString(), logMessage ?? "<Empty Message>",
                stackTrace ?? "<Empty StackTrace>", Environment.NewLine);//literally换行，但是会自动兼容当前运行的平台

            try
            {
                File.AppendAllText(m_CurrentLogPath, log, Encoding.UTF8);
            }
            catch
            {
                // ignored
            }
        }
    }
}