using DeepCore.Log;
using System;
using System.Text;


public class TLUnityLogFactory : LoggerFactory
{
    public TLUnityLogFactory()
    {
    }
    protected override Logger CreateLogger(object name)
    {
       
        return new UnityLog();
    }
}
public class UnityLog : Logger
{

    FastString sb = new FastString(1024);

    public UnityLog(): this("client")
    {
        SetLevel();
    }

    protected UnityLog(string name) : base(name)
    {
        SetLevel();
    }

    private void SetLevel()
    {
        SetLevelFlag(LoggerLevel.DEBUG | LoggerLevel.ERROR | LoggerLevel.LOG | LoggerLevel.WARNNING );
    }

    protected override void Print(LoggerLevel level, object format, string text, Exception err)
    {
        sb.Clear();
        if (err != null)
        {
            sb.Append(text);
            sb.Append(" : ");
            sb.Append(err.GetType().FullName);
            sb.Append("\n");
            sb.Append(err.StackTrace);
        }

        sb.Append("[");
        sb.Append(mName);
        sb.Append("]");
        sb.Append(text);

        //if (level == LoggerLevel.DEBUG || level == LoggerLevel.FINE || level == LoggerLevel.TRACE)
        //    Debugger.Log(sb.ToString());
        //else if (level == LoggerLevel.LOG || level == LoggerLevel.INFO || level == LoggerLevel.WARNNING)
        //    Debugger.LogWarning(sb.ToString());
        //else
        //    Debugger.LogError(sb.ToString());
        if ((level & LoggerLevel.ERROR) > 0 || (level & LoggerLevel.FATAL) > 0)
        {
            Debugger.LogError(sb.ToString());
        }
        else if ((level & LoggerLevel.WARNNING) > 0)
        {
            Debugger.LogWarning(sb.ToString());
        }
        else
        {
            Debugger.Log(sb.ToString());
        }

        if (err != null)
        {
            Debugger.LogException(err);
        }
    }

}
