local _M = {}
_M.__index = _M
 
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local MountModel = require 'Model/MountModel'
local ItemModel = require 'Model/ItemModel' 

local succCode = 200

local function Release3DModel(self)
	if self and self.model then
		UI3DModelAdapter.ReleaseModel(self.model.Key)
		self.model = nil
	end
end

local function Init3DSngleModel(self, parentCvs, pos2d, scale, rotate,menuOrder,fileName)
	local info = UI3DModelAdapter.AddSingleModel(parentCvs, pos2d, scale, menuOrder,fileName)
	self.model = info
	local trans = info.RootTrans
	info.Callback = function (info2)
		-- body
		local trans2 = info2.RootTrans
		trans2:Rotate(Vector3.up,rotate)
	end
	
	parentCvs.event_PointerMove = function(sender, data)
		local delta = -data.delta.x
		-- print('delta:',delta)
		trans:Rotate(Vector3.up, delta * 1.2)
	end
	
end

local function ReloadModel(self,modelData)
	if self == nil then
		return
	end
	Release3DModel(self)

	-- local fixpos = Vector2(0,0)
	-- local filename = '/res/unit/mount_phoenix.assetbundles'
	local filename = '/res/unit/' .. modelData.avatar_res .. '.assetbundles'
	
	local fixposdata = string.split(modelData.pos_xy,',')
	local fixpos = {x = tonumber(fixposdata[1]),y =  tonumber(fixposdata[2])}-- 偏移坐标
	local fixzoom = tonumber(modelData.zoom) -- 缩放比例
	local rotate = modelData.rotate

	local cvsModel = self.cvsModel

	local pos2d = self.ui.comps.cvs_anchor.Position2D
    Init3DSngleModel(self, cvsModel, pos2d, fixzoom,rotate, self.ui.menu.MenuOrder,filename)
end

local function showCost(self,mountData)
	-- body
	local data = GlobalHooks.DB.FindFirst('Mount',{avatar_id=mountData.avatar_id})
	local items = ItemModel.ParseCostAndCostGroup(data)
	self.costLabel.Visible = false
	local csv_itemIcon = self.costCvs
	if items then
		for i,v in ipairs(items) do 
		-- 	print_r(v)
			local item = v.detail 
			self.costCvs.Visible = true 

			local itemDetial = item 								--ItemModel.GetDetailByTemplateID(item.static.id)
  			local itShow = UIUtil.SetItemShowTo(csv_itemIcon,item.static.atlas_id, item.static.quality)
  			itShow.EnableTouch = true
			itShow.TouchClick = function ( ... )
				-- body
				local detail = UIUtil.ShowNormalItemDetail({detail=itemDetial,itemShow=itShow,autoHeight=true})
				-- detail:SetPos(0,350)
			end

			local value
  			if item.static.item_type == 0 then
				value = v.need
			else
				value =  v.cur .. '/' .. v.need
			end
			self.costLabel.Text = v.need

			local color = 0xffffff
       		if v.cur < v.need then
       			color = 0xff0000
       		end

       		self.costLabel.Visible = true
       		self.costLabel.FontColorRGB = color
  			self.buyBtn.Visible = true
  			break
		end 
	else 
		self.buyBtn.Visible = false
		self.costCvs.Visible = false
		self.costLabel.Visible = false
	end
	-- local attribute2 = ItemModel.GetXlsFixedAttribute(data2)
	-- print_r('attribute2 is:',attribute2)
	-- -- local item = ItemModel.ParseCostAndCostGroup(data)
end

local function setBtnStatus(self,mountData)
	-- body
	self.costCvs.Visible = false
	self.useBtn.Visible = false
	self.buyBtn.Visible = false
	self.ridingLabel.Visible = false
	self.unlockNeedLabel.Visible = false
	self.unlockNeedImg.Visible = false

	local data = self.userMoutMap[mountData.avatar_id]
	if data then
		-- 骑乘中
		if self.currentMountId == mountData.avatar_id then
		 	self.ridingLabel.Visible = true
		else
			self.useBtn.Visible = true
		end
	else
		local unlock = mountData.unlock
		if unlock > 0 then
			
			--local message = string.format(Constants.Text.unlock_mount_tip,mountData.unlock)
			-- local message = Util.Format1234(Constants.Text.unlock_mount_tip,unlock)
			-- self.unlockNeedLabel.Text = message
			local tips = Util.GetText(mountData.unlock_show)
			if tips ~= nil and string.len(tips) then
				self.unlockNeedLabel.Text = tips
				self.unlockNeedLabel.Visible = true
				self.unlockNeedImg.Visible = true
			end
		end

		showCost(self,mountData)
	end
 
end  

local function showDetialPanel(self,mountData)
	-- print_r('showDetialPanel mountData:',mountData)
	self.scoreLabel.Text = mountData.score
	self.speedLabel.Text = mountData.mount_speed
	self.introduceLabel.Text = Util.GetText(mountData.mount_desc)
	self.introduceLabel.TextGraphics.horizontalOverflow = UnityEngine.HorizontalWrapMode.Wrap;
    self.introduceLabel.EditTextAnchor = CommonUI.TextAnchor.L_T;
	self.moutNameLabel.Text = Util.GetText(mountData.name)
	self.selectedMountData = mountData
	-- self.useBtn.UserData = mountData
	setBtnStatus(self,mountData)

	ReloadModel(self,mountData)
end

local function showTogButton(self,node,index,mountData)
	-- body
	-- print_r('showTogButton:',mountData)

	local togButton  = UIUtil.FindChild(node,'tbt_mount')
	togButton.Text = Util.GetText(mountData.name)
	if mountData.avatar_id == self.selectMountId then
		togButton.IsChecked = true
		self.selectedTogButton = togButton
    	self.defaultNode = node
		showDetialPanel(self,mountData)
	elseif self.defaultIndex == index then
		-- print('eachupdatecb index ',index)
		togButton.IsChecked = true
		self.selectedTogButton = togButton
    	self.defaultNode = node
		showDetialPanel(self,mountData)
	else
		togButton.IsChecked = false
	end

	togButton.TouchClick = function ( ... )
		if index == self.defaultIndex then
			togButton.IsChecked = true
		elseif  self.selectedTogButton  then 
			self.selectedTogButton.IsChecked = false
			togButton.IsChecked = true
			self.selectedTogButton = togButton
      		self.defaultNode = node
			self.defaultIndex = index
			-- 网络状态成功之后再赋值吧
			-- self.selectMountId = mountData.avatar_id
			showDetialPanel(self,mountData)
		end
	end
end 

local function showItem(self,node,index)
	-- print('showItem index:',index)
	local mountData = self.mounts[index] 
	-- print_r('showItem mountData:',mountData)
	if mountData == nil then
		node.Visible = false
		return
	end

	local headIcon = UIUtil.FindChild(node,'ib_headicon')	
	UIUtil.SetImage(headIcon,mountData.icon)

	local data = self.userMoutMap[mountData.avatar_id]
	if data then
		headIcon.IsGray = false
		node.IsGray = false
	else
		headIcon.IsGray = true
		node.IsGray = true
	end

	showTogButton(self,node,index,mountData)
end


 
local function showScrollPan(self)
	-- self.defaultIndex = 1;
	-- self.selectedTogButton = nil
	self.mounts = MountModel.GetMountBySheetType(self.sheetType,self.userMoutMap,self.currentMountId)

	-- print('showScrollPan mounts.length: ',#self.mounts)
	local function eachupdatecb(node, index) 
		showItem(self,node,index)
	end
 
	UIUtil.ConfigVScrollPan(self.pan,self.tempnode,#self.mounts,eachupdatecb)
 
end
 

local function showMountInfos(self)

	MountModel.RequestMountInfo(function (data)
     
    	self.userMoutMap = data.userMoutMap
		self.currentMountId = data.currentMountId
		-- print_r('self.userMoutMap:',self.userMoutMap)
		-- print('self.currentMountId:',self.currentMountId)
		showScrollPan(self)
    end,true)
end 

local function UnlockMount(self,mountId)
	-- print('UnlockMount mountId:',mountId)
	MountModel.RequestUnlock(mountId,function (resp)
		if resp.s2c_code == succCode then
			self.selectMountId = resp.mountId 
			self.selectedTogButton = nil
			self.defaultIndex = nil
			showMountInfos(self)
		else
			self.selectMountId = nil
    	end
    end)
end

local function ChangeMount(self,mountId)
	-- print('ChangeMount mountId:',mountId)
	MountModel.ChangeMount(mountId,function (resp)
    	-- body
		 
		if resp.s2c_code == succCode then
			-- TODO 等服务器更新之后改成 resp.mountId
			self.selectMountId = resp.mountId 
			self.selectedTogButton = nil
			self.defaultIndex = nil

			showMountInfos(self)
		else
			self.selectMountId = nil
    	end
    end)
end


-- self.ui BaseUI实例例
-- self.ui.comps 节点访问器
-- self.ui.menu csharp MenuBase
function _M.OnEnter( self, ...)
	print('MountInfo OnEnter:',self,...)
 	
	self.defaultIndex = 1
  	self.selectedTogButton = nil
	self.selectMountId = nil
	self.sheetType = 1
	showMountInfos(self)

	-- local default = self.ui.comps.tbt_1
	-- UIUtil.ConfigToggleButton(self.tbts, default, false, ToggleFunc)
end

function _M.OnExit( self )
	print('MountInfo OnExit')

end

function _M.OnDestory( self )
	print('MountInfo OnDestory')

end

function _M.OnInit( self )

 	self.cvsModel = self.ui.comps.cvs_model
	self.scoreLabel = self.ui.comps.lb_tipsnum1
	self.speedLabel = self.ui.comps.lb_tipsnum2
	self.introduceLabel = self.ui.comps.lb_introduce
 	self.moutNameLabel = self.ui.comps.lb_name
 	self.unlockNeedLabel = self.ui.comps.lb_need
 	self.unlockNeedImg = self.ui.comps.ib_need

 	self.ridingLabel = self.ui.comps.lb_ride
 	self.ridingLabel.Visible = false
 	self.useBtn = self.ui.comps.btn_use
	self.useBtn.TouchClick = function (sender)
		local mountData = self.selectedMountData
		if mountData ~= nil then
			ChangeMount(self,mountData.avatar_id)
		end
	end

	self.costCvs = self.ui.comps.cvs_costitem
	self.costLabel = UIUtil.FindChild(self.costCvs,'lb_num')

	self.buyBtn = self.ui.comps.btn_buy
	self.buyBtn.TouchClick = function (sender)
		-- body
		local mountData = self.selectedMountData
		if mountData ~= nil then
			UnlockMount(self,mountData.avatar_id)
		end
	end

  	self.pan =  self.ui.comps.sp_oar
 	self.tempnode = self.ui.comps.cvs_mount
 	self.tempnode.Visible = false
 

 	self.userMoutMap = {}
 	self.currentMountId = 0


 	self.pifubtn = self.ui.comps.btn_pifu
	self.pifubtn.TouchClick = function (sender)
		GlobalHooks.UI.OpenUI('WardrobeMain',0,5)
	end




  --   local function ToggleFunc(sender)
  --   	self.defaultIndex = 1;
  --   	self.selectMountId = nil
		-- self.sheetType = sender.UserTag
		-- print('self.sheetType:',self.sheetType)
  --   	showMountInfos(self)
  --   end
    
end

return _M