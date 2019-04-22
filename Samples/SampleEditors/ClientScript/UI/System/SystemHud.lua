local _M = {}
_M.__index = _M
local Util = require 'Logic/Util'
local SystemModel=require('Model/SystemModel')

local lb_peoples = nil
local lb_music = nil
local lb_sound = nil

local function PeoplesValueChanged(sender, value)
    lb_peoples.Text = Util.GetText("guild_carriage_pcount",math.modf(value))
    DataMgr.Instance.SettingData:ChangerData('personcount', value)
end

local function MusicValueChanged(sender, value)
    lb_music.Text = math.modf(value) .. '%'
    SoundManager.Instance.DefaultBGMVolume = value * 0.01
    if value ~= 0 then
        DataMgr.Instance.SettingData:ChangerData('music', value)
    end
end

local function SoundValueChanged(sender, value)
    lb_sound.Text = math.modf(value) .. '%'
    SoundManager.Instance.DefaultSoundVolume = value * 0.01
    if value ~= 0 then
        DataMgr.Instance.SettingData:ChangerData('audio', value)
    end
end

local function DelegateClean(self)
    self.gg_peoples.event_ValueChanged = {'-=', PeoplesValueChanged}
    self.gg_music.event_ValueChanged = {'-=', MusicValueChanged}
    self.gg_sound.event_ValueChanged = {'-=', SoundValueChanged}
end

function _M.Notify(status, settingdata, opt)
    -- if settingdata:ContainsKey(status, SettingData.NotifySettingState.ALL) then
    -- 	local value = DataMgr.Instance.SettingData:GetAttribute(status)--SettingData.NotifySettingState.ALL)
    -- 	print_r(status,'------',value)
    -- end
end

local function ToggleBtnSet(self,...)
    local btnlist = {...}
    for i, v in ipairs(btnlist) do
        v.TouchClick = function(sender)
            for i, v in ipairs(btnlist) do
                v.IsChecked = false
            end
            sender.IsChecked = true
            DataMgr.Instance.SettingData:ChangerData('quality', i)
            if i==3 then
                self.tbt_bloom.IsChecked = true
                DataMgr.Instance.SettingData:ChangerData('bloom',1)
            else
                self.tbt_bloom.IsChecked = false
                DataMgr.Instance.SettingData:ChangerData('bloom',0)
            end
        end
    end
end

local function SetGG(self, ggnode, dragnode, min, max, value)
    ggnode:SetGaugeMinMax(min, max)
    ggnode.Value = value
    ggnode:SetPullNode(dragnode)
end

local function SetQuality(self)
    ToggleBtnSet(self,self.tbt_low, self.tbt_middle, self.tbt_high)
end

local function SetUseOK(bool, node1, node2)
    node1.Enable = bool
    node1.IsGray = not bool
    node2.Enable = bool
    node2.IsGray = not bool
end


local function SetOther(self)
    SetGG(
        self,
        self.gg_peoples,
        self.ib_ppcheck,
        0,
        50,
        DataMgr.Instance.SettingData:GetAttribute(SettingData.NotifySettingState.PERSONCOUNT)
    )
    self.gg_peoples.event_ValueChanged = {'+=', PeoplesValueChanged}

    self.tbt_vague.TouchClick = function(sender)
        if sender.IsChecked then
            DataMgr.Instance.SettingData:ChangerData('isfoggy', 1)
        else
            DataMgr.Instance.SettingData:ChangerData('isfoggy', 0)
        end
    end

    self.tbt_bloom.TouchClick=function(sender)
        if sender.IsChecked then
            DataMgr.Instance.SettingData:ChangerData('bloom', 1)
        else
            DataMgr.Instance.SettingData:ChangerData('bloom', 0)
        end
    end
    
    --省电按钮，通知model开始计时或停止计时
    self.tbt_poweroff.TouchClick = function(sender)
        if sender.IsChecked then
           DataMgr.Instance.SettingData:ChangerData('ispowersaving', 1)
            SystemModel.Timing()
        else
            DataMgr.Instance.SettingData:ChangerData('ispowersaving', 0)
            SystemModel.CancelTiming()
        end
    end
end

local function SetMusic(self)
    SetGG(
        self,
        self.gg_music,
        self.ib_mcheck,
        0,
        100,
        DataMgr.Instance.SettingData:GetAttribute(SettingData.NotifySettingState.MUSIC)
    )
    self.gg_music.event_ValueChanged = {'+=', MusicValueChanged}
    SetGG(
        self,
        self.gg_sound,
        self.ib_scheck,
        0,
        100,
        DataMgr.Instance.SettingData:GetAttribute(SettingData.NotifySettingState.AUDIO)
    )
    self.gg_sound.event_ValueChanged = {'+=', SoundValueChanged}

    if self.tbt_music.IsChecked then
        SetUseOK(true, self.gg_music, self.ib_mcheck)
        DataMgr.Instance.SettingData:ChangerData('ismusic', 1)
    else
        self.gg_music.Value = 0
        SetUseOK(false, self.gg_music, self.ib_mcheck)
        DataMgr.Instance.SettingData:ChangerData('ismusic', 0)
    end

    if self.tbt_sound.IsChecked then
        SetUseOK(true, self.gg_sound, self.ib_scheck)
        DataMgr.Instance.SettingData:ChangerData('isaudio', 1)
    else
        self.gg_sound.Value = 0
        SetUseOK(false, self.gg_sound, self.ib_scheck)
        DataMgr.Instance.SettingData:ChangerData('isaudio', 0)
    end

    self.tbt_music.TouchClick = function(sender)
        if sender.IsChecked then
            SetUseOK(true, self.gg_music, self.ib_mcheck)
            self.gg_music.Value = DataMgr.Instance.SettingData:GetAttribute(SettingData.NotifySettingState.MUSIC)
            DataMgr.Instance.SettingData:ChangerData('ismusic', 1)
        else
            SetUseOK(false, self.gg_music, self.ib_mcheck)
            lb_music.Text = '0%'
            self.gg_music.Value = 0
            DataMgr.Instance.SettingData:ChangerData('ismusic', 0)
        end
    end

    self.tbt_sound.TouchClick = function(sender)
        if sender.IsChecked then
            SetUseOK(true, self.gg_sound, self.ib_scheck)
            self.gg_sound.Value = DataMgr.Instance.SettingData:GetAttribute(SettingData.NotifySettingState.AUDIO)
            DataMgr.Instance.SettingData:ChangerData('isaudio', 1)
        else
            SetUseOK(false, self.gg_sound, self.ib_scheck)
            lb_sound.Text = '0%'
            self.gg_sound.Value = 0
            DataMgr.Instance.SettingData:ChangerData('isaudio', 0)
        end
    end
end

local function SetTbt(tbtnode, nodifystate)
    if DataMgr.Instance.SettingData:GetAttribute(nodifystate) == 1 then
        tbtnode.IsChecked = true
    else
        tbtnode.IsChecked = false
    end
end

local function InitConfig(self)
    --ui层控制显示，实际在进入游戏时在C#修改值，保证MV数据相同，同时避免需要打开ui才能生效的问题（下方人数同理）
    local qualityvalue = DataMgr.Instance.SettingData:GetAttribute(SettingData.NotifySettingState.QUALITY)
    if qualityvalue == 1 then
        self.tbt_low.IsChecked = true
        self.tbt_middle.IsChecked = false
        self.tbt_high.IsChecked = false
    elseif qualityvalue == 2 then
        self.tbt_low.IsChecked = false
        self.tbt_middle.IsChecked = true
        self.tbt_high.IsChecked = false
    elseif qualityvalue == 3 then
        self.tbt_low.IsChecked = false
        self.tbt_middle.IsChecked = false
        self.tbt_high.IsChecked = true
    end
    
    SetTbt(self.tbt_vague, SettingData.NotifySettingState.ISFOGGY)
    SetTbt(self.tbt_poweroff, SettingData.NotifySettingState.ISPOWERSAVING)
    SetTbt(self.tbt_music, SettingData.NotifySettingState.ISMUSIC)
    SetTbt(self.tbt_sound, SettingData.NotifySettingState.ISAUDIO)
    SetTbt(self.tbt_bloom,SettingData.NotifySettingState.BLOOM)

    self.gg_peoples.Value = DataMgr.Instance.SettingData:GetAttribute(SettingData.NotifySettingState.PERSONCOUNT)
    lb_peoples.Text = Util.GetText("guild_carriage_pcount",DataMgr.Instance.SettingData:GetAttribute(SettingData.NotifySettingState.PERSONCOUNT))
    self.gg_music.Value = DataMgr.Instance.SettingData:GetAttribute(SettingData.NotifySettingState.MUSIC)
    self.gg_sound.Value = DataMgr.Instance.SettingData:GetAttribute(SettingData.NotifySettingState.AUDIO)
end

function _M.OnEnter(self, params)
    DataMgr.Instance.SettingData:AttachLuaObserver('SystemHud', self)
    DelegateClean(self)
    InitConfig(self)
    SetQuality(self)
    SetOther(self)
    SetMusic(self)
end

function _M.OnExit(self)
    DelegateClean(self)
    DataMgr.Instance.SettingData:SaveData()
    DataMgr.Instance.SettingData:DetachLuaObserver('SystemHud')
end

function _M.OnInit(self)
    --画质设置
    self.tbt_low = self.ui.comps.tbt_low
    self.tbt_middle = self.ui.comps.tbt_middle
    self.tbt_high = self.ui.comps.tbt_high

    --其他设置
    self.tbt_vague = self.ui.comps.tbt_vague
    self.tbt_poweroff = self.ui.comps.tbt_poweroff
    self.gg_peoples = self.ui.comps.gg_peoples
    lb_peoples = self.ui.comps.lb_peoples
    self.ib_ppcheck = self.ui.comps.ib_ppcheck
    self.tbt_bloom=self.ui.comps.tbt_bloom

    --音乐设置
    self.tbt_music = self.ui.comps.tbt_music
    self.ib_mcheck = self.ui.comps.ib_mcheck
    self.gg_music = self.ui.comps.gg_music
    lb_music = self.ui.comps.lb_mnum

    self.tbt_sound = self.ui.comps.tbt_sound
    self.ib_scheck = self.ui.comps.ib_scheck
    self.gg_sound = self.ui.comps.gg_sound
    lb_sound = self.ui.comps.lb_snum

    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
    self.ui.menu.ShowType = UIShowType.Cover
end

return _M
