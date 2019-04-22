using DeepCore.GameData;
using DeepCore.Reflection;

namespace TLBattle.Plugins
{
    /// </summary>
    [DescAttribute("TL编辑器配置")]
    public class TLEditorConfig : ICommonConfig
    {
        [DescAttribute("怪物感叹号时间", "战斗相关")]
        public int SignTime = 1000;
        [DescAttribute("摄像机延迟移动时间", "怪物死亡")]
        public int CameraMoveDelayTime = 200;
        [DescAttribute("摄像机移动时间", "怪物死亡")]
        public int CameraMoveTime = 100;
        [DescAttribute("镜头拉近距离", "怪物死亡")]
        public float CameraMoveDistance = 0;
        [DescAttribute("恢复正常的时间", "怪物死亡")]
        public int OverTime = 700;
        [DescAttribute("镜头拉回的事件", "怪物死亡")]
        public int RecoverTime = 800;
        [DescAttribute("时间缩放比例", "怪物死亡")]
        public float TimeScale = 0.5f;
        [DescAttribute("玩家队伍跟随距离上限（超过就会被瞬间拉过来）", "组队相关")]
        public float PLAYER_FOLLOW_DISTANCE_LIMIT = 15f;
        [DescAttribute("玩家跟随距离（靠近队长多少停止）", "组队相关")]
        public float PLAYER_FOLLOW_DISTANCE_MIN = 2f;
        [DescAttribute("玩家跟随距离（离队长多远时跟随）", "组队相关")]
        public float PLAYER_FOLLOW_DISTANCE_MAX = 5f;
        [DescAttribute("召唤物跟随距离上限（超过就会被瞬间拉过来）", "召唤物相关")]
        public float SUMMON_FOLLOW_DISTANCE_LIMIT = 15f;
        [DescAttribute("召唤物跟随距离（靠近主人多少停止）", "召唤物相关")]
        public float SUMMON_FOLLOW_DISTANCE_MIN = 2f;
        [DescAttribute("召唤物待机跟随距离（离主人多远时跟随）", "召唤物相关")]
        public float SUMMON_IDLEFOLLOW_DISTANCE_MAX = 5f;
        [DescAttribute("召唤物攻击跟随距离（攻击状态下离主人多远时跟随）", "召唤物相关")]
        public float SUMMON_ATKFOLLOW_DISTANCE_MAX = 10f;

        [DescAttribute("宠物跟随距离上限（超过就会被瞬间拉过来）", "宠物相关")]
        public float PET_FOLLOW_DISTANCE_LIMIT = 15f;
        [DescAttribute("宠物跟随距离（靠近主人多少停止）", "宠物相关")]
        public float PET_FOLLOW_DISTANCE_MIN = 2f;
        [DescAttribute("宠物待机跟随距离（离主人多远时跟随）", "宠物相关")]
        public float PET_IDLEFOLLOW_DISTANCE_MAX = 5f;
        [DescAttribute("宠物攻击跟随距离（攻击状态下离主人多远时跟随）", "宠物相关")]
        public float PET_ATKFOLLOW_DISTANCE_MAX = 10f;

        [DescAttribute("多杀间隔时间", "Moba")]
        public int Moba_Interval_MS = 3000;
        [DescAttribute("复活基础时间", "Moba")]
        public int Moba_Rebirth_MS = 3000;
        [DescAttribute("复活添加时间", "Moba")]
        public int Moba_Rebirth_Add_MS = 1000;

        [DescAttribute("全屏显示是buffid", "公会战")]
        public int AlwaysShowBuff = 10000;

        [DescAttribute("距离目标多远时启动加速", "任务自动寻路相关")]
        public float SPEEDUP_TASKDISTANCE = 15f;
        [DescAttribute("距离目标多近时关闭加速", "任务自动寻路相关")]
        public float SPEEDOVER_TASKDISTANCE = 1f;
        [DescAttribute("起步多少距离后开启加速", "任务自动寻路相关")]
        public float SPEEDUP_TASKSTARTDISTANCE = 2f;

        [DescAttribute("自我回血周期时间", "战斗相关")]
        public int AUTO_RECOVER_HP_CYCLETIME = 1000;
        [DescAttribute("自我回蓝周期时间", "战斗相关")]
        public int AUTO_RECOVER_MP_CYCLETIME = 1000;

        [DescAttribute("伤害转换仇恨系数.", "战斗相关")]
        public float DAMAGE_TO_HATE_VALUE = 1.0f;
        [DescAttribute("治疗转换仇恨系数.", "战斗相关")]
        public float HEAL_TO_HATE_VALUE = 0.5f;
        [DescAttribute("未受伤害脱离战斗时间.", "战斗相关")]
        public int COMBAT_TIME_EXPIRE = 10000;


        [DescAttribute("玩家跟随NPC距离（靠近NPC多少停止）", "跟随npc相关")]
        public float PLAYER_FOLLOWNPC_DISTANCE_MIN = 2f;
        [DescAttribute("玩家跟随距离（离NPC多远时跟随）", "跟随npc相关")]
        public float PLAYER_FOLLOWNPC_DISTANCE_MAX = 5f;

        [DescAttribute("PK 灰名持续时间", "PK相关")]
        public int PK_GRAY_STATUS_TIMEMS = 6000;

        [DescAttribute("PK 灰名检测", "PK相关")]
        public int PK_GRAY_STATUS_CYCLE_TIMEMS = 3000;

        [DescAttribute("PK 灰名颜色(RGBA)", "PK相关")]
        public uint PK_GRAY_STATUS_CORLOR = 0xa2a2a2ff;

        [DescAttribute(" 副本中自动战斗索敌范围.", "战斗扩展")]
        public int DUNGEON_AUTO_GUARD_RANGE = 120;
        [DescAttribute(" 非副本场景自动战斗索敌范围.", "战斗扩展")]
        public int NORMALSCENE_AUTO_GUARD_RANGE = 20;
   
        public override string ToString()
        {
            return "TL 配置扩展属性";
        }
        public static TLEditorConfig Instance
        {
            get;
            private set;
        }
        public TLEditorConfig() { Instance = this; }

    }
}
