#define ObjectPool

using DeepCore.Unity3D;
using System.Collections.Generic;
using TLBattle.Message;
using UnityEngine;

public enum HintTypeU
{
    ht_Block = 0,
    ht_CriticalAttack,
    ht_DeadlyAttack,
    ht_Dodge,

    ht_Num
}
public class BattleNumberManager : MonoBehaviour
{

    private static BattleNumberManager mInstance;

    public GameObject dmgE;
    public GameObject dmgS;
    public GameObject crtE;
    public GameObject crtS;
    public GameObject rcvE;
    public GameObject rcvS;
    public GameObject blkE;
    public GameObject blkS;
    public GameObject rcvCrtE;
    public GameObject rcvCrtS;
    public GameObject stateE;
    public GameObject stateS;
    public GameObject exp;
    public GameObject combat;
    public GameObject prestige;

    public Transform Cache;

    public static LRUCache<string, Transform> ObjectCache = new LRUCache<string, Transform>(100, RemoveObjectCallback);

    private List<SplitHitData> mSplitList = new List<SplitHitData>();

    private static Camera uiCamera = null;

    public BattleNumberManager()
    {
        //instanceCount++;
        //Debug.LogError("-------------- instanceCount ---------------" + instanceCount);
    }

    public static BattleNumberManager Instance
    {
        get
        {
            return mInstance;
        }
    }

    public static void ClearCache()
    {
        ObjectCache.Clear();
    }

    // Use this for initialization
    void Start()
    {
        if (mInstance == null)
        {
            mInstance = this;
        }

        if (!uiCamera)
            uiCamera = GameSceneMgr.Instance.UICamera;

    }

    void OnDestroy()
    {
        mInstance = null;
    }


    // Update is called once per frame
    void Update()
    {
        if (mSplitList.Count > 0)
        {
            for (int i = 0; i < mSplitList.Count; ++i)
            {
                SplitHitData split = mSplitList[i];
                SplitHitInfo info = split.Info[split.Index];
                if (split.DeltaTime < (float)(info.FrameMS / 1000.0f))
                {
                    split.DeltaTime += Time.deltaTime;
                    continue;
                }
                else
                {
                    if (info.hitType == (byte)BattleAtkNumberEventB2C.AtkNumberType.Normal
                        || info.hitType == (byte)BattleAtkNumberEventB2C.AtkNumberType.Crit
                        || info.hitType == (byte)BattleAtkNumberEventB2C.AtkNumberType.Block)
                    {
                        ShowHintNum(split.Trans, info.Damage, info.hitType, false, split.IsActor);
                    }
                    else
                    {
                        ShowState(split.Trans, info.hitType, split.IsActor);
                    }
                    if (split.Callback != null)
                        split.Callback(info.FrameMS);
                    split.DeltaTime = 0;
                    split.Index++;
                }

                if (split.Index >= split.Info.Count)
                    mSplitList.RemoveAt(i);
                else
                    ++i;
            }
        }
    }

    private static void RemoveObjectCallback(Transform go)
    {
        //Debug.Log("remove cache");
        if (go)
        {
            go.SetParent(null);
            DeepCore.Unity3D.UnityHelper.Destroy(go.gameObject, 0.3f);
        }
        else
        {
            if (!GameUtil.IsObjectExists(go))
                Debugger.LogWarning("remove error: null");
            else
                Debugger.LogWarning("remove error: " + go.name);
        }

    }

    string GetCahceKey(GameObject o)
    {
        return o.name;
    }

    GameObject GetObject(GameObject prefab)
    {
        GameObject o = null;
        if (GetCacheObject(prefab, out o))
        {
            //Debug.Log("Get from cache:" + o.name);
            return o;
        }
        return CreateObject(prefab);
    }

    bool GetCacheObject(GameObject prefab, out GameObject o)
    {
        string key = GetCahceKey(prefab);
        if (ObjectCache.ContainsKey(key))
        {
            o = ObjectCache.Get(key).gameObject;
            BattleNumber bn = o.GetComponent<BattleNumber>();
            uTools.uTweener[] uts = bn.tweens;
            for (int i = 0; i < uts.Length; i++)
            {
                uts[i].Replay();
            }
            Animation[] anis = bn.animes;
            for (int i = 0; i < anis.Length; i++)
            {
                anis[i].Rewind();
                anis[i].Play();
            }
            return true;
        }
        else
        {
            o = null;
            return false;
        }
    }

    GameObject CreateObject(GameObject prefab)
    {
        GameObject o = (GameObject)GameObject.Instantiate(prefab);
        o.name = GetCahceKey(prefab);
        UIFollowPosition ufp = o.AddComponent<UIFollowPosition>();
        ufp.OnUpdate = NumberUpdate;
        ufp.offset = new Vector3(0, 0, 0);
        o.layer = LayerMask.NameToLayer("UI");
        //Debug.Log("Get from create:" + o.name);
        return o;
    }

    public void ShowSplitHit(Transform t, List<SplitHitInfo> hitData, bool isActor, System.Action<int> callback)
    {
        SplitHitData data = new SplitHitData(t, hitData, isActor);
        data.Callback = callback;
        mSplitList.Add(data);
    }

    ///<summary>
    ///显示战斗数字
    ///</summary>
    public void ShowHintNum(Transform t, float _num, byte state, bool isBuff, bool isActor)
    {
        GameObject o = null;
        BattleNumberType type;
        if (_num == 0) return;
        if (_num > 0)
        {
            if (state == (byte)BattleAtkNumberEventB2C.AtkNumberType.Crit)
            {
                if (isActor)
                {
                    type = BattleNumberType.SELF_CRIT;
                    o = GetObject(crtS);
                }
                else
                {
                    type = BattleNumberType.ENEMY_CRIT;
                    o = GetObject(crtE);
                }
            }
            else if (state == (byte)BattleAtkNumberEventB2C.AtkNumberType.Block)
            {
                if (isActor)
                {
                    type = BattleNumberType.SELF_BLOCK;
                    o = GetObject(blkS);
                }
                else
                {
                    type = BattleNumberType.ENEMY_BLOCK;
                    o = GetObject(blkE);
                }
            }
            else
            {
                if (isActor)
                {
                    type = BattleNumberType.SELF_DAMAGE;
                    o = GetObject(dmgS);
                }
                else
                {
                    type = BattleNumberType.ENEMY_DAMAGE;
                    o = GetObject(dmgE);
                }
            }
        }
        else
        {
            if (state == (byte)BattleAtkNumberEventB2C.AtkNumberType.Crit)
            {
                if (isActor)
                {
                    type = BattleNumberType.SELF_RECOVER;
                    o = GetObject(rcvCrtS);
                }
                else
                {
                    type = BattleNumberType.ENEMY_RECOVER;
                    o = GetObject(rcvCrtE);
                }

            }
            else
            {
                if (isActor)
                {
                    type = BattleNumberType.SELF_RECOVER;
                    o = GetObject(rcvS);
                }
                else
                {
                    type = BattleNumberType.ENEMY_RECOVER;
                    o = GetObject(rcvE);
                }
            }
        }

        o.transform.SetParent(gameObject.transform);
        o.transform.localScale = Vector3.one;
        o.GetComponent<BattleNumber>().Init((int)_num, type);
        UIFollowPosition uft = o.GetComponent<UIFollowPosition>();
        uft.Reset(t);
    }

    ///<summary>
    ///显示状态中文字
    ///</summary>
    public void ShowState(Transform t, int type, bool isActor)
    {
        GameObject o = null;
        o = GetObject(isActor ? stateS : stateE);
        o.transform.SetParent(gameObject.transform);
        o.transform.localScale = Vector3.one;
        o.GetComponent<BattleNumber>().Init(type, BattleNumberType.STATE);
        UIFollowPosition uft = o.GetComponent<UIFollowPosition>();
        uft.Reset(t);
    }

    ///<summary>
    ///显示经验数字
    ///</summary>
    public void ShowExpNum(Vector2 scnPos, float num)
    {
        if (num == 0) return;
        GameObject o = GetObject(exp);
        BattleNumberType type = BattleNumberType.EXP;

        o.transform.SetParent(gameObject.transform);
        o.transform.localScale = Vector3.one;
        o.transform.position = uiCamera.ScreenToWorldPoint(scnPos);
        o.GetComponent<BattleNumber>().Init((int)num, type);
        UIFollowPosition uft = o.GetComponent<UIFollowPosition>();
        uft.Reset(null);
    }

    ///<summary>
    ///显示经验数字
    ///</summary>
    public void ShowExpNum(Vector2 scnPos, string num)
    {
        GameObject o = GetObject(exp);
        BattleNumberType type = BattleNumberType.EXP;

        o.transform.SetParent(gameObject.transform);
        o.transform.localScale = Vector3.one;
        o.transform.position = uiCamera.ScreenToWorldPoint(scnPos);
        o.GetComponent<BattleNumber>().InitCustom(num);
        UIFollowPosition uft = o.GetComponent<UIFollowPosition>();
        uft.Reset(null);
    }

    ///<summary>
    ///显示声望数字
    ///</summary>
    public void ShowPrestige(Vector2 scnPos, float num)
    {
        if (num == 0) return;
        GameObject o = GetObject(prestige);
        BattleNumberType type = BattleNumberType.PRESTIGE;

        o.transform.SetParent(gameObject.transform);
        o.transform.localScale = Vector3.one;
        o.transform.position = uiCamera.ScreenToWorldPoint(scnPos);
        o.GetComponent<BattleNumber>().Init((int)num, type);
        UIFollowPosition uft = o.GetComponent<UIFollowPosition>();
        uft.Reset(null);
    }

    ///<summary>
    ///显示进出战斗
    ///</summary>
    public void ShowCombat(Vector2 scnPos, bool isCombat)
    {
        GameObject o = GetObject(combat);
        BattleNumberType type = isCombat ? BattleNumberType.COMBAT : BattleNumberType.UNCOMBAT;

        o.transform.SetParent(gameObject.transform);
        o.transform.localScale = Vector3.one;
        o.transform.position = uiCamera.ScreenToWorldPoint(scnPos);
        o.GetComponent<BattleNumber>().Init(0, type);
        UIFollowPosition uft = o.GetComponent<UIFollowPosition>();
        uft.Reset(null);
    }

    private void NumberUpdate(GameObject o, bool is_visible)
    {
        //Debug.Log("NumberUpdate " + is_visible.ToString());
        if (!is_visible)
        {
            BattleNumber n = o.GetComponent<BattleNumber>();
            if (n != null)
            {
                n.Destroy();
            }
            else
            {
                DeepCore.Unity3D.UnityHelper.Destroy(o, 0.3f);
            }
        }
    }

    private class SplitHitData
    {
        public Transform Trans { get; set; }
        public List<SplitHitInfo> Info { get; set; }
        public bool IsActor { get; set; }
        public int Index { get; set; }
        public float DeltaTime { get; set; }
        public System.Action<int> Callback { get; set; }
        public SplitHitData(Transform t, List<SplitHitInfo> hitData, bool isActor)
        {
            this.Trans = t;
            this.Info = hitData;
            this.IsActor = isActor;
            this.Index = 0;
            this.DeltaTime = 0;
        }
    }

}
