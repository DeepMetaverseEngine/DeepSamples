
using DeepCore.GameData.Zone;
using DeepCore.GameSlave;
using DeepCore.Unity3D.Battle;
using DeepCore.Unity3D.Utils;
using TLBattle.Common.Plugins;
using UnityEngine;





public class TLAISpell : ComAISpell
{
    private Quaternion mYOffset;
    private Vector3 mOriPos;
    private Vector3 mRealPos;
    private float mUnActiveTime = 0.05f;

    public TLAISpell(BattleScene battleScene, ZoneSpell obj) : base(battleScene, obj)
    {
        if (Target != null)
        {
            mYOffset = Quaternion.AngleAxis(Random.Range(-3000f, 3000f) / 100f, this.Position - Target.Position);
            mOriPos = mRealPos = ObjectRoot.Position();
        }
        if ((ZSpell.Info.Properties as TLSpellProperties).BallisticType
                            == TLSpellProperties.ClientBallisticType.Parabola)
        {
            ObjectRoot.SetActive(false);
        }
    }

    public override bool SoundImportant()
    {
        if (this.Launcher == null)
        {
            return true;
        }
        return (this.Launcher is TLAIActor || !(this.Launcher is TLAIPlayer));
    }

    protected override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);

        if ((ZSpell.Info.Properties as TLSpellProperties).BallisticType
                            == TLSpellProperties.ClientBallisticType.Parabola)
        {
            mUnActiveTime -= deltaTime;
            if (ObjectRoot.activeSelf == false && mUnActiveTime < 0f)
            {
                ObjectRoot.SetActive(true);
            }
        }
    }
    protected static Vector3 currentVelocity;
    protected override void SyncState()
    {
        //调整高度 朝向等
        switch (ZSpell.Info.MType)
        {
            //抛物线
            case SpellTemplate.MotionType.Cannon:
                {
                    //Debugger.LogError("ZobjDirect" + ZObj.Direction);
                   // Debugger.LogError("ZObj.Parent.TerrainSrc.TotalHeight =" + ZObj.Parent.TerrainSrc.TotalHeight);
                    Vector3 tmp = Extensions.ZonePos2NavPos(ZObj.Parent.TerrainSrc.TotalHeight
                    , ZObj.X, ZObj.Y, ZObj.Z);
                    //Debugger.LogError("Positiontmp" + tmp);
                    tmp.y = tmp.y + ZSpell.LaunchHeight + ZObj.Z;
                    this.ObjectRoot.Position(tmp);

                }
                break;
            //追踪弹道
            case SpellTemplate.MotionType.Missile:
                {
                    if (Target != null && !Target.IsDisposed)
                    {
                        Vector3 prePos = this.ObjectRoot.Position();
                        bool dirct = true;
                        if (prePos == mRealPos)
                        {
                            dirct = false;
                        }
                        Vector3 from = mRealPos;
                        var chest = Target.GetDummyNode("chest_buff");
                        var to = ObjectRoot.Position() + ObjectRoot.Forward() * 100;
                        if (chest != null)
                        {
                            to = chest.transform.position;
                        }
                        var direct = (to - from).normalized * ZSpell.Info.MSpeedSEC * Time.deltaTime;
                        mRealPos = DeepCore.Unity3D.Utils.Extensions.ZonePos2UnityPos(ZObj.Parent.TerrainSrc.TotalHeight
                        , ZObj.X, ZObj.Y, ZObj.Z);
                        mRealPos = (from + direct);
                        if ((ZSpell.Info.Properties as TLSpellProperties).BallisticType
                            == TLSpellProperties.ClientBallisticType.Parabola)
                        {
                            var l1 = Vector3.Distance(this.ObjectRoot.Position(), mOriPos);
                            var l2 = Vector3.Distance(mOriPos, Target.Position);

                            this.ObjectRoot.Position(mRealPos + mYOffset * Vector3.up * Mathf.Sin(l1 / l2 * Mathf.PI) * (1.5f * l2 / 15));
                            if (dirct)
                            {
                                var d = Vector3.SmoothDamp(this.ObjectRoot.Forward(), this.ObjectRoot.Position() - prePos, ref currentVelocity, 0.055f, 100, Time.deltaTime);
                                this.ObjectRoot.Forward(d);
                            }
                        }
                        else
                        {
                            this.ObjectRoot.Forward(direct);
                            this.ObjectRoot.Position(mRealPos);
                        }
                    }
                    else
                    {
                        ObjectRoot.ZoneRot2UnityRot(ZObj.Direction);
                        Vector3 tmp = Extensions.ZonePos2UnityPos(ZObj.Parent.TerrainSrc.TotalHeight
                        , ZObj.X, ZObj.Y, ZObj.Z);
                        tmp.y = this.ObjectRoot.Position().y;
                        this.ObjectRoot.Position(tmp);
                    }
                }
                break;
            default:
                {
                    ObjectRoot.ZoneRot2UnityRot(ZObj.Direction);
                    Vector3 pos = Extensions.ZonePos2NavPos(ZObj.Parent.TerrainSrc.TotalHeight
                        , ZObj.X, ZObj.Y, ZObj.Z);
                    ObjectRoot.Position(new Vector3(pos.x, pos.y + ZSpell.LaunchHeight, pos.z));
                }
                break;
        }
    }
}
