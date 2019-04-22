local _M = {}
_M.__index = _M

-- self.ui BaseUI实例例
-- self.ui.comps 节点访问器
-- self.ui.menu csharp MenuBase
local this
local function ShowSelf()
	--设置半身像
	--SetBustPic(PeekNpcData())
	--设置名称
	local userdata = DataMgr.Instance.UserData
	--local lv = tonumber(userdata:GetAttribute(UserData.NotiFyStatus.LEVEL))
	local name = userdata.Name
	--lv = Util.GetText(TextConfig.Type.PUBLICCFG,'LongLv.n',lv)
	--name = string.format("<f>%s %s</f>",lv,name)
	this.lb_name.Text = name	
end

local function ShowNpc(npc_data)
	--设置名称
	local name = string.format("<f><name color='fffee663'>%s</name>",npc_data.Name)
	this.lb_name.Text = name	
end
local function ParseDialogContent(str)
	print("ParseDialogContent"..str)
	local userName = "玩家的名字"--DataMgr.Instance.UserData.Name
	local content = string.gsub(str or '','%%name',userName)
	print("content"..content)
	local tmps = string.split(content,':')
	local npc_tempid = nil
	if #tmps == 2 then
		npc_tempid = tonumber(tmps[1])
		content = tmps[2]
	end
	-- print('parse----',PrintTable(tmps))
	return content,npc_tempid
end

local function SetNpcDialogText(self,str)
	-- this.cvs_reward.Visible = false
	
	print("SetNpcDialogText"..str)
	local content,npc_tempid = ParseDialogContent(str or '')
	if npc_tempid then
		if npc_tempid == 0 then
			--自己
			ShowSelf()
		else
			local npc_data = DataMgr.Instance.NPCManager:GetNPCByTemplateID(npc_tempid)
			--ShowNpc(npc_data)
		end
	else
		--ShowNpc(PeekNpcData())
	end
	
	this.tbh_text.XmlText = string.format("<f>%s</f>",content)
end

local function CloseUI(self)
	self.ui:Close()
end

function _M.OnEnter( self, params)
	SetNpcDialogText(params.talkinfo)
	LuaTimer.Add(params.keepTime, function()

		CloseUI(self)
		return false
	end)
	

	-------------------------------Start-------------------------
	
	------------------------------END----------------------------

end
function _M.OnExit( self )
	print('OnExit')
end

function _M.OnDestory( self )
	print('OnDestory')
end

function _M.OnInit( self )
	print('OnInit')
	this = self.ui.comps
	self.ui.menu.ShowType = UIShowType.HideBackMenu 
	-- self.ui.menu.event_PointerUp = function(sender)
	-- 	CloseUI(self)
	-- end
	-- self.ui.comps.cvs_talk.event_PointerUp = function(sender)
	-- 	CloseUI(self)
	-- end
	-- self.ui.comps.btn_close.TouchClick = function(sender)
	-- 	CloseUI(self)
	-- end
	
	
end

--打开聊天界面
local function OnStoryTip(eventname,params)
	if GlobalHooks.UI.FindUI('UIStoryTip') ~= nil then return end
    GlobalHooks.UI.OpenUI('UIStoryTip',0,params)
end
local function initial()
	EventManager.Subscribe(Events.UI_STORYTIP,OnStoryTip)
end

local function fin()
	EventManager.Unsubscribe(Events.UI_STORYTIP,OnStoryTip)
end

_M.fin = fin
_M.initial = initial
_M.dont_destroy = false

return _M