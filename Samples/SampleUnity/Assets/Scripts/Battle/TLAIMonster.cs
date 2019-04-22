using Assets.Scripts;
using DeepCore.GameSlave;
using DeepCore.Unity3D;
using DeepCore.Unity3D.Battle;
using System.IO;
using TLBattle.Client.Client;
using TLBattle.Message;
using UnityEngine;

public class TLAIMonster : TLAIUnit
{
    private string name = null;

    public TLAIMonster(BattleScene battleScene, ZoneUnit obj) : base(battleScene, obj)
    {
        name = HZLanguageManager.Instance.GetString(GetVirtual().GetName());
        var vm = Virtual as TLClientVirtual_Monster;
        if (vm != null && vm.IsOwnerShipMonster())
            TLBattleScene.Instance.AddOwnerShipMonster(this.ObjectID);
    }
    public override string Name()
    {
        return name;
    }

    private TLClientVirtual_Monster GetVirtual()
    {
        return ZUnit.Virtual as TLClientVirtual_Monster;
    }

    protected override void OnLoadModelFinish(FuckAssetObject aoe)
    {
        base.OnLoadModelFinish(aoe);

        //精英怪加光圈
        if (this.GetVirtual().GetMonsterType() == MonsterVisibleDataB2C.MonsterType.Elite)
        {
            Vector3 scale = Vector3.one * this.Info.BodySize * 2;
            string circlePath = "/res/effect/ef_buff_elitecircle.assetbundles";
            FuckAssetObject.GetOrLoad(circlePath, Path.GetFileNameWithoutExtension(circlePath), (obj) =>
            {
                if (IsDisposed)
                {
                    obj.Unload();
                    return;
                }
                if (obj)
                {
                    obj.transform.parent = this.EffectRoot.transform;
                    obj.transform.localPosition = Vector3.zero;
                    obj.transform.localEulerAngles = Vector3.zero;
                    obj.transform.localScale = scale;
                }
            });
        }
    }

    protected override void RegistAllObjectEvent()
    {
        base.RegistAllObjectEvent();
        RegistObjectEvent<MonsterOwnerShipChangeEventB2C>(ObjectEvent_MonsterOwnerShipChangeEventB2C);
    }

    private void ObjectEvent_MonsterOwnerShipChangeEventB2C(MonsterOwnerShipChangeEventB2C e)
    {
        CheckShowHPBanner(true);
        TLBattleScene.Instance.OwnerShipChange();
    }

    protected override void OnDispose()
    {
        TLBattleScene.Instance.RemoveOwnerShipMonster(0);
        base.OnDispose();
    }
}


