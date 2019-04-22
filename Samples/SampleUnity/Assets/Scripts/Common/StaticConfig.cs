using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class StaticConfig
{
    public static StaticConfig main = new StaticConfig();

    public string GetString(string key, string defaultValue = "")
    {
        return defaultValue;
    }
}