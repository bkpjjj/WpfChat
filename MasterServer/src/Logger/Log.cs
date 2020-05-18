using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterServer.src.Logger
{
    public class Log
    {
        private List<string> m_MessageBuffer;
        private List<string> m_WarrningBuffer;
        private List<string> m_ErrorBuffer;

        private string m_mesFormat = @"[{0}] Message : {1}";
        private string m_warFormat = @"[{0}] Warring : {1}";
        private string m_errFormat = @"[{0}] Error : {1}";

        private static Log m_ref;
        private Log() 
        {
            m_MessageBuffer = new List<string>();
            m_WarrningBuffer = new List<string>();
            m_ErrorBuffer = new List<string>();
        }

        static Log()
        {
            m_ref = new Log();
        }

        public static Log Inst => m_ref;

        //Methods
        public void Message(string message)
        {
            var mess = string.Format(m_mesFormat, m_MessageBuffer.Count + 1, message);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(mess);
            m_MessageBuffer.Add(mess);
        }

        public void Warring(string message)
        {
            var mess = string.Format(m_warFormat, m_WarrningBuffer.Count + 1, message);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(mess);
            m_WarrningBuffer.Add(mess);
        }

        public void Error(string message)
        {
            var mess = string.Format(m_errFormat, m_ErrorBuffer.Count + 1, message);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(mess);
            m_ErrorBuffer.Add(mess);
            SaveLogFile();
        }

        public void SaveLogFile()
        {
            string logs_dir = "..\\Logs";
            if (!Directory.Exists(logs_dir))
                Directory.CreateDirectory(logs_dir);
            try
            {
                File.WriteAllText(logs_dir + $"\\Messages_{FormatDateTime(DateTime.Now)}.txt", string.Join("\n", m_MessageBuffer));
                File.WriteAllText(logs_dir + $"\\Warrnings_{FormatDateTime(DateTime.Now)}.txt", string.Join("\n", m_WarrningBuffer));
                File.WriteAllText(logs_dir + $"\\Errors_{FormatDateTime(DateTime.Now)}.txt", string.Join("\n", m_ErrorBuffer));
            }
            catch (Exception e)
            {
                Error(e.Message);
            }
        }

        private string FormatDateTime(DateTime now)
        {
            return now.ToString().Replace('.', '_').Replace(':', '_');
        }
    }
}
