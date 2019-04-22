local _M = {}
_M.__index = _M

local Util = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local PagodaModel = require 'Model/PagodaModel'
local ActivityModel = require 'Model/ActivityModel'
local QuestUtil = require("UI/Quest/QuestUtil")
local DisplayUtil = require 'Logic/DisplayUtil'



local function LoadEffect(self,parent,clip,filename,pos,deg,duration)
	local param = 
				{
					Pos = pos,
					Deg = deg and deg or Vector3.zero,
					Clip = clip.Transform,
					DisableToUnload = true,
					Parent = parent.UnityObject.transform,
					LayerOrder = self.ui.menu.MenuOrder,
					Scale = Vector3(1,1,1),
					UILayer = true,
					Vectormove = {x = parent.Size2D[1],  y = parent.Size2D[2]}
				}
	
	return Util.PlayEffect(filename,param,duration)
end

local function UnLoadEffect(id)
	RenderSystem.Instance:Unload(id)
end

local function SetPreview(self,node,index,params)
	local itemData = ActivityModel.GetItemData(params.id[index])
	UIUtil.SetItemShowTo(node,itemData.atlas_id,itemData.quality,params.num[index])
	node.TouchClick = function( sender )
		UIUtil.ShowTips(self,sender,itemData.id)
	end
end

local function ShowRefrshImgEffc(self,node,i)
	self.effs[i] = LoadEffect(
			self,
			node,
			node.Parent,
			"/res/effect/ui/ef_ui_interface_miwen_triangle.assetbundles",
			Vector3(node.Size2D[1]/2,-node.Size2D[2]/2,0),
			Vector3(0,0,(2-i)*120),
			0
	)
end

local function SetStory(self,index)
	if self.index == 1 then
		self.btn_left.Visible = false
	else
		self.btn_left.Visible = true
	end

	if self.index == #self.storydata then
		self.btn_right.Visible = false
	else
		self.btn_right.Visible = true
	end
	
	for _, v in pairs(self.effs) do
		UnLoadEffect(v)
		v = nil
	end
	local tempdata = PagodaModel.GetPagodaStoryClueData(index)
	for i=1,3 do
		if tempdata[i] ~= nil then
			self.club[i].ib_clue.Visible = true
			self.club[i].lb_clue.Visible = true
			self.club[i].ib_effclue.Visible = true
			self.club[i].ib_clue.IsGray = true
			self.club[i].lb_clue.IsGray = true
			self.club[i].ib_effclue.IsGray = true
			self.club[i].lb_clue.Text = Util.GetText(tempdata[i].clue_name)
			self.club[i].lb_cluetips.Text = Util.GetText(tempdata[i].clue_desc)
			self.club[i].ib_line.Visible = true
			self.club[i].lb_cluetips.Visible = true
		else
			self.club[i].ib_line.Visible = false
			self.club[i].lb_cluetips.Visible = false
			self.club[i].ib_clue.Visible = false
			self.club[i].lb_clue.Visible = false
			self.club[i].ib_effclue.Visible = false
		end
	end

	if self.params[index].ClueList ~= nil and #self.params[index].ClueList ~= 0 then
		for i,v in ipairs(self.params[index].ClueList) do
			self.club[i].ib_clue.Visible = true
			self.club[i].lb_clue.Visible = true
			self.club[i].ib_effclue.Visible = true
			self.club[v].ib_clue.IsGray = false
			self.club[v].lb_clue.IsGray = false
			self.club[v].ib_effclue.IsGray = false
			ShowRefrshImgEffc(self,self.club[i].ib_effclue,v)
		end
	end
	self.lb_red_story.Visible = self.params[index].state == 2

	self.lb_name.Text = Util.GetText(self.storydata[index].story_name)
	
	self.btn_go.Text = Util.GetText(Constants.PagodaStoryBtn[self.params[index].state])

	if self.effectbtn then
		UnLoadEffect(self.effectbtn)
	end
	
	if self.params[index].state == 0 or self.params[index].state == 3 then
		self.btn_go.Enable = false
		self.btn_go.IsGray = true
	else
		self.btn_go.Enable = true
		self.btn_go.IsGray = false
	end

	if (#self.params[index].ClueList == #tempdata and self.params[index].state == 0) or self.params[index].state == 2 then
		self.btn_go.Enable = true
		self.btn_go.IsGray = false
		
		self.effectbtn = LoadEffect(
				self,
				self.btn_go,
				self.btn_go.Parent,
				"/res/effect/ui/ef_ui_frame.assetbundles",
				Vector3.zero,
				nil,
				true
		)
	end
	

	self.cvs_item.Visible = false
	UIUtil.ConfigHScrollPan(self.sp_reward,self.cvs_item, #self.storydata[index].item.id, function(node, index1)
		SetPreview(self,node,index1,self.storydata[index].item)
	end)
end

local function SetFirstIndex(self)
	for i, v in pairs(self.params) do
		if v.state == 2 then
			self.index = i
			break
		end
		local tempdata = PagodaModel.GetPagodaStoryClueData(i)
		if #v.ClueList == #tempdata and v.state == 0 then
			self.index = i
			break
		end
		if v.state == 1 then
			self.index = i
			break
		end
		if v.state == 0 then
			self.index = i
			break
		end
	end
end

function _M.OnEnter( self ,storyid)
	self.effs = {}
	self.index = 1
	PagodaModel.RequireStoryData(function(rsp)
		self.params = rsp.secretBookList
		if not storyid then
			SetFirstIndex(self)
		else
			if storyid then
				for i, _ in ipairs(self.params) do
					if i == storyid then
						self.index = i
					end
				end
			end
		end
		
		SetStory(self,self.index)
	end)
	


	self.btn_go.TouchClick = function(sender)
		if self.params[self.index].state == 0 then
			PagodaModel.ActiveStory(self.storydata[self.index].story_id,function()
				PagodaModel.RequireStoryData(function(rsp)
					self.params = rsp.secretBookList
				end)
			end)
		elseif self.params[self.index].state == 1 then
			QuestUtil.doQuestById(self.storydata[self.index].quest_id)
			local ui = GlobalHooks.UI.FindUI('PagodaMain')
			if ui then
				ui:Close()
			end
			self:Close()
		elseif self.params[self.index].state == 2 then
			PagodaModel.GetStoryReward(self.storydata[self.index].story_id,function()
				PagodaModel.RequireStoryData(function(rsp)
					self.params = rsp.secretBookList
					SetFirstIndex(self)
					SetStory(self,self.index)
				end)
			end)
		end
	end

	self.btn_left.TouchClick = function(sender)
		--if self.effs then
		--	for i, v in pairs(self.effs) do
		--		UnLoadEffect(v)
		--		v = nil
		--	end
		--end
		self.index = self.index - 1
		if self.index == 1 then
			sender.Visible = false
		end
		if self.btn_right.Visible == false then
			self.btn_right.Visible = true
		end
		SetStory(self,self.index)
	end

	self.btn_right.TouchClick = function(sender)
		--if self.effs then
		--	for i, v in pairs(self.effs) do
		--		UnLoadEffect(v)
		--		v = nil
		--	end
		--end
		self.index = self.index + 1
		if self.index == #self.storydata then
			sender.Visible = false
		end
		if self.btn_left.Visible == false then
			self.btn_left.Visible = true
		end
		SetStory(self,self.index)
	end
	
	self.ui.comps.btn_close.TouchClick = function(sender)
		GlobalHooks.UI.SetRedTips("pagoda",PagodaModel.pagodaStory)
		self:Close()
	end

end

function _M.OnExit( self )
	if self.effectbtn then
		UnLoadEffect(self.effectbtn)
		self.effectbtn = nil
	end
	if self.effs then
		for i, v in pairs(self.effs) do
			UnLoadEffect(v)
			v = nil
		end
	end
	self.effs = nil
	self.index = 1
end


function _M.OnInit(self)
	self.storydata = PagodaModel.GetPagodaStoryData()
	self.clubdata = PagodaModel.GetPagodaStoryData()
	self.lb_name = UIUtil.FindChild(self.ui.comps.cvs_background, 'lb_name', true)
	self.club = {}
	for i=1,3 do
		self.club[i] = {}
		self.club[i].ib_clue = UIUtil.FindChild(self.ui.comps.cvs_story, 'ib_clue'..i, true)
		self.club[i].lb_clue = UIUtil.FindChild(self.ui.comps.cvs_story, 'lb_clue'..i, true)
		self.club[i].ib_line = UIUtil.FindChild(self.ui.comps.cvs_story, 'ib_line'..i, true)
		self.club[i].lb_cluetips = UIUtil.FindChild(self.ui.comps.cvs_story, 'lb_cluetips'..i, true)
		self.club[i].ib_effclue = UIUtil.FindChild(self.ui.comps.cvs_story, 'ib_effclue'..i, true)
		self.club[i].lb_cluetips = UIUtil.FindChild(self.ui.comps.cvs_story, 'lb_cluetips'..i, true)
	end
	self.btn_left = UIUtil.FindChild(self.ui.comps.cvs_story, 'btn_left', true)
	self.btn_right = UIUtil.FindChild(self.ui.comps.cvs_story, 'btn_right', true)
	self.btn_go = UIUtil.FindChild(self.ui.comps.cvs_story, 'btn_go', true)
	self.lb_red_story = UIUtil.FindChild(self.ui.comps.cvs_story, 'lb_red_story', true)
	self.sp_reward = UIUtil.FindChild(self.ui.comps.cvs_story, 'sp_reward', true)
	self.cvs_item = UIUtil.FindChild(self.ui.comps.cvs_story, 'cvs_item', true)
	
	self.ui.menu.ShowType = UIShowType.Cover
	self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
end


return _M