local _M = {}
_M.__index = _M


local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local ActivityUIBase = require 'UI/Business/ActivityUIBase'
setmetatable(_M,ActivityUIBase)

local nodepos = nil
local effects = {}
local effc = nil

local function LoadEffect(self,pan,parent,filename,pos,cb)
	local param =
	{
		Pos = pos,
		Clip = pan.Transform,
		Parent = parent.UnityObject.transform,
		LayerOrder = self.ui.menu.MenuOrder,
		Scale = Vector3(1,1,1),
		UILayer = true,
		Vectormove = {x = parent.Size2D[1],  y = parent.Size2D[2]}
	}

	return Util.PlayEffect(filename,param,true,cb)
end

local function UnLoadEffect(id)
	RenderSystem.Instance:Unload(id)
end

function ActivityUIBase.SetTbnChecked(self,node,tbn,sheetname,index,state)
	if sheetname == 'common_level' then
		if not nodepos then
			nodepos = node.Y
		end
		if tbn.IsChecked == true then
			node.Y = nodepos
		else
			node.Y = nodepos - 15
		end

		if state == 1 and node.UserTag == 0 then
			node.UserTag = LoadEffect(self,
					self.sp_list,
					node,
					"/res/effect/ui/ef_ui_reward_receive.assetbundles",
					Vector3(node.Size2D[1]/2,-node.Size2D[2]/2-15,0))
		elseif state ~= 1 and node.UserTag ~= 0 then
			UnLoadEffect(node.UserTag)
			node.UserTag = 0
		end
	end
end

function ActivityUIBase.ShowCanGetEffevt(self,shownode,index,state,sheetname)
	if sheetname ~= 'special' then
		if effects[index] then
			UnLoadEffect(effects[index])
			effects[index] = nil
		end
		if state == 1 then
			effects[index] = LoadEffect(
					self,
					shownode.Parent,
					shownode,
					"/res/effect/ui/ef_ui_frame.assetbundles",
					Vector3(0,0,0))
		end
	else
		if effc then
			UnLoadEffect(effc)
			effc = nil
		end
		if state == 1 then
			effc = LoadEffect(
					self,
					shownode.Parent,
					shownode,
					"/res/effect/ui/ef_ui_frame.assetbundles",
					Vector3(0,0,0))
		end
	end
end

function ActivityUIBase.UIExit(self)
	for _, v in pairs(effects) do
		if v then
			UnLoadEffect(v)
			v = nil
		end
	end
	if effc then
		UnLoadEffect(effc)
		effc = nil
	end
	effects = {}
end

function ActivityUIBase.SetScrollPanHorV(self,tempnode,cb,sheetname)
	if sheetname == 'h' then
		UIUtil.ConfigVScrollPan(self.sp_list,tempnode, #self.activityinfo, function(node, index)
			cb(self,node, index)
		end)
	else
		UIUtil.ConfigHScrollPan(self.sp_list,tempnode, #self.activityinfo, function(node, index)
			cb(self,node, index)
		end)
	end
end

return _M 