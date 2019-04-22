--! @addtogroup Client
--! @{
local DEBUG = false
local Api = {}
Api.Task = {}
Api.Listen = {}
Api.Guide = {Task = {}, Listen = {}}
function Api.Guide.GetGuideStep(name)
    local step = DataMgr.Instance.UserData:GetFreeData(name)
    --print("step",step)
    return step
end

function Api.Guide.RemoveRepeat(ScriptDesc,id)
    local idlist = {EventApi.GetEventID(ScriptDesc)}
    for i, v in ipairs(idlist) do
        if (v ~= id) then
            EventApi.Task.StopEvent(v,false)
        end
    end
end
function Api.Guide.SetGuideStep(name,content)
    --print("GetStep"..self.name)
    DataMgr.Instance.UserData:SetFreeData(name,content)
end
function Api.Guide.WaitMenuIsOpenAndGuide(text,isshow)
    local ui = EventApi.UI.FindHud('xml/hud/ui_hud_other.gui.xml')
    local tbt_an1 = EventApi.UI.FindChild(ui, 'tbt_an1',true)
    local isChecked = EventApi.UI.IsChecked(tbt_an1)

    local needopen
    if isshow == nil then
        needopen = true
    else
        needopen = isshow
    end
    --print("isChecked",isChecked,needopen)
    if isChecked ~= needopen then
       local id = EventApi.Guide.Listen.Touch(tbt_an1, {text = text or '', y = 0, right = true, force = true,reverse = 1})
       EventApi.Task.Wait(id)
    end
end

function Api.Guide.Listen.IsNoneOpenFun(fn)
    local eid
    local function main()
        EventApi.Task.Sleep(0.3)
        local id = EventApi.Listen.AddPeriodicSec(
                0,
                function()
                    if DEBUG then
                        print("IsNoneOpenFun")
                    end
                    if not EventApi.UI.FindByTag('FuncOpen') then
                        EventApi.InvokeListenCallBack(eid,fn)
                    end
                end
        )
        EventApi.Task.Wait(id)
    end
    eid = EventApi.Task.AddEvent({main = main})
    EventApi.Task.Wait(eid)
end
function Api.Guide.WaitTopMenuIsOpenAndGuide(text,isshow)
    local ui = EventApi.UI.FindHud('xml/hud/ui_hud_other.gui.xml')
    local tbt_an2 = EventApi.UI.FindChild(ui, 'tbt_an2',true)
    local isChecked = EventApi.UI.IsChecked(tbt_an2)

    local needopen
    if isshow == nil then
        needopen = true
    else
        needopen = isshow
    end
    --print("isChecked",isChecked,needopen)
    if isChecked ~= needopen then
        local id = EventApi.Guide.Listen.Touch(tbt_an2, {text = text or '', y = 0, right = true, force = true,reverse = 1})
        EventApi.Task.Wait(id)
    end
end

function Api.Guide.Listen.FindUIByTag(uitag,time,fn)
    time = time or 10
    local eid
    local function main()
        local id =
            EventApi.Listen.AddPeriodicSec(
            0,
            function()
                if DEBUG then
                    print("FindUIByTag")
                end
                local AdvancedTips = EventApi.UI.FindByTag(uitag)
                if AdvancedTips ~= nil then
                    EventApi.InvokeListenCallBack(eid, fn)
                end
            end
        )
        EventApi.Task.Wait(id)
    end
    eid = EventApi.Task.AddEvent({main = main})
    local id2 = EventApi.Task.DelaySec(time)
    local success,selectid = EventApi.Task.WaitSelect(eid,id2)
    if selectid == id2 then
        local rootid = EventApi.GetRootEventID(eid)
        EventApi.Task.StopEvent(rootid,false)
    end
end

function Api.Guide.Listen.IsChecked(uitag,fn)
     local eid
     local function main()
            local id = EventApi.Listen.AddPeriodicSec(
                0,
                function()
                    if DEBUG then
                        print("IsChecked")
                    end
                    if EventApi.UI.IsChecked(uitag) then
                       EventApi.InvokeListenCallBack(eid,fn)
                    end
                end
            )
            EventApi.Task.Wait(id)
    end
    eid = EventApi.Task.AddEvent({main = main})
    EventApi.Task.Wait(eid)
end


--代码一定会产生的ui需要监听 慎用
function Api.Guide.Listen.FindUIChild(key,childname,fn,time)
    time = time or 10
    local eid
    local function main()
        local id =
        EventApi.Listen.AddPeriodicSec(
                0,
                function()
                    if DEBUG then
                        print("FindUIChild")
                    end
                    local node = EventApi.UI.FindChild(key,childname)
                    if node ~= nil then
                        if fn then
                            fn(node)
                        end
                        EventApi.InvokeListenCallBack(eid)
                    end
                end)
        EventApi.Task.Wait(id)
    end
    eid = EventApi.Task.AddEvent({main = main})
    local id2 = EventApi.Task.DelaySec(time)
    local success,selectid = EventApi.Task.WaitSelect(eid,id2)
    if selectid == id2 then
        local rootid = EventApi.GetRootEventID(eid)
        EventApi.Task.StopEvent(rootid,false)
    end
end

--代码一定会产生的ui需要监听 慎用
function Api.Guide.Listen.GetScrollListCell(node,slot,fn,time)
    time = time or 10
    local eid
    local function main()
        local id =
        EventApi.Listen.AddPeriodicSec(
                0,
                function()
                    if DEBUG then
                        print("GetScrollListCell")
                    end
                    local cell = EventApi.UI.GetScrollListCell(node, slot)
                    if cell ~= nil and EventApi.UI.IsActiveInHierarchy(cell) then
                        if fn then
                            fn(cell)
                        end
                        EventApi.InvokeListenCallBack(eid)
                    end
                end)
        EventApi.Task.Wait(id)
    end
    eid = EventApi.Task.AddEvent({main = main})
    local id2 = EventApi.Task.DelaySec(time)
    local success,selectid = EventApi.Task.WaitSelect(eid,id2)
    if selectid == id2 then
        local rootid = EventApi.GetRootEventID(eid)
        EventApi.Task.StopEvent(rootid,false)
    end
end


function Api.Guide.Listen.Touch(target, params)
    --print("target",target)
    params = params or {}
    if params.clean == nil then
        params.clean = true
    end

    params.alpha = params.alpha or 0.6
    local time = params.time or 1000
    local ui = params.ui
    local isCreater = false
    local target_trans
    local target_ui
    local cvs_guide, guide_w, guide_parent_trans,ib_tuanzi
    local w1, h1
    local id
    local effectid = 0
    local OnlyText = params.OnlyText
    local showw, showh
    if EventApi.UI.IsUIObject(target) then
        target_ui = target
        target_trans = EventApi.UI.GetUnityObject(target)
    else
        target_trans = target
    end
    local pivot = EventApi.Scene.GetPivot(target_trans)
    -- 全局偏移
    local offtext_x, offtext_y = -10, -30
    local space_offset = 15
    offtext_x = (params.x or 0) + offtext_x
    offtext_y = (params.y or 0) + offtext_y
    local s = EventApi.Scene.GetRectSize(target_trans)
    local function SyncPos()
        if not EventApi.Scene.ExistsGameObject(target_trans) then
            return false
        end
        local wp = EventApi.Scene.GetWorldSpace(target_trans)
        local lp = EventApi.Scene.WorldSpaceToLoaclSpace(guide_parent_trans, wp)
        lp = EventApi.Scene.ToMFUIPosition(lp)
        local x = lp.x - pivot.x * s.x
        local y = lp.y - (1 - pivot.y) * s.y
        if params.text then
            local frame_x, frame_y = x, y
            if frame_x > HZUISystem.SCREEN_WIDTH*0.5 then
                -- left
                frame_x = frame_x - guide_w - space_offset + offtext_x
            else
                frame_x = frame_x + s.x + offtext_x + space_offset
            end
            if DEBUG then
                print("target_trans position",frame_x,frame_y)
            end
            frame_y = frame_y + offtext_y
            frame_y = frame_y > 0 and frame_y or 0
            frame_y = frame_y < HZUISystem.SCREEN_HEIGHT - h1 -5 and frame_y or HZUISystem.SCREEN_HEIGHT - h1 -5
            frame_x = frame_x > 0 and frame_x or 0
            EventApi.UI.SetPosition(cvs_guide, frame_x, frame_y)
            EventApi.UI.SetPositionZ(cvs_guide, -500)
        end

        if effectid and effectid ~= 0 then
            local pos = Vector3(x +showw*0.5, -y-showh*0.5,-1500)
            EventApi.SetEffectPos(effectid, pos)
        end
    end

    local function SyncValid()
        if not EventApi.Scene.ExistsGameObject(target_trans) then
            return false
        end
        -- if DEBUG then
        --     print("SyncValid")
        -- end
        --EventApi.UI.SetVisible(cvs_guide, EventApi.Scene.IsActiveInHierarchy(target_trans))
		--print("EventApi.Scene.CheckRaycast(target_trans)",EventApi.Scene.CheckRaycast(target_trans))
        local show = true
        if not EventApi.Scene.CheckRaycast(target_trans) or not EventApi.Scene.IsActiveInHierarchy(target_trans) then
            show = false
            EventApi.Task.StopEvent(id,true)
        end
        --EventApi.UI.SetRootVisible(ui,show)

    end
    local function GuideMain()
            local showarrow = params.showarrow
            if params.force then
                EventApi.EnterBlockTouch(target_trans,params.alpha)
            end
            if params.sound then
                EventApi.PlaySound(params.sound)
            end
            local offsetx,offsety
            if not ui then
                ui = EventApi.UI.Open('xml/common/common_guide.gui.xml', UIShowType.Cover, {Layer = 'Drama'})
                EventApi.UI.SetFrameEnable(ui, false)
                isCreater = true
                local tb_guide = EventApi.UI.FindChild(ui, 'tb_guide')
                offsetx,offsety = EventApi.UI.GetPosition(tb_guide)
            end
            cvs_guide = EventApi.UI.FindChild(ui, 'cvs_guide')
            ib_tuanzi = EventApi.UI.FindChild(ui, 'ib_tuanzi')
            local cvs_guide_parent = EventApi.UI.GetParent(cvs_guide)
            guide_parent_trans = EventApi.UI.GetUnityObject(cvs_guide_parent)
            --local girl = EventApi.UI.FindChild(ui, 'girl')
            local tb_guide = EventApi.UI.FindChild(ui, 'tb_guide')
            local cvs_tips = EventApi.UI.FindChild(ui, 'cvs_tips')
            --EventApi.UI.SetVisible(girl, true)
            --EventApi.UI.SetVisible(tb_guide, true)
        
            local tb_guide = EventApi.UI.FindChild(ui, 'tb_guide')
            -- local ib_left = EventApi.UI.FindChild(ui, 'ib_left')
            -- local ib_right = EventApi.UI.FindChild(ui, 'ib_right')
            -- local ib_top = EventApi.UI.FindChild(ui, 'ib_top')
            -- local ib_bottom = EventApi.UI.FindChild(ui, 'ib_bottom')
            local x1, y1 = EventApi.UI.GetPosition(cvs_guide)
            w1, h1 = EventApi.UI.GetSize(cvs_guide)
            local w2, h2 = EventApi.UI.GetSize(ib_tuanzi)
            local w3, h3 = EventApi.UI.GetSize(cvs_tips)
            local w4, h4 = EventApi.UI.GetSize(tb_guide)
            local ib_show
            guide_w = w1
            if params.left then
            --     ib_show = ib_left
                EventApi.UI.SetPosition(ib_tuanzi, w1 - w2, h1 - h2)
                EventApi.UI.SetPosition(cvs_tips,  0, h1- h3)
                EventApi.UI.SetPosition(tb_guide,  offsetx, offsety)
                EventApi.UI.SetRotation(ib_tuanzi, 0,0,0)
                EventApi.UI.SetTextAnchor(tb_guide, CommonUI.TextAnchor.L_C)
            elseif params.right then
            --     ib_show = ib_right
            -- elseif params.bottom then
            --     ib_show = ib_bottom
            -- elseif params.top then
            --     ib_show = ib_top
                EventApi.UI.SetRotation(ib_tuanzi, 0,180,0)
                EventApi.UI.SetPosition(ib_tuanzi, w2, h1- h2)
                EventApi.UI.SetPosition(cvs_guide,  w1 - w3, h1 - h3)
                EventApi.UI.SetPosition(cvs_tips,  w1, h1 - h3)
                EventApi.UI.SetRotation(cvs_tips, 0,180,0)
                EventApi.UI.SetPosition(tb_guide,  w1 - w4 - offsetx, offsety)
                EventApi.UI.SetTextAnchor(tb_guide, CommonUI.TextAnchor.L_C)
            end
            SyncPos()
        
            --文本框
            
            if not string.IsNullOrEmpty(params.text) then
                EventApi.UI.SetXmlText(tb_guide, EventApi.GetText(params.text))
                EventApi.UI.SetVisible(cvs_guide, true)
                
            else
                -- EventApi.UI.SetVisible(girl, false)
                EventApi.UI.SetVisible(cvs_guide, false)
            end
            
            
            --EventApi.Listen.AddPeriodicSec(0.3, SyncValid)
            --if not EventApi.Scene.IsActiveInHierarchy(target_trans) or not EventApi.Scene.CheckRaycast(target_trans) then
            --    --print("IsActiveInHierarchy")
            --    return true
            --end
            EventApi.Listen.AddPeriodicSec(0.5, SyncValid)

        --EventApi.Task.PlayEffect('/res/effect/ui/ef_ui_event_elite.assetbundles', {UILayer=true,Pos={x = -30, y = 0, z = -333}})

        showw, showh = EventApi.BlockSize(target_trans)
        local wp = EventApi.Scene.GetWorldSpace(target_trans)
        local lp = EventApi.Scene.WorldSpaceToLoaclSpace(guide_parent_trans, wp)
        lp = EventApi.Scene.ToMFUIPosition(lp)
        local x = lp.x - pivot.x * s.x
        local y = lp.y - (1 - pivot.y) * s.y
        --print("showw",x,y)
        local rockertype = params.type or 3
        local reverse = params.reverse or 0
        print("OnlyText",OnlyText,rockertype)
        if rockertype and  not OnlyText then
           
            effectid = EventApi.PlayEffect('/res/effect/ui/ef_ui_rocker0'..rockertype..'.assetbundles',
            {Parent = cvs_guide_parent,
             Pos = {x = x +showw*0.5, y = -y-showh*0.5, z = -1500},
             Deg = {x = 0,y = reverse == 1 and 180 or 0},
             },999999)
        end
        EventApi.UI.SetRootVisible(ui,true)
        EventApi.Listen.AddPeriodicSec(0, SyncPos)
        local eid = EventApi.Scene.Listen.TouchClick(target_trans,params.fn)
        EventApi.Task.Wait(eid)
    end

    local function main()
        EventApi.Task.WaitActorReady()
        id = EventApi.Task.AddEvent(GuideMain)
        local id2 = EventApi.Task.DelaySec(time)
        local id3 = EventApi.Listen.CGSTATE()
        local success,selectid = EventApi.Task.WaitSelect(id,id2,id3)
        if selectid == id2 or selectid == id3 then
            --print("selectid",selectid,id3)
            local rootid = EventApi.GetRootEventID(id)
            if not OnlyText then
                EventApi.Task.StopEvent(rootid,false)
            else
                EventApi.Task.StopEvent(rootid,true)
            end
        end
    end
    local function clean()
        if params.clean then
            if isCreater then
                EventApi.UI.Close(ui)
            end
            EventApi.ExitBlockTouch()
        end
    end

    local event = {main = main, clean = clean}
    return EventApi.Task.AddEvent(event)
end

return Api
--! @}
