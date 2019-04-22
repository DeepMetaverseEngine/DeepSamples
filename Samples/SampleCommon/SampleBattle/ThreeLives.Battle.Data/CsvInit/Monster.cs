using System.Collections.Generic;

namespace TLBattle.CsvInit
{
   
    public class Monster
    {
        public int id;
        public string name;
        public int scene;
        public int   Prefer;
        public int   Type;
        public int   appear;
        public int  exp;
        public int   god;
        public string  item;
        public string  note;
    }

    /// <summary>
    /// 怪物能力.
    /// </summary>
    public class MonsterAbility
    {
        public int MaxHP;
        public int HP;
        public int HealPS;
        public int HPPer;
        public int HPModifer;
        public int HealPer;
        public int HPStealPer;
        public int Atk;
        public int AtkPer;
        public int Power;
        public int PowerPer;
        public int Parry;
        public int ParryPer;
        public int Def;
        public int DefPer;
        public int CriRate;
        public int CriDamage;
        public int DodgeRate;
        public int MaxAnger;
        public int Anger;
        public int AngerReborn;
        public int AngerKillReborn;
        public int AngerHitReborn;  
        public int RunSpeed;
        public int FasterAttack;
        
        public int UnitSystem;
        public int CatchPetPer;
        public int GetItemPer;
        public int DamMonster;
        public int DamPlayer;
        public int DamageA;
        public int DamageB;
        public int DamageC;
        public int DamageMinusPer;
        public List<int> State_harmList = new List<int>();
    }

    /// <summary>
    /// 怪物AI信息.
    /// </summary>
    public class MonsterAIInfo
    {
        public int TemplateId;
        public int AIType;
        public int Level;
        public int Parameter1;
        public int Parameter2;
        public int Parameter3;
        public int Parameter4;
        public int Parameter5;
        public int Parameter6;
        public int Parameter7;
        public int Parameter8;
    }
}
