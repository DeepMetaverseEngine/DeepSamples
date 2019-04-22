using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BattleNumberSimple : MonoBehaviour
{

    public enum NumberStyle
    {
        Red,
        Green,
        Yellow,
        Blue,
        Orange,
        Brown,
        White,
    }

    public RectTransform root;

    public Sprite[] redNumbers;
    public Sprite[] greenNumbers;
    public Sprite[] yellowNumbers;
    public Sprite[] blueNumbers;
    public Sprite[] orangeNumbers;
    public Sprite[] brownNumbers;
    public Sprite[] whiteNumbers;
    private float mTotalW, mTotalH;
    
    private List<Image> mObjCache = new List<Image>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowNumber(NumberStyle type, int value, float spacing = 0)
    {
        HideNumber();
        mTotalW = mTotalH = 0;
        char[] vs = value.ToString().ToCharArray();
        Vector2 size;
        for (int i = 0; i < vs.Length; ++i)
        {
            GameObject n = GetNum(type, vs, i, out size);
            RectTransform rt = n.GetComponent<RectTransform>();
            rt.anchoredPosition3D = new Vector3(mTotalW, 0, 0);
            rt.sizeDelta = size;
            mTotalW += size.x + spacing;
            mTotalH = size.y;
        }

        root.anchoredPosition3D = new Vector3(-mTotalW * 0.5f, 0, 0);
    }

    //public void HideNumber()
    //{
    //    StartCoroutine(WaitFunc(_HideNumber));
    //}

    public void HideNumber()
    {
        for (int i = mObjCache.Count - 1; i >= 0; --i)
        {
            Image obj = mObjCache[i];
            obj.enabled = false;
        }
    }

    //private IEnumerator WaitFunc(System.Action act)
    //{
    //    yield return new WaitForEndOfFrame();

    //    act();
    //}

    private GameObject GetNum(NumberStyle type, char[] vs, int index, out Vector2 size)
    {
        char v = vs[index];
        Sprite s = null;
        Image img = null;
        GameObject o;
        if (index < root.childCount)
        {
            //o = root.Find("num" + (index + 1)).gameObject;
            img = mObjCache[index];
            o = img.gameObject;
            img.enabled = true;
        }
        else
        {
            o = new GameObject();
            o.name = "num" + (index + 1);
            img = o.AddComponent<Image>();
            mObjCache.Add(img);
            o.layer = root.gameObject.layer;
            RectTransform rt = o.GetComponent<RectTransform>();
            rt.SetParent(root);
            rt.localScale = Vector3.one;
            rt.pivot = new Vector2(0, 0.5f);
        }
        if (type == NumberStyle.Red)
        {
            s = redNumbers[int.Parse(v.ToString())];
        }
        else if (type == NumberStyle.Green)
        {
            s = greenNumbers[int.Parse(v.ToString())];
        }
        else if (type == NumberStyle.Yellow)
        {
            s = yellowNumbers[int.Parse(v.ToString())];
        }
        else if (type == NumberStyle.Blue)
        {
            s = blueNumbers[int.Parse(v.ToString())];
        }
        else if (type == NumberStyle.Orange)
        {
            s = orangeNumbers[int.Parse(v.ToString())];
        }
        else if (type == NumberStyle.Brown)
        {
            s = brownNumbers[int.Parse(v.ToString())];
        }
        else if (type == NumberStyle.White)
        {
            s = whiteNumbers[int.Parse(v.ToString())];
        }

        size.x = (int)s.rect.width;
        size.y = (int)s.rect.height;
        img.sprite = s;
        //img.SetNativeSize();
        return o;
    }

    //public void Destroy()
    //{
    //    DeepCore.Unity3D.UnityHelper.Destroy(gameObject);
    //}
}
