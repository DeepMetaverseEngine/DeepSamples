local _M = {}
_M.__index = _M

local UIUtil = require 'UI/UIUtil'
local SocielModel = require 'Model/SocialModel'
local Util = require 'Logic/Util'

local myself = nil
local urlconfig = "?imageView2/1/w/500/h/500/q/85/format/jpg"
local imagestyle = UILayoutStyle.IMAGE_STYLE_BACK_4
local normalstyle = UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER


local function SetDefuleIcon()
    myself.photo[0].cvs_photo.UserData = ""
    UIUtil.SetImage(myself.photo[0].cvs_photo,
            'static/target/'..myself.roledata.Pro..'_'..myself.roledata.Gender..'.png',
            false,
            imagestyle)
end

local function InitCompents(self)

    for i, v in pairs(self.photo) do
        if i ~= 0 then
            v.lb_nothing.Visible = true
            v.ib_photo.Layout = nil
        end
        v.cvs_photo.UserData = ""
    end
    
    local menuheight = self.ui.menu.Transform.rect.height
    local menuwidth = self.ui.menu.Transform.rect.width
    local rootheight = self.root.Transform.rect.height
    local rootwidth = self.root.Transform.rect.width
    self.cvs_enlarge.Height = menuheight
    self.cvs_enlarge.Width = menuwidth
    self.cvs_enlarge.Y = -(menuheight -rootheight)/2
    self.cvs_enlarge.X = -(menuwidth -rootwidth)/2
    self.ib_picture.Size2D = Vector2(menuheight, menuheight)
    self.ib_picture.X = (menuwidth - menuheight)/2
    self.ib_picture.Y = 0
    self.lb_explain.EditTextAnchor = CommonUI.TextAnchor.L_T
    self.cvs_enlarge.TouchClick = function(sender)
        self.cvs_enlarge.Visible = false
    end
end

local function ShowMaxImage(self,Layout)
    self.ib_picture.Layout = Layout
    self.cvs_enlarge.Visible = true
end

local function SetPhoto(self,photoinfo)
    --print_r("================= self.photo =================",self.photo)
    for i1, v1 in pairs(photoinfo) do
        local name = v1.photoName
        local url = self.cosurl.."/"..self.prefix.."/"..self.uuid..'/'..name..".png"
        --print_r('============== url ============== ',url)
		for _, v2 in pairs(self.photo) do
			if string.IsNullOrEmpty(v2.cvs_photo.UserData) then
				v2.cvs_photo.UserData = name
				break
			end
		end
        CacheImage.Instance:DownLoad(url..urlconfig, name,false, function(params)
            if params[1] then
                for i2, v2 in pairs(self.photo) do
                    if v2.cvs_photo.UserData == params[2] then
                        if i2 == 0 then
                            UIUtil.SetImage(v2.cvs_photo,params[3],false,UILayoutStyle.IMAGE_STYLE_BACK_4)
                        else
                            v2.lb_nothing.Visible = false
                            UIUtil.SetImage(v2.ib_photo,params[3],false,UILayoutStyle.IMAGE_STYLE_BACK_4)
                        end
                        break
                    end
                end
            end
        end)
    end
    for i, v in pairs(self.photo) do
        if i ~= 0 then
            v.cvs_photo.TouchClick = function(sender)
                ShowMaxImage(self,v.ib_photo.Layout)
            end
        end
    end
end

local function SetSocialData(self,socialdata)
    self.lb_name.Text = self.roledata.Name
    UIUtil.SetImage(self.ib_sex,
            "#dynamic/TL_social/output/TL_social.xml|TL_social|"..tostring(83+self.roledata.Gender),
            false,
            normalstyle)
    self.lb_wx.Text = socialdata.ID
    self.lb_city.Text = socialdata.City
    self.lb_explain.TextSprite.Graphics.horizontalOverflow = UnityEngine.HorizontalWrapMode.Wrap
    self.lb_explain.Text = socialdata.Introduce
    self.lb_keyword[1].Text = Util.GetText(Constants.ProName[self.roledata.Pro])
    self.lb_keyword[2].Text = Util.GetText(Constants.QuestCgLang.Level,self.roledata.Level)
    self.lb_keyword[3].Text = Util.GetText(Constants.PhotoText.VipText,self.roledata.VipLevel)
    self.lb_keyword[4].Text = self.roledata.PracticeLv == 0 and Util.GetText('NoAnything') or GameUtil.GetPracticeName(self.roledata.PracticeLv, 0)
    self.lb_keyword[5].Text = string.IsNullOrEmpty(self.roledata.Options["TitleNameExt"]) and Util.GetText('Single') or Util.GetText('Married')
end

function _M.OnEnter(self,uuid)
    if uuid then
        self.uuid = uuid
    else
        self:Close()
        return
    end
    InitCompents(self)

    Util.GetRoleSnap(self.uuid, function(data)
        self.roledata = data
        SocielModel.GetPhotoInfo(self.uuid,function(photoinfo)
            self.cosurl = photoinfo.s2c_url
            self.prefix = photoinfo.s2c_prefix
            local photoinfotemp = {}
            for k, v in pairs(photoinfo.s2c_datas.data) do
                photoinfotemp[tonumber(string.sub(k,-1,-1))] = v
            end
            self.photoinfo = photoinfotemp
            self.socialdata = photoinfo.s2c_datas.socialData
            if not self.photoinfo[0] then
                SetDefuleIcon()
            end
            SetSocialData(self,self.socialdata)
            SetPhoto(self,self.photoinfo)
        end)
    end)
end

function _M.OnExit(self)
    CacheImage.Instance:ClearCallback()
end



function _M.OnInit(self)
    myself = self
    self.cvs_namecard = self.root:FindChildByEditName('cvs_namecard', true)
    self.cvs_photoinfo = self.root:FindChildByEditName('cvs_photoinfo', true)
    self.cvs_enlarge = self.root:FindChildByEditName('cvs_enlarge', true)
    
    self.ib_picture = self.cvs_enlarge:FindChildByEditName('ib_picture', true)
    
    self.lb_name = self.cvs_namecard:FindChildByEditName('lb_name', true)
    self.ib_sex = self.cvs_namecard:FindChildByEditName('ib_sex', true)
    self.lb_wx = self.cvs_namecard:FindChildByEditName('lb_wx', true)
    self.lb_city = self.cvs_namecard:FindChildByEditName('lb_city', true)
    self.lb_explain = self.cvs_namecard:FindChildByEditName('lb_explain', true)
    
    self.lb_keyword = {}
    for i = 1, 10 do
        self.lb_keyword[i] = self.cvs_photoinfo:FindChildByEditName('lb_keyword'..i, true)
        if not self.lb_keyword[i] then
            break
        end
    end
    self.photo = {}
    for i = 0, 8 do
        self.photo[i] = {}
        if i == 0 then
            self.photo[i].cvs_photo = self.cvs_namecard:FindChildByEditName('cvs_headpic', true)
        else
            self.photo[i].cvs_photo = self.cvs_photoinfo:FindChildByEditName('cvs_photo'..i, true)
            self.photo[i].ib_photo = self.cvs_photoinfo:FindChildByEditName('ib_photo'..i, true)
            self.photo[i].lb_nothing = self.cvs_photoinfo:FindChildByEditName('lb_nothing'..i, true)
        end
    end
    
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
end

return _M
