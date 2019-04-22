local _M = {}
_M.__index = _M

local GuildModel = require 'Model/GuildModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local TimeUtil = require 'Logic/TimeUtil'
local QuestUtil = require("UI/Quest/QuestUtil")

local function RefreshBuilding( self, buildList )
    self.ui.comps.lb_lv.Text = Util.GetText('common_level2', buildList[1])
    self.ui.comps.lb_majiu.Text = Util.GetText('common_level2', buildList[2])
    self.ui.comps.lb_shenshou.Text = Util.GetText('common_level2', buildList[3])
    self.ui.comps.lb_xueyuan.Text = Util.GetText('common_level2', buildList[4])
    self.ui.comps.lb_zahuopu.Text = Util.GetText('common_level2', buildList[5])
end

local function InitInfo( self, data, position, giftCount )
    self.data = data
    RefreshBuilding(self, data.buildList)
    self.ui.comps.lb_guilename.Text = data.guildBase.name
    self.ui.comps.lb_paiming.Text = data.rank == -1 and Util.GetText('rank_outrank') or (data.rank + 1)
    self.ui.comps.lb_fight.Text = data.guildBase.fightPower
    self.ui.comps.lb_cbr.Text = data.guildBase.presidentName
    self.ui.comps.lb_num.Text = String.Format('{0}/{1}', data.guildBase.memberNum, data.guildBase.maxMemberNum)
    self.ui.comps.lb_money.Text = data.fund
    self.ui.comps.lb_weihu.Text = Util.GetText('guild_maintenance', data.maintenance)
    self.ui.comps.lb_contribution.Text = tostring(data.contribution)
    self.ui.comps.lb_contributionall.Text = tostring(data.contributionMax)
    self.ui.comps.lb_liwu.Text = Util.GetText('common_level2', data.giftLv)
    if data.guildBase.fort ~= 0 then
        local dbfort = GlobalHooks.DB.FindFirst('guild_fort', { id = data.guildBase.fort })
        self.ui.comps.lb_station.Text = Util.GetText(dbfort.fort_name)
    else
        self.ui.comps.lb_station.Text = Util.GetText('common_none')
    end

    local positiondb = GlobalHooks.DB.FindFirst('guild_position', { position_id = position })
    self.ui.comps.ti_shuru.Input.Text = data.noticeBoard
    self.ui.comps.ti_shuru.Enable = positiondb.notice == 1
    self.ui.comps.ib_xieru.Visible = positiondb.notice == 1

    table.sort(data.logList, function( a, b )
        return TimeUtil.TimeLeftSec(a.time) < TimeUtil.TimeLeftSec(b.time)
    end)
    local log = ''
    -- print_r(data.logList)
    for i = 1, #data.logList do
        local timeStr
        local sec = TimeUtil.TimeLeftSec(data.logList[i].time)
        if sec < 60 then
            timeStr = Util.GetText('common_now')
        else
            timeStr = Util.GetText('common_ago', TimeUtil.FormatToCN(sec))
        end
        local content = data.logList[i].content
        for k=1,#data.logList[i].args do
            local v = data.logList[i].args[k]
            data.logList[i].args[k] = Util.GetText(v)
        end
        content = Util.GetText(content, unpack(data.logList[i].args))
        log = log..timeStr..'\n'..content..'\n\n'
    end

    local logComp = self.ui.comps.tb_log
    logComp.Text = log
    logComp.Height = logComp.TextComponent.PreferredSize.y

    GlobalHooks.UI.SetRedTips('guild_gift', giftCount)
end

local function RequestGuildInfo( self )
    GuildModel.ClientGetGuildInfoRequest(function( rsp )
        InitInfo(self, rsp.s2c_guildInfo, rsp.s2c_position, rsp.s2c_giftCount)
    end)
end

function _M.OnEnter( self, param )
    if param ~= nil then
        InitInfo(self, param.data, param.position, 0)
    else
        RequestGuildInfo(self)
    end
end

function _M.OnExit( self )
    
end

function _M.OnDestory( self )
    
end

function _M.OnInit( self )
    self.ui.comps.ti_shuru.Input.characterLimit = GlobalHooks.DB.GetGlobalConfig('guild_noticelimit')
    self.ui.comps.ti_shuru.Input.lineType = UnityEngine.UI.InputField.LineType.MultiLineSubmit
    self.ui.comps.ti_shuru.event_endEdit = function( send, text )
        if self.data.noticeBoard ~= text then
            GuildModel.ClientChangeNoticeRequest(text, function( rsp )
                if rsp:IsSuccess() then
                    self.ui.comps.ti_shuru.Input.Text = rsp.s2c_context
                    self.data.noticeBoard = rsp.s2c_context
                    GameAlertManager.Instance:ShowNotify(Util.GetText('common_setover'))
                else
                    self.ui.comps.ti_shuru.Input.Text = self.data.noticeBoard
                end
            end)
        end
    end

    self.ui.comps.btn_an1.TouchClick = function( sender )
        GlobalHooks.UI.OpenUI("GuildDonate", 0, { cb = function( data )
            RequestGuildInfo(self)
        end})
    end

    self.ui.comps.btn_an2.TouchClick = function( sender )
        GlobalHooks.UI.OpenUI("GuildBuild", 0, {buildList = self.data.buildList, fund = self.data.fund, cb = function( data )
            if data then
                RequestGuildInfo(self)
            end
        end})
    end

    self.ui.comps.btn_an3.TouchClick = function( sender )
        GlobalHooks.UI.OpenUI("GuildGift", 0, {cb = function( data )
            self.ui.comps.lb_liwu.Text = Util.GetText('common_level2', data.giftLv)
        end})
    end

    self.ui.comps.btn_an4.TouchClick = function( sender )
        local db = GlobalHooks.DB.FindFirst('guild', { id == 1})
        QuestUtil.tryOpenFunction(db.funtion_tag)
        MenuMgr.Instance:CloseAllMenu()
    end
end

return _M