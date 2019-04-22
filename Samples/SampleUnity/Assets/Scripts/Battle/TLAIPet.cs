using Assets.Scripts;
using DeepCore.GameSlave;
using DeepCore.Unity3D.Battle;


public class TLAIPet : TLAIUnit
{
    public TLAIPet(BattleScene battleScene, ZoneUnit obj) : base(battleScene, obj)
    {

    }

    public override string Name()
    {
        string key = ClientVirtual != null ? ClientVirtual.GetName() : "";
        return HZLanguageManager.Instance.GetString(key);
    }
}

