using System.Text;
using _Unity = UnityEngine;
using System.Runtime.CompilerServices;

namespace VT.Core
{
    public class Logger
    {
        private StringBuilder sb = new StringBuilder(2048);
        public virtual string Name { get; } = nameof(VT.Core);

        public void Trace(
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            sb.Clear();
            sb.AppendFormat("[TRC] [{0}] {1}:{2}+{3}", Name, System.IO.Path.GetFileName(sourceFilePath), memberName, sourceLineNumber);
            _Unity.Debug.Log(sb.ToString());
        }

        public void Debug(object message, [CallerMemberName] string memberName = "")
        {
            sb.Clear();
            sb.AppendFormat("[DBG] [{0}] {1} > {2}", Name, memberName, message);
            _Unity.Debug.Log(sb.ToString());
        }

        public void Info(object message, [CallerMemberName] string memberName = "")
        {
            sb.Clear();
            sb.AppendFormat("[NFO] [{0}] {1} > {2}", Name, memberName, message);
            _Unity.Debug.Log(sb.ToString());
        }

        public void Warning(object message)
        {
            sb.Clear();
            sb.AppendFormat("[WRN] [{0}] {1}", Name, message);
            _Unity.Debug.LogWarning(sb.ToString());
        }

        public void Error(object message)
        {
            sb.Clear();
            sb.AppendFormat("[ERR] [{0}] {1}", Name, message);
            _Unity.Debug.LogError(sb.ToString());
        }
    }
}