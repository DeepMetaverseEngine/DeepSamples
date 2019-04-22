local _M = {}
_M.__index = _M

local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'


local function ShowDetail(self,index,data)
    self.cvs[index].tb_textsub.UnityRichText = string.gsub(Util.GetText(data.text_sub), "\\n", "\n")
    self.cvs[index].tb_textmin.UnityRichText = string.gsub(Util.GetText(data.text_min), "\\n", "\n")
    self.cvs[index].tb_textother.UnityRichText = string.gsub(Util.GetText(data.text_other), "\\n", "\n")
    UIUtil.SetImage(self.cvs[index].ib_pic1,data.show_pic1)
    UIUtil.SetImage(self.cvs[index].ib_pic2,data.show_pic2)
    self.btn_gogo.TouchClick = function(sender)
        Application.OpenURL(data.goto)
    end
end

function _M.OnEnter( self )
    local OSType = PublicConst.OSType == 5 and "IOS" or "Android"
    for i, v in ipairs(self.staticdata) do
        if v.ostype ~= OSType and not string.IsNullOrEmpty(v.ostype) then
            table.remove(self.staticdata,i)
            break
        end
    end
    for i, v in ipairs(self.cvs) do
        v.title.Text = Util.GetText(self.staticdata[i].name)
        UIUtil.SetImage(v.imageicon,self.staticdata[i].icon)
    end
    UIUtil.ConfigToggleButton(self.tbts,self.tbts[1],nil,function(node)
        for i, v in ipairs(self.cvs) do
            if i ~= node.UserTag then
                v.detail.Visible = false
            else
                v.detail.Visible = true
            end
        end
        ShowDetail(self,node.UserTag,self.staticdata[node.UserTag])
    end)
    
    

end

function _M.OnInit( self )
    local cvs_detail = self.root:FindChildByEditName('cvs_detail', true)
    local cvs_list = self.root:FindChildByEditName('cvs_list', true)
    
    self.btn_gogo = cvs_detail:FindChildByEditName('btn_gogo', true)
    self.staticdata = GlobalHooks.DB.GetFullTable("shequ")
    
    self.cvs = {}
    self.tbts = {}
    for i = 1, 20 do
        local tempnode = cvs_list:FindChildByEditName('cvs_list'..i, true)
        if tempnode then
            self.cvs[i] = {}
            self.cvs[i].detail = cvs_detail:FindChildByEditName('cvs_tips'..i, true)
            self.cvs[i].tbt = tempnode:FindChildByEditName('tbt_qudao'..i, true)
            self.tbts[i] = self.cvs[i].tbt
            self.tbts[i].UserTag = i
            self.cvs[i].title = tempnode:FindChildByEditName('lb_text'..i, true)
            self.cvs[i].imageicon = tempnode:FindChildByEditName('ib_img'..i, true)
            self.cvs[i].tb_textsub = self.cvs[i].detail:FindChildByEditName('tb_textsub', true)
            self.cvs[i].tb_textother = self.cvs[i].detail:FindChildByEditName('tb_textother', true)
            self.cvs[i].tb_textmin = self.cvs[i].detail:FindChildByEditName('tb_textmin', true)
            self.cvs[i].ib_pic1 = self.cvs[i].detail:FindChildByEditName('ib_pic1', true)
            self.cvs[i].ib_pic2 = self.cvs[i].detail:FindChildByEditName('ib_pic2', true)
        else
            break
        end 
    end

    self.menu.ShowType = UIShowType.HideHudAndMenu
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
end

function _M.OnExit( self )

end

return _M