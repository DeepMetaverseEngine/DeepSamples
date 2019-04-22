local _M = {}
_M.__index = _M

local PracticeModel = require 'Model/PracticeModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local ItemModel = require 'Model/ItemModel'
local QuestUtil = require 'UI/Quest/QuestUtil'

local USE_NEW_MODEL = false
local function Release3DModel(self)
	if self.model then
		if USE_NEW_MODEL then
			RenderSystem.Instance:Unload(self.model)
		else
			UI3DModelAdapter.ReleaseModel(self.model.Key)
		end
		self.model = nil
	end
end

local function Init3DModel(self, parentCvs, pos2d, scale, menuOrder, avatar, filter)
	if USE_NEW_MODEL then
		local t = {
			Parent = parentCvs.Transform,
			Pos = {x = pos2d.x,y = -pos2d.y},
			Scale = scale,
			LayerOrder = menuOrder,
			UILayer = true,
			Deg = {y = -180}
		}
		self.model = Util.LoadGameUnit(avatar, t)
	else
		local info = UI3DModelAdapter.AddAvatar(parentCvs, pos2d, scale, menuOrder, avatar, filter)
		self.model = info
		local trans = info.RootTrans
		UI3DModelAdapter.SetLoadCallback(info.Key,function(UIModelInfo)
			UIModelInfo.DynamicBoneEnable = true
		end)
		parentCvs.event_PointerMove = function(sender, data)
			local delta = -data.delta.x
			trans:Rotate(Vector3.up, delta * 1.2)
		end
	end
end

local function Release3DEffect( self )
	if self.effect then
		RenderSystem.Instance:Unload(self.effect)
		self.effect = nil
	end
end

local function Play3DEffect(self, parentCvs, pos2d, scale, menuOrder, fileName)
	local transSet = TransformSet()
	transSet.Pos = Vector3(pos2d.x, pos2d.y, 0)
	transSet.Scale = Vector3(scale, scale, scale)
	transSet.Parent = parentCvs.Transform
	transSet.Layer = Constants.Layer.UI
	transSet.LayerOrder = menuOrder
	self.effect = RenderSystem.Instance:PlayEffect(fileName, transSet)
end

local function RefreshSlider( self )
	local index = self.selectedIndex < self.data.practiceLv and 4 or self.data.stageLv
	self.scene:SetSlider(index)
	local dbStageLv = GlobalHooks.DB.Find('practice_stage', { artifact_stage = self.selectedIndex })
	for i = 0, 4 - 1 do
		self.scene:SetLightPoint(i, i < index)
		if i <= index then
			self.scene.pointgroup[i + 1].gameObject:SetActive(true)
			local powerStr = Util.GetText('common_fight')..' '..dbStageLv[i + 1].power
			local lvnameStr = Util.GetText(dbStageLv[i + 1].lv_name)
			self.scene:SetFightPower(i, powerStr, lvnameStr, HZUISystem.Instance.DefaultFont)
		else
			self.scene.pointgroup[i + 1].gameObject:SetActive(false)
		end
	end
	if index == 4 then
		self.scene:ShowUpEffect(true)
	else
		self.scene:ShowUpEffect(false)
	end
end

local function OnItemSelected( self, index )
	-- print('--------OnItemSelected---------', index)
	self.selectedIndex = index
	Release3DModel(self)

	if self.initFinish then 
		SoundManager.Instance:PlaySoundByKey('button',false)
	end
	
	if index <= self.data.practiceLv then
		self.scene.slider.gameObject:SetActive(true)
		self.ui.comps.cvs_model.Visible = false
		if index == self.data.practiceLv then
			self.ui.comps.btn_quest.Visible = self.data.needQuest
			self.ui.comps.cvs_use.Visible = not self.data.needQuest
			self.ui.comps.btn_quest.IsGray = self.data.stageLv < 4
			self.ui.comps.btn_use.IsGray = self.data.stageLv < 4 or self.data.practiceLv >= self.listLen
			self.ui.comps.btn_quest.Enable = self.data.stageLv == 4
			self.ui.comps.btn_use.Enable = self.data.stageLv == 4 and self.data.practiceLv < self.listLen
			self.ui.comps.lb_done.Visible = false
		else
			self.ui.comps.btn_quest.Visible = false
			self.ui.comps.cvs_use.Visible = false
			self.ui.comps.lb_done.Visible = true
		end
		RefreshSlider(self)
	else
		self.scene.slider.gameObject:SetActive(false)
		self.ui.comps.cvs_use.Visible = false
		self.ui.comps.btn_quest.Visible = false
		self.ui.comps.lb_done.Visible = false
		self.ui.comps.cvs_model.Visible = true
		--avatar
		local groupId = self.dbStage[index].group_id
		local pro = DataMgr.Instance.UserData.Pro
		local gen = DataMgr.Instance.UserData.Gender
		local modeldb = GlobalHooks.DB.FindFirst('practice_model', { group_id = groupId, pro = pro, sex = gen })
		local model = modeldb.modle
		local avatarSrc = {}
		local avatarmap = {}
		for k, v in pairs(model.key) do
			local partTag = v
			local assetname = model.value[k]
			if not string.IsNullOrEmpty(assetname) then
				avatarSrc[partTag] = assetname 
				avatarmap[Constants.AvatarPart[partTag]] = assetname
			end
		end
		local avatar = GameUtil.GetNewAvatar(avatarSrc, nil)
		if USE_NEW_MODEL then
			Init3DModel(self, self.ui.comps.cvs_model, self.comps.cvs_anchor.Position2D, 150, self.ui.menu.MenuOrder, avatarmap)
		else
			Init3DModel(self, self.ui.comps.cvs_model, self.ui.comps.cvs_anchor.Position2D, 150, self.ui.menu.MenuOrder, avatar, 0)
		end
	end

	if index > self.data.practiceLv then
		local groupId = self.dbStage[index].group_id
		local pro = DataMgr.Instance.UserData.Pro
		local gen = DataMgr.Instance.UserData.Gender
		local modeldb = GlobalHooks.DB.FindFirst('practice_model', { group_id = groupId, pro = pro, sex = gen })
		local reward = modeldb.reward
		local len = self.ui.comps.cvs_itemlist.NumChildren > #reward.id and self.ui.comps.cvs_itemlist.NumChildren or #reward.id
		local cvsRoot = self.ui.comps.cvs_itemlist
		cvsRoot.Visible = true
		local showItemCount = 0
		for i = 1, len do
			local cvs_item = cvsRoot:FindChildByName('cvs_item'..i, true)
			if i <= #reward.id and reward.id[i] > 0 then
				showItemCount = showItemCount + 1
				if cvs_item == nil then
					if i == 1 then
						cvs_item = cvsRoot:FindChildByEditName('cvs_item', true)
						cvs_item.Name = 'cvs_item1'
					else
						local prefab = cvsRoot:FindChildByName('cvs_item1', true)
						cvs_item = prefab:Clone()
						cvsRoot:AddChild(cvs_item)
						cvs_item.Name = 'cvs_item'..i
						cvs_item.X = prefab.X + (i - 1) * 80
						cvs_item.Y = prefab.Y
					end
				end
				cvs_item.Visible = true
				local itemdetail = ItemModel.GetDetailByTemplateID(reward.id[i])
				local icon = itemdetail.static.atlas_id
				local quality = itemdetail.static.quality
				local num = reward.num[i]
				local itshow = UIUtil.SetItemShowTo(cvs_item, icon, quality, num)
		        itshow.EnableTouch = true
		        itshow.TouchClick = function()
		            local detail = UIUtil.ShowNormalItemDetail({detail = itemdetail, itemShow = itshow, autoHeight = true})
		            detail:SetPos(30, 556,'l_b')
		        end
			else
				if cvs_item then
					cvs_item.Visible = false
				end
			end
		end
		self.ui.comps.ib_back.Width = 160 + (showItemCount - 1) * 80
	else
		self.ui.comps.cvs_itemlist.Visible = false
	end
end

local function OnSceneOrDataCallBack( self )
	if self.scene and self.data then
		--临时先改一版不转的
		self.scene:RotateTo(self.data.practiceLv - 1)
		-- self.scene:RotateTo(0)
		self.scene:SetButtonSelected(self.data.practiceLv - 1)
		for i = 0, self.listLen - 1 do
			local bgImg = self.scene:GetButtonImage(i, false)
			local lightImg = self.scene:GetButtonImage(i, true)
			GameUtil.ConvertToUnityUISpriteFromAtlas(bgImg, '$dynamic/TL_practice/output/TL_practice.xml|TL_practice|pa_'..(i+1))
			GameUtil.ConvertToUnityUISpriteFromAtlas(lightImg, '$dynamic/TL_practice/output/TL_practice.xml|TL_practice|pl_'..(i+1))
			if i < self.data.practiceLv then
				self.scene:SetButtonPressed(i, true)
			end
		end

		if self.data.practiceLv > 0 then
			RefreshSlider(self)
		else
			self.scene:SetSlider(self.data.stageLv)
			for i = 0, 4 - 1 do
				self.scene.pointgroup[i + 1].gameObject:SetActive(false)
			end
		end
		self.initFinish = true
	end
end

local function Scroll( self, flag, isStart ) --flag: 0 stop. 1 left. 2 right
	if self.scene then
		if isStart then
			if flag == 1 then
				self.scene:StartRotate(true)
			elseif flag == 2 then
				self.scene:StartRotate(false)
			end
			self.ui.comps.btn_left.Visible = true
			self.ui.comps.btn_right.Visible = true
		else
			local edge = self.scene:StopRotate()
			self.ui.comps.btn_left.Visible = edge ~= 1 
			self.ui.comps.btn_right.Visible = edge ~= 2
		end
	end
end

local function RefreshPracticeInfo( self, cb )
	PracticeModel.RequestClientGetPracticeInfo(function( rsp )
		self.data = {}
		self.data.fightPower = rsp.s2c_data.fightPower
		self.data.practiceLv = rsp.s2c_data.practiceLv
		self.data.stageLv = rsp.s2c_data.stageLv
		self.data.needQuest = rsp.s2c_data.needQuest
		self.ui.comps.lb_fight.Text = self.data.fightPower
		self.ui.comps.cvs_di.Visible = self.data.practiceLv > 0
		self.ui.comps.cvs_di2.Visible = self.data.practiceLv == 0
		if self.data.practiceLv > 0 then
			self.ui.comps.btn_quest.Visible = self.data.needQuest
			self.ui.comps.cvs_use.Visible = not self.data.needQuest
			self.ui.comps.btn_quest.IsGray = self.data.stageLv < 4
			self.ui.comps.btn_use.IsGray = self.data.stageLv < 4 or self.data.practiceLv >= self.listLen
			self.ui.comps.btn_quest.Enable = self.data.stageLv == 4
			self.ui.comps.btn_use.Enable = self.data.stageLv == 4 and self.data.practiceLv < self.listLen
		end
		self.ui.menu.Enable = self.data.practiceLv == 0
		OnSceneOrDataCallBack(self)
		if cb then
			cb()
		end
	end)
end

function _M.OnEnter( self )
	self.ui.comps.cvs_itemlist.Visible = false
	self.dbStage = GlobalHooks.DB.Find('practice', {})
	self.listLen = #self.dbStage
	self.initFinish = false

	local path = '/res/effect/ui/ef_ui_xiuxing.assetbundles'
	local id = FuckAssetObject.GetOrLoad(path, 'ef_ui_xiuxing', function(assetLoader)
		self.assetLoader = assetLoader
		self.assetLoader.DontMoveToCache = true
		self.scene = self.assetLoader:GetComponent('PracticeScene')
		self.scene:Init(self.listLen)
		self.scene:SetButtonListener(function( index )
			OnItemSelected(self, index)
		end)
		OnSceneOrDataCallBack(self)
		Scroll(self, 1, false)
	end)
	self.loader = FuckAssetLoader.GetLoader(id)

	RefreshPracticeInfo(self)

end

function _M.OnExit( self )
	if self.assetLoader then
		self.assetLoader:Unload()
		self.assetLoader = nil
		self.scene = nil
		self.data = nil
	elseif self.loader then
		self.loader:Discard()
	end
	self.loader = nil
	Release3DModel(self)
	Release3DEffect(self)
end

function _M.OnDestory( self )
	
end

local function OnLevelUp(self)
	local Api = EventApi
	Api.Task.StartEvent(function()
		local eid = Api.Task.StartEvent('Client/practicebreak',self.data.practiceLv)
		Api.Task.Wait(eid)

		PracticeModel.RequestClientPracticeUp(function( rsp )
			Play3DEffect(self, self.ui.menu, Vector2(0, 0), 1, self.ui.menu.MenuOrder, '/res/effect/ui/ef_ui_interface_advanced.assetbundles')
			SoundManager.Instance:PlaySoundByKey('jinjie',false)
			RefreshPracticeInfo(self, function()
				GlobalHooks.UI.OpenUI('PracticeReward', 0, self.data.practiceLv)
			end)
		end)
	end)
end

function _M.OnInit( self )
	self.ui.menu.ShowType = UIShowType.HideBackAll
	self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
	self.ui:EnableTouchFrame(false)

	HudManager.Instance:InitAnchorWithNode(self.ui.comps.btn_close, bit.bor(HudManager.HUD_LEFT, HudManager.HUD_TOP))
	-- HudManager.Instance:InitAnchorWithNode(self.ui.comps.btn_left, HudManager.HUD_LEFT)
	-- HudManager.Instance:InitAnchorWithNode(self.ui.comps.btn_right, HudManager.HUD_RIGHT)
	HudManager.Instance:InitAnchorWithNode(self.ui.comps.cvs_use, HudManager.HUD_BOTTOM)
	HudManager.Instance:InitAnchorWithNode(self.ui.comps.cvs_itemlist, bit.bor(HudManager.HUD_LEFT, HudManager.HUD_BOTTOM))
	HudManager.Instance:InitAnchorWithNode(self.ui.comps.btn_wen, bit.bor(HudManager.HUD_TOP, HudManager.HUD_RIGHT))
	HudManager.Instance:InitAnchorWithNode(self.ui.comps.cvs_title, HudManager.HUD_TOP)

	self.ui.comps.btn_wen.TouchClick = function()
		GlobalHooks.UI.OpenUI('PracticeHelp', 0)
	end

	self.ui.comps.btn_active.TouchClick = function()
		OnLevelUp(self)
	end

	self.ui.comps.btn_use.TouchClick = function()
		OnLevelUp(self)
	end

	self.ui.comps.btn_quest.TouchClick = function()
		if self.data.stageLv == 4 then
			local practiceLv = self.data.practiceLv
			PracticeModel.RequestClientPracticeQuest(function( rsp )
				local questId = self.dbStage[practiceLv].requireid
				QuestUtil.doQuestById(tonumber(questId))
				self.ui.menu:Close()
			end)
		end
	end

	self.ui.comps.btn_left.event_PointerDown = function()
		Scroll(self, 1, true)
	end
	self.ui.comps.btn_left.event_PointerUp = function()
		Scroll(self, 1, false)
	end
	self.ui.comps.btn_right.event_PointerDown = function()
		Scroll(self, 2, true)
	end
	self.ui.comps.btn_right.event_PointerUp = function()
		Scroll(self, 2, false)
	end
end

return _M