using DeepMMO.Client;

namespace TLClient
{
    public class TLClientBattleManager : RPGClientBattleManager
    {

        private static TLClientBattleManager mInstance;
        public static TLClientBattleManager TLInstance
        {
            get
            {
                if (mInstance == null)
                    new TLClientBattleManager();
                return mInstance;
            }
        }

        public TLClientBattleManager()
        {
            mInstance = this;
        }

//         public virtual TLBattleLocal CreateLocalBattle(SceneData scene)
//         {
//             TLBattleLocal localBattle = new TLBattleLocal(scene);
//             //BattleLocalPlay localBattle = new BattleLocalPlay(DataRoot, scene);
//             return localBattle;
//         }
// 
//         public void AddLocalActor(TLBattleLocal battle, int templateID)
//         {
//             UnitInfo info = TLClientBattleManager.DataRoot.Templates.GetUnit(templateID);
//             info.UType = UnitInfo.UnitType.TYPE_PLAYER;
//             info = info.Clone() as UnitInfo;
// 
//             ZoneRegion startRegion = battle.Zone.GetEditStartRegion(0);
//             InstanceUnit au = battle.Zone.AddUnit(new AddUnit()
//             {
//                 info = info,
//                 editor_name = "ACTOR",
//                 force = 0,
//                 level = 0,
//                 pos = new CommonLang.Vector.Vector2(startRegion.Pos.x, startRegion.Pos.y)
//             });
//             LockActorEvent act = new LockActorEvent();
//             act.GameServerProp = CUtils.TryClone<IUnitProperties>(au.Info.Properties);
//             act.UnitData = au.GenSyncUnitInfo();
//             act.Skills = au.GetSkillEvent();
//             //battle.Zone.queueEvent(act);
//             battle.onEventHandler(act);
//         }
    }

}
