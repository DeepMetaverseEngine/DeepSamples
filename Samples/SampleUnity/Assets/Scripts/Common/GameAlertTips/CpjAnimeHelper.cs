using UnityEngine;
using System.Collections.Generic;

using DeepCore.GUI.Cell;
using DeepCore.Unity3D.UGUIEditor;
using DeepCore.Unity3D.UGUI;
using DeepCore.GUI.Cell.Game;

public class CpjAnimeHelper {

    private Dictionary<string, UIComponent> mAnimeCache = new Dictionary<string, UIComponent>();
    private Dictionary<string, UIComponent> mAnimePlayingQueue = new Dictionary<string, UIComponent>();
    private DisplayNode mCacheNode;
    private int mSerialNumber = 0;

    public CpjAnimeHelper()
    {
        mCacheNode = new DisplayNode("CpjCacheNode");
        HZUISystem.Instance.UIAlertLayerAddChild(mCacheNode);
    }

    //播放一个cpj动画，返回key用于stop，key默认是xmlPath_spliteName
    public string PlayCpjAnime(DisplayNode parentNode, string xmlPath, string spliteName, float offX, float offY, int times, CSpriteEventHandler finishCb)
    {
        return PlayCpjAnime(parentNode, xmlPath, spliteName, offX, offY, times, false, finishCb);
    }

    //播放一个cpj动画并创建缓存，返回key用于stop，key默认是xmlPath_spliteName
    public string PlayCacheCpjAnime(DisplayNode parentNode, string xmlPath, string spliteName, float offX, float offY, int times, CSpriteEventHandler finishCb)
    {
        return PlayCpjAnime(parentNode, xmlPath, spliteName, offX, offY, times, true, finishCb);
    }

    /// <summary>
    /// 播放cpj动画，默认居中父节点.
    /// </summary>
    /// <param name="parentNode">父节点，可以为null</param>
    /// <param name="xmlPath">xml路径，"dynamic/effects/aaa/bbb.xml"</param>
    /// <param name="spliteName">内部名称，需打开编辑器查看</param>
    /// <param name="offX">偏移坐标X</param>
    /// <param name="offY">偏移坐标Y</param>
    /// <param name="times">播放次数，-1为循环播放，需要用返回的key在外部手动调用StopCpjAnime</param>
    /// <param name="cache">是否缓存，绝大多数情况应该是false</param>
    /// <param name="finishCb">播放完后的回调</param>
    /// <returns>此动画对应的唯一key值，用于stop.默认是xmlPath_spliteName</returns>
    private string PlayCpjAnime(DisplayNode parentNode, string xmlPath, string spliteName, float offX, float offY, int times, bool cache, CSpriteEventHandler finishCb)
    {
        UIComponent animeNode = CreateCpjAnime(parentNode, xmlPath, spliteName, offX, offY, cache);
        if (animeNode != null)
        {
            //创建唯一key，放入播放队列.
            string key = xmlPath + '_' + spliteName;
            if (mAnimePlayingQueue.ContainsKey(key))
            {
                if (cache)  //缓存模式，并且已经在播放了，取出来重新播放.
                {
                    mAnimePlayingQueue.Remove(key);
                }
                else  //非缓存模式，支持多个播放.
                {
                    key = key + '_' + mSerialNumber++;
                }
            }
            mAnimePlayingQueue.Add(key, animeNode);

            animeNode.Layout.SpriteController.PlayAnimate(0, times, (sender) =>
            {
                if (finishCb != null)
                {
                    finishCb(sender);
                }

                mAnimePlayingQueue.Remove(key);
                string cacheKey = xmlPath + '_' + spliteName;
                if (mAnimeCache.ContainsKey(cacheKey))
                {
                    //DisplayNode cacheNode = HZUISystem.Instance.GetUIAlertLayer();
                    mCacheNode.AddChild(animeNode);
                    animeNode.Visible = false;
                }
                else
                {
                    animeNode.RemoveFromParent(true);
                }
            });

            return key;
        }

        return null;
    }

    //创建cpj动画并返回，默认不使用缓存，除非真的需要.
    public UIComponent CreateCpjAnime(DisplayNode parentNode, string xmlPath, string spliteName, float offX, float offY, bool cache = false)
    {
        UIComponent animeNode = null;
        string key = xmlPath + '_' + spliteName;
        //先从缓存取.
        if (cache)
        {
            if (mAnimeCache.ContainsKey(key))
            {
                animeNode = mAnimeCache[key];
                animeNode.Visible = true;
            }
        }
        //没有缓存，创建新的.
        if (animeNode == null)
        {
            UILayout layout = HZUISystem.CreateLayoutFromCpj(xmlPath, spliteName);
            if (layout == null)
                return null;

            animeNode = new UIComponent();
            animeNode.UnityObject.name = "CpjAnime_" + xmlPath + "." + spliteName;
            animeNode.Layout = layout;

            //如果需要缓存，加入到缓存池.
            if (cache)
            {
                mAnimeCache.Add(key, animeNode);
            }
        }
        
        if (parentNode == null)
        {
            parentNode = HZUISystem.Instance.GetUIAlertLayer();
        }
        parentNode.AddChild(animeNode);
        //animeNode.Position2D = new Vector2(parentNode.Transform.rect.width / 2 + offX, parentNode.Transform.rect.height / 2 + offY);
        animeNode.SetAnchor(new Vector2(0.5f, 0.5f));
        animeNode.Transform.anchoredPosition = new Vector2(offX, -offY);

        return animeNode;
    }

    public bool IsAnimePlaying(string key)
    {
        if (mAnimePlayingQueue.ContainsKey(key))
        {
            return true;
        }
        return false;
    }

    public void StopCpjAnime(string key)
    {
        if (mAnimePlayingQueue.ContainsKey(key))
        {
            UIComponent anime = mAnimePlayingQueue[key];
            anime.Layout.SpriteController.StopAnimate(true);
            mAnimePlayingQueue.Remove(key);
        }
    }

    public void StopAllCpjAnime()
    {
        if (mAnimePlayingQueue.Count > 0)
        {
            List<string> keyList = new List<string>();
            foreach (KeyValuePair<string, UIComponent> q in mAnimePlayingQueue)
            {
                string key = q.Key;
                keyList.Add(key);
            }
            for (int i = 0; i < keyList.Count; ++i)
            {
                string key = keyList[i];
                StopCpjAnime(key);
            }
        }
    }

    public void ClearCacheAnime(string key)
    {
        if (mAnimeCache.ContainsKey(key))
        {
            UIComponent anime = mAnimeCache[key];
            anime.Dispose();
            mAnimeCache.Remove(key);
        }
    }

    public void ClearAllCacheAnime()
    {
        foreach (KeyValuePair<string, UIComponent> q in mAnimeCache)
        {
            UIComponent anime = q.Value;
            anime.Dispose();
        }
        mAnimeCache.Clear();
    }

    public void Clear(bool reLogin, bool reConnect)
    {
        StopAllCpjAnime();
        if (reLogin)
        {
            ClearAllCacheAnime();
        }
    }

}
