#if (UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN) && !UNITY_ANDROID && !UNITY_IOS


using DeepCore;
using DeepCore.GameData;
using DeepCore.GameData.Zone;
using DeepCore.GameData.Zone.ZoneEditor;
using DeepCore.GameData.ZoneClient;
using DeepCore.GameHost.Instance;
using DeepCore.Vector;
using System.Collections.Generic;
using TLClient;

public class BattleRunManager
{
    public static TLBattleLocal CreateLocalBattle(SceneData scene)
    {
        TLBattleLocal localBattle = new TLBattleLocal(scene);
        return localBattle;
    }

    public static void AddLocalActor(TLBattleLocal battle, int templateID)
    {
        //玩家阵营为2.
        int regionForce = 2;
        ZoneRegion startRegion = battle.Zone.GetEditStartRegion(regionForce);

        if (startRegion == null)
        {
            using (var kvs = ListObjectPool<KeyValuePair<int, ZoneRegion>>.AllocAutoRelease())
            {
                battle.Zone.GetEditStartRegions(kvs);
                if (kvs.Count > 0)
                {
                    var t = battle.Zone.RandomN.GetRandomInArray(kvs);
                    startRegion = t.Value;
                    regionForce = t.Key;
                    if (startRegion == null)
                    {
                        throw new System.Exception("Can Not Find Actor StartRegion");
                    }
                }
            }
        }

        InitStartRegion(battle,battle.Zone.Data.GetStartRegions().Get(regionForce), templateID);
    }

    private static void InitStartRegion(TLBattleLocal battle, RegionData startRegion, int actorID)
    {

        UnitInfo info = TLClientBattleManager.DataRoot.Templates.GetUnit(actorID);
        PlayerStartAbilityData tgd = startRegion.GetAbilityOf<PlayerStartAbilityData>();
        if (info == null)
        {
            actorID = tgd.TestActorTemplateID;
            info = TLClientBattleManager.DataRoot.Templates.GetUnit(actorID);
        }

        InstanceUnit au = battle.Zone.AddUnit(new AddUnit()
        {
            info = info,
            editor_name = "ACTOR",
            force = (byte)tgd.START_Force,
            level = tgd.TestActorLevel,
            pos = new Vector2(startRegion.X, startRegion.Y)
        });

        LockActorEvent act = new LockActorEvent();
        act.GameServerProp = CUtils.TryClone<IUnitProperties>(au.Info.Properties);
        act.UnitData = au.GenSyncUnitInfo();
        act.Skills = au.GetSkillEvent();
        battle.onEventHandler(act);

    }
}
#endif
