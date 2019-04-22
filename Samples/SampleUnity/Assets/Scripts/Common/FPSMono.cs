using UnityEngine;
using System.Collections.Generic;
using DeepCore.Unity3D;
using UnityEngine.EventSystems;
using DeepCore.Unity3D.Impl;
using DeepCore.Unity3D.UGUI;

/// <summary>
/// FPS 及内存管理
/// </summary>
public class FPSMono : MonoBehaviour
{
    public float updateInterval = 1.0f;
    public bool FPSOnly = false;
    public bool isEveryUpdate = true;
    private float accum = 0; // FPS accumulated over the interval
    private int frames = 0; // Frames drawn over the interval
    private float timeleft; // Left time for current interval
    // UI controls.
    public int fomsSize = 20;
    public Rect labRect = new Rect(10, 70, 100, 20);
    public Rect labRect2 = new Rect(10, 200, 100, 20);
    public Rect labRect3 = new Rect(10, 300, 100, 20);
    public Rect labRect4 = new Rect(10, 400, 100, 20);

    public Rect labRect5 = new Rect(10, 430, 100, 20);
    public Rect labRect6 = new Rect(10, 360, 100, 20);
    private GUIStyle labStyle = new GUIStyle();
    string format;
    float fps;
    string target;
    string target_varsion;
    string actorPos;
    void Start()
    {
        //labRect.y = Screen.height* 0.84;
        labStyle.fontSize = fomsSize;
        //HZUnityAssetBundleLoader.OnBegionLoadData += UnityInstance_OnBegionLoadData;
    }

    //private static List<string> mCurAssetList = new List<string>();
    //private static List<int> mCurSceneList = new List<int>();

    //public static void AddToCurrentList(string path)
    //{
    //    int index = path.IndexOf("res");
    //    string str = path.Substring(index);
    //    str = "+\\\\" + str.Replace("/", "\\\\") + ";";
    //    if (mCurAssetList.IndexOf(str) < 0)
    //    {
    //        mCurAssetList.Add(str);
    //    }

    //    //if (mCurSceneList.IndexOf(DataMgr.Instance.UserData.SceneId) < 0)
    //    //{
    //    //    mCurSceneList.Add(DataMgr.Instance.UserData.SceneId);
    //    //}
    //}
    //private void UnityInstance_OnBegionLoadData(string path)
    //{
    //    if (path.StartsWith(UnityDriver.PREFIX_MPQ) && path.EndsWith("assetbundles"))
    //    {
    //        //"mpq:///StreamingAssets/PC/res/unit/a1efc8367844566dd243c4a1abd0667d.assetbundles"
    //        AddToCurrentList(path);
    //    }
    //}

    void OnDestroy()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        if (TLUnityDebug.GetInstance() != null && TLUnityDebug.GetInstance().ShowDebugInfo)
        {
            if (isEveryUpdate)
            {
                //Debugger.Log("FPS: " + Time.deltaTime);
                fps = 1 / Time.deltaTime;
                var pingval = 0;
                if(TLBattleScene.Instance != null&& TLBattleScene.Instance.Actor!=null)
                {
                    pingval = TLBattleScene.Instance.Actor.ZObj.Parent.CurrentPing;
                }
                format = System.String.Format("Fps:{0:F1}\nAC/RC:{1}/{2}\nPing:{3}", fps,DisplayNode.AliveCount, DisplayNode.RefCount, pingval);
                freshColor();
            }
            else
            {
                timeleft -= Time.deltaTime;
                accum += Time.timeScale / Time.deltaTime;
                ++frames;
                // Interval ended - update GUI text and start new interval
                if (timeleft <= 0.0)
                {
                    // display two fractional digits (f2 format)
                    fps = accum / frames;
                    var pingval = 0;
                    if (TLBattleScene.Instance != null && TLBattleScene.Instance.Actor != null)
                    {
                        pingval = TLBattleScene.Instance.Actor.ZObj.Parent.CurrentPing;
                    }
                    format = System.String.Format("Fps:{0:F1}\nAC/RC:{1}/{2}\nPing:{3}", fps, DisplayNode.AliveCount, DisplayNode.RefCount, pingval);
                    //pingstr = System.String.Format("Ping:{0:F1}\n节点个数:{1}\n", fps, DisplayNode.GetDisplayNodeReferenceCount());
                    freshColor();
                    timeleft = updateInterval;
                    accum = 0.0f;
                    frames = 0;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (TLUnityDebug.GetInstance().ShowDebugInfo)
        {
            PointerEventData pe = new PointerEventData(EventSystem.current);
            pe.position = Input.mousePosition;

            List<RaycastResult> hits = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pe, hits);
            string name = "无";
            if (hits.Count > 0)
            {
                name = hits[0].gameObject.name;
            }
            string name2 = "无";
            GameObject o = EventSystem.current.currentSelectedGameObject;
            if (o != null)
            {
                name2 = o.name;
            }
            target = System.String.Format("当前目标：{0}\n点击目标：{1}", name, name2);
            //if (BattleClientBase.GetActor() != null)
            //{
            //    var layer = BattleClientBase.GetActor().ZUnit.Parent;
            //    target_varsion = System.String.Format("客户端资源版本：{0}\n服务器资源版本：{1}",
            //        layer.ClientResourceVersion, layer.ServerResourceVersion);
            //}
            //else
            //{
            //    target_varsion = "";
            //}
            if (TLBattleScene.Instance != null)
            {
                if (TLBattleScene.Instance.Actor != null)
                {
                    actorPos = "ActorPos{"+ TLBattleScene.Instance.Actor.X + ","+ TLBattleScene.Instance.Actor.Y + "}";
                }
            }
        }
    }
    void freshColor()
    {
        if (fps < 10)
        {
            labStyle.normal.textColor = Color.red;
        }
        else if (fps < 25)
        {
            labStyle.normal.textColor = Color.yellow;
        }
        else
        {
            labStyle.normal.textColor = Color.green;
        }
    }
    void OnGUI()
    {
        if (TLUnityDebug.GetInstance().ShowDebugInfo)
        {
            //if (UIFactory.Instance != null)
            //{
            //    GUI.skin.font = UIFactory.Instance.DefaultFont;
            //}
            GUI.Label(labRect, format, labStyle);
            GUI.Label(labRect2, target, labStyle);
            GUI.Label(labRect3, target_varsion, labStyle);
            GUI.Label(labRect4, PublicConst.ClientVersion + '.' + PublicConst.SVNVersion, labStyle);
            GUI.Label(labRect6, actorPos, labStyle);
            if (GUI.Button(labRect5, "mpq章节筛选器"))
            {
                //if (!ProjectSetting.useMpq)
                //{
                //    GameAlertManager.Instance.ShowAlertDialog(AlertDialog.PRIORITY_NORMAL, @"必须用mpq模式使用", "", null, null);
                //}
                //else
                //{
                //    mCurAssetList.Sort();
                //    mCurSceneList.Sort();
                //    string fileName = "";
                //    foreach (var id in mCurSceneList)
                //    {
                //        fileName += id + "_";
                //    }
                //    System.IO.File.WriteAllLines(fileName+"ab.txt", mCurAssetList.ToArray(), System.Text.Encoding.UTF8);
                //}
            }
            //if (GUI.Button(new Rect(20, 20, 70, 30), "Test"))
            //{
            //    GameUtil.ShowPickItemEffect(BattleClientBase.GetActor().GameObject.transform.position);
            //}
        }

    }

}
