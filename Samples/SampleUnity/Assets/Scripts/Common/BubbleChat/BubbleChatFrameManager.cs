using DeepCore;
using DeepCore.Unity3D.Battle;
using System;

 
using UnityEngine;

/// <summary>
/// 游戏气泡框功能管理类.
/// </summary>
public class BubbleChatFrameManager
{

    private bool mInitFinish = false;
    
    private BubbleChatFrame mBubbleChatFrame = null;
    private BubbleChatFrame mCGBubbleChatFrame = null;
    HashMap<string, BubbleChatFrameTimeUI> mBubbleChatList = new HashMap<string, BubbleChatFrameTimeUI>();
    public BubbleChatFrame BubbleChatFrame
    {
        get
        {
            if (mBubbleChatFrame == null)
            {
                Init();
                InitCGFrame();
            }
            return mBubbleChatFrame;
        }
    }

    public BubbleChatFrame CGBubbleChatFrame
    {
        get
        {
            if (mCGBubbleChatFrame == null)
            {
                InitCGFrame();
            }
            return mCGBubbleChatFrame;
        }
    }

    private void InitCGFrame()
    {
        mCGBubbleChatFrame = new BubbleChatFrame();
    }

    private static BubbleChatFrameManager mInstance = null;
    public static BubbleChatFrameManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new BubbleChatFrameManager();
            }
            return mInstance;
        }
    }

    private BubbleChatFrameManager()
    {

    }

    private void Init()
    {
        if (!mInitFinish)
        {
            mBubbleChatFrame = new BubbleChatFrame();
            mInitFinish = true;
        }
    }
    
    public void HideBubbleChat(bool value)
    {
        if (mBubbleChatFrame != null)
        {
            mBubbleChatFrame.Hide(value);
        }
    }

    public void HideCGBubbleChat(bool value)
    {
        if (mCGBubbleChatFrame != null)
        {
            mCGBubbleChatFrame.HideCG(value);
        }
    }
    //cg剧情用 要手动关闭
    public BubbleChatFrameTimeUI ShowBubbleChatFrameByObject(GameObject unit,string content)
    {
        if (unit == null)
        {
            Debugger.LogError("ShowBubbleChatFrame unit is null");
            return null;
        }
        BubbleChatFrameTimeUI bubblechat = CGBubbleChatFrame.ShowCGBubbleChatFrame("CgObject", content);
        bubblechat.OnPositionSync(unit.transform);
        return bubblechat;
    }

    public BubbleChatFrameTimeUI CreateBubbleChatFrame(string key, string content, float time)
    {
        BubbleChatFrameTimeUI bubblechat = null;
        if (mBubbleChatList.TryGetValue(key, out bubblechat))
        {
            bubblechat.SetContent(content, time);
        }
        else
        {
            bubblechat = BubbleChatFrame.ShowBubbleChatFrameTime(key, content, time, () =>
            {
                mBubbleChatList.RemoveByKey(key);
            });
        }
        return bubblechat;
    }


    public BubbleChatFrameTimeUI ShowBubbleChatFrame(BattleObject unit,string key, string content, float time)
    {
        BubbleChatFrameTimeUI bubblechat = null;
        if (mBubbleChatList.TryGetValue(key,out bubblechat))
        {
            bubblechat.SetContent(content,time);
        }
        else
        {
            bubblechat = BubbleChatFrame.ShowBubbleChatFrameTime(key, content, time,() =>
            {
                mBubbleChatList.RemoveByKey(bubblechat.Key);
                if (unit != null)
                {
                    if(unit is TLAIUnit)
                    {
                        (unit as TLAIUnit).OnPositionChange -= bubblechat.OnPositionSync;
                    }
                    if (unit is TLCGUnit)
                    {
                        (unit as TLCGUnit).OnPositionChange -= bubblechat.OnPositionSync;
                    }
                }
                
            });
            mBubbleChatList.Add(key, bubblechat);

            if (unit != null )
            {
                if(unit is TLAIUnit)
                {
                    (unit as TLAIUnit).OnPositionChange += bubblechat.OnPositionSync;
                }
                
                if (unit is TLCGUnit)
                {
                    (unit as TLCGUnit).OnPositionChange += bubblechat.OnPositionSync;
                }
            }
        }
        return bubblechat;
    }
    
    public void Clear(bool reLogin, bool reConnect)
    {
        mBubbleChatList.Clear();
        BubbleChatFrame.Clear();
       
    }
}
