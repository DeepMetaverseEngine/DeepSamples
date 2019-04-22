function main(NpcID,inner,center)
    --添加NPC脚底特效
    local eff1 = {
        Name = '/res/effect/ef_task_circle01.assetbundles',
        BindBody = true,
        BindPartName = 'Foot_Buff',
        IsLoop = true,
        EffectTimeMS = 9999999,
        ScaleToBodySize = inner * 2,
    }
    local eff2 = {
        Name = '/res/effect/ef_task_circle02.assetbundles',
        BindBody = true,
        BindPartName = 'Foot_Buff',
        IsLoop = true,
        EffectTimeMS = 9999999,
        ScaleToBodySize = center * 2,
    }
    
    Api.Listen.UnitInView(
        NpcID,
        function()
            Api.PlayUnitEffect(NpcID, eff1)
            Api.PlayUnitEffect(NpcID, eff2)
        end
    )
    Api.Task.WaitAlways()
end
