local _M = {}
_M.__index = _M

-- local ActivityUIBase = require 'UI/Business/ActivityUIBase'
-- setmetatable(_M,ActivityUIBase)


local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local BusinessModel = require 'Model/BusinessModel'
local ActivityModel = require 'Model/ActivityModel'

local Checkedindex = nil
local showedindex = nil


local function Init3DSngleModel(self, parentCvs, pos2d, scale, rotate,fileName,index)
	local info = UI3DModelAdapter.AddSingleModel(parentCvs, pos2d, scale, rotate,fileName)
	self.model[index] = info
	return info
end

local function Release3DModel(self,index)
	if self and self.model then
		if self.model[index] then
			UI3DModelAdapter.ReleaseModel(self.model[index].Key)
			self.model[index] = nil
		end
	end
end

function _M.ShowCanGetEffevt(self,shownode,index,state,sheetname)

end

local function SetNum(textnode,num)
	if num == 1 then
		textnode.Visible = false
	else
		textnode.Visible = true
		textnode.Text = num
	end
end

local function RefreshBtn(self,index)
	if self.btn_get then
		_M.ShowCanGetEffevt(self,self.btn_get,index,self.rspdata[index].state,'special')
		if self.rspdata[index].state == 0 then
			self.btn_get.Enable = false
			self.btn_get.IsGray = true
			self.lb_got.Visible = false
			self.btn_get.Visible = true
		elseif self.rspdata[index].state == 1 then
			self.btn_get.Enable = true
			self.btn_get.IsGray = false
	    	self.lb_got.Visible = false
	    	self.btn_get.Visible = true
	    else
			self.lb_got.Visible = true
	    	self.btn_get.Visible = false
	    end
	end
end

function _M.ShowGetRewardEffect(self,shownode,index,cb,sheetname)
	cb()
end

local function RequireGet(self,btn ,activityid ,subid ,setvisable ,index,itemnum,node,cb)
	ActivityModel.BagIsCanUse(itemnum,function()
		_M.ShowGetRewardEffect(self,node,index,function()
			BusinessModel.RequireGet(self.activitytype,activityid ,subid,function(rsp)
					if rsp.s2c_code == 200 then
						self.rspdata[index].state = 2
						if setvisable then
						setvisable.Visible = true
					end
					if self.sp_list then
						self.sp_list:RefreshShowCell()
					end
					RefreshBtn(self,Checkedindex)
					self.parentui.pan:RefreshShowCell()
					if cb then
						cb()
					end
				end
			end,self)
		end,self.activityinfo.sheet_name)
	end)
end

local function ShowDetail(self,activityinfo,index,node)
	showedindex = index
	local itemnum = 0
	for i=1,#activityinfo.showmodel do
		if self.cvs_showpic[i] and activityinfo.showpic[i] == '' then
			self.cvs_showpic[i].Layout = nil
		end
		Release3DModel(self,i)
		if activityinfo.showmodel[i] ~= '' then
			if self.cvs_showmodel[i] and activityinfo.showmodel[i] then
				local posxy = string.split(activityinfo.pos_xy[i], ",")
				local info = Init3DSngleModel(self,
						self.cvs_showmodel[i],
						Vector2(posxy[1],posxy[2]),
						tonumber(activityinfo.zoom[i]),
						self.ui.menu.MenuOrder,
						activityinfo.showmodel[i],i
				)
				info.Callback = function(model)
					model.DC.localEulerAngles = Vector3(0,activityinfo.rotate[i],0)
				end
			end
		end
		if self.cvs_showpic[i] and activityinfo.showpic[i] ~= '' then
			UIUtil.SetImage(self.cvs_showpic[i],activityinfo.showpic[i])
		end
	end
	

	for i=2,#activityinfo.show.item do
		if self.cvs_showitem[i] then
			if activityinfo.show.item[i] ~= 0 then
				self.lb_showitemnum[i].Visible = true
				self.cvs_showitem[i].Visible = true
				local itemData = ActivityModel.GetItemData(activityinfo.show.item[i])
				if itemData then
					itemnum = itemnum + 1
					UIUtil.SetItemShowTo(self.cvs_showitem[i],itemData.id,1,itemData.quality)
					SetNum(self.lb_showitemnum[i],activityinfo.show.itemnum[i])

					self.cvs_showitem[i].TouchClick = function(sender)
						UIUtil.ShowTips(self,sender,itemData.id)
					end
				end
			else
				if self.lb_showitemnum[i] then
					self.lb_showitemnum[i].Visible = false
				end
				self.cvs_showitem[i].Visible = false
			end 
		end
	end

	if self.btn_get then
		RefreshBtn(self,index)
		self.btn_get.TouchClick = function(sender)
			RequireGet(self,self.btn_get,self.activityid,activityinfo.id, nil,index,itemnum,node)
		end
	end 
	for i=1,#self.rspdata[index].requireList do
		if self.requireminmax[i] and self.rspdata[index].requireList[i].curVal then
			if self.rspdata[index].requireList[i].curVal/self.rspdata[index].requireList[i].minVal < 1 then
				self.requireminmax[i].FontColor = GameUtil.RGB2Color(Constants.Color.Red)
			else
				self.requireminmax[i].FontColor = GameUtil.RGB2Color(Constants.Color.Normal)
			end
			self.requireminmax[i].Text =Util.GetText('mail_list', self.rspdata[index].requireList[i].curVal, self.rspdata[index].requireList[i].minVal)
		end
		if self.requiremax[i] and self.rspdata[index].requireList[i].minVal then
			self.requiremax[i].Text = self.rspdata[index].requireList[i].minVal
		end
	end

end

function _M.SetTbnChecked(self,node,tbn,sheetname,index,state)

end

local function InitList(self,node, index)
	local cvs_showitem = node:FindChildByEditName('cvs_showitem[1]', true)
	local lb_showitemnum = node:FindChildByEditName('lb_showitemnum[1]', true)
    local lbtext1 = node:FindChildByEditName('lbtext1', true)
	local ib_get1 = node:FindChildByEditName('ib_get1', true)
	local tbn_level = node:FindChildByEditName('tbn_level', true)
	ib_get1.Visible = self.rspdata[index].state == 2
	tbn_level.IsChecked = index == Checkedindex
	
	local count = 0
	if self.rspdata[index].state == 1 then
		count = 1
	end
	GlobalHooks.UI.SetRedTips("business",count,self.rspdata[index].id)
	_M.SetTbnChecked(self,node,tbn_level,self.activityinfo.sheet_name,index,self.rspdata[index].state)

	UIUtil.SetItemShowTo(cvs_showitem,self.activityinfo[index].show.item[1], 0)
	SetNum(lb_showitemnum,self.activityinfo[index].show.itemnum[1])
    lbtext1.Text = Util.GetText(self.activityinfo[index].lbtext)

    tbn_level.TouchClick = function(sender)
		if Checkedindex ~= index then
            Checkedindex = index
    		ShowDetail(self,self.activityinfo[index],index,node)
            self.sp_list:RefreshShowCell()
        end
    end
end

local function CloneItem( self )
	local tempnode = nil
	for i=1,#self.activityinfo do
		local cvs_reward  = self.cvs_showitem[1].Parent
		self.cvs_reward[i] = self.cvs_showitem[1].Parent:Clone()
		local row = math.floor((i-1)/7)
		local col = (i-1)%7

		self.cvs_reward[i].X = cvs_reward.X + (cvs_reward.Size2D[1])*col
		self.cvs_reward[i].Y = cvs_reward.Y + (cvs_reward.Size2D[2])*row

		self.cvs_reward[i].Visible = true
		self.cvs_reward[i].UserTag = i
		self.ui.comps.cvs_reward:AddChild(self.cvs_reward[i])
		local lb_showitemnum = UIUtil.FindChild(self.cvs_reward[i], 'lb_showitemnum[1]', true)
		local btn_get = UIUtil.FindChild(self.cvs_reward[i], 'btn_get', true)
		local lbtext = UIUtil.FindChild(self.cvs_reward[i], 'lbtext', true)
		local lb_got = UIUtil.FindChild(self.cvs_reward[i], 'lb_got', true)
		local cvs_showitem = UIUtil.FindChild(self.cvs_reward[i], 'cvs_showitem[1]', true)

		lb_got.Visible = self.rspdata[i].state == 2
		self.cvs_reward[i].IsGray = self.rspdata[i].state == 0
		
		_M.ShowCanGetEffevt(self,cvs_showitem,i,self.rspdata[i].state,self.activityinfo.sheet_name)

		lbtext.Text = Util.GetText(self.activityinfo[i].lbtext)
		UIUtil.SetItemShowTo(cvs_showitem,self.activityinfo[i].show.item[1], 0)
		SetNum(lb_showitemnum,self.activityinfo[i].show.itemnum[1])

		btn_get.TouchClick = function(sender)
			if self.rspdata[i].state == 1 then
				RequireGet(self, btn_get,self.activityid,self.activityinfo[i].id,lb_got,i,1,self.cvs_reward[i],function()
					_M.ShowCanGetEffevt(self,cvs_showitem,i,self.rspdata[i].state,self.activityinfo.sheet_name)
				end)
			else
				UIUtil.ShowTips(self,sender,self.activityinfo[i].show.item[1])
			end
			if showedindex ~= i then
				ShowDetail(self, self.activityinfo[i],i,self.cvs_reward[i])
			end
		end
		if Checkedindex == i then
			tempnode = self.cvs_reward[i]
		end
	end
	ShowDetail(self,self.activityinfo[Checkedindex],Checkedindex,tempnode)
end

function _M.SetScrollPanHorV(self,tempnode,cb,sheetname)
	
end

local function SetUI(self)
	self.cvs_showitem[1].Parent.Visible = false

	if self.subindex then
		Checkedindex = self.subindex
	else
		for i, v in pairs(self.rspdata) do
			if v.state == 1 then
				Checkedindex = i
				break
			end
		end
		if not Checkedindex then
			for i, v in pairs(self.rspdata) do
				if v.state == 0 then
					Checkedindex = i
					break
				end
			end
		end
		if not Checkedindex then
			Checkedindex = 1
		end
	end
	
	
	if self.sp_list then
		local tempnode = self.cvs_showitem[1].Parent
		local temp = nil
		_M.SetScrollPanHorV(self,tempnode,function(self,node, index)
			if index == Checkedindex then
				temp = node
			end
			InitList(self,node, index)
		end,self.activityinfo.sheet_name)
		ShowDetail(self,self.activityinfo[Checkedindex],Checkedindex,temp)
	else
		CloneItem( self )
	end

end

function _M.OnEnter( self ,activityid,subindex)
	
	self.activityid = activityid
	Checkedindex = nil
	self.subindex = subindex

	BusinessModel.RequireData(self.activitytype,self.activityid,function(rsp)
        self.parentui.pan:RefreshShowCell()
		local temptable = {}
		for _,v in pairs(rsp.activityMap) do
			table.insert(temptable,v)
		end
		table.sort(temptable,function( a,b)
	        return a.id < b.id
	    end)
		self.rspdata = temptable
		SetUI(self)
	end)
	
end

function _M.OnInit( self ,activityinfo)
	self.activitytype = activityinfo.activitytype
	self.activityinfo = activityinfo
	self.cvs_reward = {}
	self.model = {}
	

	self.cvs_showitem={}
	self.sp_list = UIUtil.FindChild(self.ui.comps.cvs_reward, 'sp_levellist', true)

	self.cvs_showmodel = {}
	self.cvs_showpic = {}
	self.lb_showitemnum = {}
	for i=1,5 do
		self.lb_showitemnum[i] = UIUtil.FindChild(self.ui.root, 'lb_showitemnum['..i..']', true)
		self.cvs_showitem[i] = UIUtil.FindChild(self.ui.root, 'cvs_showitem['..i..']', true)
		self.cvs_showmodel[i] = UIUtil.FindChild(self.ui.root, 'cvs_showmodel['..i..']', true)
		self.cvs_showpic[i] = UIUtil.FindChild(self.ui.comps.cvs_show, 'cvs_showpic['..i..']', true)
	end

	self.btn_get = UIUtil.FindChild(self.ui.comps.cvs_show, 'btn_get', true)

	self.requireminmax = {}
	self.requiremax = {}
	for i=1,2 do
		self.requireminmax[i] = UIUtil.FindChild(self.ui.root, 'require'..i..'minmax', true)
		self.requiremax[i] = UIUtil.FindChild(self.ui.root, 'require'..i..'max', true)
	end
	self.lb_got = UIUtil.FindChild(self.ui.comps.cvs_show, 'lb_got', true)
end

function _M.UIExit(self)

end

function _M.OnExit( self )
	for i=1,#self.cvs_reward do
		self.cvs_reward[i]:RemoveFromParent(true)
		self.cvs_reward[i] = nil
	end
	GlobalHooks.UI.SetRedTips(BusinessModel.GetRedKey(self.activitytype),BusinessModel.cachedata[self.activitytype][self.activityid].count,self.activityid)
	_M.UIExit(self)
end


_M.Init3DSngleModel = Init3DSngleModel
_M.Release3DModel = Release3DModel
_M.RefreshBtn = RefreshBtn
_M.RequireGet = RequireGet
_M.ShowDetail = ShowDetail
_M.InitList = InitList
_M.CloneItem = CloneItem
_M.SetUI = SetUI
return _M

