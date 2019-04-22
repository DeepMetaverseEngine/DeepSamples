local _M = {}
_M.__index = _M
-- self.ui BaseUI实例例
-- self.ui.comps 节点访问器
-- self.ui.menu csharp MenuBase
local Util  = require "Logic/Util"
local UIUtil = require 'UI/UIUtil'
local PartnerUtil = require 'UI/Partner/FallenPartnerUtil'
local UIUtil = require 'UI/UIUtil'
local DisplayUtil = require("Logic/DisplayUtil")

local function Release3DEffect(self)
	if self.effect ~= nil then
		RenderSystem.Instance:Unload(self.effect)
		self.effect = nil
	end
end

local function Init3DEffect(self, parentCvs, pos2d, menuOrder, fileName)
	local transSet = TransformSet()
	transSet.Pos = Vector3(pos2d.x, -pos2d.y, 0)
	transSet.Parent = parentCvs.Transform
	transSet.Layer = Constants.Layer.UI
	transSet.LayerOrder = menuOrder
	self.effect = RenderSystem.Instance:PlayEffect(fileName, transSet)
end

local function Release3DModel(self)
	if self.assetLoader then
		self.assetLoader:Unload()
		self.assetLoader = nil
	elseif self.loaderid then
		self.loaderid:Discard()
	end
	if self and self.model then
		UI3DModelAdapter.ReleaseModel(self.model.Key)
		self.model = nil
	end
end

local function Init3DSngleModel(self, parentCvs,fixpos, scale, rotate,menuOrder,fileName,cb)
	self.model = UIUtil.InitFix3DSngleModel(parentCvs,fixpos, scale,rotate, menuOrder,fileName,true,cb)
end
-- local function Init3DSngleModel(self, parentCvs, pos2d, scale, menuOrder,fileName)
-- 	local info = UI3DModelAdapter.AddSingleModel(parentCvs, pos2d, scale, menuOrder,fileName)
-- 	self.model = info
-- end

local function ReloadModel(self,rank)

		if self == nil then
			return
		end
		local filename = self.curRoleData.avatar_res
		if not string.empty(filename) then
			Release3DModel(self)
			Release3DEffect(self)
			local fixposdata = string.split(self.curRoleData.pos_xy,',')
			local fixpos = {x = tonumber(fixposdata[1]),y =  tonumber(fixposdata[2])}-- 偏移坐标
		    local fixzoom = tonumber(self.curRoleData.zoom) -- 缩放比例
		    local rotate = 0
			Init3DSngleModel(self, self.ui.comps.cvs_model,fixpos, fixzoom, rotate,self.ui.menu.MenuOrder,filename,function(info)
				local path = PartnerUtil.GetRankEffect(self.curRoleData.god_id,rank)
				if not string.IsNullOrEmpty(path) then
					local arr = string.split(path, '/')
				    local name = arr[#arr]
				    arr = string.split(name, '.')
				    if #arr > 1 then table.remove(arr) end
				    name = table.concat(arr, '.')
					local id = FuckAssetObject.GetOrLoad(path,name, function(assetLoader)
						self.assetLoader = assetLoader
						--self.assetLoader.DontMoveToCache = true
						local gameobj = assetLoader.gameObject
        				UILayerMgr.SetLocalLayerOrder(gameobj, self.ui.menu.MenuOrder, false, 5)
        				gameobj.transform:SetParent(info.RootTrans)
        				gameobj.transform.localPosition = Vector3(0, 0, 0) 
        				gameobj.transform.localScale = Vector3(1,1,1)
					end)
					self.loaderid = FuckAssetLoader.GetLoader(id)
		    	else
		    		print("model is nil",self.curRoleData.god_id,rank)
				end
			end)
			local node = self.ui.comps.cvs_partnerup
			local width = node.Width/2
			local height = node.Height/2
			Init3DEffect(self, node, Vector2(width,height), self.ui.menu.MenuOrder, '/res/effect/ui/ef_ui_interface_advanced.assetbundles')
		end
		-- if self == nil then
		-- 	return
		-- end
		-- local filename = self.curRoleData.avatar_res
		-- --print("filename",filename)
		-- if not string.empty(filename) then
		-- 	local fixposdata = string.split(self.curRoleData.pos_xy,',')
		-- 	local fixpos = {x = tonumber(fixposdata[1]),y =  tonumber(fixposdata[2])}-- 偏移坐标
		--     local fixzoom = tonumber(self.curRoleData.zoom) -- 缩放比例

	 --        if self.lastAvatar ~= self.curRoleData.avatar_res then
		-- 		self.lastAvatar = self.curRoleData.avatar_res 
		-- 		Release3DModel(self)
		-- 		Init3DSngleModel(self, self.ui.comps.cvs_model, Vector2(self.ui.comps.cvs_model.Width/2+fixpos.x,self.ui.comps.cvs_model.Height+fixpos.y), fixzoom, self.ui.menu.MenuOrder,filename)
		-- 	end

		-- 	local model = PartnerUtil.GetRankEffect(self.curRoleData.god_id,rank)
		-- 	if model ~= nil then
		-- 		local params = {
		--         scale = Vector3(fixzoom, fixzoom, fixzoom),
		--         offsetPos = Vector2(fixpos.x,fixpos.y),
		--         cb = function(go)
		--         	if self.effect~= nil then
		--         		FuckAssetObject.Unload(self.effect)
		--         		self.effect = nil
		--         	end
		--            go.transform.localPosition = Vector3(0, 0, 0) 
		--            self.effect = go
		--         end
	 --   			}
	 --    		DisplayUtil.loadEffect(model,self.ui.comps.cvs_model, params)
	 --    	else
	 --    		print("model is nil",self.curRoleData.god_id,rank)
		-- 	end
			
		-- end

		
end

function _M.ShowRole(self,rank,power,pos)
	ReloadModel(self,rank)
	self.ui.comps['lb_skill'..pos].Text = string.format(Constants.Text.fallenpartner_skilllv,rank)
	self.ui.comps['lb_fight'..pos].Text = string.format(Constants.Text.fallenpartner_power,power)
	local rank = Constants.Text['fallenpartner_rank'..rank]
	PartnerUtil.SetNodeName(self.ui.comps['lb_lv'..pos],Util.GetText(self.curRoleData.god_name),rank,self.curRoleData.god_quality)
end


function _M.OnEnter( self)

	self.lastAvatar = nil
	self.curRoleData = self.ui.menu.ExtParam[1].PartnerData
	self.oldRoleData = self.ui.menu.ExtParam[1].OldPartnerData
	local advanceData = PartnerUtil.GetAdvanceData(self.oldRoleData.god_id,self.oldRoleData.Lv)
	power = PartnerUtil.GetPartnerPower(advanceData)
	self:ShowRole(advanceData.client_rank,power,1)
	advanceData = PartnerUtil.GetAdvanceData(self.curRoleData.god_id,self.curRoleData.Lv)
	power = PartnerUtil.GetPartnerPower(advanceData)
	self:ShowRole(advanceData.client_rank,power,2)
	self.ui.menu:SetFullBackground(UILayout.CreateUILayoutColor(UnityEngine.Color(0,0,0,0.5),UnityEngine.Color(0,0,0,0.5)))
	self.ui.menu.Enable = true
	self.ui.menu.IsInteractive = true
	self.ui.menu.event_PointerUp = function (sender, e)
			self.ui:Close()
	end
end


function _M.OnExit( self )
	Release3DEffect(self)
	Release3DModel(self)
end


function _M.OnInit( self )
	self.ui.comps.menu.Enable = false
	self.model = nil

	-- self.tbts = {}
	-- for i = 1,3 do
	-- 	local tbt = self.ui.comps['tbt_an'..i]
	-- 	tbt.UserData = i
	-- 	table.insert(self.tbts,tbt)
	-- end
end

return _M