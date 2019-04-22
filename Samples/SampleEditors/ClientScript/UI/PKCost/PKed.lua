---作者：任祥建
---时间：2019/2/21
---PKed
local _M = {}
_M.__index = _M

local PKModel = require 'Model/PKModel'
local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local SocialModel = require 'Model/SocialModel'



function _M.OnEnter( self,params )
    if not params then
        params = PKModel.receive
    end

    if params.pktype == 1 then
        self.cvs_bg.Height = self.cvs_bg_Height
        self.ib_infobg.Height = self.ib_infobg_Height
        self.cvs_zhuangshi.Y = self.cvs_zhuangshi_Y
        self.cvs_button.Y = self.cvs_button_Y
        self.ib_bgimg.Height = self.ib_bgimg_Height
        self.cvs_staketip.Visible = true
    else
        self.cvs_bg.Height = self.cvs_bg_Height - 100
        self.ib_infobg.Height = self.ib_infobg_Height - 100
        self.cvs_zhuangshi.Y = self.cvs_zhuangshi_Y - 100
        self.cvs_button.Y = self.cvs_button_Y - 100
        self.ib_bgimg.Height = self.ib_bgimg_Height - 100
        self.cvs_staketip.Visible = false
    end

    self.lb_countdown2.Text = Util.GetText('business_activity_sec',params.cd)
    self.Update = function(cd)
        self.lb_countdown2.Text = Util.GetText('business_activity_sec',cd)
        if cd <= 0 then
            self:Close()
        end
    end
    
    Util.GetRoleSnap(params.uuid,function(snap)
        self.lb_title2.Text = Util.Format1234(self.title,snap.Name,Util.GetText(Constants.PK[params.pktype]))
        self.tb_declaration2.Text = params.text
        self.lb_level2.Text = Util.GetText(Constants.QuestCgLang.Level,snap.Level)
        self.lb_powernum2.Text = snap.FightPower
        self.lb_camp2.Text = Util.GetText(Constants.ProName[snap.Pro])

        self.lb_staketip1.Text = Util.Format1234(self.tip1,snap.Name,params.gold)
        self.lb_staketip2.Text = Util.Format1234(self.tip2,math.floor((params.gold)*0.7))
        local photoname = snap.Options['Photo0']
        if not string.IsNullOrEmpty(photoname) then
            SocialModel.SetHeadIcon(snap.ID,photoname,function(UnitImg)
                if not self.root.IsDispose then
                    UIUtil.SetImage(self.cvs_touxiang2,UnitImg,false,UILayoutStyle.IMAGE_STYLE_BACK_4)
                end
            end)
        else
            UIUtil.SetImage(self.cvs_touxiang2, 'static/target/'..snap.Pro..'_'..snap.Gender..'.png',false,UILayoutStyle.IMAGE_STYLE_BACK_4)
        end
    end)
    
    self.btn_start2.TouchClick = function(sender)
        PKModel.ResponseChallenge(1,function()
            PKModel.receive = nil
            self:Close()
            self.hudnode.Visible = false
        end,function(rsp)
            self:Close()
            self.hudnode.Visible = false
        end)
    end
    self.btn_cancel2.TouchClick = function(sender)
        PKModel.ResponseChallenge(0,function()
            PKModel.receive = nil
            self:Close()
            self.hudnode.Visible = false
        end,function()
            self:Close()
            self.hudnode.Visible = false
        end)
    end
end

function _M.OnInit( self )

    self.lb_countdown2 = self.root:FindChildByEditName('lb_countdown2', true)
    self.btn_start2 = self.root:FindChildByEditName('btn_start2', true)
    self.btn_cancel2 = self.root:FindChildByEditName('btn_cancel2', true)
    self.btn_wait2 = self.root:FindChildByEditName('btn_wait2', true)
    self.cvs_touxiang2 = self.root:FindChildByEditName('cvs_touxiang2', true)
    self.tb_declaration2 = self.root:FindChildByEditName('tb_declaration2', true)
    self.lb_level2 = self.root:FindChildByEditName('lb_level2', true)
    self.lb_powernum2 = self.root:FindChildByEditName('lb_powernum2', true)
    self.lb_camp2 = self.root:FindChildByEditName('lb_camp2', true)
    self.lb_title2 = self.root:FindChildByEditName('lb_title2', true)
    self.lb_staketip1 = self.root:FindChildByEditName('lb_staketip1', true)
    self.lb_staketip2 = self.root:FindChildByEditName('lb_staketip2', true)
    self.cvs_staketip = self.root:FindChildByEditName('cvs_staketip', true)

    self.title = self.lb_title2.Text
    self.tip1 = self.lb_staketip1.Text
    self.tip2 = self.lb_staketip2.Text

    self.cvs_bg = self.ui.comps.cvs_stake:FindChildByEditName('cvs_bg', true)
    self.ib_bgimg = self.ui.comps.cvs_stake:FindChildByEditName('ib_bgimg', true)
    self.ib_infobg = self.cvs_bg:FindChildByEditName('ib_infobg', true)
    self.cvs_zhuangshi = self.ui.comps.cvs_stake:FindChildByEditName('cvs_zhuangshi', true)
    self.cvs_button = self.ui.comps.cvs_stake:FindChildByEditName('cvs_button', true)

    self.ib_bgimg_Height = self.ib_bgimg.Height
    self.cvs_bg_Height = self.cvs_bg.Height
    self.ib_infobg_Height = self.ib_infobg.Height
    self.cvs_zhuangshi_Y = self.cvs_zhuangshi.Y
    self.cvs_button_Y = self.cvs_button.Y


    self.btn_wait2.TouchClick = function(sender)
        self:Close()
    end
    
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
    self.ui.menu.ShowType = UIShowType.Cover
end

function _M.OnExit( self )
    self.hudnode.Visible = true
end

return _M