



using DeepCore.Unity3D.UGUIEditor.UI;
using TLBattle.Common.Plugins;

public class SkillProgressUI : HZUIBehaviour
{
    TLAIActor mActor;

    private UEImageBox imgIcon;
    private UEGauge gaugeSkill;
    private UELabel lblSkill;

    protected override void OnInit()
    {
        gaugeSkill = mfui.FindChildByEditName<UEGauge>("jn");
        imgIcon = mfui.FindChildByEditName<UEImageBox>("jn_icon");
        lblSkill = mfui.FindChildByEditName<UELabel>("jn_ms");
        gameObject.SetActive(false);
    }

    public void ShowSkillAction(TLAIActor actor, DeepCore.Unity3D.Battle.ComAIUnit.SkillActionStatus status, float actionTime)
    {
        var data = status.SkillAction.SkillData;
        var property = (TLSkillProperties)data.Properties;

        mActor = actor;

        //if (property.progressType == TLSkillProperties.SkillProgressType.Progress)
        //{
        //    gaugeSkill.Value = 0;
        //    lblSkill.Text = string.Format("{0} {1:0.0}/{2:0.#}", status.Skill.Data.Name, 0, actionTime);
        //}
        //else
        {
            gaugeSkill.Value = 100;
            lblSkill.Text = string.Format("{0} {1:0.0}/{1:0.#}", data.Name, actionTime);
        }
        imgIcon.Layout = HZUISystem.CreateLayoutFromAtlas(data.IconName, DeepCore.GUI.Data.UILayoutStyle.IMAGE_STYLE_BACK_4, 0);
        this.gameObject.SetActive(true);
    }

    void Update()
    {
        DeepCore.Unity3D.Battle.ComAIUnit.SkillActionStatus status;

        float elapsed;
        float totalTime;

        if (!mActor.GetShowSkillPasstime(out status, out elapsed, out totalTime))
        {
            gameObject.SetActive(false);
            return;
        }

        //if (elapsed < totalTime)
        //{
        //    TLSkillProperties property = (TLSkillProperties)status.Data.Properties;
        //}

        //if (property.progressType == TLSkillProperties.SkillProgressType.Progress)
        //{
        //    gaugeSkill.Value = 100 * elapsed / totalTime;
        //    lblSkill.Text = string.Format("{0} {1:0.0}/{2:0.#}", status.Skill.Data.Name, elapsed, totalTime);
        //}
        //else

        {
            gaugeSkill.Value = 100 - 100 * elapsed / totalTime;
            lblSkill.Text = string.Format("{0} {1:0.0}/{2:0.#}", status.Data.Name, totalTime - elapsed, totalTime);
        }

        //else
        {
            if (elapsed > totalTime + 1)
            {
                gameObject.SetActive(false);
            }
        }
    }
}