using DeepCore.Unity3D;
using UnityEngine;
using UnityEngine.UI;

namespace TL.UGUI.Skill
{

    public class SkillButton : ItemButton
    {

        public Image buf = null;
        public Image ban = null;
        public Text lockImg = null;
        public Image lockbg = null;

        public int Index { get; set; }
        public int Id { get; set; }
        public float BufCDPercent { get; private set; }

        private uTools.uButtonScale mButtonScale;
        private uTools.uTweenPosition mUTweenPos;
        private UnityEngine.Events.UnityEvent mUnityEvent;
        private SkillBarHud.SkillData mNextSkillData;

        private System.Action mShrinkCallback;

        public GameObject miracleEffect = null;
       

        protected override void Awake()
        {
            base.Awake();
            mButtonScale = GetComponent<uTools.uButtonScale>();
            mUnityEvent = new UnityEngine.Events.UnityEvent();
        }

        public void Init(SkillBarHud.SkillData sd)
        {
            SkillKeyStruct data = sd.Data;
            this.Index = data.keyPos;
            this.Id = data.advancedSkillId;
            SkillButton.IconType type = data.flag == 0 ? IconType.Lock : (data.baseSkillId == -1 ? IconType.UnEquip : IconType.Icon);
            this.SetLockInfo(data.flag == 0, data.unlockLevel);
            this.SetIcon(data.icon, type);
            this.ShowBan(sd.IsShowBan);
            
            if ((this.Index==5 ||this.Index==6) && miracleEffect==null)
            {
                PlayUI3DEffect("/res/effect/ui/ef_ui_partner_activation.assetbundles");
            }
        }
        
        //加载神器可释放特效(同仙侣)
        private void PlayUI3DEffect(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                FuckAssetObject.GetOrLoad(path, System.IO.Path.GetFileNameWithoutExtension(path), (loader) =>
                {
                    if (loader)
                    {
                        miracleEffect = loader.gameObject;
                        Transform trans = loader.transform;
                        trans.SetParent(this.cdEffect.transform);
                        trans.localPosition = new Vector3(0, 0, -60);
                        trans.localEulerAngles = Vector3.zero;
                        trans.localScale = Vector3.one;
                        loader.gameObject.SetActive(true);
                        UILayerMgr.SetLayerOrder(loader.gameObject, 1000, false, (int)PublicConst.LayerSetting.UI);
                    }
                });
            }
        }
        
        public override void SetIcon(string iconName, IconType type = IconType.Icon)
        {
            if (type == IconType.UnEquip)
            {
                this.gameObject.SetActive(false);
                icon.gameObject.SetActive(false);
                lockImg.gameObject.SetActive(false);
                lockbg.gameObject.SetActive(false);

                unEquip.SetActive(true);
                return;
            }
            else if (type == IconType.Lock)
            {
                this.gameObject.SetActive(true);
                icon.gameObject.SetActive(true);
                lockImg.gameObject.SetActive(true);
                lockbg.gameObject.SetActive(true);
                unEquip.SetActive(false);
            }
            else
            {
                this.gameObject.SetActive(true);
                icon.gameObject.SetActive(true);
                lockImg.gameObject.SetActive(false);
                lockbg.gameObject.SetActive(false);
                unEquip.SetActive(false);
            }

            base.SetIcon(iconName);
        }

        public void SetLockInfo(bool isLock, int unlockLv)
        {
            if (isLock)
            {
                SetCD(0, 1);
                string lockStr = Assets.Scripts.HZLanguageManager.Instance.GetString("skill_openLv");
                lockImg.text = string.Format(lockStr, unlockLv);
            }
            else if (lockbg.gameObject.activeSelf) //解锁
            {
                ShowUnLockAnime();
            }
            lockImg.gameObject.SetActive(isLock);
            lockbg.gameObject.SetActive(isLock);
        }

        public void ShowBan(bool isShow)
        {
            if (ban.gameObject.activeSelf != isShow)
                ban.gameObject.SetActive(isShow);
        }

        public void SetBufCD(float percent)
        {
            this.BufCDPercent = percent;
            buf.fillAmount = percent;
        }

        public void PlayShrink(Vector2 endPos, SkillBarHud.SkillData nextSkillData, System.Action shrinkCallback = null)
        {
            if (mParentTrans == null)
                return;
            mUTweenPos = uTools.uTweenPosition.Begin(this.gameObject, mParentTrans.anchoredPosition, endPos, 0.1f, 0);
            mUnityEvent.RemoveAllListeners();
            mUnityEvent.AddListener(OnShrinkFinish);
            mUTweenPos.onFinished = mUnityEvent;
            mNextSkillData = nextSkillData;
            mShrinkCallback = shrinkCallback;
        }

        private void PlaySpread()
        {
            mUTweenPos = uTools.uTweenPosition.Begin(this.gameObject, mParentTrans.anchoredPosition, DefaultPos, 0.1f, 0);
            mUnityEvent.RemoveAllListeners();
            mUnityEvent.AddListener(OnSpreadFinish);
            mUTweenPos.onFinished = mUnityEvent;
        }

        private void OnShrinkFinish()
        {
            SetCD(0, 0);
            SetBufCD(0);
            if (mNextSkillData == null)
            {
                SetIcon(null, IconType.UnEquip);
                ShowBan(false);
                this.Id = 0;
                mNextSkillData = null;
            }
            else
            {
                SkillKeyStruct data = mNextSkillData.Data;
                IconType type = data.flag == 0 ? IconType.Lock : (data.baseSkillId == -1 ? IconType.UnEquip : IconType.Icon);
                SetIcon(data.icon, type);
                SetLockInfo(data.flag == 0, data.unlockLevel);
                ShowBan(mNextSkillData.IsShowBan);
                this.Id = data.advancedSkillId;
                this.Index = data.keyPos;
            }

            PlaySpread();
        }

        private void OnSpreadFinish()
        {
            if (mShrinkCallback != null)
                mShrinkCallback();
        }

        public void IconScale(System.Action callback)
        {
            Vector3 targetScale = new Vector3(2, 2, 2);
            icon.transform.localScale = targetScale;
            uTools.uTweenScale utScale = uTools.uTweenScale.Begin(icon.gameObject, targetScale, Vector3.one, 0.5f);
            mUnityEvent.RemoveAllListeners();
            mUnityEvent.AddListener(() =>
            {
                if (callback != null)
                    callback();
            });
            utScale.onFinished = mUnityEvent;
        }

        protected override bool OnTouchDown(Vector2 fingerPos)
        {
            if (IsCD())
                return true;
            else if (ban == null)   //不是技能 
                return true;
            else
            {
                if (!ban.gameObject.activeSelf)
                {
                    if (mTouchListener != null)
                        mTouchListener.OnButtonDown(mParentTrans.gameObject, fingerPos);
                }
            }

            return true;
        }

        protected override bool OnTouchMove(Vector2 fingerPos)
        {
            if (ban == null)   //不是技能 
                return true;
            else if (!ban.gameObject.activeSelf)
            {
                if (mTouchListener != null)
                    mTouchListener.OnButtonDrag(mParentTrans.gameObject, fingerPos);
            }

            return true;
        }

        protected override bool OnTouchUp(Vector2 fingerPos)
        {
            if (ban == null)   //不是技能 
                return true;
            else if (!ban.gameObject.activeSelf)
            {
                if (mTouchListener != null)
                    mTouchListener.OnButtonUp(mParentTrans.gameObject, fingerPos);
            }

            return true;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (mUnityEvent != null)
            {
                mUnityEvent.RemoveAllListeners();
            }
        }

        public void ShowUnLockAnime()
        {
            var transSet = new TransformSet();
            transSet.Pos = new Vector3(0, 0, -5);
            transSet.Scale = Vector3.one;
            transSet.Parent = transform;
            transSet.Layer = (int)PublicConst.LayerSetting.UI;
            transSet.LayerOrder = 1500;
            RenderSystem.Instance.PlayEffect("/res/effect/ui/ef_ui_skillunlock.assetbundles", transSet);
        }

    }

}
