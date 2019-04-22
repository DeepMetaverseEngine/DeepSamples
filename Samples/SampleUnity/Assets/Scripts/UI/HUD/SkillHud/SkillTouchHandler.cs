using UnityEngine;





public class SkillTouchHandler : FingerHandlerInterface
{
    
    private int mFingerIndex = -1;

    private TLAIActor mActor;
    private SkillLaunchListener mSkillLaunch;

    public SkillTouchHandler(SkillLaunchListener skillLaunch)
    {
        if (skillLaunch != null)
        {
            mSkillLaunch = skillLaunch;
            HudManager.Instance.SkillBar.skillRocker.OnRockerMove += this.OnSkillRockerMove;
            HudManager.Instance.SkillBar.skillRocker.OnRockerStop += this.OnSkillRockerStop;
        }
        GameGlobal.Instance.FGCtrl.AddFingerHandler(this, (int)PublicConst.FingerLayer.SkillTouch);
        GameGlobal.Instance.FGCtrl.AddFingerHandler(HudManager.Instance.SkillBar.skillRocker, (int)PublicConst.FingerLayer.SkillRocker);
    }

    public bool OnFingerDown(int fingerIndex, Vector2 fingerPos)
    {
        if (!HudManager.Instance.SkillBar.Visible)
            return false;
        
        if (mFingerIndex == -1)
        {
            RectTransform[] skillBarRects = UGUIMgr.TouchRects.skillBar;
            for (int i = 0; i < skillBarRects.Length; ++i)
            {
                if (UGUIMgr.CheckInRect(skillBarRects[i], fingerPos))
                {
                    RectTransform[] skillRects = UGUIMgr.TouchRects.skills;
                    for (int j = 0; j < skillRects.Length; ++j)
                    {
                        if (UGUIMgr.CheckInRect(skillRects[j], fingerPos))
                        {
                            mFingerIndex = fingerIndex;
                            if (mSkillLaunch != null)
                            {
                                mSkillLaunch.OnReadyToLaunchSkill(j + 1);
                                //mCurMovePos = mEndMovePos = mSkillLaunch.OnReadyToLaunchSkill(j + 1);
                                mStartRocker = true;
                            }
                            break;
                        }
                    }
                    break;
                }
            }
        }
        return false;
    }

    public bool OnDragMove(int fingerIndex, Vector2 fingerPos, Vector2 delta)
    {
        if (mFingerIndex != -1 && mFingerIndex == fingerIndex)
        {
            //return true;
        }

        return false;
    }

    public bool OnFingerUp(int fingerIndex, Vector2 fingerPos, float timeHeldDown)
    {
        if (mFingerIndex != -1 && mFingerIndex == fingerIndex)
        {
            mFingerIndex = -1;
            //return true;
        }

        return false;
    }

    public void OnFingerClear()
    {
        this.mFingerIndex = -1;
    }
    public static float speed = 2.0f;
    public static bool delayMode = false;
    public void OnUpdate()
    {
        if (!delayMode)
            return;
        if (mCurMovePos != mEndMovePos)
        {
            mCurMovePos = Vector2.MoveTowards(mCurMovePos, mEndMovePos, Time.deltaTime * speed);
            if (mSkillLaunch != null)
            {
                mSkillLaunch.OnSkillRockerMove(mCurMovePos.x, mCurMovePos.y, mFingerMovePos.x, mFingerMovePos.y);
            }
        }
    }

    private Vector2 mCurMovePos, mEndMovePos, mFingerMovePos;
    private bool mStartRocker;
    void OnSkillRockerMove(float x, float y, float px, float py)
    {
        //Debugger.Log("x= " + x + " , y= " + y);
        if (mSkillLaunch != null)
        {
            if ((x != 0 && y != 0) || mStartRocker)
            {
                if (delayMode)
                {
                    mStartRocker = true;
                    mEndMovePos = new Vector2(x, y);
                    mFingerMovePos = new Vector2(px, py);
                }
                else
                {
                    mSkillLaunch.OnSkillRockerMove(x, y, px, py);
                }
            }
        }
    }

    void OnSkillRockerStop(float x, float y, float px, float py)
    {
        if (mSkillLaunch != null)
        {
            if (delayMode)
            {
                mSkillLaunch.OnSkillRockerStop(mCurMovePos.x, mCurMovePos.y, px, py);
                mCurMovePos = mEndMovePos = Vector2.zero;
                mStartRocker = false;
            }
            else
            {
                mSkillLaunch.OnSkillRockerStop(x, y, px, py);
            }
        }
    }

    public void Destroy()
    {
        if (mSkillLaunch != null)
        {
            HudManager.Instance.SkillBar.skillRocker.OnRockerMove -= this.OnSkillRockerMove;
            HudManager.Instance.SkillBar.skillRocker.OnRockerStop -= this.OnSkillRockerStop;
            mSkillLaunch = null;
        }
        GameGlobal.Instance.FGCtrl.RemoveFingerHandler(this);
    }
    
    public interface SkillLaunchListener
    {
        Vector2 OnReadyToLaunchSkill(int index);
        void OnSkillRockerMove(float x, float y, float px, float py);
        void OnSkillRockerStop(float x, float y, float px, float py);
    }
	
}
