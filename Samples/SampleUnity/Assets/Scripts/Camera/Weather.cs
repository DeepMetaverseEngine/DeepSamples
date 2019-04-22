using DeepCore;
using DeepCore.Unity3D;
using SLua;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Weather : MonoBehaviour {

    class WeatherObject
    {
        public string key;
        public FuckAssetObject assetObject;
        public FuckAssetLoader assetLoader;
        public WeatherObject(string key)
        {
            this.key = key;
        }

        public void Dispose()
        {
            if (assetObject != null)
            {
                assetObject.Unload();
            }
            else
            {
                assetLoader.Discard();
            }
        }
    }

    private static Dictionary<string, WeatherObject> mModels = new Dictionary<string, WeatherObject>();

    //老蔡 todo
    public void Awake()
    {
        //foreach(var obj in WeatherObjects)
        //{
        //    obj.SetActive(false);
        //}
        EventManager.Subscribe("Event.Scene.ChangeWeather", ChangeWeather);
    }
    private static WeatherObject CreateWeatherObject(string name)
    {
        string key = name;
        WeatherObject info = new WeatherObject(key);
        mModels.Add(key, info);
        return info;
    }


    private void RemoverWeather(string path)
    {
        WeatherObject info;
        if (mModels.TryGetValue(path,out info))
        {
            info.Dispose();
            mModels.Remove(path);
        }
    }

    private void RemoveAll()
    {
        var iter = mModels.GetEnumerator();
        while (iter.MoveNext())
        {
            iter.Current.Value.Dispose();
        }

        mModels.Clear();
    }
    //private void ChangeScene(EventManager.ResponseData res)
    //{
    //    //foreach (var obj in WeatherObjects)
    //    //{
    //    //    obj.SetActive(false);
    //    //}
    //    RemoveAll();
    //}

    private void ChangeWeather(EventManager.ResponseData res)
    {
        Dictionary<object, object> data = (Dictionary<object, object>)res.data[1];
        object ShowWeather;

        if (data.TryGetValue("ShowWeather", out ShowWeather))
        {
            var showtype = ShowWeather as LuaTable;
            foreach (var table in showtype)
            {

                var respath = table.value.ToString();
                if (mModels.ContainsKey(respath))
                {
                    continue;
                }
                var info = CreateWeatherObject(respath);
                var filePath = "/res/effect/"+ respath + ".assetbundles";
                var id = FuckAssetObject.GetOrLoad(filePath, Path.GetFileNameWithoutExtension(filePath), (loader) =>
                {
                    if (loader)
                    {
                        if (!mModels.ContainsKey(respath))
                        {
                            loader.Unload();
                            return;
                        }
                        loader.gameObject.SetActive(true);
                        info.assetObject = loader;
                        info.assetObject.transform.parent = this.gameObject.transform;
                        info.assetObject.transform.localPosition = Vector3.zero;
                        info.assetObject.transform.localEulerAngles = Vector3.zero;
                        info.assetObject.transform.localScale = Vector3.one;

                    }
                });
                info.assetLoader = FuckAssetLoader.GetLoader(id);
            }
        }
        if (data.TryGetValue("CloseWeather", out ShowWeather))
        {
            var showtype = ShowWeather as LuaTable;
            foreach (var table in showtype)
            {
                RemoverWeather(table.value.ToString());
                //foreach (var obj in WeatherObjects)
                //{
                //    if (obj.name == table.value.ToString())
                //    {
                //        obj.SetActive(false);
                //        break;
                //    }

                //}
            }
        }

        if (data.TryGetValue("CloseALL", out ShowWeather))
        {
            RemoveAll();
        }
    }


    private void OnDestroy()
    {
        EventManager.Unsubscribe("Event.Scene.ChangeWeather", ChangeWeather);
    }

    internal void Clear()
    {
        RemoveAll();
    }
}
