using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace uTools {
	[AddComponentMenu("uTools/Tween/Tween Alpha(uTools)")]
	public class uTweenAlpha : uTweenValue {

		public bool includeChilds = false;

		private Text mText;
		private Light mLight;
		private Material mMat;
		private Image mImage;
		private SpriteRenderer mSpriteRender;

        private Text[] mChildText;
        private Light[] mChildLight;
        private Material[] mChildMat;
        private Image[] mChildImage;
        private SpriteRenderer[] mChildSpriteRender;

        float mAlpha = 0f;

		public float alpha {
			get {
				return mAlpha;
			}
			set {
				SetAlpha(transform, value);
				mAlpha = value;
			}
		}

        protected override void OnStart()
        {
            mText = gameObject.GetComponent<Text>();
            mLight = gameObject.GetComponent<Light>();
            mImage = gameObject.GetComponent<Image>();
            mSpriteRender = gameObject.GetComponent<SpriteRenderer>();
            if (gameObject.GetComponent<Renderer>() != null)
            {
                mMat = gameObject.GetComponent<Renderer>().material;
            }
            base.OnStart();
        }

        protected override void ValueUpdate (float value, bool isFinished)
		{
			alpha = value;
		}

		void SetAlpha(Transform _transform, float _alpha) {
			Color c = Color.white;
			if (mText != null){
				c = mText.color;
				c.a = _alpha;
				mText.color = c;
			}
			if (mLight != null){ 
				c = mLight.color;
				c.a = _alpha;
				mLight.color = c;
			}
			if (mImage != null) {
				c = mImage.color;
				c.a = _alpha;
				mImage.color = c;
			}
			if (mSpriteRender != null) {
				c = mSpriteRender.color;
				c.a = _alpha;
				mSpriteRender.color = c;
			}
			if (mMat != null) {
				c = mMat.color;
				c.a = _alpha;
				mMat.color = c;
			}
			if (includeChilds) {
                if(mChildText == null && mChildLight == null && mChildMat == null && mChildImage == null && mChildSpriteRender == null)
                {
                    mChildText = transform.GetComponentsInChildren<Text>();
                    mChildLight = transform.GetComponentsInChildren<Light>();
                    Renderer[] rs = transform.GetComponentsInChildren<Renderer>();
                    List<Material> mats = new List<Material>();
                    for (int i = 0; i < rs.Length; i++)
                    {
                        mats.Add(rs[i].material);
                    }
                    mChildMat = mats.ToArray();
                    mChildImage = transform.GetComponentsInChildren<Image>();
                    mChildSpriteRender = transform.GetComponentsInChildren<SpriteRenderer>();
                }
                if (mChildText != null && mChildText.Length > 0)
                {
                    for (int i = 0; i < mChildText.Length; ++i)
                    {
                        Text child = mChildText[i];
                        if (child != null)
                        {
                            c = child.color;
                            c.a = _alpha;
                            child.color = c;
                        }
                    }
                }
                if (mChildLight != null && mChildLight.Length > 0)
                {
                    for (int i = 0; i < mChildLight.Length; ++i)
                    {
                        Light child = mChildLight[i];
                        if (child != null)
                        {
                            c = child.color;
                            c.a = _alpha;
                            child.color = c;
                        }
                    }
                }
                if (mChildMat != null && mChildMat.Length > 0)
                {
                    for (int i = 0; i < mChildMat.Length; ++i)
                    {
                        Material child = mChildMat[i];
                        if (child != null)
                        {
                            if (child.HasProperty("_Color"))
                            {
                                c = child.color;
                                c.a = _alpha;
                                child.color = c;
                            }
                        }
                    }
                }
                if (mChildImage != null && mChildImage.Length > 0)
                {
                    for (int i = 0; i < mChildImage.Length; ++i)
                    {
                        Image child = mChildImage[i];
                        if (child != null)
                        {
                            c = child.color;
                            c.a = _alpha;
                            child.color = c;
                        }
                    }
                }
                if (mChildSpriteRender != null && mChildSpriteRender.Length > 0)
                {
                    for (int i = 0; i < mChildSpriteRender.Length; ++i)
                    {
                        SpriteRenderer child = mChildSpriteRender[i];
                        if (child != null)
                        {
                            c = child.color;
                            c.a = _alpha;
                            child.color = c;
                        }
                    }
                }
            }

		}

        public void ResetRenders()
        {
            mChildText = null;
            mChildLight = null;
            mChildMat = null;
            mChildImage = null;
            mChildSpriteRender = null;
        }



	}

}