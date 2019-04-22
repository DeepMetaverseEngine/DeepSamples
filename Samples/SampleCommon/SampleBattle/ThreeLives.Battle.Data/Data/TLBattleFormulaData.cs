namespace ThreeLives.Battle.Data.Data
{
    public class TLBattleFormulaData
    {
        public int id;
        public float level_limit = 300;
        public float hit_rate_c1 = 0.5f;
        public int hit_rate_c2 = 19800;
        public float hit_rate_c3 = 20000.0f;
        public float block_c1 = 0.5f;
        public int block_c2 = 233;
        public int block_c3 = 5000;
        public int block_c4 = 6000;
        public float crit_rate_c1 = 0.005f;
        public float crit_rate_c2 = 20000.0f;
        public float crit_rate_c3 = 300;
        public float crit_rate_c4 = 100.0f;
        public float crit_rate_c5 = 2000;
        public float crit_rate_c6 = 0.8f;
        public float crit_damage_per_c1 = 1.20f;
        public float crit_heal_c1 = 0.005f;
        public float crit_heal_c2 = 40000;
        public int damage_reduce_rate_c1 = 67;

        public float phy_damage_reduce_rate_c1 = 0.01f;
        public int phy_damage_reduce_rate_c2 = 80910;
        public int phy_damage_reduce_rate_c3 = 300;
        public float phy_damage_reduce_rate_c4 = 100;
        public int phy_damage_reduce_rate_c5 = 3300;
        public float phy_damage_reduce_rate_c6 = 0.9f;
        public float phy_damage_reduce_rate_c7 = 2;
        public float phy_damage_reduce_rate_c8 = 3;

        public float mag_damage_reduce_rate_c1 = 0.01f;
        public int mag_damage_reduce_rate_c2 = 80910;
        public int mag_damage_reduce_rate_c3 = 300;
        public float mag_damage_reduce_rate_c4 = 100;
        public int mag_damage_reduce_rate_c5 = 3300;
        public float mag_damage_reduce_rate_c6 = 0.9f;
        public float mag_damage_reduce_rate_c7 = 2;
        public float mag_damage_reduce_rate_c8 = 3;

        public int cal_damage_c1 = 9000;
        public int cal_damage_c2 = 10000;
        public float cal_total_damage_per_rate_c1 = 0.01f;

        public float pve_coefficient = 1;
        public float pvp_coefficient = 0.8f;

    }
}
