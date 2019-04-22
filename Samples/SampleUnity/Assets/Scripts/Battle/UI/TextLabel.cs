using UnityEngine;
using UnityEngine.UI;

public class TextLabel : MonoBehaviour {

    public GameObject prefabObj;

    private static GameObject prefab = null;
    private static Transform mShowNode = null;
    private static Transform mCacheNode = null;
    private static Transform mTrans;

    public static Transform ShowNode
    {
        get
        {
            return mShowNode;
        }
    }
    // Use this for initialization
    void Start () {
        mTrans = transform;
        if (prefab == null)
        {
            prefab = prefabObj;
        }

        //创建根节点
        CreateRootNode();
    }

    private static void CreateRootNode()
    {
        //创建显示根节点.
        if (mShowNode == null)
        {
            mShowNode = new GameObject("TextLabelShowNode").transform;
            mShowNode.parent = mTrans;
            mShowNode.localScale = Vector3.one;
            mShowNode.transform.localPosition = new Vector3(1000, 1000, 0);
        }
        //创建缓存根节点.
        if (mCacheNode == null)
        {
            mCacheNode = new GameObject("TextLabelCacheNode").transform;
            mCacheNode.parent = mTrans;
            mCacheNode.transform.localPosition = new Vector3(1000, 1000, 0);
            mCacheNode.localScale = Vector3.one;
        }
    }

    // Update is called once per frame
    void Update () {
	
	}

    public static Text AddTextLabel(Transform parent, Vector3 offset, float aoi = 0)
    {
        //先尝试从缓存取.
        GameObject childObj;
        if (mCacheNode.childCount > 0)
        {
            childObj = mCacheNode.GetChild(0).gameObject;
        }
        else
        {
            childObj = (GameObject)GameObject.Instantiate(prefab, new Vector3(1000, 1000, 1000), Quaternion.identity);
        }

        Text t = TextLabel.InitTextLabel(childObj, parent, offset, aoi);

        return t;
    }

    private static Text InitTextLabel(GameObject obj, Transform parent, Vector3 offset, float aoi)
    {
        obj.transform.SetParent(mShowNode);
        obj.transform.localScale = prefab.transform.localScale;

        UIFollowTarget3D ft = obj.GetComponent<UIFollowTarget3D>();
        ft.target = parent;
        ft.offset = offset;
        ft.aoiDistance = aoi;
        ft.enabled = true;

        obj.SetActive(true);

        Text t = obj.GetComponent<Text>();

        return t;
    }

    public static void SetText(Text t, string text, Color c, int size)
    {
        t.text = text;
        t.color = c;
        t.fontSize = size;
        t.gameObject.name = text;
    }

    public static void SetText(Text t, string text)
    {
        t.text = text;
        t.gameObject.name = text;
    }

    public static void SetColor(Text t, Color c)
    {
        t.color = c;
    }

    public static void SetSize(Text t, int size)
    {
        t.fontSize = size;
    }

    public static void SetAlignment(Text t, TextAnchor anchor)
    {
        t.alignment = anchor;
    }

    public static void SetOutline(Text t, Color c)
    {
        Outline outline = t.GetComponent<Outline>();
        outline.effectColor = c;
        outline.enabled = !c.Equals(Color.clear);
        if (outline.enabled)
        {
            Shadow shadow = t.GetComponent<Shadow>();
            shadow.enabled = false;
        }
    }

    public static void SetShadow(Text t, Color c)
    {
        Shadow shadow = t.GetComponent<Shadow>();
        shadow.effectColor = c;
        shadow.effectDistance = new Vector2(0, -1);
        shadow.enabled = !c.Equals(Color.clear);
        if (shadow.enabled)
        {
            Outline outline = t.GetComponent<Outline>();
            outline.enabled = false;
        }
    }

    public static float GetTextHeight(Text t)
    {
        float height = 0;
        height = string.IsNullOrEmpty(t.text) ? 0 : t.preferredHeight;
        return height;
    }

    public static void Remove(Text t)
    {
        //回收对象，放入缓存节点下.
        t.gameObject.SetActive(false);
        t.transform.SetParent(mCacheNode);
        t.transform.localPosition = Vector3.zero;
        UIFollowTarget3D ft = t.GetComponent<UIFollowTarget3D>();
        ft.target = null;
        ft.enabled = false;
    }

    /// <summary>
    /// 销毁所有正在显示的和缓存中的实例对象
    /// </summary>
    public static void Clear()
    {
        if (mShowNode != null)
        {
            DeepCore.Unity3D.UnityHelper.Destroy(mShowNode.gameObject, 0.3f);
            mShowNode = null;
        }
        if (mCacheNode != null)
        {
            DeepCore.Unity3D.UnityHelper.Destroy(mCacheNode.gameObject, 0.3f);
            mCacheNode = null;
        }

        CreateRootNode();
    }

}
