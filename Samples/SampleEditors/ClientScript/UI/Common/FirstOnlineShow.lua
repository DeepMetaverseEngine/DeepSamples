---date:2019.01.17
---author:任祥建
---scriptname:FirstOnlineShow
local _M = {}
_M.__index = _M

local ServerTime = require 'Logic/ServerTime'


function _M.OnEnter(self)
    --print_r("========================   ",ServerTime.getServerTime():ToLocalTime())
    local date = tostring(ServerTime.getServerTime().Date)
    local tipdatas = GlobalHooks.DB.GetFullTable("onlinetip")
    local uuid = DataMgr.Instance.UserData.RoleID
    self.needshow = {}
    for i, v in ipairs(tipdatas) do
        if FunctionUtil.CheckNowIsOpen(v.open_time,false) then
            if PlayerPrefs.GetString(v.open_time..uuid) ~= date then
                table.insert(self.needshow,v.activity_id)
                PlayerPrefs.SetString(v.open_time..uuid, date)
                self.btn[v.activity_id].TouchClick = function(sender)
                    FunctionUtil.OpenFunction(v.goto_functions)
                end
            end
        end
    end
    if #self.needshow > 0 then
        self.cvs[self.needshow[1]].Visible = true
    else
        self:Close()
    end
end

function _M.OnInit(self )
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
    self.ui.menu.ShowType = UIShowType.Cover
    self.ui.menu:SetFullBackground(UILayout.CreateUILayoutColor(UnityEngine.Color(0,0,0,0.5),UnityEngine.Color(0,0,0,0.5)))
    self.btn_close = self.root:FindChildByEditName('btn_close', true)
    self.cvs = {}
    self.btn = {}
    for i = 1, 100 do
        local cvstemp = self.root:FindChildByEditName('cvs_'..i, true)
        local btntemp = self.root:FindChildByEditName('btn_'..i, true)
        if cvstemp then
            cvstemp.Visible = false
            self.cvs[i] = cvstemp
            if btntemp then
                self.btn[i] = btntemp
            end
        else
            break
        end
    end
    self.btn_close.TouchClick = function(sender)
        if #self.needshow > 0 then
            self.cvs[self.needshow[1]].Visible = false
            table.remove(self.needshow,1)
            if #self.needshow > 0 then
                self.cvs[self.needshow[1]].Visible = true
            else
                self:Close()
            end
        else
            self:Close()
        end
    end
    
end

function _M.OnExit( self )

end

return _M