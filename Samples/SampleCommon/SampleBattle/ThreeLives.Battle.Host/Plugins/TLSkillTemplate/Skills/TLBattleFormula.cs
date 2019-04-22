using ThreeLives.Battle.Data.Data;

namespace ThreeLives.Battle.Host.Plugins.TLSkillTemplate.Skills
{
    public class TLBattleFormula
    {
        public static float LEVEL_LIMIT = 300;
        public static float HIT_RATE_C1 = 0.5f;
        public static int HIT_RATE_C2 = 19800;
        public static float HIT_RATE_C3 = 20000.0f;
        public static float BLOCK_C1 = 0.5f;
        public static int BLOCK_C2 = 233;
        public static int BLOCK_C3 = 5000;
        public static int BLOCK_C4 = 6000;
        public static float CRIT_RATE_C1 = 0.005f;
        public static float CRIT_RATE_C2 = 20000.0f;
        public static float CRIT_RATE_C3 = 300;
        public static float CRIT_RATE_C4 = 100.0f;
        public static float CRIT_RATE_C5 = 2000;
        public static float CRIT_RATE_C6 = 0.8f;
        public static float CRIT_DAMAGE_PER_C1 = 1.20f;
        public static float CRIT_HEAL_C1 = 0.005f;
        public static float CRIT_HEAL_C2 = 40000;
        public static int DAMAGE_REDUCE_RATE_C1 = 67;
        public static float PHY_DAMAGE_REDUCE_RATE_C1 = 0.01f;
        public static int PHY_DAMAGE_REDUCE_RATE_C2 = 80910;
        public static int PHY_DAMAGE_REDUCE_RATE_C3 = 300;
        public static float PHY_DAMAGE_REDUCE_RATE_C4 = 100;
        public static int PHY_DAMAGE_REDUCE_RATE_C5 = 3300;
        public static float PHY_DAMAGE_REDUCE_RATE_C6 = 0.9f;
        public static float PHY_DAMAGE_REDUCE_RATE_C7 = 2;
        public static float PHY_DAMAGE_REDUCE_RATE_C8 = 3;

        public static float MAG_DAMAGE_REDUCE_RATE_C1 = 0.01f;
        public static int MAG_DAMAGE_REDUCE_RATE_C2 = 80910;
        public static int MAG_DAMAGE_REDUCE_RATE_C3 = 300;
        public static float MAG_DAMAGE_REDUCE_RATE_C4 = 100;
        public static int MAG_DAMAGE_REDUCE_RATE_C5 = 3300;
        public static float MAG_DAMAGE_REDUCE_RATE_C6 = 0.9f;
        public static float MAG_DAMAGE_REDUCE_RATE_C7 = 2;
        public static float MAG_DAMAGE_REDUCE_RATE_C8 = 3;

        public static int CAL_DAMAGE_C1 = 9000;
        public static int CAL_DAMAGE_C2 = 10000;
        public static float CAL_TOTAL_DAMAGE_PER_RATE_C1 = 0.01f;
        public static float PVE_C = 1;
        public static float PVP_C = 0.8f;


        public static void Init(TLBattleFormulaData data)
        {
            LEVEL_LIMIT = data.level_limit;
            HIT_RATE_C1 = data.hit_rate_c1;
            HIT_RATE_C2 = data.hit_rate_c2;
            HIT_RATE_C3 = data.hit_rate_c3;
            BLOCK_C1 = data.block_c1;
            BLOCK_C2 = data.block_c2;
            BLOCK_C3 = data.block_c3;
            BLOCK_C4 = data.block_c4;
            CRIT_RATE_C1 = data.crit_rate_c1;
            CRIT_RATE_C2 = data.crit_rate_c2;
            CRIT_RATE_C3 = data.crit_rate_c3;
            CRIT_RATE_C4 = data.crit_rate_c4;
            CRIT_RATE_C5 = data.crit_rate_c5;
            CRIT_RATE_C6 = data.crit_rate_c6;
            CRIT_DAMAGE_PER_C1 = data.crit_damage_per_c1;
            CRIT_HEAL_C1 = data.crit_heal_c1;
            CRIT_HEAL_C2 = data.crit_heal_c2;
            DAMAGE_REDUCE_RATE_C1 = data.damage_reduce_rate_c1;
            PHY_DAMAGE_REDUCE_RATE_C1 = data.phy_damage_reduce_rate_c1;
            PHY_DAMAGE_REDUCE_RATE_C2 = data.phy_damage_reduce_rate_c2;
            PHY_DAMAGE_REDUCE_RATE_C3 = data.phy_damage_reduce_rate_c3;
            PHY_DAMAGE_REDUCE_RATE_C4 = data.phy_damage_reduce_rate_c4;
            PHY_DAMAGE_REDUCE_RATE_C5 = data.phy_damage_reduce_rate_c5;
            PHY_DAMAGE_REDUCE_RATE_C6 = data.phy_damage_reduce_rate_c6;
            PHY_DAMAGE_REDUCE_RATE_C7 = data.phy_damage_reduce_rate_c7;
            PHY_DAMAGE_REDUCE_RATE_C8 = data.phy_damage_reduce_rate_c8;

            MAG_DAMAGE_REDUCE_RATE_C1 = data.mag_damage_reduce_rate_c1;
            MAG_DAMAGE_REDUCE_RATE_C2 = data.mag_damage_reduce_rate_c2;
            MAG_DAMAGE_REDUCE_RATE_C3 = data.mag_damage_reduce_rate_c3;
            MAG_DAMAGE_REDUCE_RATE_C4 = data.mag_damage_reduce_rate_c4;
            MAG_DAMAGE_REDUCE_RATE_C5 = data.mag_damage_reduce_rate_c5;
            MAG_DAMAGE_REDUCE_RATE_C6 = data.mag_damage_reduce_rate_c6;
            MAG_DAMAGE_REDUCE_RATE_C7 = data.mag_damage_reduce_rate_c7;
            MAG_DAMAGE_REDUCE_RATE_C8 = data.mag_damage_reduce_rate_c8;

            CAL_DAMAGE_C1 = data.cal_damage_c1;
            CAL_DAMAGE_C2 = data.cal_damage_c2;
            CAL_TOTAL_DAMAGE_PER_RATE_C1 = data.cal_total_damage_per_rate_c1;

            PVE_C = data.pve_coefficient;
            PVP_C = data.pvp_coefficient;
        }


    }
}
