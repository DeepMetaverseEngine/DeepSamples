local _M = {}
_M.__index = _M

local UIUtil = require 'UI/UIUtil'
local SocielModel = require 'Model/SocialModel'
local Helper = require 'Logic/Helper'
local Util = require 'Logic/Util'

local myself = nil
local urlconfig = "?imageView2/1/w/500/h/500/q/85/format/jpg"
local imagestyle = UILayoutStyle.IMAGE_STYLE_BACK_4
local normalstyle = UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER
local generated = false

local function SetSelfPrivate(self)
    self.tbt_showicon.IsChecked = self.privatedata.key_profile_photo == '1'
    self.tbt_showwxid.IsChecked = self.privatedata.key_profile_id == '1'
    self.tbt_showphoto.IsChecked = self.privatedata.key_photo == '1'
    self.tbt_showpos.IsChecked = self.privatedata.key_city == '1'

    self.cvs_prset.TouchClick = function(sender)
        self.cvs_prset.Visible = false
    end
end

local function GenerateLicense(data)
    local appId = string.split(data.bucket,"-")
    OneGameSDK.Instance:CreateCredentialProvider(
            appId[#appId],
            data.region,
            data.bucket,
            data.tmpSecretId,
            data.tmpSecretKey,
            data.sessionToken,
            tonumber(data.beginTime),
            tonumber(data.expiredTime))
end

local function GetPhotoAddress(cb)
    if generated then
        if cb then
            cb()
        end
    else
        SocielModel.GetPhotoAddress(function (licenseinfo)
            generated = true
            GenerateLicense(licenseinfo.s2c_data)
            if cb then
                cb()
            end
        end)
    end
end

local function SetDefuleIcon()
    myself.photo[0].cvs_photo.UserData = ""
    myself.photo[0].lb_checking.Visible = false
    UIUtil.SetImage(myself.photo[0].cvs_photo,
            'static/target/'..myself.roledata.Pro..'_'..myself.roledata.Gender..'.png',
            false,
            imagestyle)
end

local function InitCompents(self)
    self.cvs_prset.Visible = false
    self.btn_ok.TouchClick = function(sender)
        self.cvs_prset.Visible = false
        self.privatedata.key_profile_photo = self.tbt_showicon.IsChecked and '1' or '0'
        self.privatedata.key_profile_id = self.tbt_showwxid.IsChecked and '1' or '0'
        self.privatedata.key_photo = self.tbt_showphoto.IsChecked and '1' or '0'
        self.privatedata.key_city = self.tbt_showpos.IsChecked and '1' or '0'
        local msg = { c2s_options = self.privatedata }
        Protocol.RequestHandler.ClientSaveOtherOptionsRequest(msg, function(rsp)
            for k,v in pairs(self.privatedata) do
                DataMgr.Instance.UserData:LuaSaveOptionsData(k,v)
            end
        end)
    end
    self.btn_privacy.TouchClick = function(sender)
        self.cvs_prset.Visible = true
        SetSelfPrivate(self)
    end
    self.btn_privacy.Visible = true
    for i, v in pairs(self.photo) do
        if i == 0 then
            v.lb_checking.Visible = false
        else
            v.ib_add.Visible = true
            v.ib_photo.Layout = nil
        end
        v.cvs_photo.Visible = true
        v.cvs_photo.UserData = ""
    end


    self.ti_wxnum.Input.characterLimit = 20
    self.ti_city.Input.characterLimit = 10
    self.ti_explain.Input.characterLimit = 50
    
    self.photo[0].lb_checking.Visible = false
    
end

local function DeletePhoto(sender,extras)
    SocielModel.DeletePhotoRequest(extras.index,function(rsp)
        --print_r('============= deletephoto ============= ',rsp)
        if extras.index == 0 then
            myself.photo[extras.index].cvs_photo.Layout = nil
            SetDefuleIcon()
        else
            myself.photo[extras.index].ib_photo.Layout = nil
            myself.photo[extras.index].ib_add.Visible = true

        end
        myself.photo[extras.index].cvs_photo.UserData = ""
    end)
end

local function UploadPhoto(sender,extras)
    GetPhotoAddress(function()
        OneGameSDK.Instance:OpenAlbum(Util.GetText("album_select_pic"),1,1, function (filepath)
            --print_r('================= filepath ====================',filepath)
            if filepath ~= "-1" then
                SocielModel.UploadPhotoRequest(extras.index,function(rsp)
                    local name = rsp.s2c_photoName
                    local url = myself.prefix.."/"..myself.uuid..'/'..name..".png"
                    OneGameSDK.Instance:Upload(filepath,"/"..url, function(result,desc)
                        if result == "success" then
                            CacheImage.Instance:DownLoad(myself.cosurl..url..urlconfig, name,false, function(params)
                                if params[1] then
                                    if extras.index == 0 then
                                        EventManager.Fire("Event.User.SwapHeadIcon", {path=params[3]})
                                        UIUtil.SetImage(myself.photo[extras.index].cvs_photo,params[3],false,UILayoutStyle.IMAGE_STYLE_BACK_4)
                                    else
                                        UIUtil.SetImage(myself.photo[extras.index].ib_photo,params[3],false,UILayoutStyle.IMAGE_STYLE_BACK_4)
                                        myself.photo[extras.index].ib_add.Visible = false
                                    end
                                    myself.photo[extras.index].cvs_photo.UserData = params[2]
                                end
                            end)
                        else
                            GameAlertManager.Instance:ShowNotify(Util.GetText("album_load_fail"))
                        end
                    end)
                end)
            end
        end)
    end)
end

local function SetShowBtn(self,...)
    local temptable = {}
    for i, v in pairs({...}) do
        if self.custombtns[v] then
            table.insert(temptable,self.custombtns[v])
        end
    end
    while #temptable < 2 do
        table.insert(temptable,{})
    end
    return temptable
end

local function SetPhoto(self,photoinfo,custombtns)
    --print_r("================= self.photo =================",self.photo)
    for i1, v1 in pairs(photoinfo) do
        local name = v1.photoName
        local url = self.cosurl.."/"..self.prefix.."/"..self.uuid..'/'..name..".png"
        self.photo[i1].cvs_photo.UserData = name
        CacheImage.Instance:DownLoad(url..urlconfig, name,false, function(params)
            if params[1] then
                for i2, v2 in pairs(self.photo) do
                    if v2.cvs_photo.UserData == params[2] then
                        if i2 == 0 then
                            UIUtil.SetImage(v2.cvs_photo,params[3],false,UILayoutStyle.IMAGE_STYLE_BACK_4)
                        else
                            v2.ib_add.Visible = false
                            UIUtil.SetImage(v2.ib_photo,params[3],false,UILayoutStyle.IMAGE_STYLE_BACK_4)
                        end
                        break
                    end
                end
            end
        end)
    end
    local menuheight = self.ui.menu.Transform.rect.height
    local menuwidth = self.ui.menu.Transform.rect.width
    local rootheight = self.root.Transform.rect.height
    local rootwidth = self.root.Transform.rect.width
            
    
    for i, v in pairs(self.photo) do
        local tempnode = v.cvs_photo
        if i == 0 then
            tempnode = v.btn_add
        end
        tempnode.TouchClick = function(sender)
            local temp = self.ui.root:GlobalToLocal(sender:LocalToGlobal(),true)
            temp[1] = temp[1] + sender.Width+(menuwidth -rootwidth)/2
            temp[2] = temp[2]+(menuheight -rootheight)/2
            local tempbtns = nil
            if string.IsNullOrEmpty(v.cvs_photo.UserData) then
                tempbtns = SetShowBtn(self,1)
            else
                tempbtns = SetShowBtn(self,3,2)
            end
            local params = {
                col = 1,
                pos = temp,
                extras = {uuid = self.uuid,index = i,imagenode = v.cvs_photo},
                btntxts = {tempbtns[1].btntxt,tempbtns[2].btntxt},
                cbs = {tempbtns[1].cb,tempbtns[2].cb}
            }
            GlobalHooks.UI.OpenUI("CustomButtonLayout",0,params)
        end
    end
    
end

local function SetSocialData(self,socialdata)
    self.lb_name.Text = self.roledata.Name
    UIUtil.SetImage(self.ib_sex,
            "#dynamic/TL_social/output/TL_social.xml|TL_social|"..tostring(83+self.roledata.Gender),--self.roledata.Gender+83),
            false,
            normalstyle)
    --print_r("=============== self.socialdata ===============",self.socialdata)
    self.ti_wxnum.Input.Text = socialdata.ID
    self.ti_city.Input.Text = socialdata.City
    self.ti_explain.Input.Text = socialdata.Introduce
    self.ti_explain.Input.lineType = UnityEngine.UI.InputField.LineType.MultiLineSubmit
    self.lb_keyword[1].Text = Util.GetText(Constants.ProName[self.roledata.Pro])
    self.lb_keyword[2].Text = Util.GetText(Constants.QuestCgLang.Level,self.roledata.Level)
    self.lb_keyword[3].Text = Util.GetText(Constants.PhotoText.VipText,self.roledata.VipLevel)
    self.lb_keyword[4].Text = self.roledata.PracticeLv == 0 and Util.GetText('NoAnything') or GameUtil.GetPracticeName(self.roledata.PracticeLv, 0)
    self.lb_keyword[5].Text = string.IsNullOrEmpty(self.roledata.SpouseId) and Util.GetText('Single') or Util.GetText('Married')

end

function _M.OnEnter(self)
    self.uuid = DataMgr.Instance.UserData.RoleID
    self.privatedata = CSharpMap2Table(DataMgr.Instance.UserData.GameOptionsData.Options)
    
    InitCompents(self)
    --print_r("=============== self.privatedata ===============  ",self.privatedata)
    self.custombtns = {
        [1] = {btntxt = Util.GetText("album_upload_pic"),cb = UploadPhoto},
        [2] = {btntxt = Util.GetText("album_delete_pic"),cb = DeletePhoto},
        [3] = {btntxt = Util.GetText("album_replace_pic"),cb = UploadPhoto}
    }
    
    self.roledata = {
        Gender = DataMgr.Instance.UserData.Gender,
        Name = DataMgr.Instance.UserData.Name,
        Pro = DataMgr.Instance.UserData.Pro,
        Level = DataMgr.Instance.UserData.Level,
        VipLevel = DataMgr.Instance.UserData.VipLv,
        PracticeLv = DataMgr.Instance.UserData:TryGetIntAttribute(UserData.NotiFyStatus.PRACTICELV, 0),
        SpouseId = DataMgr.Instance.UserData.SpouseId
    }

    --print_r('-------------licenseinfo---------------  ',licenseinfo)
    SocielModel.GetPhotoInfo(self.uuid,function(photoinfo)
        --print_r('============= photoinfo ============= ',photoinfo)
        self.cosurl = photoinfo.s2c_url
        self.prefix = photoinfo.s2c_prefix
        local photoinfotemp = {}
        for k, v in pairs(photoinfo.s2c_datas.data) do
            photoinfotemp[tonumber(string.sub(k,-1,-1))] = v
        end
        self.photoinfo = photoinfotemp
        self.socialdata = photoinfo.s2c_datas.socialData
        SetSocialData(self,self.socialdata)
        if not self.photoinfo[0] then
            SetDefuleIcon()
        end
        SetPhoto(self,self.photoinfo,Helper.copy_table(self.custombtns))
    end)
    
end

function _M.OnExit(self)
    generated = false
    CacheImage.Instance:ClearCallback()
    if self.socialdata then
        self.socialdata.ID = self.ti_wxnum.Text
        self.socialdata.City = self.ti_city.Text
        self.socialdata.Introduce = self.ti_explain.Text
        --print_r("=============== self.socialdata ===============",self.socialdata)
        SocielModel.UpdateSoicalRequest(self.socialdata)
    end
end

function _M.OnInit(self)
    myself = self
    self.cvs_namecard = self.root:FindChildByEditName('cvs_namecard', true)
    self.cvs_photoinfo = self.root:FindChildByEditName('cvs_photoinfo', true)
    self.cvs_prset = self.root:FindChildByEditName('cvs_prset', true)
    self.cvs_enlarge = self.root:FindChildByEditName('cvs_enlarge', true)
    
    
    self.ib_picture = self.cvs_enlarge:FindChildByEditName('ib_picture', true)

    self.tbt_showicon = self.cvs_prset:FindChildByEditName('tbt_select1', true)
    self.tbt_showwxid = self.cvs_prset:FindChildByEditName('tbt_select2', true)
    self.tbt_showphoto = self.cvs_prset:FindChildByEditName('tbt_select3', true)
    self.tbt_showpos = self.cvs_prset:FindChildByEditName('tbt_select4', true)
    self.btn_ok = self.cvs_prset:FindChildByEditName('btn_ok', true)
    
    --self.cvs_headicon = self.cvs_namecard:FindChildByEditName('cvs_headpic', true)
    --self.btn_add = self.cvs_namecard:FindChildByEditName('btn_add', true)
    --self.lb_checking = self.cvs_namecard:FindChildByEditName('lb_checking', true)
    self.lb_name = self.cvs_namecard:FindChildByEditName('lb_name', true)
    self.ib_sex = self.cvs_namecard:FindChildByEditName('ib_sex', true)
    self.ti_wxnum = self.cvs_namecard:FindChildByEditName('ti_wxnum', true)
    self.ti_city = self.cvs_namecard:FindChildByEditName('ti_city', true)
    self.ti_explain = self.cvs_namecard:FindChildByEditName('ti_explain', true)
    
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
            self.photo[i].btn_add = self.cvs_namecard:FindChildByEditName('btn_add', true)
            self.photo[i].lb_checking = self.cvs_namecard:FindChildByEditName('lb_checking', true)
        else
            self.photo[i].cvs_photo = self.cvs_photoinfo:FindChildByEditName('cvs_photo'..i, true)
            self.photo[i].ib_add = self.cvs_photoinfo:FindChildByEditName('ib_add'..i, true)
            self.photo[i].ib_photo = self.cvs_photoinfo:FindChildByEditName('ib_photo'..i, true)
            self.photo[i].lb_checking = self.cvs_namecard:FindChildByEditName('lb_checking'..i, true)
        end
    end
    
    self.btn_privacy = self.cvs_photoinfo:FindChildByEditName('btn_privacy', true)
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
end


return _M
