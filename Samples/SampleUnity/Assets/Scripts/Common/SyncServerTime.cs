using DeepCore;
using SLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TLProtocol.Protocol.Client;

/// <summary>
/// 用于计算服务端时间.
/// </summary>
public class SyncServerTime
{
    public long lag { private set; get; }
    private long serverTime;
    private long clientTime;
    private TimeInterval<int> mTimer;
    private bool hasSync = false;

    public SyncServerTime()
    {
        //10秒一次.
        mTimer = new TimeInterval<int>(10000);
    }

    public void SyncTime(int timeintervalMS)
    {
        if (mTimer.Update(timeintervalMS))
        {
            if (TLNetManage.Instance.IsGameEntered())
            {
                ClientSyncServerTimeRequest req = new ClientSyncServerTimeRequest();
                req.c2s_ticks = DateTime.UtcNow.Ticks;
                TLNetManage.Instance.Request<ClientSyncServerTimeResponse>(req, (err, rsp) =>
                {
                    if (rsp != null && rsp.IsSuccess)
                    {
                        clientTime = rsp.s2c_clientTicks;
                        long curClientTime = DateTime.UtcNow.Ticks;
                        serverTime = rsp.s2c_serverTicks;

                        lag = (curClientTime - clientTime) / 2;
                        hasSync = true;
                    }
                }, new TLNetManage.PackExtData(false, false));
            }
        }
    }

    public DateTime GetServerTimeUTC()
    {
        return new DateTime(GetServerTimeUTCTicks());
    }

    public long GetServerTimeUTCTicks()
    {
        if (TLNetManage.Instance.IsNet == true && hasSync)
        {
            long curTime = DateTime.UtcNow.Ticks;
            long ret = serverTime + curTime - (clientTime + lag);
            return ret;
        }
        else
        {
            return DateTime.UtcNow.Ticks;
        }

    }

    public DateTime GetTodayTimeToUtcTime(string time)
    {
        DateTime dt = GetServerTimeUTC().ToLocalTime();
        DateTime setTime = Convert.ToDateTime(time);
        DateTime newTime = new DateTime(dt.Year,dt.Month,dt.Day, setTime.Hour,setTime.Minute,setTime.Second);
        return newTime.ToUniversalTime();
    }

    public int GetTodayOfWeek()
    {
        DateTime datetime = GetServerTimeUTC().ToLocalTime();
        int week = 0;
        string dt = datetime.DayOfWeek.ToString();
        switch (dt)
        {
            case "Monday":
                week = 1;
                break;
            case "Tuesday":
                week = 2;
                break;
            case "Wednesday":
                week = 3;
                break;
            case "Thursday":
                week = 4;
                break;
            case "Friday":
                week = 5;
                break;
            case "Saturday":
                week = 6;
                break;
            case "Sunday":
                week = 7;
                break;
        }
        return week;
    }

    public const uint MASK_WEEK_SUNDAY = 1 << 1;
    public const uint MASK_WEEK_MONDAY = 1 << 2;
    public const uint MASK_WEEK_TUESDAY = 1 << 3;
    public const uint MASK_WEEK_WEDNESDAY = 1 << 4;
    public const uint MASK_WEEK_THURSDAY = 1 << 5;
    public const uint MASK_WEEK_FRIDAY = 1 << 6;
    public const uint MASK_WEEK_SATURDAY = 1 << 7;
    public const uint MASK_WEEK_ALL = 0xFFFF;

    public bool IsInToday(string daysrange)
    {
        DateTime datetime = GetServerTimeUTC().ToLocalTime();
        if (string.IsNullOrEmpty(daysrange))
        {
            return true;
        }
        if (daysrange == "0")
        {
            return true;
        }
        var ret = daysrange.Split(',');
        uint OpenDay_Mask = 0;
        for (int i = 0; i < ret.Length; i++)
        {
            switch (int.Parse(ret[i]))
            {
                case 1:
                    OpenDay_Mask |= MASK_WEEK_MONDAY;
                    break;
                case 2:
                    OpenDay_Mask |= MASK_WEEK_TUESDAY;
                    break;
                case 3:
                    OpenDay_Mask |= MASK_WEEK_WEDNESDAY;
                    break;
                case 4:
                    OpenDay_Mask |= MASK_WEEK_THURSDAY;
                    break;
                case 5:
                    OpenDay_Mask |= MASK_WEEK_FRIDAY;
                    break;
                case 6:
                    OpenDay_Mask |= MASK_WEEK_SATURDAY;
                    break;
                case 7:
                    OpenDay_Mask |= MASK_WEEK_SUNDAY;
                    break;
                default:
                    OpenDay_Mask |= MASK_WEEK_ALL;
                    break;
            }
        }
        bool isOpen = false;
        switch (datetime.DayOfWeek)
        {
            case DayOfWeek.Friday:
                isOpen = (MASK_WEEK_FRIDAY & OpenDay_Mask) != 0;
                break;
            case DayOfWeek.Monday:
                isOpen = (MASK_WEEK_MONDAY & OpenDay_Mask) != 0;
                break;
            case DayOfWeek.Saturday:
                isOpen = (MASK_WEEK_SATURDAY & OpenDay_Mask) != 0;
                break;
            case DayOfWeek.Sunday:
                isOpen = (MASK_WEEK_SUNDAY & OpenDay_Mask) != 0;
                break;
            case DayOfWeek.Thursday:
                isOpen = (MASK_WEEK_THURSDAY & OpenDay_Mask) != 0;
                break;
            case DayOfWeek.Tuesday:
                isOpen = (MASK_WEEK_TUESDAY & OpenDay_Mask) != 0;
                break;
            case DayOfWeek.Wednesday:
                isOpen = (MASK_WEEK_WEDNESDAY & OpenDay_Mask) != 0;
                break;
        }
        return isOpen;
    }

  
    public bool IsBetweenTime(string start,string end)
    {
        if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end))
        {
            //Debugger.LogError("IsBetweenTime error"+" start = "+ start +" end = "+end);
            return true;
        }
        DateTime datetime = GetServerTimeUTC().ToLocalTime();
        DateTime startDate = DateTime.Parse(start);
        DateTime stopDate = DateTime.Parse(end);
        if (DateTime.Compare(datetime, startDate) < 0)
        {
            return false;
        }
        else if (DateTime.Compare(datetime, stopDate) > 0)
        {
            return false;
        }
        return true;
    }

    public bool IsBetweenTimes(List<string> start, List<string> close)
    {
        if (start.Count != close.Count)
        {
            Debugger.LogError("IsBetweenTimes count not match [" + start.Count + ":" + close.Count + "]");
            return false;
        }
        
        for(int i = 0;i < start.Count; i++)
        {
            if (IsBetweenTime(start[i], close[i]))
            {
                return true;
            }
        }
        return false;
    }


    public bool IsBetweenTimes(LuaTable starttable,LuaTable closetable)
    {
        List<string> start = new List<string>() ;
        List<string> close = new List<string>() ;
        foreach (SLua.LuaTable.TablePair p in starttable)
        {
            start.Add(p.value.ToString());
        }

        foreach (SLua.LuaTable.TablePair p in closetable)
        {
            close.Add(p.value.ToString());
        }

        return IsBetweenTimes(start, close);
    }
}

