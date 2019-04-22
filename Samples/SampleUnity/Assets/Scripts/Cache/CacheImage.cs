using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using DeepCore.GUI.Display;
using DeepCore.GUI.Editor;
using DeepCore.GUI.Loader;
using DeepCore.Unity3D.Impl;
using SLua;
using TLProtocol.Protocol.Client;
using UnityEngine.Networking;
using Image = UnityEngine.UI.Image;
using TLProtocol.Data;

/// <summary>
/// 图片缓存
/// </summary>
namespace Cache
{
    public class CacheImage
    {
        private static CacheImage mInstance;
        private List<string> mCacheImageName;
        private List<string> mCacheIconName;
        private int mMaxImageNum = 200;
        private int mMaxIconNum = 200;
        private int mIconCacheMax = 10;


        private string imagePath = "";
        private string iconPath = "";
        private string cachePath = "cachePath";
        private string url = "";

        private Dictionary<string, Action<object[]>> mUnityImages;

        private CacheImage()
        {

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            cachePath = Application.streamingAssetsPath + "/../../CacheImage";
#elif UNITY_IPHONE || UNITY_ANDROID
            cachePath = Application.persistentDataPath + "/CacheImage";
#else
            cachePath = string.Empty;
#endif
            cachePath = cachePath.StartsWith("/") ? cachePath.Substring(1) : cachePath;
            Delete(cachePath, "dir");
            mCacheImageName = new List<string>();
            mCacheIconName = new List<string>();
            mUnityImages = new Dictionary<string, Action<object[]>>();
            imagePath = Path.Combine(cachePath, "image");
            iconPath = Path.Combine(cachePath, "icon");
        }

        [DoNotToLua]
        public string ImageCachePath
        {
            get { return cachePath; }
        }

        public static CacheImage Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new CacheImage();
                }
                return mInstance;
            }
        }

        public void DownLoad(TLClientRoleSnap snap, Action<object[]> callback)
        {
            string photoname = snap.Options["Photo0"];
            if (photoname != null && photoname != "")
            {
                DownLoad(snap.ID, photoname, callback);
            }
        }

        public void DownLoad(string uuid, Action<object[]> callback)
        {
            DataMgr.Instance.UserData.RoleSnapReader.Get(uuid, (snap) =>
            {
                string photoname = snap.Options["Photo0"];
                if (photoname != null && photoname != "")
                {
                    DownLoad(snap.ID, photoname, callback);
                }
            });
        }

        public void DownLoad(string uuid,string photoname,Action<object[]> callback)
        {
            GetUrl((url) =>
            {
                DownLoad(String.Format(url, uuid, photoname)+"?imageView2/1/w/72/h/72/q/85/format/png",photoname,true,callback);
            });
        }
 
        public void DownLoad(string url, string filename,bool isicon, Action<object[]> callback)
        {
            if (mUnityImages.ContainsKey(filename))
            {
                mUnityImages[filename] += callback;
                return;
            }
            mUnityImages.Add(filename,callback);
            GameGlobal.Instance.StartCoroutine(LoadByFile(url, filename,isicon));
        }

        public void ClearCallback()
        {
            mUnityImages.Clear();
        }

        /// <summary>
        /// 加载图片
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [DoNotToLua]
        private IEnumerator LoadByFile(string url, string filename,bool isIcon = false)
        {
            string path = isIcon ? iconPath : imagePath;
            if (!string.IsNullOrEmpty(url))
            {
                if (!File.Exists(Path.Combine(path, filename)))
                {
                    //网络上下载
                    yield return DownLoadByUnityWebRequest(url,filename, (cachefilename,data) =>
                    {
                        //保存至缓存路径
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);//创建新路径
                        }
                        File.WriteAllBytes(Path.Combine(path, cachefilename), data);
                        if (isIcon)
                        {
                            if (mMaxIconNum<=mCacheIconName.Count)
                            {
                                Delete(Path.Combine(path,mCacheIconName[0]),"file");
                                mCacheIconName.RemoveAt(0);
                            }
                            mCacheIconName.Add(cachefilename);
                        }
                        else
                        {
                            if (mMaxImageNum<=mCacheImageName.Count)
                            {
                                Delete(Path.Combine(path,mCacheImageName[0]),"file");
                                mCacheImageName.RemoveAt(0);
                            }
                            mCacheImageName.Add(cachefilename);
                        }
                        if (mUnityImages.ContainsKey(cachefilename)&&mUnityImages[cachefilename] != null)
                        {
                            mUnityImages[cachefilename].Invoke(new object[]
                            {
                                true,
                                cachefilename,
                                Path.Combine(isIcon?iconPath:imagePath,cachefilename)
                            });
                            mUnityImages[cachefilename] = null;
                            mUnityImages.Remove(cachefilename);
                        }
                    });
                }
                else
                {
                    //已在本地缓存  
                    string filePath = Path.Combine(path,filename);
                    if (mUnityImages.ContainsKey(filename)&&mUnityImages[filename] != null)
                    {
                        mUnityImages[filename].Invoke(new object[]
                        {
                            true,
                            filename,
                            Path.Combine(isIcon?iconPath:imagePath,filename)
                        });
                        mUnityImages[filename] = null;
                        mUnityImages.Remove(filename);
                    }
                    yield return DownLoadByUnityWebRequest(filePath,filename);
                }
            }
        }


        /// <summary>
        /// UnityWebRequest
        /// </summary>
        /// <param name="url"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        [DoNotToLua]
        private IEnumerator DownLoadByUnityWebRequest(string url,string filename, Action<string,byte[]> callback = null)
        {
            UnityWebRequest uwr = UnityWebRequest.Get(url);
            uwr.timeout = 30;//设置超时，若m_webRequest.SendWebRequest()连接超时会返回，且isNetworkError为true
            yield return uwr.SendWebRequest();
            if (callback != null && uwr.downloadHandler.isDone)
            {
                callback.Invoke(filename,uwr.downloadHandler.data);
            }
            else
            {
                if (mUnityImages.ContainsKey(filename)&&mUnityImages[filename] != null)
                {
                    mUnityImages[filename].Invoke(new object[]{false,filename,""});
                }
            }
        }

        /// <summary>
        /// 删除文件或文件夹
        /// </summary>
        /// <param name="srcPath">文件或文件夹路径</param>
        /// <param name="flag">file or dir二选一</param>
        [DoNotToLua]
        private void Delete(string srcPath,string flag)
        {
            try
            {
                if (flag == "file" && File.Exists(srcPath))
                {
                    File.Delete(srcPath);
                    return;
                }

                if (flag == "dir" && Directory.Exists(srcPath))
                {
                    Directory.Delete(srcPath,true);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        public void GetUrl(Action<string> Callback)
        {         
            if (url == "")
            {
                ClientGetPhotoUrlRequest request = new ClientGetPhotoUrlRequest();
                TLNetManage.Instance.Request<ClientGetPhotoUrlResponse>(request, (err, rsp) =>
                {
                    if (!rsp.s2c_url.EndsWith("/"))
                    {
                        rsp.s2c_url += "/";
                    }
                    url = rsp.s2c_url + rsp.s2c_prefix + "/{0}/{1}.png";
                    Callback.Invoke(url);
                });
            }
            else
            {
                Callback.Invoke(url);
            }
        }
    }
}