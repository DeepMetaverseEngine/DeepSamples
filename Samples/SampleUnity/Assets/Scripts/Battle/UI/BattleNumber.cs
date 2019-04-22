using Assets.Scripts;
using System.Collections.Generic;
using System.Text;
using TLBattle.Message;
using UnityEngine;
using UnityEngine.UI;

public enum BattleNumberType
{
    //数字
    ENEMY_DAMAGE,
    ENEMY_CRIT,
    ENEMY_RECOVER,
    ENEMY_BLOCK,
    SELF_DAMAGE,
    SELF_CRIT,
    SELF_RECOVER,
    SELF_BLOCK,

    //状态
    STATE = 10,

    DODGE,      //闪避
    ABSORB,     //吸收
    IMMUNITY,   //免疫
    IRONMAIDEN,

    POISON,
    ATK_UP,

    //其他
    EXP = 100,
    COMBAT,
    UNCOMBAT,
    PRESTIGE,
    
}

public class BattleNumber : MonoBehaviour
{
    public Transform root;
    public Text text;
    public uTools.uTweener[] tweens;
    public Animation[] animes;

    private int w, h;
    
    private static Dictionary<char, char> numberDict = new Dictionary<char, char>(10);
    private static Dictionary<int, string> stateDict = new Dictionary<int, string>(10);

    // Use this for initialization
    void Start()
    {
        tweens = transform.GetChild(0).GetComponents<uTools.uTweener>();
        animes = transform.GetChild(0).GetComponents<Animation>();
    }

    private void InitEnumDict()
    {
        if (numberDict.Count == 0)
        {
            numberDict['0'] = ')';
            numberDict['1'] = '!';
            numberDict['2'] = '@';
            numberDict['3'] = '#';
            numberDict['4'] = '$';
            numberDict['5'] = '%';
            numberDict['6'] = '^';
            numberDict['7'] = '&';
            numberDict['8'] = '*';
            numberDict['9'] = '(';
        }
        if (stateDict.Count == 0)
        {
            var db = GameUtil.GetDBDataFull("battle_text");
            if (db != null)
            {
                for (int i = 0; i < db.Count; ++i)
                {
                    var tb = db[i];
                    int key = System.Convert.ToInt32(tb["id"]);
                    string value = tb["key"].ToString();
                    stateDict[key] = value;
                }
            }
        }
    }

    public void Init(int value, BattleNumberType type)
    {
        if (numberDict.Count == 0)
        {
            InitEnumDict();
        }
        if (type == BattleNumberType.STATE)
        {
            InitState(value);
        }
        else if (type == BattleNumberType.COMBAT)
        {
            InitState((int)type);
        }
        else if (type == BattleNumberType.UNCOMBAT)
        {
            InitState((int)type);
        }
        else
        {
            InitNumber(value, type);
        }
    }

    public void InitCustom(string content)
    {
        text.text = content;
    }

    void GetNum(BattleNumberType type, char v, ref FastString sb)
    {
        CreateNum(type, v, ref sb);
    }

    void CreateNum(BattleNumberType type, char v, ref FastString sb)
    {
        if (type == BattleNumberType.ENEMY_DAMAGE)
        {
            sb.Append(v);
        }
        else if (type == BattleNumberType.ENEMY_CRIT)
        {
            sb.Append(numberDict[v]);
        }
        else if (type == BattleNumberType.ENEMY_RECOVER)
        {
            sb.Append(v);
        }
        else if (type == BattleNumberType.ENEMY_BLOCK)
        {
            sb.Append(v);
        }
        else if (type == BattleNumberType.SELF_DAMAGE)
        {
            sb.Append(v);
        }
        else if (type == BattleNumberType.SELF_CRIT)
        {
            sb.Append(numberDict[v]);
        }
        else if (type == BattleNumberType.SELF_RECOVER)
        {
            sb.Append(v);
        }
        else if (type == BattleNumberType.SELF_BLOCK)
        {
            sb.Append(v);
        }
        else if (type == BattleNumberType.EXP)
        {
           sb.Append(v);
        }
        else if (type == BattleNumberType.PRESTIGE)
        {
            sb.Append(v);
        }
    }

    string GetPrefex(BattleNumberType type)
    {
        return CreatePrefex(type);
    }

    string CreatePrefex(BattleNumberType type)
    {
        if (type == BattleNumberType.EXP)
        {
			return "E=";
        }
        else if (type == BattleNumberType.PRESTIGE)
        {
			return HZLanguageManager.Instance.GetString("get_prestige");
        }
        else if (type == BattleNumberType.SELF_CRIT || type == BattleNumberType.ENEMY_CRIT)
        {
            return stateDict[(int)BattleAtkNumberEventB2C.AtkNumberType.Crit];
        }
        else if (type == BattleNumberType.SELF_BLOCK || type == BattleNumberType.ENEMY_BLOCK)
        {
            return stateDict[(int)BattleAtkNumberEventB2C.AtkNumberType.Block];
        }
        else if (type == BattleNumberType.SELF_RECOVER || type == BattleNumberType.ENEMY_RECOVER)
        {
            return "=";
        }

        return "";
    }

    void InitState(int type)
    {
        if (GameGlobal.Instance.netMode)
        {
            text.text = stateDict[type];
        }
        else
        {
            text.text = type.ToString();
        }
        
    }

    //void InitOther()
    //{
    //    root.GetComponent<uTools.uTweenPosition>().to.x *= Random.Range(1, 3) > 1 ? -1 : 1;
    //}

    string GetPlus(BattleNumberType type)
    {
        return CreatePlus(type);
    }

    string CreatePlus(BattleNumberType type)
    {
        return "";
    }

    private static FastString sb = new FastString(20);
    void InitNumber(int value, BattleNumberType type)
    {
        char[] vs = value.ToString().ToCharArray();
        sb.Set(GetPrefex(type));
        foreach (var v in vs)
        {
            if (v == '-')
            {
                sb.Append(GetPlus(type));
            }
            else
            {
                GetNum(type, v, ref sb);
            }
        }
        text.text = sb.ToString();
    }

    public void Destroy()
    {
        transform.SetParent(BattleNumberManager.Instance.Cache);
        transform.localPosition = Vector3.zero;
        BattleNumberManager.ObjectCache.Add(gameObject.name, transform);
    }
}
