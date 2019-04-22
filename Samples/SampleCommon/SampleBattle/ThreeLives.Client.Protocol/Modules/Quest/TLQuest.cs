using System;
using System.Collections.Generic;
using System.Text;

namespace ThreeLives.Client.Common.Modules.Quest
{
    public class TLQuestCondition
    {
        public const string KillMonster = "eKillMonster";              //击杀怪物
        public const string FindNPC = "eFindNPC";                      //寻找NPC
        public const string PlayerAttribute = "p";                    //玩家属性
        public const string SubmitItem = "eSubmitItem";                //提交物品
        public const string KillPlayer = "eKillPlayer";                //击杀玩家
        public const string FinishEvent = "eFinishEvent";              //完成事件
        public const string TakePartDungeon = "eTakePartDungeon";      //参与副本
        public const string FinishDungeon = "eFinishDungeon";          //完成副本
        public const string GetVirtualItem = "eGetVirtualItem";        //获得虚拟物品
        public const string UseItem = "eUseItem";                      //使用物品
        public const string UseVirtualItem = "eUseVirtualItem";        //使用虚拟道具
        public const string PickItem = "ePickItem";                    //拾取物品
        public const string ProtectedNPC = "eProtectedNPC";            //保护NPC
        public const string TrusteeshipEvent = "eTrusteeshipEvent";    //由战斗服托管任务状态.
        public const string SubmitCustomItem = "eSubmitCustomItem";    //提交自定义物品
        public const string TipLoopQuest = "eTipLoopQuest";            //提醒任务开启
        public const string EquipIntensify = "eEquipIntensify";        //强化ｘ以上的装备ｘ件
        public const string CurEquipQuality = "eCurEquipQuality";       //当前装备指定品质&品级等级x件
        public const string CurEquipFateQuality = "eCurEquipFateQuality";// 当前装备指定品质&等级的命轮
        public const string CurRelationLvTotalNumber = "eRelationLvTotalNumber";//当前亲密度等级的总人数


    }
}
