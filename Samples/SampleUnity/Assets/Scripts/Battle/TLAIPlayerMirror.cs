using System;
using Assets.Scripts;
using DeepCore.GameSlave;
using DeepCore.Unity3D;
using DeepCore.Unity3D.Battle;
using System.IO;
using DeepCore;
using TLBattle.Client;
using TLBattle.Client.Client;
using TLBattle.Common.Plugins;
using TLBattle.Message;
using UnityEngine;

public class TLAIPlayerMirror : TLAIPlayer
{
    private string curName = String.Empty;
    public TLAIPlayerMirror(BattleScene battleScene, ZoneUnit obj) : base(battleScene, obj)
    {
       
    }
    private TLClientVirtual_PlayerMirror GetVirtual()
    {
        return ZUnit.Virtual as TLClientVirtual_PlayerMirror;
    }

    protected override bool IsShowHPBar()
    {
        return true;
    }
    
    public override string Name()
    {
        if (!(this.SyncInfo.VisibleInfo is PlayerVisibleDataB2C))
        {
            return base.Name();
        }

        if (!string.IsNullOrEmpty(curName))
        {
            return curName;
        }
        if ((this.SyncInfo.VisibleInfo as PlayerVisibleDataB2C).BaseInfo.PlayerMirrorType ==
            TLUnitBaseInfo.MirrorType.Monster)
        {
            curName = HZLanguageManager.Instance.GetString(base.Name());
        }
        else
        {
            curName = base.Name();
        }
        return curName;
    }
}


