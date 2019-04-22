using DeepCore.Unity3D.Utils;
using System.Collections.Generic;
using DeepCore.Unity3D;

public class LocationSystem : ISystem
{
    public bool Filter(UnitEntity entity)
    {
        return entity.Model != null && entity.Model.Asset;
    }

    private void SetTransform(UnitEntity entity)
    {
        var obj = entity.UnityObject.Obj;
        obj.SetActive(entity.Location.Visible);
        obj.transform.SetParent(entity.Location.Parent);
        obj.transform.localPosition = entity.Location.Pos;
        obj.transform.localScale = entity.Location.Scale;
        if (entity.Location.HasQuaternion)
        {
            obj.transform.rotation = entity.Location.Rotation;
        }

        if (entity.Location.HasDeg)
        {
            obj.transform.localEulerAngles = entity.Location.Deg;
        }
    }

    /// <summary>
    /// todo 拆到其他System去
    /// </summary>
    /// <param name="entity"></param>
    private void LogicLocation2Real(UnitEntity entity)
    {
        entity.Location.Pos = Extensions.ZonePos2NavPos(TLBattleScene.Instance.TotalHeight, entity.LogicLocation.Pos.x, entity.LogicLocation.Pos.y);
        entity.Location.Rotation = UnityHelper.LogicRad2Quaternion(entity.LogicLocation.Direction);
    }

    public void Execute(ICollection<UnitEntity> entities)
    {
        foreach (var entity in entities)
        {
            LogicLocation2Real(entity);
            SetTransform(entity);
        }
    }
}