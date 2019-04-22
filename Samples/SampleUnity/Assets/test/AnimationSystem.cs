using System;
using System.Collections.Generic;

public class AnimationSystem : ISystem
{
    public bool Filter(UnitEntity entity)
    {
        return entity.Animation != null && entity.Location.Visible && entity.Model.Asset;
    }

    public void Execute(ICollection<UnitEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity.Model.Asset)
            {
                entity.Model.Asset.Play(entity.Animation.Name);
            }

            if (entity.Avatar != null)
            {
                foreach (var entry in entity.Avatar.Avatars)
                {
                    if (entry.Value.Asset)
                    {
                        entry.Value.Asset.Play(entity.Animation.Name);
                    }
                }
            }
            entity.Animation = null;
        }
    }
}