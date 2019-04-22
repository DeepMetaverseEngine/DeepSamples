
using DeepCore.Unity3D.Battle;
using DeepCore.Unity3D.UGUIEditor.UI;

public class BossSelectHud : HZUIBehaviour
{
    private UECanvas[] cavBoss;

    protected override void OnInit()
    {
        cavBoss = new UECanvas[2];
        cavBoss[0] = GetUI("bosskuang1") as UECanvas;
        cavBoss[1] = GetUI("bosskuang2") as UECanvas;

        cavBoss[0].Visible = false;
        cavBoss[1].Visible = false;
    }

    void ShowBoss(ComAIUnit boss1, ComAIUnit boss2)
    {
        cavBoss[0].Visible = true;
        cavBoss[1].Visible = true;
    }
}