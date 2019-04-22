
using DeepCore.GameSlave;
using DeepCore.Unity3D.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TLDecoration : BattleDecoration
{
    private BattleInfoBar mInfoBar;

    public TLDecoration(BattleScene battleScene, ZoneEditorDecoration zf) : base(battleScene, zf)
    {
        this.mInfoBar = BattleInfoBarManager.AddInfoBar(this.ObjectRoot.transform
            , new Vector3(0, this.ZDecoration.Data.H * this.ZDecoration.Data.Scale, 0)
            , false
            , false);
        this.mInfoBar.SetName(this.ZDecoration.Data.DisplayName, GameUtil.ARGB_To_RGBA(0xFF5DE771));
    }

    protected override void OnDispose()
    {
        base.OnDispose();

        if (mInfoBar != null)
        {
            mInfoBar.Remove();
            mInfoBar = null;
        }
    }
}
