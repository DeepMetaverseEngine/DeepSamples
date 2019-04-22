using DeepCore.Unity3D.Battle;

using UnityEngine;
//目标对象
public class DogUnitTarget
{
    //自己对象ID
    private uint mObjectID;

    //当前选中的目标对象ID
    public uint TargetID { get; protected set; }

    //目标对象，当前位置
    public Vector3 TargetPos { get; set; }

    //目标单位
    public ComAIUnit TargetUnit { get; set; }


    public DogUnitTarget(uint objectID)
    {
        mObjectID = objectID;
        TargetID = 0;
        TargetUnit = null;
    }

    public void SetTarget(uint targetID, ComAIUnit unit)
    {
        if(targetID != this.TargetID)
        {

        }
    }

    public void RemoveTarget()
    {
        if(TargetUnit != null)
        {
            this.TargetID = 0;
        }
    }

}
