local _M = {}
_M.__index = _M

local FuncOpen = require "Model/FuncOpen"
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'


local function Release3DModel(self)
	if self.model then
		-- UI3DModelAdapter.ReleaseModel(self.model.Key)
		RenderSystem.Instance:Unload(self.model)
		self.model = nil
	end
end

local function Play3DEffect(self, parentCvs, pos2d, scale, menuOrder, fileName, cb)
	local transSet = {}
	transSet.Pos = Vector3(pos2d.x, pos2d.y, 0.01)
	transSet.Scale = Vector3(scale, scale, scale)
	transSet.Parent = parentCvs.Transform
	transSet.Layer = Constants.Layer.UI
	transSet.LayerOrder = menuOrder
	self.model = Util.PlayEffect(fileName, transSet, cb)
end

local function DoScaleAction(self, node, scale, duration, isCenter, cb)
	local originScale = node.Scale
	local scaleAction = ScaleAction()
	scaleAction.ScaleX = scale
	scaleAction.ScaleY = scale
	if isCenter then
		scaleAction.ScaleType = ScaleAction.ScaleTypes.Center
	end
	scaleAction.Duration = duration
	node:AddAction(scaleAction)
	scaleAction.ActionFinishCallBack = cb
end

local function DoMoveAction(self, node, target, duration, cb)
	local v  = target:LocalToGlobal()
	local v1 = node.Parent:GlobalToLocal(v, true)
	local moveAction = MoveAction()
	moveAction.TargetX = v1.x
	moveAction.TargetY = v1.y
	moveAction.Duration = duration
	-- moveAction.ActionEaseType = EaseType.easeOutBack
	node:AddAction(moveAction)
	moveAction.ActionFinishCallBack = cb
end

local function DoFadeAction(self, node, duration, cb)
	local alphaAction = FadeAction()
	alphaAction.TargetAlpha = 0
	alphaAction.Duration = duration
	node:AddAction(alphaAction)
	alphaAction.ActionFinishCallBack = cb
end

local function DoDelayAction(self, node, duration, cb)
	local delayAction = DelayAction()
	delayAction.Duration = duration
	node:AddAction(delayAction)
	delayAction.ActionFinishCallBack = cb
end

local function SetData(self, funcInfo, finishCb)
	-- print("FuncOpenUI.SetData")
	self.finishCb = finishCb
	self.menu.Visible = true
	local fType = funcInfo.menu_type

	local frame = self.menu:FindChildByEditName("cvs_openfun", true)
	if fType == 1 then --顶层hud
		EventManager.Fire("Event.Hud.ShowTopHud", { isShow = true, showAnime = false })
	elseif fType == 2 then --二级hud
		EventManager.Fire("Event.Hud.ShowFunctionMenu", { isShow = true, showAnime = false, reset = true })
	end

	local funName = Util.GetText(funcInfo.name)
	self.ui.comps.lb_name.Text = Util.GetText('common_openfun', funName)
	local icon = self.menu:FindChildByEditName("ib_icon", true)
	UIUtil.SetImage(icon, funcInfo.icon)
	Play3DEffect(self, self.ui.comps.ib_ef, Vector2(0, 0), 1, self.ui.menu.MenuOrder, '/res/effect/ui/ef_ui_functionopen_01.assetbundles')
	DoScaleAction(self, icon, 1.2, 0.3, true, function()
		DoDelayAction(self, frame, 2, function()
			local ui
			--根据类型取出对应UI
			if fType == 1 or fType == 3 then --主界面
				ui = HudManager.Instance:GetHudUI("MainHud")
			elseif fType == 2 then --二级hud
				ui = HudManager.Instance:GetHudUI("FunctionHud")
			else --其他
				ui = nil
			end
			--根据UI和名字取出对应控件
			local target = nil
			if ui ~= nil then
				target = ui:FindChildByEditName(funcInfo.comp, true)
			end
			if target ~= nil then --飞图标
				local moveNode = self.menu:FindChildByEditName("cvs_menu1", true)
				moveNode.Size2D = target.Size2D
				icon.Size2D = target.Size2D
				icon.Transform.pivot = target.Transform.pivot
				Release3DModel(self)
				DoScaleAction(self, icon, 1, 0.4, true, nil)
				DoMoveAction(self, moveNode, target, 0.6, function()
					icon.Visible = funcInfo.open_type ~= 4
					Play3DEffect(self, self.ui.comps.ib_ef, Vector2(0, 0), 1, self.ui.menu.MenuOrder, '/res/effect/ui/ef_ui_functionopen_02.assetbundles', function( ... )
						self.ui:Close()
					end)
				end)
			else --结束
				self.ui:Close()
			end
		end)
	end)
end

function _M.OnEnter( self )
	self.eventid = EventApi.Task.StartEvent(function()
		EventApi.Task.BlockActorAutoRun()
	end)
	print("FuncOpenUI OnEnter",self.eventid)
end

function _M.OnExit( self )
	print("FuncOpenUI OnExit",self.eventid)
	if self.finishCb then
		self.finishCb()
		self.finishCb = nil
	end
	Release3DModel(self)
	EventApi.Task.StopEvent(self.eventid)
end

function _M.OnDestory( self )
    
end

function _M.OnInit( self )
	self.ui.menu.ShowType = UIShowType.Cover
end

_M.SetData = SetData

return _M
