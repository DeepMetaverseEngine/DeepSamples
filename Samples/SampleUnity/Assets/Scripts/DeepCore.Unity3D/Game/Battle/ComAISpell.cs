using DeepCore.Unity3D.Utils;
using DeepCore.GameData.Zone;
using DeepCore.GameSlave;
using UnityEngine;

namespace DeepCore.Unity3D.Battle
{
    public partial class ComAISpell : ComAICell
    {
        private ComAICell mLauncher;
        private ComAICell mTarget;


        protected ZoneSpell ZSpell { get { return ZObj as ZoneSpell; } }
        protected ComAICell Launcher { get { return mLauncher; } }
        protected ComAICell Target { get { return mTarget; } }

        public ComAISpell(BattleScene battleScene, ZoneSpell obj)
            : base(battleScene, obj)
        {
            if (ZSpell.Sender != null)
            {
                mLauncher = BattleScene.GetBattleObject(ZSpell.Sender.ObjectID);
            }
            if (ZSpell.Target != null)
            {
                mTarget = BattleScene.GetBattleObject(ZSpell.Target.ObjectID);
            }

            Vector3 pos = Extensions.ZonePos2NavPos(ZObj.Parent.TerrainSrc.TotalHeight
                , ZObj.X, ZObj.Y, ZObj.Z);

            ObjectRoot.Position(new Vector3(pos.x, pos.y + ZSpell.LaunchHeight, pos.z));
            //默认从胸部节点发射
            if (mLauncher != null)
            {
                var chestNode = mLauncher.GetDummyNode("Chest_Buff");
                if (chestNode)
                {
                    ObjectRoot.Position(new Vector3(pos.x, chestNode.transform.position.y, pos.z));
                }
            }

            if (mTarget != null)
            {
                this.ObjectRoot.Forward(mTarget.Position - this.ObjectRoot.Position());
            }

            if (!string.IsNullOrEmpty(ZSpell.Info.FileName))
            {
                DisplayCell.LoadModel(ZSpell.Info.FileName, System.IO.Path.GetFileNameWithoutExtension(ZSpell.Info.FileName), (loader) =>
                {
                    if (loader)
                    {
                        if (IsDisposed)
                        {
                            DisplayCell.Unload();
                            return;
                        }
                        OnLoadModelSuccess(loader);
                    }
                });
            }
        }

        protected virtual GameObject GetBindingPart(ComAICell unit)
        {
            DummyNode node = unit.GetDummyNode("chest_buff");
            if (node == null)
                return unit.EffectRoot;
            return node.gameObject;
        }

        protected virtual void OnLoadModelSuccess(FuckAssetObject aoe)
        {
            if (this.ZSpell.Info.FileBodyScale != 0)
            {
                aoe.transform.localScale = new Vector3(
                    this.ZSpell.Info.FileBodyScale,
                    this.ZSpell.Info.FileBodyScale,
                    this.ZSpell.Info.FileBodyScale);
            }

            if (!string.IsNullOrEmpty(ZSpell.Info.FileNameSpawn))
            {
                LaunchEffect effect = new LaunchEffect();
                effect.Name = ZSpell.Info.FileNameSpawn;
                PlayEffect(effect, EffectRoot.Position(), EffectRoot.Rotation());
            }

            if (ZSpell.Info.BodyShape == SpellTemplate.Shape.LineToTarget
                && Launcher != null && !Launcher.IsDisposed
                && Target != null && !Target.IsDisposed)
            {
                //必须自己实现连接
                GameObject from = GetBindingPart(Launcher);
                GameObject to = GetBindingPart(Target);
                BattleFactory.Instance.MakeDamplingJoint(aoe.gameObject, from, to);
            }
        }

        protected override void SyncState()
        {
            //调整高度 朝向等
            switch (ZSpell.Info.MType)
            {
                //抛物线
                case SpellTemplate.MotionType.Cannon:
                    {

                    }
                    break;
                //追踪弹道
                case SpellTemplate.MotionType.Missile:
                    {
                        if (Target != null && !Target.IsDisposed)
                        {
                            var from = this.ObjectRoot.Position();
                            var to = Target.GetDummyNode("chest_buff").transform.position;
                            var direct = (to - from).normalized * ZSpell.Info.MSpeedSEC * Time.deltaTime;
                            this.ObjectRoot.Forward(direct);
                            Vector3 tmp = Extensions.ZonePos2UnityPos(ZObj.Parent.TerrainSrc.TotalHeight
                            , ZObj.X, ZObj.Y, ZObj.Z);
                            tmp.y = from.y + direct.y;
                            this.ObjectRoot.Position(tmp);
                            //Debug.LogError("MISSILE POS x=" + ZObj.X + " y=" + ZObj.Y + " z=" + ZObj.Z);
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

        protected override void OnDispose()
        {
            //if (!string.IsNullOrEmpty(ZSpell.Info.FileNameDestory))
            //{
            //    LaunchEffect effect = new LaunchEffect();
            //    effect.Name = ZSpell.Info.FileNameDestory;
            //    PlayEffect(effect, EffectRoot.Position(), EffectRoot.Rotation());
            //}
            base.OnDispose();
        }

        public override bool IsImportant()
        {
            if (Launcher != null)
            {
                var actor = BattleScene.GetActor();
                if (actor != null)
                {
                    return (actor.ObjectID() == Launcher.ObjectID);
                }
            }

            return base.IsImportant();
        }
    }
}