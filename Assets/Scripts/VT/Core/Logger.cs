using System.Text;
using _Unity = UnityEngine;
using System.Runtime.CompilerServices;

namespace VT.Core
{
    public class Logger
    {
        private StringBuilder sb = new StringBuilder(2048);
        public string Name { get; private set; } = string.Empty;

        public Logger(string name)
        {
            Name = name;
        }

        public void Trace(
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            sb.Clear();
            sb.AppendFormat("[{0}] {1}:{2}+{3}", Name, System.IO.Path.GetFileName(sourceFilePath), memberName, sourceLineNumber);
            _Unity.Debug.Log(sb.ToString());
        }

        public void Debug(object message)
        {
            sb.Clear();
            sb.AppendFormat("[{0}] {1}", Name, message);
            _Unity.Debug.Log(sb.ToString());
        }
    }
}