---作者：任祥建
---时间：2019/2/20
---PKCostMain
local _M = {}
_M.__index = _M
local myself = nil

local PKModel = require 'Model/PKModel'
local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local SocialModel = require 'Model/SocialModel'



local function SetCost(self)
    self.myMoney = DataMgr.Instance.UserData:GetAttribute(UserData.NotiFyStatus.DIAMOND)-200
    self.myMoney = self.myMoney>10000 and 10000 or self.myMoney
    self.costNum = 100
    self.lb_num.Text = 100
    self.lb_num.event_PointerClick = function(sender)
        GlobalHooks.UI.OpenUI('NumInput', 0, 100, self.myMoney, {pos={x=450,y=100}, anchor=nil}, function(value)
            self.lb_num.Text = value
            self.costNum = value
        end,function(value)
            self.lb_num.Text = value
            self.costNum = value
        end)
    end

    self.btn_less.TouchClick = function(sender)
        local num = self.costNum
        if num > 100 and num >= 200 then
            self.lb_num.Text = num-100
            self.costNum = num-100
        elseif num > 100 and num < 200 then
            self.lb_num.Text = 100
            self.costNum = 100
        end
    end

    self.btn_more.TouchClick = function(sender)
        local num = self.costNum
        if num < self.myMoney and num <= self.myMoney - 100 then
            self.lb_num.Text = num + 100
            self.costNum = num + 100
        elseif num < self.myMoney and num > self.myMoney - 100 then
            self.lb_num.Text = self.myMoney
            self.costNum = self.myMoney
        end
    end

    self.btn_min.TouchClick = function(sender)
        self.lb_num.Text = 100
        self.costNum = 100
    end

    self.btn_max.TouchClick = function(sender)
        self.lb_num.Text = self.myMoney
        self.costNum = self.myMoney
    end
end

local function OnMenuEnter()
    
end

--处理utf-8文字的计算长度问题
local function Bytes4Character(theByte)
    local seperate = {0, 0xc0, 0xe0, 0xf0}
    for i = #seperate, 1, -1 do
        if theByte >= seperate[i] then return i end
    end
    return 1
end

local function GetStringLen(utf8Str, aChineseCharBytes)
    aChineseCharBytes = aChineseCharBytes or 1
    local i = 1
    local characterSum = 0
    while (i <= #utf8Str) do      -- 编码的关系
        local bytes4Character = Bytes4Character(string.byte(utf8Str, i))
        characterSum = characterSum + (bytes4Character > aChineseCharBytes and aChineseCharBytes or bytes4Character)
        i = i + bytes4Character
    end

    return characterSum
end

local function OnMenuExit()
    if myself.pkType == 1 then
        myself.pktype[1].Visible = true
        myself.pktype[0].Visible = false
        myself.myMoney = DataMgr.Instance.UserData:GetAttribute(UserData.NotiFyStatus.DIAMOND) - 200
        if myself.myMoney < 100 then
            myself.cvs_stake.Visible = false
            myself.cvs_alert.Visible = true
        else
            myself.cvs_stake.Visible = true
            myself.cvs_alert.Visible = false
            SetCost(myself)
        end
    else
        myself.pktype[1].Visible = false
        myself.pktype[0].Visible = true
        if DataMgr.Instance.UserData:GetAttribute(UserData.NotiFyStatus.SILVER) < 200 then
            GameAlertManager.Instance:ShowNotify(Util.GetText("arena_silver_nomoney"))
            myself:Close()
        else
            myself.cvs_stake.Visible = true
            myself.cvs_alert.Visible = false
        end
    end
end

local function TextValueChanged(node,text)
    if string.IsNullOrEmpty(text) then
        myself.btn_search2.Enable = false
        myself.btn_search2.IsGray = true
    else
        myself.btn_search2.Enable = true
        myself.btn_search2.IsGray = false
    end
end

local function TextValueNumShow(node,text)
    myself.lb_inputnum.Text = 60-string.utf8len(text)
end

local function SetOther(self)
    self.ti_targetname2.Input.characterLimit = 10
    self.ti_declaration2.Input.characterLimit = 60
    self.ti_targetname2.event_ValueChanged = {'+=',TextValueChanged}
    self.ti_declaration2.event_ValueChanged = {'+=',TextValueNumShow}
    
end

local function SetGoto(self)
    self.bt_charge.TouchClick = function(sender)
        GlobalHooks.UI.OpenUI('Recharge',0, 'RechargePay')
    end

    self.bt_no.TouchClick = function(sender)
        self:Close()
    end
end

local function InitCompents(self)
    self.btn_search2.Enable = not string.IsNullOrEmpty(self.ti_targetname2.Input.Text)
    self.btn_search2.IsGray = string.IsNullOrEmpty(self.ti_targetname2.Input.Text)
    self.cvs_searchresult.Visible = false
    self.lb_inputnum.Text = 60
    self.costNum = 0
    
    self.ti_declaration2.Input.lineType = UnityEngine.UI.InputField.LineType.MultiLineSubmit
    self.ti_declaration2.Input.Text = ""
    self.ti_targetname2.Input.Text = ""

    self.btn_search2.TouchClick = function(sender)
        self.selectedplayer = nil
        self.selectedplayername = nil
        self.btn_confirm.Enable = false
        self.btn_confirm.IsGray = true
        PKModel.SeachPlayer(self.ti_targetname2.Input.Text,function(ids)
            if #ids.s2c_roleIdList > 0 then
                Util.GetManyRoleSnap(ids.s2c_roleIdList,function(snaps)
                    self.cvs_searchresult.Visible = true
                    UIUtil.ConfigVScrollPan(self.sp_list,self.cvs_targetinfo,#snaps,function(node,index)
                        local cvs_touxiang = node:FindChildByEditName('cvs_touxiang', true)
                        local lb_level = node:FindChildByEditName('lb_level', true)
                        local lb_name = node:FindChildByEditName('lb_name', true)
                        local ib_choose = node:FindChildByEditName('ib_choose', true)
                        local lb_camp = node:FindChildByEditName('lb_camp', true)

                        ib_choose.Visible = self.selectedplayer == snaps[index].ID
                        lb_level.Text = Util.GetText(Constants.QuestCgLang.Level,snaps[index].Level)
                        lb_name.Text = snaps[index].Name
                        lb_camp.Text = Util.GetText(Constants.ProName[snaps[index].Pro])
                        local photoname = snaps[index].Options['Photo0']
                        if not string.IsNullOrEmpty(photoname) then
                            SocialModel.SetHeadIcon(snaps[index].ID,photoname,function(UnitImg)
                                if not self.root.IsDispose then
                                    UIUtil.SetImage(cvs_touxiang,UnitImg,false,UILayoutStyle.IMAGE_STYLE_BACK_4)
                                end
                            end)
                        else
                            UIUtil.SetImage(cvs_touxiang, 'static/target/'..snaps[index].Pro..'_'..snaps[index].Gender..'.png',false,UILayoutStyle.IMAGE_STYLE_BACK_4)
                        end

                        node.TouchClick = function(sender)
                            if self.selectedplayer == snaps[index].ID then
                                ib_choose.Visible = false
                                self.selectedplayer = nil
                                self.btn_confirm.Enable = false
                                self.btn_confirm.IsGray = true
                            else
                                ib_choose.Visible = true
                                self.selectedplayer = snaps[index].ID
                                self.selectedplayername = snaps[index].Name
                                self.btn_confirm.Enable = true
                                self.btn_confirm.IsGray = false
                            end
                        end
                    end)
                end)
            else
                GameAlertManager.Instance:ShowNotify(Util.GetText("arena_target_noexist"))
            end
        end)
    end

    self.btn_confirm.TouchClick = function(sender)
        if self.selectedplayer then
            self.cvs_searchresult.Visible = false
            self.ti_targetname2.Input.Text = self.selectedplayername
            self.selectedplayername = nil
        else
            GameAlertManager.Instance:ShowNotify("未选中要挑战的玩家")
        end
    end

    self.btn_listclose.TouchClick = function(sender)
        self.cvs_searchresult.Visible = false
        self.selectedplayer = nil
    end

    self.btn_start2.TouchClick = function(sender)
        if self.selectedplayer then
            PKModel.IssueChallenge(self.selectedplayer,self.pkType,self.costNum,self.ti_declaration2.Input.Text,function()
                --local tip = GlobalHooks.UI.CreateUI('WaitPK', 0,100)
                --MenuMgr.Instance:AddMsgBox(tip.ui.menu)
                --PKModel.StartWait(10)
                self:Close()
            end)
        else
            PKModel.SeachPlayer(self.ti_targetname2.Input.Text,function(ids)
                Util.GetManyRoleSnap(ids,function(snaps)
                    for i, v in ipairs(snaps) do
                        if v.Name == self.ti_targetname2.Input.Text then
                            self.selectedplayer = v.ID
                            PKModel.IssueChallenge(self.selectedplayer,self.pkType,self.costNum,self.ti_declaration2.Input.Text,function()
                                --local tip = GlobalHooks.UI.CreateUI('WaitPK', 0,100)
                                --MenuMgr.Instance:AddMsgBox(tip.ui.menu)
                                --PKModel.StartWait()
                                self:Close()
                            end)
                            return
                        end
                    end
                    GameAlertManager.Instance:ShowNotify(Util.GetText("arena_target_noexist"))
                end)
            end)
        end
    end
    
    self.btn_cancel2.TouchClick = function(sender)
        self:Close()
    end
end

function _M.OnEnter( self,pkType)
    MenuMgr.Instance:AttachLuaObserver('Recharge','PKCostMain',{OnMenuEnter = OnMenuEnter,OnMenuExit = OnMenuExit})

    self.pkType = tonumber(pkType)
    if pkType == 1 then
        self.cvs_bg.Height = self.cvs_bg_Height
        self.ib_infobg.Height = self.ib_infobg_Height
        self.cvs_zhuangshi.Y = self.cvs_zhuangshi_Y
        self.cvs_button.Y = self.cvs_button_Y
        self.ib_bgimg.Height = self.ib_bgimg_Height
        self.cvs_stakenum.Visible = true
    else
        self.cvs_bg.Height = self.cvs_bg_Height - 100
        self.ib_infobg.Height = self.ib_infobg_Height - 100
        self.cvs_zhuangshi.Y = self.cvs_zhuangshi_Y - 100
        self.cvs_button.Y = self.cvs_button_Y - 100
        self.ib_bgimg.Height = self.ib_bgimg_Height - 100
        self.cvs_stakenum.Visible = false
    end
    InitCompents(self)
    SetOther(self)
    SetGoto(self)
    OnMenuExit()


end

function _M.OnInit( self )
    myself = self
    self.cvs_stakenum = self.root:FindChildByEditName('cvs_stakenum', true)
    self.cvs_stake = self.root:FindChildByEditName('cvs_stake', true)
    self.cvs_alert = self.root:FindChildByEditName('cvs_alert', true)
    self.lb_alerttip2 = self.root:FindChildByEditName('lb_alerttip2', true)
    self.cvs_searchresult = self.root:FindChildByEditName('cvs_searchresult', true)
    self.bt_charge = self.cvs_alert:FindChildByEditName('bt_charge', true)
    self.bt_no = self.cvs_alert:FindChildByEditName('bt_yes', true)
    self.lb_costnum = self.cvs_alert:FindChildByEditName('lb_costnum', true)

    self.btn_cancel2 = self.cvs_stake:FindChildByEditName('btn_cancel2', true)
    self.btn_start2 = self.cvs_stake:FindChildByEditName('btn_start2', true)
    self.cvs_bg = self.cvs_stake:FindChildByEditName('cvs_bg', true)
    self.ib_infobg = self.cvs_stake:FindChildByEditName('ib_infobg', true)
    self.cvs_zhuangshi = self.cvs_stake:FindChildByEditName('cvs_zhuangshi', true)
    self.cvs_button = self.cvs_stake:FindChildByEditName('cvs_button', true)
    self.ib_bgimg = self.cvs_stake:FindChildByEditName('ib_bgimg', true)

    self.ib_bgimg_Height = self.ib_bgimg.Height
    self.cvs_bg_Height = self.cvs_bg.Height
    self.ib_infobg_Height = self.ib_infobg.Height
    self.cvs_zhuangshi_Y = self.cvs_zhuangshi.Y
    self.cvs_button_Y = self.cvs_button.Y

    self.lb_num = self.cvs_stakenum:FindChildByEditName('lb_num', true)
    self.btn_min = self.cvs_stakenum:FindChildByEditName('btn_min', true)
    self.btn_max = self.cvs_stakenum:FindChildByEditName('btn_max', true)
    self.btn_less = self.cvs_stakenum:FindChildByEditName('btn_less', true)
    self.btn_more = self.cvs_stakenum:FindChildByEditName('btn_more', true)
    self.ti_targetname2 = self.cvs_stake:FindChildByEditName('ti_targetname2', true)
    self.ti_declaration2 = self.cvs_stake:FindChildByEditName('ti_declaration2', true)
    self.lb_inputnum = self.cvs_stake:FindChildByEditName('lb_inputnum', true)
    self.btn_search2 = self.cvs_stake:FindChildByEditName('btn_search2', true)
    self.pktype = {}
    self.pktype[1] = self.cvs_stake:FindChildByEditName('ib_yuanbao', true)
    self.pktype[0] = self.cvs_stake:FindChildByEditName('ib_yinliang', true)

    self.lb_num.IsInteractive = true
    self.lb_num.Enable = true

    self.btn_listclose = self.cvs_searchresult:FindChildByEditName('btn_listclose', true)
    self.btn_left = self.cvs_searchresult:FindChildByEditName('btn_left', true)
    self.btn_right = self.cvs_searchresult:FindChildByEditName('btn_right', true)
    self.btn_confirm = self.cvs_searchresult:FindChildByEditName('btn_confirm', true)
    self.sp_list = self.cvs_searchresult:FindChildByEditName('sp_list', true)
    self.cvs_targetinfo = self.cvs_searchresult:FindChildByEditName('cvs_targetinfo', true)
    self.cvs_targetinfo.Visible = false
    

    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
    self.ui.menu.ShowType = UIShowType.HideHudAndMenu
end

function _M.OnExit( self )
    MenuMgr.Instance:DetachLuaObserver('Recharge','PKCostMain')
    self.ti_targetname2.event_ValueChanged = {'-=',TextValueChanged}
    self.ti_declaration2.event_ValueChanged = {'-=',TextValueNumShow}

end

return _M