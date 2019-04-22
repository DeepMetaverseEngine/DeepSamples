using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MiniJSON;
using UnityEngine;
using System.Collections;

public class GameConfig
{
    const string GAMECONFIG_OPTION = "GameConfigOption";
    static GameConfig s_instance;
    public static GameConfig Instance {
        get {
            if (s_instance == null)
            {
                s_instance = new GameConfig();
            }
            return s_instance;
        }
    }

    Dictionary<string, object> data;

    Dictionary<string, object> orignData;
    public List<object> options;

    public int selectedOption;

    public GameConfig()
    {
        options = new List<object>();
        selectedOption = PlayerPrefs.GetInt(GAMECONFIG_OPTION);

        var jsconfigs = ApplicationHelper.LoadTextStreamingAssets("configs.json");
        LoadFromJson(jsconfigs);
    }

    public object Get(string key)
    {
        object value;
        if (data.TryGetValue(key, out value))
        {
            return value;
        }

        return null;
    }

    public void Set(string key, object value)
    {
        data[key] = value;
    }

    public string GetString(string key)
    {
        return (string)Get(key);
    }

    /// <summary>
    /// 改变当前配置选择
    /// </summary>
    /// <param name="index">第几个配置, 0, 1, 2...</param>
    public void ChangegOption(int index)
    {
        if (selectedOption != index)
        {
            selectedOption = index;
            PlayerPrefs.SetInt(GAMECONFIG_OPTION, index);
            ApplyOption();
        }
    }

    /// <summary>
    /// 应用配置参数, 先加载基础配置, 然后应用选择配置
    /// </summary>
    /// <param name="index"></param>
    private void ApplyOption()
    {
        data = new Dictionary<string, object>(orignData);

        if (selectedOption > 0 && selectedOption < options.Count)
        {
            var option = options[selectedOption] as Dictionary<string, object>;
            foreach (var kv in option)
            {
                data[kv.Key] = kv.Value;
            }
        }
    }

    public bool LoadFromJson(string js)
    {
        if (string.IsNullOrEmpty(js)) return false;

        var json = Json.Deserialize(js);

        if (json is Dictionary<string, object>)
        {
            orignData = json as Dictionary<string, object>;
            options = new List<object>();
            options.Add(orignData);

            if (orignData.ContainsKey("options"))
            {
                options.AddRange(orignData["options"] as List<object>);
                orignData.Remove("options");
            }
            ApplyOption();

            return true;
        }

        return false;
    }

}