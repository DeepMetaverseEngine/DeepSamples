using UnityEngine;
using UnityEngine.UI;

namespace TL.UGUI.Skill
{

    public class ItemButton : CellButton
    {

        public Image cd = null;
        public GameObject unEquip = null;
        public BattleNumberSimple num = null;
        public SkillBarEffect cdEffect = null;
        public Text count = null;
        public GameObject countbg = null;

        private int CountValue;
        public float CDPercent { get; private set; }
        public float CDTime { get; private set; }


        public enum IconType
        {
            Lock = 0,       //未解锁
            Icon,           //技能图标
            Interactive,    //交互图标
            UnEquip         //未装备
        }

        public void SetCD(float time, float percent)
        {
            if (time > 0)
            {
                ShowNumber((int)(time + 1), -1);
                if (time >= 100)
                    num.transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);
                else if (time >= 10)
                    num.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                else
                    num.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            }
            else
            {
                num.HideNumber();
                if (this.CDTime > 0)
                    ShowCDEffect();
            }
            this.CDTime = time;
            this.CDPercent = percent;
            cd.fillAmount = percent;
        }

        public bool IsCD()
        {
            return (this.CDTime > 0);
        }

        public void ResetCD()
        {
            this.CDTime = 0;
            this.CDPercent = 0;
            cd.fillAmount = 0;
            num.HideNumber();
        }

        public void ShowCDEffect()
        {
            if (cdEffect != null)
                cdEffect.PlayCDEffect();
        }

        public void ShowNumber(int value, int spacing)
        {
            num.ShowNumber(BattleNumberSimple.NumberStyle.Yellow, value, spacing);
        }

        public void HideNumber()
        {
            num.HideNumber();
        }

        public void SetCount(int value)
        {
            CountValue = value;
            if (count != null)
            {
                string str = value > 99 ? "99+" : value.ToString();
                count.text = str;
            }
        }

        public int GetCount()
        {
            return CountValue;
        }

        public virtual void SetIcon(string iconName, IconType type = IconType.Icon)
        {
            if (type == IconType.UnEquip)
            {
                unEquip.SetActive(true);
                icon.gameObject.SetActive(false);
                if (count != null)
                {
                    count.gameObject.SetActive(false);
                    if (countbg != null)
                        countbg.gameObject.SetActive(false);
                }
                return;
            }
            else
            {
                unEquip.SetActive(false);
                icon.gameObject.SetActive(true);
                if (count != null)
                { 
                    count.gameObject.SetActive(true);
                    if (countbg != null)
                        countbg.gameObject.SetActive(true);
                }
            }

            base.SetIcon(iconName);
        }

    }

}
