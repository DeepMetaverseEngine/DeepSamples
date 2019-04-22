local Api = EventApi
local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local ItemModel = require 'Model/ItemModel'
local function FixEffectRes(filename)
    return '/res/effect/ui/' .. filename .. '.assetbundles'
end

local function TestEvent()
    -- Api.PlayEffect(FixEffectRes('EF_UI_Interface_GamePokey_resolve'),{UILayer = true})
    -- Api.PlayPlayerEffect(Api.GetActorUUID(), {BindBody = true, Name = '/res/effect/ef_buff_kill.assetbundles'})
    -- Api.Task.AddEventByKey('client_excel_cost.20003')
    -- Api.Task.Wait()
    -- Api.Task.BlockActorAutoRun()

    local ui = Api.UI.FindByTag('RoleBag')
    if ui then
        local btn_close = Api.UI.FindChild(ui, 'btn_close')
        print(Api.UI.CheckRaycast(btn_close))
    -- Api.UI.DoPointerClick(btn_close)
    end
    local avatarMap = Api.GetActorAvartarMap()
    local x, y = Api.GetActorPostion()
    local pos = Api.GetUnityPosistion(x, y)

    local id = Util.LoadGameUnit(avatarMap, {Pos = pos, Deg = {y = 180}})
    -- local id = Api.Scene.Task.LoadAvatarGameObject(avatarMap, {Pos = pos})
    -- local ok, resid = Api.Task.Wait(id)
    Api.Task.Sleep(1)
    -- RenderSystem.Instance:Unload(id)
    -- Api.Scene.Unload(resid)
    -- Api.Scene.PlayAnimation(id,'m_idle01')
end

local test_avatarmap = {
    [Constants.AvatarPart.Avatar_Body] = {
        'player_242001',
        'player_242002',
        'player_242003',
        'player_221004',
        'player_221005',
        'player_221006'
        -- '/res/effect/ef_buff_trailing_02.assetbundles',
    },
    [Constants.AvatarPart.Avatar_Head] = {'player_142001', 'player_142002', 'player_142002'},
    [Constants.AvatarPart.R_Hand_Weapon] = {'weapon_qq_002', 'weapon_qq_003', 'weapon_qq_004', 'weapon_qq_005'},
    [Constants.AvatarPart.Rear_Equipment] = {'wing_101', 'wing_102', 'wing_103', 'wing_104', 'wing_105'},
    [Constants.AvatarPart.Foot_Buff] = {'/res/effect/ef_buff_trailing_02.assetbundles', '/res/effect/ef_buff_trailing_01.assetbundles'},
    [Constants.AvatarPart.Ride_Avatar01] = {'mount_zheng01', 'mount_zheng02', 'mount_zheng03', 'mount_qiongqi01', 'mount_qiongqi02', 'mount_qiongqi03'}
}

local function RandomAvatarMap(reload)
    local new_map = {}
    for k, v in pairs(Constants.AvatarPart) do
        local exist = true
        if v ~= Constants.AvatarPart.Avatar_Body and v ~= Constants.AvatarPart.Avatar_Head then
            if math.random(0, 100) < 40 then
                exist = false
            end
        end
        if exist and test_avatarmap[v] then
            new_map[v] = test_avatarmap[v][math.random(#test_avatarmap[v])]
        end
    end
    if not reload then
        for k, v in pairs(new_map) do
            if math.random(0, 100) < 40 then
                local ret = {}
                ret[k] = v
                return ret
            end
        end
    end
    return new_map
end
local function TestUI()
    local RedForce = 2
    local BlueForce = 3
    local function FillPlayerResult(node, index, data)
        local lb_rank = Api.UI.FindChild(node, 'lb_rank')
        local lb_point = Api.UI.FindChild(node, 'lb_point')
        local lb_name = Api.UI.FindChild(node, 'lb_name')
        local lb_kill = Api.UI.FindChild(node, 'lb_kill')
        local lb_result = Api.UI.FindChild(node, 'lb_result')
        local ib_blueback = Api.UI.FindChild(node, 'ib_blueback')
        local ib_redback = Api.UI.FindChild(node, 'ib_redback')
        local ib_rank1 = Api.UI.FindChild(node, 'ib_rank1')
        local ib_rank2 = Api.UI.FindChild(node, 'ib_rank2')
        local ib_rank3 = Api.UI.FindChild(node, 'ib_rank3')

        
        Api.UI.SetVisible(ib_rank1, index == 1)
        Api.UI.SetVisible(ib_rank2, index == 2)
        Api.UI.SetVisible(ib_rank3, index == 3)
        Api.UI.SetVisible(ib_blueback, data.force == BlueForce)
        Api.UI.SetVisible(ib_redback, data.force == RedForce)

        -- Api.UI.SetText(lb_rank, tostring(index))
        -- Api.UI.SetText(lb_point, tostring(data.score))
        -- Api.UI.SetText(lb_name, tostring(data.name))
        -- Api.UI.SetText(lb_kill, tostring(data.killed))
        -- local txt_exploit = Api.GetText(Constants.Text.pvp_exploit, data.exploit)
        -- Api.UI.SetText(lb_result, txt_exploit)
    end

    local gameover = {
        players = {
            {force = BlueForce, name = 'xxxxx'},
            {force = RedForce, name = 'xxxxx'},
            {force = BlueForce, name = 'xxxxx'},
            {force = RedForce, name = 'xxxxx'},
            {force = RedForce, name = 'xxxxx'},
            {force = BlueForce, name = 'xxxxx'},
            {force = BlueForce, name = 'xxxxx'},
        }
    }
    --结算界面
    local ui_result = Api.UI.Open('xml/battleground/ui_battleground_account.gui.xml', {Layer = 'MessageBox'})
    local sp_rank = Api.UI.FindChild(ui_result, 'sp_rank')
    local cvs_rank = Api.UI.FindChild(ui_result, 'cvs_rank')
    local cvs_own = Api.UI.FindChild(ui_result, 'cvs_own')
    local lb_time = Api.UI.FindChild(ui_result, 'lb_time')
    local btn_start1 = Api.UI.FindChild(ui_result, 'btn_start1')
    local lb_redresource = Api.UI.FindChild(ui_result, 'lb_redresource')
    local lb_blueresource = Api.UI.FindChild(ui_result, 'lb_blueresource')
    local lb_num = Api.UI.FindChild(ui_result, 'lb_num')
    local lb_title = Api.UI.FindChild(ui_result, 'lb_title')
    local lb_over = Api.UI.FindChild(ui_result, 'lb_over')
    Api.UI.SetScrollList(
        sp_rank,
        cvs_rank,
        #gameover.players,
        function(node, index)
            FillPlayerResult(node, index, gameover.players[index])
        end
    )
end
function main()
    local test = Api.FindExcelData('reward/common_reward.xlsx/common_reward',1)
    pprint(test)
    MenuMgr.Instance:CloseAllMenu()
    MenuMgr.Instance:CloseAllMsgBox()
        Api.PlayEffect('/res/effect/ui/EF_UI_ChunJie01.assetbundles', {UILayer=true,Pos={x = -30, y = 0, z = -333}},5)
        Api.PlayEffect('/res/effect/ui/EF_UI_ChunJie01.assetbundles', {UILayer=true,Pos={x = -30, y = 0, z = -333}},5)
    -- Api.Task.StartEvent(TestUI)
    -- Api.Task.StartEvent(function()
    --     local ok,ret = Api.Task.Wait(Api.Task.HttpGet('http://127.0.0.1:8080/item.lua'))
    --     pprint(ok, ret)
    -- end)
    -- GlobalHooks.UI.OpenUI('GuildCarriage',0)

    -- Api.Task.StartEvent(function()
    --     local d1 = ItemModel.ListenByTemplateID(1,function() print('1111') end)
    --     local d2 = ItemModel.ListenByTemplateID(2,function()  print('2222')  end)
    --     local d3 = ItemModel.ListenByTemplateID(3,function()  print('3333')  end)
    --     Api.Task.Sleep(60)
    -- end)
    -- Api.SetAllDirty()
    -- Api.FollowSelectUnit()
    -- local ui = GlobalHooks.UI.FindUI('SmithyCompose')
    -- print(ui.treeMenu)
    -- ui.treeMenu:SetChildrenEnable(false)
    -- GlobalHooks.UI.OpenUI('Test',0)
    -- GlobalHooks.UI.OpenUI('BattleGround',0)
    -- ItemModel.RequestDetailByID('5cd2f2e9-0c34-4e1d-bdd2-4c73e43f5601',function(detail)
    --     print_r(detail)
    --     UIUtil.ShowNormalItemDetail({detail = detail})
    -- end)
    -- Api.SetActorAutoGuard(not Api.IsActorAutoGuard())
    -- ID = Api.Task.StartEvent(TestEvent)
    -- EventApi.Task.StartEvent(Api.Task.BlockActorAutoRun,10)
    -- local ele = Api.GetExcelByEventKey('reward.1')
    -- TLBattleScene.Instance.Actor:TestLoadAvatar(true,RandomAvatarMap(true))
    -- do return end
    -- TLBattleScene.Instance.Actor:TestLoadAvatar(true,RandomAvatarMap(true))
    -- TLBattleScene.Instance.Actor:TestLoadAvatar(false,RandomAvatarMap())
    -- for i = 1, 300 do
    --     TLBattleScene.Instance.Actor:TestLoadAvatar(false,RandomAvatarMap())
    -- end
    -- Api.PlayBGM('/res/sound/dynamic/bgm/junjishan.assetbundles')
    -- Api.PlayBGM('/res/sound/dynamic/bgm/zhongrongjiaowai.assetbundles')
end
