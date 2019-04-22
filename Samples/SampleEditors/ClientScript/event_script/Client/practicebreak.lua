local function GetAvatarMap(stage)
    local pro = Api.GetActorPro()
    local gender = Api.GetActorGender()
    if stage == 0 then
        local map = Api.GetActorAvartarMap()
        local ret = {}
        ret[Constants.AvatarPart.Avatar_Body] = map[Constants.AvatarPart.Avatar_Body]
        ret[Constants.AvatarPart.Avatar_Head] = map[Constants.AvatarPart.Avatar_Head]
        return ret
    end
    local p = Api.FindFirstExcelData('practice/practice.xlsx/practice', {artifact_stage = stage})
    local modelinfo = Api.FindFirstExcelData('practice/practice.xlsx/practice_modle_group', {group_id = p.group_id, sex = gender, pro = pro})
    local avatarmap = {}
    for i, v in ipairs(modelinfo.modle.key) do
        local part = Constants.AvatarPart[v]
        avatarmap[part] = modelinfo.modle.value[i]
    end
    return avatarmap
end

function main(stage)
    Api.SubscribeGlobalBack('event.'..ID, function() return true end)
    stage = stage or 0
    Api.Task.BlockActorAutoRun()
    local ele = Api.FindFirstExcelData('event/break.xlsx/break', {artifact_stage = stage})
    ui = Api.UI.Open('xml/practice/ui_practicebreak.gui.xml')

    Api.UI.Listen.MenuExit(
        ui,
        function()
            Api.Task.StopEvent(ID, false, 'MenuExit')
        end
    )
    local gg_pt = Api.UI.FindChild(ui, 'gg_pt')
    local ib_back = Api.UI.FindChild(ui, 'ib_back')
    local btn_break = Api.UI.FindChild(ui, 'btn_break')
    local cvs_showup = Api.UI.FindChild(ui, 'cvs_showup')
    local cvs_practicebreak = Api.UI.FindChild(ui, 'cvs_practicebreak')
    Api.UI.SetEnable(btn_break, false)

    Api.UI.SetGaugeMinMax(gg_pt, 0, ele.total)
    CurrentBGM = Api.GetCurrentBGM()
    Api.ChangeBGM('/res/sound/static/uisound/xiuxingzhidao.assetbundles')

    local eff_t = {
        Parent = Api.UI.GetRoot(ui),
        Pos = {y = -320},
        Deg = {y = 180},
        Scale = 100,
        Layer = 17,
        DisableToUnload = true
        -- AnimatorState = 'EF_UI_XXZDC1'
    }
    local ok, effroot = Api.Task.Wait(Api.Scene.Task.LoadGameObject('/res/effect/ui/ef_ui_xxzd.assetbundles', eff_t))
    local eff1 = Api.Scene.FindChild(effroot, 'CAMC/Camera_1')
    local eff2 = Api.Scene.FindChild(effroot, 'CAMC/Camera_2')
    local cs = Api.Scene.FindChild(effroot, 'cs_1')
    local avatar_t = {
        Parent = cs,
        AnimatorState = 'c_xiuxing',
        Layer = 17
    }
    local avatarmap = GetAvatarMap(ele.artifact_stage)
    local ok, actor_obj = Api.Task.Wait(Api.Scene.Task.LoadAvatarGameObject(avatarmap, avatar_t))
    local chest_obj = Api.Scene.FindChild(actor_obj, 'Chest_Buff')
    Api.Scene.SetActive(eff1, true)
    Api.Scene.SetActive(eff2, false)
    local function GameOver()
        Api.UI.SetVisible(btn_break, false)
        Api.UI.SetVisible(gg_pt, false)
        Api.UI.SetVisible(ib_back, false)
        Api.PlayEffect('/res/effect/ui/ef_ui_xxzd_juqi.assetbundles', {Parent = cs,Layer = 17})
        Api.PlaySound('/res/sound/static/uisound/tupochenggong.assetbundles')
        Api.Task.Sleep(1.5)
        Api.PlayEffect('/res/effect/ui/ef_ui_xxzd_baiping.assetbundles', {Parent = eff2,Layer = 17})
        Api.Scene.Unload(actor_obj)
        Api.UI.SetVisible(btn_break, false)
        Api.Scene.SetActive(eff1, false)
        Api.Scene.SetActive(eff2, true)
        local next_avatarmap = GetAvatarMap(ele.artifact_stage + 1)
        Api.Task.Wait(Api.Scene.Task.LoadAvatarGameObject(next_avatarmap, avatar_t))
        Api.Task.Sleep(3)
        Api.Task.StopEvent(ID)
    end

    Api.UI.Listen.TouchClick(
        btn_break,
        function()
            Api.Task.AddEventTo(ID, GameOver)
        end
    )

    local current_value = 0
    local add_btn_eff
    local function AddValue(val)
        current_value = current_value + val
        current_value = math.min(current_value, ele.total)
        Api.UI.SetGaugeValue(gg_pt, current_value)
        Api.UI.SetEnable(btn_break, ele.total == current_value)
        if not add_btn_eff and Api.UI.IsEnable(btn_break) then
            add_btn_eff = true
            Api.PlayEffect('/res/effect/ui/ef_ui_xiuxing_breach02.assetbundles', {Parent = btn_break,Pos={x = 48, y = -38},Scale = {x=1,y=1,z=1}})
            Api.PlayEffect('/res/effect/ui/ef_ui_xiuxing_breach01.assetbundles', {Parent = gg_pt,Pos={x = 430, y = -10},Scale = {x=1.4,y=1,z=1}})
            Api.PlaySound('/res/sound/static/uisound/tupotishi.assetbundles')
        end
    end

    Api.Listen.AddPeriodicSec(
        1,
        function()
            AddValue(ele.speed)
        end
    )
    local pt_w = Api.UI.GetSize(gg_pt)
    local min_x, min_y = Api.UI.GetPosition(gg_pt)
    -- 难度系数基准值
    local base_pct = 0.5
    local function MoveTop(cvs, x, y)
        local pct = current_value / ele.total
        Api.UI.Task.MoveTo(cvs, 5 * (1 - pct + base_pct), x, y)
        Api.Task.Wait()
        Api.Task.Sleep(2)
        -- Api.UI.SetVisible(cvs, false)
        Api.UI.RemoveFromParent(cvs)
    end

    local function BreakEffect(cvs)
        local x, y = Api.UI.GetPosition(cvs)
        local w, h = Api.UI.GetSize(cvs_practicebreak)
        local ww, hh = Api.UI.GetSize(cvs)
        local eff_t = {
            Parent = cvs_practicebreak,
            Pos = {x = x + ww * 0.5, y = -y - hh * 0.5}
        }

        Api.Task.PlayEffect('/res/effect/ui/ef_ui_xiuxing_click_01.assetbundles', eff_t)
        --Api.Task.Sleep(0.5)
        Api.UI.RemoveFromParent(cvs)
        -- Api.Scene.SetWorldPosition(obj, world_pos)
        local ok, obj = Api.Task.Wait(Api.Scene.Task.LoadGameObject('/res/effect/ui/ef_ui_xiuxing_click_02.assetbundles', eff_t))
        Api.Scene.SetAsEffect(obj, 1.5)
        Api.PlaySound('/res/sound/static/uisound/feixing.assetbundles')
        local cvs_practicebreak_obj = Api.UI.GetUnityObject(cvs_practicebreak)
        local move_pos = {x = w * 0.5, y = -h * 0.5 + 100}
        local chest_worldpos = Api.Scene.GetWorldSpace(chest_obj)
        local chest_pos = Api.Scene.WorldSpaceToLoaclSpace(cvs_practicebreak_obj, chest_worldpos)
        Api.Task.Wait(Api.Scene.Task.MoveTo(obj, move_pos, 1))
    end

    local random_indexs = {}
    for i, v in ipairs(ele.show.type) do
        local add_v = ele.show.val[i]
        if add_v > 0 then
            table.insert(random_indexs, i)
        end
    end
    while not Api.UI.IsEnable(btn_break, ele.total == current_value) do
        local pct = current_value / ele.total
        local sec = Api.RandomInteger(1, 5) * (1 - pct + base_pct)
        local index = Api.RandomInteger(1, #random_indexs + 1)
        local add_v = ele.show.val[index]
        local img = ele.show.type[index]
        local cvs = Api.UI.Clone(cvs_showup)
        local x = Api.RandomInteger(min_x, min_x + pt_w)
        local y = min_y + 300
        Api.UI.SetImage(cvs, img)
        Api.UI.SetVisible(cvs, true)
        Api.UI.SetPosition(cvs, x, y)
        if Api.RandomPercent(60) then
            x = Api.RandomInteger(min_x, min_x + pt_w)
        end
        local moveeid = Api.Task.AddEvent(MoveTop, cvs, x, y - 1000)
        Api.UI.Listen.TouchClick(
            cvs,
            function()
                Api.UI.SetEnable(cvs,false)
                AddValue(add_v)
                Api.Task.StopEvent(moveeid)
                Api.Task.AddEventTo(ID, BreakEffect, cvs)
            end
        )
        Api.Task.Sleep(sec)
    end
    Api.Task.WaitAlways()
end

function clean()
    if CurrentBGM then
        Api.ChangeBGM(CurrentBGM)
    end
    Api.UI.Close(ui)
    Api.UnsubscribeGlobalBack('event.'..ID)
end
