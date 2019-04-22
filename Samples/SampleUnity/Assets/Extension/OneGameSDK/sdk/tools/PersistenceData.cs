using MiniJSON;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace OneGameSDKTool
{
    public class PersistenceData
    {
        public static T Load<T>(string key) where T : class, new()
        {
            if (PlayerPrefs.HasKey(key))
            {
                try
                {
                    return Json.Deserialize(PlayerPrefs.GetString(key)) as T;
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            }
            return new T();
        }

        public static void Save<T>(string key, T data) where T : class
        {
            PlayerPrefs.SetString(key, Json.Serialize(data));
        }
    }
}
