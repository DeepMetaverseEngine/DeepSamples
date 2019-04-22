local _M = {}
_M.__index = _M


local UIUtil = require 'UI/UIUtil'
local ActivityModel = require 'Model/ActivityModel'
local Util = require 'Logic/Util'
local DisplayUtil = require"Logic/DisplayUtil"




local CheckIndex = 1
local roleLevel = nil
local modelname = nil
local CheckedActivityType = 1
local CheckedSubindex = nil


local function LoadEffect(self,parent,pan,filename,pos,scale)
	local param =
	{
		Pos = pos,
		Clip = pan.Transform,
		DisableToUnload = true,  --显示状态未Disable时自动Unload
		Parent = parent.UnityObject.transform,
		LayerOrder = self.ui.menu.MenuOrder,
		Scale = scale,
		UILayer = true,          --和 Layer = Constants.Layer.UI相同效果
		Vectormove = {x = parent.Size2D[1],  y = parent.Size2D[2]}
	}

	return Util.PlayEffect(filename,param,true)
end

local function UnLoadEffect(id)
	RenderSystem.Instance:Unload(id)
end

local function Init3DSngleModel(self, parentCvs, pos2d, scale, menuOrder,fileName)
	
	local info = UI3DModelAdapter.AddSingleModel(parentCvs, pos2d, scale, menuOrder,fileName)
	self.model = info
	return info
end

local function SetItemHide(self)
	for i=1,#self.reward_item do
		self.reward_item[i].Visible = false
	end
end

local function Release3DModel(self)

	if self and self.model then
		--print("Release3DModel"..self.model.Key)
		UI3DModelAdapter.ReleaseModel(self.model.Key)
		self.model = nil
		modelname = nil
	end
end

local function SetDetailInfo(self,activityItem,activityType)
	self.lb_npcname.Text = Util.GetText(activityItem.npc_name)
	self.tb_npctalk.Text = Util.GetText(activityItem.npc_talk)
	self.lb_form.Text = Util.GetText(activityItem.activity_form)
	self.lb_time.Text = Util.GetText(activityItem.activity_time)
	self.lb_level.Text = activityItem.player_lv
	self.ib_title.Text = Util.GetText(activityItem.activity_name)

	if activityItem.play_num == -1 then
		self.lb_num.Text = Util.GetText(Constants.Activity.Count)
	else
		self.lb_num.Text = activityItem.cur_val/activityItem.point_single..'/'..activityItem.play_num
	end	
	self.tb_des.Text = Util.GetText(activityItem.activity_desc)
	
	if ActivityModel.IsOpenLevel(activityItem,roleLevel) == false then
		self.btn_go.Visible = false
	else
		local opentime,isopen = nil,nil
		if activityType == 1 then
			isopen = true
		else
			opentime,isopen = ActivityModel.OpenTime(activityItem.function_id)
		end
		if isopen then
			self.btn_go.Visible = true
		else
			self.btn_go.Visible = false
		end
	end

	self.btn_go.TouchClick = function(sender)
		if activityItem.function_id then
			FunctionUtil.OpenFunction(activityItem.function_id)
			self:Close()
		end
	end

	self.cvs_item1.Visible = false

	for i=1,#activityItem.reward.id do
		if activityItem.reward.id[i] ~= 0 then
			self.reward_item[i].Visible = true
			local itemData = ActivityModel.GetItemData(activityItem.reward.id[i])
			if itemData then
				UIUtil.SetItemShowTo(self.reward_item[i],itemData.id)
				
				self.reward_item[i].TouchClick = function( sender )
				    UIUtil.ShowTips(self,sender,itemData.id)
				end
			end
		else
			self.reward_item[i].Visible = false
		end
	end
	
    local filename = activityItem.npc_model
	if filename ~= modelname then
    	local fixzoom = tonumber(activityItem.zoom) -- 缩放比例
		Release3DModel(self)
		modelname = filename
		local pos = string.split(activityItem.pos_xy,',')
		local fixpos = {x = tonumber(pos[1]),y = - tonumber(pos[2])}-- 偏移坐标
			local info = Init3DSngleModel(self, self.cvs_anchor, Vector2(fixpos.x,fixpos.y), fixzoom, self.ui.menu.MenuOrder,filename)
			info.Callback = function(model)
				model.DC.localPosition = Vector3(0,0,-2.5)
			end
	end
end

local function InitData(self,node,index,activityType,activitydata)
	--列表控件获取
	local tbt_select = node:FindChildByEditName('tbt_select', true)
	local cvs_icon = node:FindChildByEditName('cvs_icon', true)
	local lb_tuijian = node:FindChildByEditName('lb_tuijian', true)
	local lb_name = node:FindChildByEditName('lb_name', true)
	local lb_huoli = node:FindChildByEditName('lb_huoli', true)
	local lb_time = node:FindChildByEditName('lb_time', true)
	local lb_zudui = node:FindChildByEditName('lb_zuidui', true)
	local lb_finish = node:FindChildByEditName('lb_finish', true)
	
	UIUtil.SetImage(cvs_icon,activitydata.activity_icon,false,UILayoutStyle.IMAGE_STYLE_BACK_4)

	lb_tuijian.Visible = activitydata.recommend == 1
	lb_name.Text = Util.GetText(activitydata.activity_name)
	lb_huoli.Text = Util.Format1234(Constants.Activity.DoneState,tostring(activitydata.cur_val),tostring(activitydata.target_val))


    lb_finish.Visible = false
	if ActivityModel.IsOpenLevel(activitydata,roleLevel) then
		if activitydata.cur_val<activitydata.target_val or activitydata.target_val == 0 then
            node.IsGray = false
		else
            node.IsGray = true
            lb_finish.Visible = true
		end
		local _,_,showindex = ActivityModel.OpenTime(activitydata.function_id)
		if activityType ~= 1 then
			lb_time.Text = Util.Format1234(Constants.Activity.ActivityTime, ActivityModel.GetOpenTime(activitydata.function_id).time.open[showindex], ActivityModel.GetOpenTime(activitydata.function_id).time.close[showindex])
		end
	else
        node.IsGray = true
		lb_time.Text = Util.Format1234(Constants.Activity.PlayerNeedLevel, activitydata.player_lv)
	end
	
	lb_zudui.Visible = activitydata.team == 1
	node.UserData = activitydata.function_id
	tbt_select.IsChecked = index == CheckIndex
	tbt_select.TouchClick = function(sender)
		sender.IsChecked = true
		if CheckIndex ~= index then
			CheckIndex = index
			self.pan:RefreshShowCell()
			SetItemHide(self)
			SetDetailInfo(self,activitydata,activityType)
		end
	end
end

local function SetScrollList(self,activityType,subindex,activityindex)
	--self.activity = ActivityModel.GetActivityData(self.rsp,activityType)
	--如果每日活动玩法数量大于0
	CheckIndex = activityindex
	if #self.activity > 0 then
		local tempshow
		if subindex == 1 then
			tempshow = self.activity
		else
			tempshow = self.showlist[activityType][subindex]
		end
		if #tempshow > 0 then
			SetDetailInfo(self,tempshow[activityindex],activityType)
			UIUtil.ConfigVScrollPan(self.pan,self.tempnode[activityType], #tempshow, function(node, index)
				InitData(self,node,index,activityType,tempshow[index])
			end)
			DisplayUtil.lookAt(self.pan,activityindex)
			self.cvs_activityinfo.Visible = true
		else
			Release3DModel(self)
			UIUtil.ConfigVScrollPan(self.pan,self.tempnode[activityType], 0, function(node, index) end)
			self.cvs_activityinfo.Visible = false
		end
	else
		Release3DModel(self)
		UIUtil.ConfigVScrollPan(self.pan,self.tempnode[activityType], 0, function(node, index) end)
		self.cvs_activityinfo.Visible = false
	end
	self.tempnode[1].Visible = false
	self.tempnode[2].Visible = false
end

local function SetSubNode(self,activityType)
    if CheckedActivityType == activityType then
        return
    end
	self.activity = ActivityModel.GetActivityData(self.rsp,activityType)
    
    self.showlist[activityType] = {}
    for i1, v1 in pairs(self.subindexdata[activityType]) do
        self.showlist[activityType][i1] = {}
        if activityType == 1 then
            for _, v3 in pairs(v1.activity.id) do
                if v3 == 0 then
                    break
                end
                for _, v4 in pairs(self.activity) do
                    if v3 == v4.id then
                        table.insert(self.showlist[activityType][i1],v4)
                    end
                end
            end
        elseif activityType == 2 then
            for _, v2 in pairs(self.activity) do
                if v2.activitystate == i1 then
                    table.insert(self.showlist[activityType][i1],v2)
                end
            end
        end
    end


	local gotosub = 1
	if type(self.gotosub) == "number" and self.gotosub > 0 then
		gotosub = self.gotosub
		self.gotosub = nil
	end
	
	local index = 1
	if not string.IsNullOrEmpty(self.goto_function) then
		for i, v in ipairs(self.showlist[activityType][gotosub]) do
			if v.function_id == self.goto_function then
				index = i
				break
			end
		end
		self.goto_function = nil
	end
	SetScrollList(self,activityType,gotosub,index)
	CheckedSubindex = gotosub
	
    if CheckedActivityType ~= activityType then
        for i, v in pairs(self.subTbn) do
            v.UserTag = 0
            v.Visible = false
        end
        for i1, v1 in pairs(self.subindexdata[activityType]) do
            for _, v3 in pairs(self.subTbn) do
                if v3.UserTag == 0 then
                    if #self.showlist[activityType][i1] == 0 and i1 ~= 1 then
                        break
                    end
                    v3.Visible = true
                    v3.IsChecked = i1 == CheckedSubindex
                    v3.UserTag = i1
                    v3.Text = Util.GetText(v1.activity_sub_name)
                    v3.TouchClick = function(sender)
                        sender.IsChecked = true
                        if CheckedSubindex == i1 then
                            return
                        end
                        CheckedSubindex = i1
                        for _, v in pairs(self.subTbn) do
                            v.IsChecked = v.UserTag == CheckedSubindex
                        end
                        SetScrollList(self,activityType,i1,1)
                    end
                    break
                end
            end
        end
        CheckedActivityType = activityType
    else
        for i, v in pairs(self.subTbn) do
            v.IsChecked = false
        end
    end
end

local function EnterUI( self )
	self.showlist = {}
	local cvs_item_Size2D_X = self.cvs_item1.Size2D[1]
	self.reward_item = {}
	
	for i=1,5 do
		self.reward_item[i] = self.cvs_item1:Clone()
		self.cvs_itemlist:AddChild(self.reward_item[i])
		self.reward_item[i].X = (5 + cvs_item_Size2D_X) * (i - 1) - 5
		self.reward_item[i].Y = 0
		self.reward_item[i].Visible = false
	end
	
	local subindexdata = ActivityModel.GetSubData()
	self.subindexdata = {}
	for i, v in pairs(subindexdata) do
		if not self.subindexdata[v.activity_type] then
			self.subindexdata[v.activity_type] = {}
		end
		self.subindexdata[v.activity_type][v.activity_sub_type] = v
	end
	CheckedActivityType = 0
	self.tbn_limit.IsChecked = false
	self.tbn_daily.IsChecked = true
	
	--设置滚动列表
	local gototype = 1
	if self.gototype == 1 or self.gototype == 2 then
		gototype = self.gototype
		self.gototype = nil
	end
	SetSubNode(self,gototype)

	--按键的点击处理
	self.btn_close.TouchClick = function( sender )
		self.ui:Close()
	end

	self.tbn_daily.TouchClick = function( sender )
		if sender.IsChecked then
            self.tbn_limit.IsChecked = false
        else
			sender.IsChecked = true
		end
		SetSubNode(self,1)
	end

	self.tbn_limit.TouchClick = function( sender )
		if sender.IsChecked then
            self.tbn_daily.IsChecked = false
        else
			sender.IsChecked = true
		end
		SetSubNode(self,2)
	end

	self.btn_list.TouchClick = function( sender )
		UIUtil.ShowOkAlert()
	end
	
	local detail = GlobalHooks.DB.GetFullTable('Activity_reward')
	self.gg_plan:SetGaugeMinMax(0,detail[#detail].activity_point)
	local activityValue = self.rsp.ActivityPoint
	self.lb_activity_value.Text = activityValue
    self.gg_plan.Value = activityValue>140 and 140 or activityValue
	if detail ~= nil then
		for i=1,#self.item do
			local itemValue = UIUtil.FindChild(self.item[i], 'ib_huoli', true)
			itemValue.Text = tostring(detail[i].activity_point)
			local itemData = ActivityModel.GetItemData(detail[i].reward.id[1])
			local imagenode = UIUtil.SetItemShowTo(self.item[i],itemData.id,detail[i].reward.num[1])
			imagenode.Transform:SetSiblingIndex(0)--设置子物体的顺序

			if self.geteffc then
				if self.geteffc[i] then
					UnLoadEffect(self.geteffc[i])
					self.geteffc[i] = nil
				end
			end
			
			--设置未能领取的icon
			if detail[i].activity_point > activityValue then --活跃度不够的icon置灰
				self.item[i].IsGray = true
				self.getreward[i].Visible=false
			else--活跃度达到的icon
				if self.rsp.RewardRecord[detail[i].activity_point] == 0 then --未领取
					self.item[i].IsGray = false
					
					self.geteffc[i] = LoadEffect(self,
							self.item[i],
							self.item[i].Parent,
							"/res/effect/ui/ef_ui_frame.assetbundles",
							Vector3(0,0,0),
							Vector3(1,1,1))
					self.getreward[i].Visible=false
				elseif self.rsp.RewardRecord[detail[i].activity_point] == 1 then --已领取
					self.item[i].IsGray = false
					self.getreward[i].Visible=true
				end
			end

			self.item[i].TouchClick = function( sender )
				if detail[i].activity_point > activityValue then --未达到的icon点击显示物品tips
			    	UIUtil.ShowTips(self,sender,itemData.id)
				else
					if self.rsp.RewardRecord[detail[i].activity_point] == 0 then
						ActivityModel.BagIsCanUse(1,function()
							ActivityModel.SetActivityUserData(detail[i].activity_point,function(res)
								self.rsp.RewardRecord[detail[i].activity_point] = 1
								if self.geteffc[i] then
									ActivityModel.activitycount = ActivityModel.activitycount - 1
									GlobalHooks.UI.SetRedTips("activity",ActivityModel.activitycount)
									UnLoadEffect(self.geteffc[i])
									self.geteffc[i] = nil
								end
								self.getreward[i].Visible=true
								self.item[i].IsGray = false
							end)
						end)
					end
				end
			end
		end
	end
end

function _M.OnEnter( self,activity_type,subindex,function_id )
	self.geteffc = {}
	self.gototype = activity_type
	self.gotosub = subindex
	self.goto_function = function_id

	for i = 1, 5 do
		self.subTbn[i].TextSprite.Graphics.horizontalOverflow = UnityEngine.HorizontalWrapMode.Wrap
	end
	
	roleLevel = DataMgr.Instance.UserData:TryGetIntAttribute(UserData.NotiFyStatus.LEVEL, 0)
	ActivityModel.GetActivityUserData(function(rsp)
		self.rsp = rsp.s2c_data
		EnterUI(self)
	end)
	
	--SoundManager.Instance:PlaySoundByKey('uiopen',false)

end

function _M.OnInit( self )
	self.cvs_activityinfo = self.ui.comps.cvs_activityinfo
	self.cvs_activitylist = self.ui.comps.cvs_activitylist
	self.cvs_anchor = self.ui.comps.cvs_anchor
	self.cvs_huoli = self.ui.comps.cvs_huoli
	self.btn_close = self.ui.comps.btn_close
	self.ib_bg = self.ui.comps.ib_bg

	self.tbn_daily = self.comps.tbn_daily
	self.tbn_limit = self.comps.tbn_limit
	self.tbn_limit.IsChecked = false
	self.tbn_daily.IsChecked = true
	self.pan = self.ui.comps.sp_list
	self.tempnode = {}
	self.tempnode[1] = self.ui.comps.cvs_info
	self.tempnode[2] = self.ui.comps.cvs_info1
	self.lb_npcname = self.ui.comps.lb_npcname
	self.tb_npctalk = self.ui.comps.tb_npctalk
	self.lb_form = self.ui.comps.lb_form
	self.lb_time = self.ui.comps.lb_time2
	self.lb_level = self.ui.comps.lb_level
	self.ib_title = self.ui.comps.ib_title
	self.lb_num = UIUtil.FindChild(self.cvs_activityinfo, 'lb_num2', true)
	self.tb_des = self.ui.comps.tb_des
	self.cvs_item1 = self.ui.comps.cvs_item1
	self.cvs_itemlist = self.ui.comps.cvs_itemlist
    self.gg_plan = UIUtil.FindChild(self.cvs_huoli, 'gg_plan', true)
    self.lb_activity_value = UIUtil.FindChild(self.cvs_huoli, 'lb_num', true)
    self.item = {}
	self.getreward={}
	for i = 1, 7 do
		self.item[i] = UIUtil.FindChild(self.cvs_huoli, 'cvs_item'..i, true)
		self.getreward[i] = UIUtil.FindChild(self.cvs_huoli, 'ib_get'..i, true)
	end
	self.btn_go = UIUtil.FindChild(self.cvs_activityinfo, 'btn_go', true)
	self.cvs_item1.Visible = false
	self.btn_list = UIUtil.FindChild(self.cvs_huoli, 'btn_list', true)

	self.subTbn = {}
	for i = 1, 5 do
		self.subTbn[i] = self.root:FindChildByEditName('tbt_subtype'..i, true)
		self.subTbn[i].TextSprite.Graphics.horizontalOverflow = UnityEngine.HorizontalWrapMode.Wrap
	end
	
	HudManager.Instance:InitAnchorWithNode(self.cvs_activitylist, bit.bor(HudManager.HUD_LEFT))
	HudManager.Instance:InitAnchorWithNode(self.cvs_activityinfo, bit.bor(HudManager.HUD_RIGHT,HudManager.HUD_TOP))
	HudManager.Instance:InitAnchorWithNode(self.cvs_huoli, bit.bor(HudManager.HUD_LEFT,HudManager.HUD_BOTTOM))

	self.ui.menu.ShowType = UIShowType.HideHudAndMenu
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)

    UILayerMgr.SetPositionZ(self.ib_bg.UnityObject,500)
    UILayerMgr.SetPositionZ(self.comps.cvs_model.UnityObject,-380)
	DisplayUtil.adaptiveFullSceen(self.ib_bg)
end

function _M.OnExit( self )
	if self.reward_item then
		for i=1,#self.reward_item do
			self.reward_item[i]:RemoveFromParent(true)
			self.reward_item[i]=nil
		end
	end
	for i, v in pairs(self.geteffc) do
		if v then
			UnLoadEffect(v)
			v = nil
		end
	end
	self.geteffc = nil
    CheckedActivityType = 1
	CheckedSubindex = nil
	roleLevel = nil
	modelname = nil
    CheckIndex = 1
end

return _M