using DeepCore.Unity3D;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PracticeScene : MonoBehaviour, IDragHandler, IEndDragHandler,IBeginDragHandler
{

    public RectTransform group;
    public RectTransform toggle;
    public RectTransform toggle2;
    public Slider slider;
    //圆的初始化参数
    public Transform center;
    public Transform lookat;
    public float radius;
    public float distance;
    public float startAngle;
    public int count;
    public string prefabName;
    //按钮组旋转参数
    //public float rotateStart;
    //public float rotateEnd;
    public float rotateSpeed;
    //槽的档位
    public float[] sliderPos;

    public RectTransform[] pointgroup;
    public GameObject effect;


    //测试用，到时候去掉
    public bool forceUpdate;

    private int mRotateDir;
    private float mRotateMin;
    private float mRotateMax;
    private Action<int> mListener;

    void Awake(){
        if (count > 0)
            Init(count);
    }

    public void Init(int length)
    {
        mRotateMin = - distance * (length - 1) + 360;
        mRotateMax = 360;
        GameObject btnPrefab = toggle.GetChild(0).gameObject;
        GameObject btn2Prefab = toggle2.GetChild(0).gameObject;
        int listLen = Mathf.Max(length, toggle.childCount);
        for (int i = 0; i < listLen; ++i)
        {
            string btnName = prefabName + "_" + (i + 1);
            if (i < length)
            {
                GameObject button;
                GameObject button2;
                if (i == 0)
                {
                    button = btnPrefab;
                    button2 = btn2Prefab;
                }
                else
                {
                    if(i < toggle.childCount)
                    {
                        button = toggle.GetChild(i).gameObject;
                        button2 = toggle2.GetChild(i).gameObject;
                    }
                    else
                    {
                        button = GameObject.Instantiate(btnPrefab);
                        button.transform.SetParent(toggle);
                        button.transform.localScale = btnPrefab.transform.localScale;
                        button2 = GameObject.Instantiate(btn2Prefab);
                        button2.transform.SetParent(toggle2);
                        button2.transform.localScale = btn2Prefab.transform.localScale;
                    }
                }
                button.name = btnName;
                button2.name = btnName;
                float radian = (startAngle - i * distance) * Mathf.Deg2Rad;
                float px = Mathf.Cos(radian) * radius;
                float py = Mathf.Sin(radian) * radius;
                Vector2 pos = CoordinateConvert(Vector2.zero, new Vector2(center.position.x, -center.position.z), new Vector2(px, py));
                button.transform.position = new Vector3(pos.x, center.position.y, pos.y);
                button.transform.LookAt(new Vector3(0, lookat.position.y, lookat.position.z));
                button.transform.localRotation = Quaternion.Euler(button.transform.localEulerAngles.x, button.transform.localEulerAngles.y + 180, button.transform.localEulerAngles.z);
                button2.transform.position = new Vector3(pos.x, center.position.y, pos.y);
                button2.transform.LookAt(new Vector3(0, lookat.position.y, lookat.position.z));
                button2.transform.localRotation = Quaternion.Euler(button2.transform.localEulerAngles.x, button2.transform.localEulerAngles.y + 180, button2.transform.localEulerAngles.z);
            }
            else
            {
                Transform button = toggle.Find(btnName);
                Transform button2 = toggle2.Find(btnName);
                button.SetParent(null);
                DeepCore.Unity3D.UnityHelper.Destroy(button, 0.3f);
                button2.SetParent(null);
                DeepCore.Unity3D.UnityHelper.Destroy(button2, 0.3f);
            }
        }
    }

    private Vector2 CoordinateConvert(Vector2 origin_src, Vector2 origin_dst, Vector2 point)
    {
        Vector2 result = new Vector2();
        float offsetX = origin_src.x - origin_dst.x;
        float offsetY = origin_src.y - origin_dst.y;
        result.x = point.x + offsetX;
        result.y = point.y + offsetY;
        return result;
    }

    public void SetButtonPressed(int index, bool press)
    {
        if (index >= 0 && index < toggle.childCount)
        {
            Transform button = toggle.GetChild(index);
            Toggle tg = button.GetComponent<Toggle>();
            tg.isOn = press;
        }
    }

    public void SetButtonSelected(int index)
    {
        if (index >= 0 && index < toggle2.childCount)
        {
            Transform button = toggle2.GetChild(index);
            Toggle tg = button.GetComponent<Toggle>();
            tg.isOn = true;
        }
    }

    public void RotateTo(int index)
    {
        if (index >= 0 && index < toggle.childCount)
        {
            group.rotation = Quaternion.Euler(group.eulerAngles.x, index * distance * -1, group.eulerAngles.z);
        }
    }

    public void StartRotate(bool isLeft)
    {
        mRotateDir = isLeft ? 1 : -1;
    }

    /// <summary>
    /// 返回是否划到了边界 0 否 1 左 2 右
    /// </summary>
    /// <returns></returns>
    public int StopRotate()
    {
        mRotateDir = 0;

        float rotateY = group.eulerAngles.y + 360;
        rotateY = rotateY > 360 ? rotateY % 360 : rotateY;
        if (Mathf.Abs(rotateY - mRotateMax) < 0.01f)
            return 1;
        else if(Mathf.Abs(rotateY - mRotateMin) < 0.01f)
            return 2;
        else
            return 0;
        //return Mathf.Abs(rotateY - mRotateMin) < 0.01f || Mathf.Abs(rotateY - mRotateMax) < 0.01f;
    }

    public void SetSlider(int index)
    {
        if(index >= 0 && index < sliderPos.Length)
        {
            slider.value = sliderPos[index];
        }
    }

    public void SetFightPower(int index, string numStr, string lvName, Font font)
    {
        if (index >= 0 && index < pointgroup.Length)
        {
            Transform fightTrans = pointgroup[index].Find("ib_fight/fight");
            if (fightTrans != null)
            {
                Text text = fightTrans.GetComponent<Text>();
                text.text = numStr;
                text.font = font;
            }
            Transform nameTrans = pointgroup[index].Find("ib_name/name");
            if (nameTrans != null)
            {
                Text text = nameTrans.GetComponent<Text>();
                text.text = lvName;
                text.font = font;
            }
        }
    }

    public Image GetButtonImage(int index, bool highlight)
    {
        if (index >= 0 && index < toggle.childCount)
        {
            string path = highlight ? "/Toggle/Background/Checkmark" : "/Toggle/Background";
            Transform bg = toggle.Find(prefabName + "_" + (index + 1) + path);
            Image img = bg.GetComponent<Image>();
            return img;
        }
        return null;
    }

    public void SetLightPoint(int index, bool light)
    {
        Transform trans = pointgroup[index].Find("point");
        if (trans != null)
        {
            Toggle toggle = trans.GetComponent<Toggle>();
            toggle.isOn = light;
        }
    }

    public void SetButtonListener(Action<int> cb)
    {
        mListener = cb;
    }

    public void OnButtonPressed(Toggle toggle)
    {
        if (toggle.isOn)
        {
            int index;
            string numStr = toggle.name.Substring(toggle.name.LastIndexOf("_") + 1);
            if (int.TryParse(numStr, out index))
            {
                if (mListener != null)
                {
                    mListener.Invoke(index);
                }
            }
        }
    }

    private void DoRotate(int dir)
    {
        float rotateY = group.eulerAngles.y + 360;
        rotateY = rotateY > 360 ? rotateY % 360 : rotateY;
        rotateY += dir * rotateSpeed;
        rotateY = Mathf.Clamp(rotateY, mRotateMin, mRotateMax);
        group.rotation = Quaternion.Euler(group.eulerAngles.x, rotateY, group.eulerAngles.z);
        //group.Rotate(Vector3.up, Time.deltaTime * rotateSpeed * mRotateDir);
    }

    public void ShowUpEffect(bool isShow)
    {
        effect.SetActive(isShow);
    }

    void Update()
    {
        if(forceUpdate) //test
            Init(count);

        if (mRotateDir != 0)
        {
            DoRotate(mRotateDir);
        }

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySoundByKey("button", false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //DoRotate(eventData.delta.x > 0 ? 1 : -1);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        StopRotate();
    }

}
