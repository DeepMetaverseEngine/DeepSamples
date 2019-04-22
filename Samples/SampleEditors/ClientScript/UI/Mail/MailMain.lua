local _M = {}
_M.__index = _M

local DisplayUtil = require("Logic/DisplayUtil")
local UIUtil = require 'UI/UIUtil'
local Util = require "Logic/Util"
local MailModel= require("Model/MailModel")
local ItemModel = require("Model/ItemModel")

local detailMenu = nil

local function ShowItemDetail(self, detail)
	detailMenu = GlobalHooks.UI.CreateUI('ItemDetail')
	self:AddSubUI(detailMenu)
	detailMenu:Reset({detail=detail,index=1,compare=false,IsEquiped=false})
	-- detailMenu:EnableTouchFrameClose(true)
	detailMenu:SetButtons({})
end

local function AddItem(self, data, index)
	local canve = self.ui.comps.cvs_item:Clone()
	local itemdetail = ItemModel.GetDetailByTemplateID(data.TemplateID)
	local icon = itemdetail.static.atlas_id
	local quality = itemdetail.static.quality
	local num = data.Count
	local itshow = UIUtil.SetItemShowTo(canve, icon, quality, num)
    itshow.EnableTouch = true
    itshow.TouchClick = function()
        -- local detail = UIUtil.ShowNormalItemDetail({detail = itemdetail, itemShow = itshow, autoHeight = true})
        -- detail:SetPos(30, 556,'l_b')

    	if string.IsNullOrEmpty(data.ID) then	--非装备，直接取模板详情
        	UIUtil.ShowNormalItemDetail({detail = itemdetail, itemShow = itshow, autoHeight = true})
    	else --货架上的装备，向服务器请求装备详情
    		ItemModel.RequestDetailByID(data.ID, function(entityDetail)
        		UIUtil.ShowNormalItemDetail({detail = entityDetail, itemShow = itshow, autoHeight = true})
    		end)
    	end
    end

	canve.X = 18 + 74*(index-1)
	self.ui.comps.sp_oar2.ContainerPanel:AddChild(canve)
end

local function DrawMailInfo(self, data)
	self.ui.comps.lb_mailtitle.Text = data.title
	self.ui.comps.tbh_text.XmlText = "<recipe>"..data.content.txt_content.."</recipe>"
	self.ui.comps.tbh_text.Scrollable = true
end

local function GetMailData(self, index)
	MailModel.ClientGetMailDetailRequest(self.allData[index].uuid, function( data )
		DrawMailInfo(self,data)
		self.ui.comps.sp_oar2.ContainerPanel:RemoveAllChildren(true)
		if self.allData[index].attachment then 
			self.ui.comps.cvs_reward.Visible = true
			self.ui.comps.btn_get.Visible = true
			self.ui.comps.btn_del.Visible = false
			
			if data.content.attachment_list~=nil then 
				for i=1,#data.content.attachment_list do
					 AddItem(self,data.content.attachment_list[i],i)
				end
			end
		else
			self.ui.comps.cvs_reward.Visible = false
			self.ui.comps.btn_get.Visible = false
			self.ui.comps.btn_del.Visible = true
		end
	end)
end 

local function SetMailState(self, index ,ib_status)
	if self.allData[index].attachment then
		ib_status.Layout = HZUISystem.CreateLayout("#dynamic/TL_mail/output/TL_mail.xml|TL_mail|12", UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER, 1)
	else
		if self.allData[index].mail_status == 1 then 
			ib_status.Layout = HZUISystem.CreateLayout("#dynamic/TL_mail/output/TL_mail.xml|TL_mail|9", UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER, 1)
		else
			ib_status.Layout = HZUISystem.CreateLayout("#dynamic/TL_mail/output/TL_mail.xml|TL_mail|6", UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER, 1)
		end
	end
end

local function OnMailSelected(self, index, ib_bjtu1, ib_tab, ib_status)
	if self.lastImg~=nil then 
		self.lastImg.Visible = false
	end
	if self.lastTab~=nil then 
		self.lastTab.Visible = false
	end
	ib_bjtu1.Visible = true
	ib_tab.Visible = true
	self.lastImg = ib_bjtu1
	self.lastTab = ib_tab
	self.lastIndex = index
	self.allData[index].mail_status = 2
	GetMailData(self, index )
	SetMailState(self,index,ib_status)
end

local function RefreshCellData(self, node, index)
	node.X = 0
	local ib_status =  node:FindChildByEditName('ib_status', true)
	local ib_tab =  node:FindChildByEditName('ib_tab', true)
	local lb_name =  node:FindChildByEditName('lb_name', true)
	local lb_sender =  node:FindChildByEditName('lb_sender', true)
	local lb_date =  node:FindChildByEditName('lb_date', true)
	local ib_new =  node:FindChildByEditName('ib_new', true)
	local ib_bjtu1 = node:FindChildByEditName('ib_bjtu1', true)
	ib_bjtu1.Visible = false
	ib_tab.Visible = false
	if self.lastIndex == index then 
		OnMailSelected(self,index,ib_bjtu1,ib_tab,ib_status)
	end
	node.Enable = true
	node.IsInteractive = true
	node.TouchClick = function( sender, e )
		-- print("TouchClick--------------------",index)
		OnMailSelected(self,index,ib_bjtu1,ib_tab,ib_status)
	end

	if self.allData[index].b_new  then
		ib_new.Visible =true
	else
		ib_new.Visible =false
	end
	lb_sender.Text = self.allData[index].sender_name
	lb_name.Text = self.allData[index].title
	lb_date.Text = self.allData[index].create_time:ToString()
	SetMailState(self,index,ib_status)
	local nowHour = os.date("%H",os.time())
	local nowDay = os.date("%d",os.time())
	if  nowHour - self.allData[index].create_time.Hour < 2 then
		lb_date.Text = Util.GetText('mail_now')
	else
		lb_date.Text = Util.GetText('mail_today')
	end
	if nowDay - self.allData[index].create_time.Day >= 1 then 
		lb_date.Text = string.format("%d-%d-%d",self.allData[index].create_time.Year,
			self.allData[index].create_time.Month,
			self.allData[index].create_time.Day)
	end
end

local function GetAllmail(self)
    MailModel.ClientGetMailBoxInfoRequest(function(data)
    	self.maxnum =data.s2c_max_count
    	self.ui.comps.lb_num.Text = Util.GetText('mail_list', 0, self.maxnum)

    	self.allData = data.s2c_mailsnap_list
    	if self.allData ~= nil then
	    	local len = #self.allData
	    	self.ui.comps.sp_oar1.Visible = true
			self.lastIndex = self.lastIndex > len and len or self.lastIndex
	 	  	self.ui.comps.sp_oar1:ResetRowsAndColumns(len, 1)
	 	  	self.ui.comps.sp_oar1.Scrollable:LookAt(Vector2.New(0, -self.lastScrollY))
	    	if len > 0 then
	    		self.ui.comps.lb_num.Text = Util.GetText('mail_list', len, self.maxnum)
	    		self.ui.comps.cvs_none.Visible = false
	    		self.ui.comps.cvs_content.Visible = true
	    	else
	    		self.ui.comps.cvs_none.Visible = true
	    		self.ui.comps.cvs_content.Visible = false
	    		self.lastIndex = 0
	    	end
	    else
	    	self.ui.comps.sp_oar1.Visible = false
	    	self.ui.comps.cvs_none.Visible = true
	    	self.ui.comps.cvs_content.Visible = false
	    	self.lastIndex = 0
    	end
    end)
end 

local function DeleteMail(self, alldel)
	if self.allData == nil or #self.allData == 0 then
		return
	end
	local list = {self.allData[self.lastIndex].uuid}
	if alldel then
		local can = 0
		for i=1,#self.allData do
			if self.allData[i].mail_status==2 and not self.allData[i].attachment then 
				can = can + 1
			end
		end

		if can == 0 then 
			GameAlertManager.Instance:ShowNotify(Util.GetText('mail_alldelete'))
			return
		end
	end

	MailModel.ClientDeleteMailRequest(alldel,list, function(data)
		GetAllmail(self)
	end)
end

local function SendCanRemove(self)
	self.lastIndex = 1
	local list = {}
	if self.allData~=nil then 
		for i=1,#self.allData do
			if self.allData[i].can_remove and self.allData[i].mail_status==2 and self.allData[i].attachment==false then 
				table.insert(list,self.allData[i].uuid)
			end
		end
		--print_r(list)
		MailModel.ClientDeleteMailRequest(false,list, function(data)
			GetAllmail( self )
		end)

	else
		EventManager.Fire("Event.Mail.UnRead",{num=0})
	end
end

function _M.OnEnter(self, ...)
	print('MailMain OnEnter')
	self.ui.comps.btn_close.TouchClick = function()
		-- print('bt_login TouchClick')
		self.ui:Close()
	end
	self.ui.comps.btn_alldel.TouchClick = function()
		 DeleteMail(self, true)
	end
	self.ui.comps.btn_get.TouchClick = function()
		MailModel.ClientGetMailAttachmentReqeust(self.allData[self.lastIndex].uuid, function(data)
			GetAllmail(self)	
		end)
	end
	self.ui.comps.btn_allget.TouchClick = function()
		MailModel.ClientGetMailAttachmentReqeust('', function(data)
			GetAllmail(self)
		end)
	end
	self.ui.comps.btn_del.TouchClick = function()
		DeleteMail(self, false)
	end
	self.ui.comps.cvs_item.Visible = false

	self.lastIndex = 1
	self.lastScrollY = 0
	GetAllmail(self)

	self.ui.comps.sp_oar1.Scrollable.event_Scrolled = function(panel, e)
        self.ui.comps.ib_down.Visible = true
        self.lastScrollY = panel.Container.Y
        if panel.Container.Height + panel.Container.Y <= panel.ScrollRect2D.height + self.ui.comps.cvs_letter.Height then --底部
            self.ui.comps.ib_down.Visible = false
        end
    end
end

function _M.OnExit(self)
	-- print('OnExit')
	MailModel.CheckMailNum(self)
	-- SendCanRemove( self )
end

function _M.OnDestory(self)
	-- print('OnDestory')
end

function _M.OnInit(self)
	-- print('OnInit')
	self.allData = {}
	self.lastImg = nil
	self.lastTab = nil
	self.lastIndex = 1
	self.maxnum=0
    local pan = self.ui.comps.sp_oar1
	local cell = self.ui.comps.cvs_letter
	cell.Visible = false
	pan:Initialize(cell.Size2D.x, cell.Size2D.y, 0, 1, cell, function(gx, gy, node)
		RefreshCellData(self, node, gy+1)
	end, function() end)
end

return _M