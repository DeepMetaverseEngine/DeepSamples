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
		--print("filename",filename)
		if not string.empty(filename) then
			Release3DModel(self)
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
		end
		
end

function _M.ShowRole(self,rank)

	ReloadModel(self,rank)
	local rank = Constants.Text['fallenpartner_rank'..rank]
	PartnerUtil.SetNodeName(self.ui.comps.lb_name,Util.GetText(self.curRoleData.god_name),rank,self.curRoleData.god_quality)
end
function _M.OnEnter( self)
	print('-------------------PartnerShowOnEnter')
	self.lastAvatar = nil
	self.curRoleData = self.ui.menu.ExtParam[1].PartnerData
	self.ui.menu.Enable = true
	self.ui.menu.event_PointerUp = function(sender)
		self.ui:Close()
	end
	print_r(self.curRoleData)
    local function ToggleFunc(sender)        
        local tag = sender.UserData
       	self:ShowRole(tag*3)
    end	
    UIUtil.ConfigToggleButton(self.tbts, self.tbts[1], false, ToggleFunc)
end

function _M.OnExit( self )
	print('PartnerShowOnExit')
	Release3DModel(self)
end

function _M.OnDestory( self )
	print('PartnerShowOnDestory')
end

function _M.OnInit( self )
	print('PartnerShowOnInit')
	self.ui.comps.menu.Enable = false
	--self.ui.comps.cvs_partnershow.Enable = false
	self.model = nil

	self.tbts = {}
	for i = 1,3 do
		local tbt = self.ui.comps['tbt_an'..i]
		tbt.UserData = i
		table.insert(self.tbts,tbt)
	end
end

return _M