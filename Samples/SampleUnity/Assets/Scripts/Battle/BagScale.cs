//using UnityEngine;
//using System.Collections;
//using DeepCore.Unity3D.UGUI;
//using DeepCore.Unity3D.UGUIEditor;

//public class BagScale : MonoBehaviour
//{
//    private UIComponent bag;
//    private bool start = false;


//    private static BagScale mInstance;
//    public static BagScale Instance
//    {
//        get
//        {
//            if (mInstance == null)
//                mInstance = new BagScale();
//            return mInstance;
//        }

//    }
//    public BagScale()
//    {
//        mInstance = this;
//    }

//    void Start()
//    {
//        if (bag == null)
//        {
//            bag = HudManager.Instance.GetHudUI("MainHud").FindChildByEditName("btn_beibao");
//        }
//    }

//    public float scaleValue = 1.3f;

//    public float t1 = 0.05f;
//    public float t2 = 0.2f;


//    private bool startUp = false;
//    private bool startDown = false;

//    void Update()
//    {
//        //if (start)
//        //{
//        //    if(value > end)
//        //    {
//        //        value -= Time.deltaTime * speed;

//        //        if (value < end)
//        //            value = end;
//        //        ButtonScale(value);

//        //    }
//        //    else
//        //    {
//        //        start = false;
//        //        ButtonScale(end);
//        //    }

//        //}

//        if (startUp)
//        {
//            if(value < scaleValue)
//            {
//                value += Time.deltaTime * speedUp;
//                ButtonScale(value);
//            }
//            else
//            {
//                value = scaleValue;
//                ButtonScale(scaleValue);
//                startUp = false;
//                startDown = true;
//            }

//        }
//        else if(startDown)
//        {
//            if (value > endValue)
//            {
//                value -= Time.deltaTime * speedDown;

//                if (value < endValue)
//                    value = endValue;
//                ButtonScale(value);

//            }
//            else
//            {
//                startDown = false;
//                ButtonScale(endValue);
//            }
//        }
//    }
     
//    float endValue = 1.0f;


//    float speedUp = 0;
//    float speedDown = 0;

//    //float speed = 0;
//    float value = 0;

//    public void DoScale()
//    {

//        //speed = (scale - end) / t;
//        //value = end;
//        //start = true;
//        //ButtonScale(value);

//        float s = scaleValue - endValue;
//        speedUp = s / t1;
//        speedDown = s / t2;

//        value = endValue;

//        startUp = true;
//        startDown = false;

//    }


//    private void ButtonScale(float scale)
//    {
//        float x = bag.Position2D.x;
//        float y = bag.Position2D.y;
//        float w = bag.Size2D.x;
//        float h = bag.Size2D.y;
//        float offx = (bag.Scale.x - scale) * w * 0.5f;
//        float offy = (bag.Scale.y - scale) * h * 0.5f;

//        bag.Scale = new Vector2(scale, scale);
//        bag.Position2D = new Vector2(x + offx, y + offy);
//    }
//}
