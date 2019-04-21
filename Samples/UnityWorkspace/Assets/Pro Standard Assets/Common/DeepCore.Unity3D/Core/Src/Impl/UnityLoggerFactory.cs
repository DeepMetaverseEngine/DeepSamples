using DeepCore.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeepCore.Unity3D.Impl
{
    public class UnityLoggerFactory : LoggerFactory
    {
        protected override Logger CreateLogger(string name)
        {
            return new UnityLogger();
        }
    }
    public class UnityLogger : Logger
    {
        public UnityLogger()
        {
            this.SetLevel(0xFFFFFFFF);
        }

        protected override void Print(LoggerLevel level, string msg, Exception err)
        {
            if (IsErrorEnabled)
            {
                UnityEngine.Debug.LogError(msg);
            }
            else if (IsFatalEnabled)
            {
                UnityEngine.Debug.LogError(msg);
            }
            else if (IsWarnEnabled)
            {
                UnityEngine.Debug.LogWarning(msg);
            }
            else
            {
                UnityEngine.Debug.Log(msg);
            }
            if (err != null)
            {
                UnityEngine.Debug.LogException(err);
            }
        }


    }
}
