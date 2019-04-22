local _M = {}
_M.__index = _M

local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local PagodaModel = require 'Model/PagodaModel'
local ItemModel = require 'Model/ItemModel'



local function LoadEffect(self,parent,filename,size,scale,duration,cb)
	local param =
	{
		DisableToUnload = true,
		Pos = Vector3(size[1]/2,-size[2]/2),
		Parent = parent.UnityObject.transform,
		LayerOrder = self.ui.menu.MenuOrder,
		Scale = scale,
		UILayer = true,
	}

	return Util.PlayEffect(filename,param,duration,cb)
end

local function UnLoadEffect(id)
	RenderSystem.Instance:Unload(id)
end

local function OpenBox(self,cb)
	if self.box then
		UnLoadEffect(self.box)
		self.box = nil
	end
	self.boxopen = LoadEffect(self,
			self.cvs_model,
			"/res/effect/ui/ef_ui_chest_open.assetbundles",
			{0,0},
			Vector3(1,1,1),
			3,
			function()
				if cb then
					cb()
				end
			end)
end

local function SetUIData(self,isUPEff)
	--if self.box == nil then
	--	if self.boxopen then
	--		UnLoadEffect(self.boxopen)
	--		self.boxopen = nil
	--	end
	--	self.box = LoadEffect(self,
	--			self.cvs_model,
	--			"/res/effect/ui/ef_ui_chest.assetbundles",
	--			{ 0, 0 },
	--			Vector3(1, 1, 1),
	--			true
	--	)
	--end
	
	local function showGetCost(self,isok,itemid)
		local detail = ItemModel.GetDetailByTemplateID(itemid)
		local item = {
			cur = isok and 1 or 0,
			detail = detail,
			id = itemid,
			need = 1,
		}
		local tipsparams = {
			circleQuality = true,
		}
		UIUtil.SetEnoughItemShowAndLabel(self, self.costCvs, nil, item,tipsparams)
	end
	
	
	local openOk = 0
	for i=1,#self.chestdata.cost.num do
		if isUPEff == true and self.effpos[i] == true then
			self.effs[i] = LoadEffect(self,
								self.ib_key[i],
								"/res/effect/ui/ef_ui_partner_activation.assetbundles",
								self.ib_key[i].Size2D,
								Vector3(1,1,1),1)
		end
		local countOK = false
		for k,v in pairs(self.boxdata.s2c_keyMap) do
			if self.chestdata.cost.id[i] == k then
				self.lb_key[i].Text =v..'/'..self.chestdata.cost.num[i]
				--钥匙的数量颜色
				if self.chestdata.cost.num[i] > v then
					self.lb_key[i].FontColor = GameUtil.RGB2Color(Constants.Color.Red1)
					countOK = false
				else
					openOk = openOk + 1
					self.lb_key[i].FontColor = GameUtil.RGB2Color(Constants.Color.Green4)
					countOK = true
				end
			end
		end
		self.ib_key[i].EnableChildren = true
		self.ib_key[i].IsInteractive = true
		self.costLabel = self.lb_key[i]
		self.costCvs = self.ib_key[i]
		showGetCost(self,countOK,self.chestdata.cost.id[i])
		
		-- 红点显示
		local temp = ItemModel.CountItemByTemplateID(self.chestdata.cost.id[i])
		if temp == 0 then
			self.effpos[i] = false
			self.lb_tip[i].Visible = false
		else
			self.effpos[i] = true
			self.lb_tip[i].Visible = true
		end

	end

	if openOk == 6 then
		self.btn_open.Enable = true
		self.btn_open.IsGray = false
	else
		self.btn_open.Enable = false
		self.btn_open.IsGray = true
	end

	

	
	if self.boxdata.s2c_havekeys then
		self.btn_usekey.Enable = true
		self.btn_usekey.IsGray = false
	else
		self.btn_usekey.Enable = false
		self.btn_usekey.IsGray = true
	end
end

function _M.OnEnter( self )
	self.box = nil
	self.boxopen = nil
	self.effs = {}
	self.effpos = {false,false,false,false,false,false}

	self.ib_chestopen.Visible = true
	self.ib_chestopened.Visible = false


	PagodaModel.GetBoxData(function(rsp)
		self.boxdata = rsp
		SetUIData(self,false)
	end)

	self.btn_usekey.Text = Constants.PagodaChestBtn.UseKeys

	self.btn_reward.TouchClick = function(sender)
		GlobalHooks.UI.OpenUI('PagodaRewardPreview', 0, self.chestdata.preview.id)
	end

	self.btn_usekey.TouchClick = function(sender)
		--镶嵌钥匙
		PagodaModel.GetKeyData(function()
			PagodaModel.GetBoxData(function(rsp)
				self.boxdata = rsp
				SetUIData(self,true)
				self.effpos = {false,false,false,false,false,false}
			end)
		end)
	end
	self.btn_open.TouchClick = function(sender)
		--开启宝匣
		self.ib_chestopen.Visible = false
		self.ib_chestopened.Visible = true
		PagodaModel.OpenBox(function()
			sender.Enable = false
			--OpenBox(self,function ()
				PagodaModel.GetBoxData(function(rsp)
					self.boxdata = rsp
					SetUIData(self,false)
					if self.timer then
						LuaTimer.Delete(self.timer)
					end
					self.timer = LuaTimer.Add(2000,function()
						self.ib_chestopen.Visible = true
						self.ib_chestopened.Visible = false
					end)
					sender.Enable = true
				end)
			--end)
		end)
	end
end

function _M.OnExit( self )
	if self.timer then
		LuaTimer.Delete(self.timer)
	end
	for i, v in pairs(self.effs) do
		UnLoadEffect(v)
		v = nil
	end
	if self.box then
		UnLoadEffect(self.box)
		self.box = nil
	end
	if self.boxopen then
		UnLoadEffect(self.boxopen)
		self.boxopen = nil
	end
	self.effs = nil
end


function _M.OnInit(self)
	
	self.chestdata = PagodaModel.GetPagodaBoxData()
	self.lb_name = UIUtil.FindChild(self.ui.comps.cvs_chest, 'lb_name', true)
	self.ib_key = {}
	self.lb_key = {}
	self.lb_tip = {}
	for i=1,#self.chestdata.cost.num do
		self.ib_key[i] = UIUtil.FindChild(self.ui.comps.cvs_chest, 'cvs_icon'..i, true)
		self.lb_key[i] = UIUtil.FindChild(self.ui.comps.cvs_chest, 'lb_key'..i, true)
		self.lb_tip[i] = UIUtil.FindChild(self.ui.comps.cvs_chest, 'lb_tip'..i, true)
	end
	self.btn_usekey = UIUtil.FindChild(self.ui.comps.cvs_chest, 'btn_usekey', true)
	self.btn_open = UIUtil.FindChild(self.ui.comps.cvs_chest, 'btn_open', true)
	self.btn_reward = UIUtil.FindChild(self.ui.comps.cvs_chest, 'btn_reward', true)
	self.ib_chestopen = UIUtil.FindChild(self.ui.comps.cvs_chest, 'ib_chestopen', true)
	self.ib_chestopened = UIUtil.FindChild(self.ui.comps.cvs_chest, 'ib_chestopened', true)
	self.cvs_model = UIUtil.FindChild(self.ui.comps.cvs_chest, 'cvs_model', true)

	self.ui.menu.ShowType = UIShowType.Cover
end


return _M