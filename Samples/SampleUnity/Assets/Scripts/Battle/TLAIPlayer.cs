
using DeepCore;
using DeepCore.GameData.Data;
using DeepCore.GameData.Zone;
using DeepCore.GameSlave;
using DeepCore.Unity3D;
using DeepCore.Unity3D.Battle;
using System;
using System.Collections.Generic;
using TLBattle.Client;
using TLBattle.Client.Client;
using TLBattle.Common.Plugins;

public partial class TLAIPlayer : TLAIUnit
{
    public TLClientVirtual_Player PlayerVirtual { get { return this.ZUnit.Virtual as TLClientVirtual_Player; } }


    public TLAIPlayer(BattleScene battleScene, ZoneUnit obj) : base(battleScene, obj)
    {
        ShowModel = TLBattleFactory.Instance.AddDisplayPool(obj.ObjectID);
    }
    protected override void OnLoadModelFinish(FuckAssetObject aoe)
    {
        base.OnLoadModelFinish(aoe);
        if (this.PlayerVirtual != null)
        {
            this.PlayerVirtual.OnBaseInfoChanged += OnBaseInfoChanged;
            this.PlayerVirtual.OnPKLevelChanged += PlayerVirtual_OnPKLevelChanged;
            this.PlayerVirtual.OnPracticeLvChanged += PlayerVirtual_OnPracticeLvChanged;
            this.PlayerVirtual.OnTitleIdChanged += PlayerVirtual_OnTitleIdChanged;
            PlayerVirtual_OnPKLevelChanged(PlayerVirtual.GetPKLevel());
        }

        (ZObj as ZoneUnit).OnStartPickObject += OnStartPickObject;
        (ZObj as ZoneUnit).OnStopPickObject += OnStopPickObject;

        if (aoe != null)
        {
            var soundobj = aoe.gameObject.GetComponent<TLPlaySound>();
            if (soundobj == null)
            {
                soundobj = aoe.gameObject.AddComponent<TLPlaySound>();
            }
            soundobj.CanPlay = false;
        }



        //归属权监听.
        TLBattleScene.Instance.OwnerShipChangeHandler += OnOwnerShipChange;
        CheckOwnerShip();
    }

    protected virtual void OnBaseInfoChanged(TLUnitBaseInfo info)
    {
        bindBehaviour.InfoBar.SetName(info.Name);
    }

    protected virtual void PlayerVirtual_OnPKLevelChanged(int obj)
    {
        if (!GameGlobal.Instance.netMode)
            return;

        //PK等级变更时，需要判断是否变成主角的敌人.
        CheckShowHPBanner(true);
    }

    private void PlayerVirtual_OnPracticeLvChanged(int lv)
    {
        bindBehaviour.InfoBar.SetPractice(lv);
    }

    protected virtual void PlayerVirtual_OnTitleIdChanged(int titleId,string nameExt)
    {
        bindBehaviour.InfoBar.SetTitle(titleId, nameExt);
    }

    protected override void OnDispose()
    {
        base.OnDispose();
        if (this.PlayerVirtual != null)
        {
            this.PlayerVirtual.OnBaseInfoChanged -= OnBaseInfoChanged;
            this.PlayerVirtual.OnPKLevelChanged -= PlayerVirtual_OnPKLevelChanged;
            this.PlayerVirtual.OnPracticeLvChanged -= PlayerVirtual_OnPracticeLvChanged;
            this.PlayerVirtual.OnTitleIdChanged -= PlayerVirtual_OnTitleIdChanged;
        }

        (ZObj as ZoneUnit).OnStartPickObject -= OnStartPickObject;
        (ZObj as ZoneUnit).OnStopPickObject -= OnStopPickObject;
        TLBattleFactory.Instance.RemoveDisplayPool(ZObj.ObjectID);
        OnTeamMemberListChange = null;
        TLBattleScene.Instance.OwnerShipChangeHandler -= OnOwnerShipChange;
    }

    public override bool SoundImportant()
    {
        return false;
    }

    protected virtual void OnStopPickObject(ZoneUnit unit, UnitStopPickObjectEvent stop)
    {
        RemoveFishhModel();
        this.RemovePickActionStatus();
    }

    //采集动画在player里播放
    protected virtual void OnStartPickObject(ZoneUnit unit, TimeExpire<UnitStartPickObjectEvent> start)
    {
        var obj = BattleScene.GetBattleObject(start.Tag.PickObjectID);

        if (obj == null) return;

        if (obj is TLAIItem)//PICK ITEM.0
        {
            var item = obj as TLAIItem;
            if (item != null)
            {

                float angle = UnityEngine.Mathf.Atan2(item.Y - unit.Y, item.X - unit.X);
                unit.SetDirection(angle);

                TLItemProperties zp = item.GetItemProperties();
                if (!string.IsNullOrEmpty(zp.PickAction))
                {
                    if (zp.PickAction == "n_fish")
                    {
                        ChangeFish((succ) =>
                        {
                            if (succ)
                            {
                                RegistFishActionStatus(zp.PickAction, (item.ZObj as ZoneItem).Info.PickTimeMS);
                            }
                        });

                    }
                    else if (zp.PickAction == "n_fish_special")
                    {
                        ChangeFish((succ) =>
                        {
                            if (succ)
                            {
                                RegistFishActionStatus(zp.PickAction, (item.ZObj as ZoneItem).Info.PickTimeMS, true);
                            }
                        });
                    }
                    else
                    {

                        RegistPickActionStatus(zp.PickAction);
                    }

                }
            }
        }
        else//Player or Actor
        {
            RegistPickActionStatus("n_idle");
        }
    }

    protected override void ZUnit_OnActionChanged(ZoneUnit unit, UnitActionStatus status, object msg)
    {
        if (status == UnitActionStatus.Rebirth)
        {
            this.CheckShowHPBanner(true);
            //复活后通知复活界面关闭，避免服务器拉人起来，复活界面关不掉的情况
            Dictionary<string, object> args = new Dictionary<string, object>()
            {
                {"uid",unit.PlayerUUID},
            };
            EventManager.Fire("Event.PlayerRebirth", args);
        }

        //PICK在上层会有逻辑，在这里不切换.
        if (status != UnitActionStatus.Pick)
            this.ChangeAction(status);

    }

    protected override bool IsShowHPBar()
    {
        return TLBattleScene.Instance.TargetIsEnemy(this);
    }

    protected void CheckOwnerShip()
    {
        if (bindBehaviour.InfoBar == null)
            return;

        var monsterLt = TLBattleScene.Instance.GetOwnerShipMonsterID();
        if(monsterLt == null)
        {
            bindBehaviour.InfoBar.ShowOwnership(false);
            return;
        }
        uint monsterID = 0;
        int count = 0;
        for (int i = 0; i < monsterLt.Count; i++)
        {
            monsterID = monsterLt[i];

            if (monsterID != 0)
            {
                var unit = TLBattleScene.Instance.GetBattleObject(monsterID);
                if (unit == null) continue;

                TLAIMonster monster = unit as TLAIMonster;
                if (monster != null && monster.Virtual != null)
                {
                    string uuid = (monster.Virtual as TLClientVirtual_Monster).GetOwnerShipUUID();
                    if (string.IsNullOrEmpty(uuid))
                    {
                        continue;
                    }

                    if (this.PlayerUUID == uuid)
                    {
                        count++;
                    }
                }
            }
        }

        if (count != 0)
            bindBehaviour.InfoBar.ShowOwnership(true);
        else
            bindBehaviour.InfoBar.ShowOwnership(false);

    }

    public float GetX()
    {
        return X;
    }

    public float GetY()
    {
        return Y;
    }
    
    protected void OnOwnerShipChange()
    {
        CheckOwnerShip();
    }

}
