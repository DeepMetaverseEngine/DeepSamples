local _M = {}
_M.__index = _M
print('-------------load testui---------------------')
local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil' 

local function GetData(id)
	local detail = unpack(GlobalHooks.DB.Find('FallenPartnerListData', {change_skill = id}))
	local angerlimit = unpack(GlobalHooks.DB.Find('GameConfig', {id = 'anger_limit'}))
	return detail,angerlimit
end

local function EnterUI(self)

	    --点击事件监听
    self.ui.menu.event_PointerUp = function(sender)
		if not self.canPress then
			self.ui:Close()
			return
		end
	end
end

function _M.OnEnter( self ,params)
	
--数据资源加载
	local detail,angerlimit = GetData(params.id)
    self.ui.comps.lb_name.Text = Util.GetText(detail.god_name)
    
    local itemShow = ItemShow.Create(detail.icon_id, 0, 0)
	itemShow.Size2D = self.ui.comps.cvs_icon.Size2D
	itemShow.EnableTouch = false
	self.ui.comps.cvs_icon:AddChild(itemShow)
	local nuqi = self.ui.comps.gg_nuqi
	local nuqiValue = math.floor(params.cd/100*angerlimit.paramvalue)
	-- print(nuqiValue)
  	nuqi:SetGaugeMinMax(0, tonumber( angerlimit.paramvalue))
  	nuqi.Value = nuqiValue
  	nuqi.Text = tostring(nuqiValue).."/"..angerlimit.paramvalue


end


function _M.OnExit( self )
	
end

function _M.OnInit(self)
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
    self.ui.menu.ShowType = UIShowType.Cover
    EnterUI(self)
end

local function OpenNuqiTipsUI(eventname,params)
    GlobalHooks.UI.OpenUI("UINuqiTip",0,params)
end

local function initial()
    EventManager.Subscribe("ShowBufSkillTips",OpenNuqiTipsUI)

end

local function fin(relogin)
    EventManager.Unsubscribe("ShowBufSkillTips",OpenNuqiTipsUI)
end

_M.fin = fin
_M.initial = initial
return _M