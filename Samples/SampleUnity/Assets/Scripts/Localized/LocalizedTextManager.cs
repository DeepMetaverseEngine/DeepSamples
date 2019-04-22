using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class LocalizedTextManager
{
    private static LocalizedTextManager mInstance = null;
    private Dictionary<string, object> mLocalizedTextMap = null;

    private LocalizedTextManager()
    {
        mInstance = this;
    }

    public static LocalizedTextManager Instance
    {
        get
        {
            if (mInstance == null)
            {
              new LocalizedTextManager();
            }
            return mInstance;
        }
    }

    public void Init()
    {
        string lang = "CHT";

        if (Application.systemLanguage == SystemLanguage.ChineseSimplified)
        {
            lang = "CHS";
        }
        else if (Application.systemLanguage == SystemLanguage.ChineseTraditional)
        {
            lang = "CHT";
        }
        var LocalizedText = Resources.Load<TextAsset>("lang/" + lang + "/LocalizedText");
        mLocalizedTextMap = MiniJSON.Json.Deserialize(LocalizedText.text) as Dictionary<string, object>;
    }

    public string GetText(string key)
    {
        object rlt = null;
        if (mLocalizedTextMap.TryGetValue(key, out rlt))
        {
            if (rlt != null)
            {
                return Convert.ToString(rlt);
            }
        }
        return key;
    }
}
