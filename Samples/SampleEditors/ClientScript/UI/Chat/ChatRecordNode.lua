local Util	  = require "Logic/Util"
local UIUtil = require 'UI/UIUtil'
local ChatUtil = require "UI/Chat/ChatUtil"
local ChatUIDealLink = require "UI/Chat/ChatUIDealLink"
local ChatModel = require 'Model/ChatModel'
local PlayRuleModel = require 'Model/PlayRuleModel'

local _M = {}

function _M.Create(template, size_default,recordCvsBounds2D,content1Size2D,sysContentSize2D)
	local self = {}
    setmetatable(self, {__index = _M})
    -- self.context = context
    self.root = template	--:Clone()
 
	local normalCvs =  UIUtil.FindChild(template,'cvs_showtype1') 
	self.normalCvs = normalCvs
    self.cvs_record1 =  UIUtil.FindChild(normalCvs,'cvs_record1')  --self.root:FindChildByEditName("cvs_record1", true)
	self.thb_content = UIUtil.FindChild(self.cvs_record1,'thb_content')   --self.root:FindChildByEditName("thb_content", true)
    self.ib_bgimg2 =  UIUtil.FindChild(normalCvs,'ib_bgimg2')      --self.root:FindChildByEditName("ib_bgimg2", true)
    self.ib_target1 =  UIUtil.FindChild(normalCvs,'ib_target1')    --self.root:FindChildByEditName("ib_target1", true)
    self.ib_bgimg3 =  UIUtil.FindChild(normalCvs,'ib_bgimg3')      --self.root:FindChildByEditName("ib_bgimg3", true)
    self.lbl_name = UIUtil.FindChild(normalCvs,'lb_name1')         --self.root:FindChildByEditName("lb_name1", true)
    self.lb_lv =  UIUtil.FindChild(normalCvs,'lb_lv')              --self.root:FindChildByEditName("lb_lv", true)
    self.lb_step =  UIUtil.FindChild(normalCvs,'lb_step')
    self.lb_post = UIUtil.FindChild(normalCvs,'lb_post')
   
    self.size_default = size_default			--整个框的大小
 	self.recordCvsBounds2D =  recordCvsBounds2D		--聊天框大小
 	self.content1Size2D = content1Size2D		--聊天内容
 	self.sysContentSize2D = sysContentSize2D

    self.thb_content.Enable = true
    self.thb_content.IsInteractive = true
    self.thb_content.event_PointerClick = function(sender, eventData)
    	local point = self.thb_content:ScreenToLocalPoint2D(eventData)
    	local  rec = RichTextClickInfo()
        local got,ret = self.thb_content.TextComponent:TestClick(point,rec)

        if got and ret.mRegion ~= nil and ret.mRegion.Attribute ~= nil then
        	ChatUIDealLink.HandleOnLinkClick(ret.mRegion.Attribute.link, ret, self.thb_content, self.root, self.data.s2c_playerId, eventData)
        end
    end

    self.direction = 0

	local sysCvs  = UIUtil.FindChild(template,'cvs_showtype2') 
	self.sysCvs = sysCvs
    self.sysContent = UIUtil.FindChild(sysCvs,'thb_content2') 

    return self
end

function _M:MakeText(msg)
	if msg.AText then
		return msg.AText
	end
	local defaultTextAttr = self.thb_content.TextComponent.RichTextLayer.DefaultTextAttribute
	return ChatUtil.HandleChatClientDecode(msg.content, GameUtil.GetTextAttributeFontColorRGB(defaultTextAttr), nil, nil, defaultTextAttr.fontSize,msg.isSys)
end

function _M:MakeSysText(msg)
	if msg.AText then
		return msg.AText
	end
							--细微差别在这里
	local defaultTextAttr = self.sysContent.TextComponent.RichTextLayer.DefaultTextAttribute
	return ChatUtil.HandleChatClientDecode(msg.content, GameUtil.GetTextAttributeFontColorRGB(defaultTextAttr), nil, nil, defaultTextAttr.fontSize,true)
end

function _M:ResetX(cvs_record1X,thb_contentX,ib_bgimg2X,ib_target1X,ib_bgimg3X,lbl_nameX,lb_lvX)
	self.cvs_record1.X = cvs_record1X
	self.thb_content.X = thb_contentX
	self.ib_bgimg2.X = ib_bgimg2X
	self.ib_target1.X = ib_target1X
	self.ib_bgimg3.X = ib_bgimg3X
	self.lbl_name.X = lbl_nameX
	self.lb_lv.X = lb_lvX
	-- self.lb_step.X = lb_stepX
end


local function OpenFuncMenu( self, type, pos, anchor, uuid, name, cb )
    local args = {}
    args.playerId = uuid
    args.playerName = name
    args.menuKey = type
    args.pos = pos
    args.anchor = anchor
    args.cb = cb
    EventManager.Fire("Event.InteractiveMenu.Show", args)
end

function _M:setData(msg)
	self.data = msg

	self.ib_bgimg2.Enable = true
    self.ib_bgimg2.IsInteractive = true
   	self.ib_bgimg2.event_PointerClick = function ( ... )
   		-- body
   		-- print_r(msg)
   		if not msg.is_myself then
   		 	OpenFuncMenu(self, "chat", nil, nil, msg.from_uuid,msg.from_name)
   		end 
   	end

	if string.IsNullOrEmpty(msg.from_name) or string.IsNullOrEmpty(msg.from_uuid) then
 
		self.normalCvs.Visible = false
		self.sysCvs.Visible = true
		
		self.sysContent.Size2D = self.sysContentSize2D
		self.sysContent.TextComponent.RichTextLayer:SetWidth(self.sysContentSize2D.x)
		self.sysContent.AText = self:MakeSysText(msg)
 		
 		self.sysContent.Width = self.sysContent.TextComponent.RichTextLayer.ContentWidth
		self.sysContent.Height = self.sysContent.TextComponent.RichTextLayer.ContentHeight
 		self.sysContent.Y = self.sysContent.Y * 0.5 +5
  
  		self.sysCvs.Size2D = self.sysContent.Size2D   
  		self.sysCvs.Height = self.sysContent.Height +10
		self.sysCvs.Width = self.root.Width

		self.root.Height =  self.sysCvs.Height + 10

	else

		self.normalCvs.Visible = true
		self.sysCvs.Visible = false

		self.lbl_name.Text = msg.from_name
		-- TODO: 玩家名称颜色显示
		self.lbl_name.FontColor = GameUtil.RGB2Color(Constants.QualityColor[2])
		-- self.lb_lv.Text = ""
		-- FriendModel.ClientPlayerBasicInfoRequest(msg.from_uuid,function( ret )
		-- 	-- body
		-- 	self.lb_lv.Text = ret.level
		-- end)


		self.thb_content.Size2D = self.content1Size2D
		self.thb_content.TextComponent.RichTextLayer:SetWidth(self.content1Size2D.x)

		self.thb_content.AText = self:MakeText(msg)
		--self.thb_content.XmlText="<recipe>"..msg.content.."</recipe>"
		self.thb_content.Width = self.thb_content.TextComponent.RichTextLayer.ContentWidth
		self.thb_content.Height = self.thb_content.TextComponent.RichTextLayer.ContentHeight+34
		self.thb_content.Y = 22
		self.cvs_record1.Size2D = self.thb_content.Size2D + (self.recordCvsBounds2D.size - self.content1Size2D)
		if msg.is_myself then
			-- -1为喇叭消息
			if msg.channel_type == ChatModel.ChannelState.CHANNEL_HORN or msg.show_type == -1 then
        		self.cvs_record1.Layout = HZUISystem.CreateLayout("#dynamic/TL_chatnew/output/TL_chatnew.xml|TL_chatnew|74", UILayoutStyle.IMAGE_STYLE_ALL_9, 25)
			else 
				local chatbox = PlayRuleModel.GetIdentity(DataMgr.Instance.UserData.RoleID,DataMgr.Instance.UserData.Pro)
				if not chatbox or string.IsNullOrEmpty(chatbox.chatbox_self) then
					local chatboxRes = "#dynamic/TL_chatnew/output/TL_chatnew.xml|TL_chatnew|28"
					self.cvs_record1.Layout = HZUISystem.CreateLayout(chatboxRes, UILayoutStyle.IMAGE_STYLE_ALL_9, 34)
				else
					self.cvs_record1.Layout = HZUISystem.CreateLayout(chatbox.chatbox_self, UILayoutStyle.IMAGE_STYLE_ALL_9, 34)
				end
			end
			
			self.cvs_record1.X = self.cvs_record1.Parent.Width - self.recordCvsBounds2D.x - self.cvs_record1.Width

			
			self:TurnDirection()
			self.lb_lv.Text = DataMgr.Instance.UserData.Level
			local pro = DataMgr.Instance.UserData.Pro
			local gender = DataMgr.Instance.UserData.Gender
			-- local img = 'static/target/'..pro..'_'..gender..'.png'
			-- -- print('self img:',img)
			-- UIUtil.SetImage(self.ib_target1,img)
			self.ib_target1.Visible = false
			self.ib_bgimg2.Visible = false
			Util.GetRoleSnap(DataMgr.Instance.UserData.RoleID, function(data)
				-- print_r('data:',data)
      			if data and data.Options then 
					local photoname = data.Options['Photo0']
					-- print('photoname:',photoname)
					if not string.IsNullOrEmpty(photoname) then
						local SocialModel = require 'Model/SocialModel'
						SocialModel.SetHeadIcon(DataMgr.Instance.UserData.RoleID,photoname,function(UnitImg)
							-- print('111111111111')
							if not self.root.IsDispose then
								-- print('222222222222222222')
								UIUtil.SetImage(self.ib_target1,UnitImg,false,UILayoutStyle.IMAGE_STYLE_BACK_4)
								self.ib_bgimg2.Visible = true
								self.ib_target1.Visible = true
	        				end
	    				end)
    				else
						-- local pro = data.Pro
						-- local gender = data.Gender
						local img = 'static/target/'..pro..'_'..gender..'.png'
						UIUtil.SetImage(self.ib_target1,img)
						self.ib_bgimg2.Visible = true
						self.ib_target1.Visible = true
    				end
				else
					-- local pro = data.Pro
					-- local gender = data.Gender
					local img = 'static/target/'..pro..'_'..gender..'.png'
					UIUtil.SetImage(self.ib_target1,img)
					self.ib_bgimg2.Visible = true
					self.ib_target1.Visible = true
    			end
			end)

 			local PracticeLv = DataMgr.Instance.UserData:TryGetIntAttribute(UserData.NotiFyStatus.PRACTICELV, 0) or 0 
 			if PracticeLv > 0 then

 				self.lb_step.Visible = true
 				GameUtil.SetPracticeName(self.lb_step,PracticeLv,0)
				self.lb_step.X = self.lbl_name.X  + self.lbl_name.Width - self.lbl_name.PreferredSize.x - self.lb_step.Width
				-- self.lb_step.X = self.lbl_name.X + self.lbl_name.PreferredSize.x + 4
			else
				self.lb_step.Visible = false
			end

			-- if DataMgr.Instance.UserData.VipLv > 0 then
			-- 	local vipLv = Util.GetText('Vip_VIPLV',DataMgr.Instance.UserData.VipLv)
			-- 	self.lb_vip.Text = vipLv
			-- 	if PracticeLv > 0 then
			-- 		self.lb_vip.X = self.lb_step.X  + self.lb_step.Width - self.lb_step.PreferredSize.x - self.lb_vip.Width
			-- 	else
			-- 		self.lb_vip.X = self.lbl_name.X  + self.lbl_name.Width - self.lbl_name.PreferredSize.x - self.lb_vip.Width
			-- 	end
			-- end

			if msg.channel_type == ChatModel.ChannelState.CHANNEL_GUILD then
				local GuildModel = require 'Model/GuildModel'
	  			local position = GuildModel.GetMemberPosition(DataMgr.Instance.UserData.RoleID)
 
	  			if position > 0 and position < 50 then
	  				-- print('position1111111111111111111111111111111111111111111111111111111111111111:',position)
	  				local guildPath = "$static/TL_hud/output/TL_hud.xml|TL_hud|position_" .. position
	  				UIUtil.SetImage(self.lb_post,guildPath)
	  				self.lb_post.Visible = true
	  				if PracticeLv > 0 then 
	  					self.lb_post.X = self.lb_step.X  - self.lb_step.PreferredSize.x - self.lb_post.Width
	  				else 
	  					self.lb_post.X =  self.lbl_name.X  + self.lbl_name.Width - self.lbl_name.PreferredSize.x - self.lb_post.Width
	  				end
	  			else
	  				self.lb_post.Visible = false
	  			end
	  		else
	  			self.lb_post.Visible = false
	  		end

			self.lbl_name.EditTextAnchor = CommonUI.TextAnchor.R_C
			
		else

			self.lbl_name.EditTextAnchor = CommonUI.TextAnchor.L_C
			
		
 			self.ib_target1.Visible = false
 			self.ib_bgimg2.Visible = false
			Util.GetRoleSnap(msg.from_uuid, function(data)
      			-- print('snaps ------------------')
      			-- 其他玩家信息，暂时只处理了等级
      			-- print_r('other roleData:',data)
      			if not data then
      				print(' Util.GetRoleSnap msg.from_uuid is nil:',msg.from_uuid)
      				return
      			end

      			if msg.channel_type == ChatModel.ChannelState.CHANNEL_HORN or msg.show_type == -1 then
        			self.cvs_record1.Layout = HZUISystem.CreateLayout("#dynamic/TL_chatnew/output/TL_chatnew.xml|TL_chatnew|73", UILayoutStyle.IMAGE_STYLE_ALL_9, 25)
				else
					local chatbox = PlayRuleModel.GetIdentity(data.ID,data.Pro)
					-- print_r('chatbox1111111111111111111:',chatbox)
					if not chatbox or string.IsNullOrEmpty(chatbox.chatbox_other) then
						local chatboxRes = "#dynamic/TL_chatnew/output/TL_chatnew.xml|TL_chatnew|27"
						self.cvs_record1.Layout = HZUISystem.CreateLayout(chatboxRes, UILayoutStyle.IMAGE_STYLE_ALL_9, 34)
					else
						self.cvs_record1.Layout = HZUISystem.CreateLayout(chatbox.chatbox_other, UILayoutStyle.IMAGE_STYLE_ALL_9, 34)
					end
				end

      		 	self.lb_lv.Text = data.Level
      		 	-- data.Pro
    --   		 	local pro = data.Pro
				-- local gender = data.Gender
				-- local img = 'static/target/'..pro..'_'..gender..'.png'
				-- -- print('other img:',img)
				-- UIUtil.SetImage(self.ib_target1,img)

				self.ib_bgimg2.Visible = false
				if data.Options then 
					local photoname = data.Options['Photo0']
					if not string.IsNullOrEmpty(photoname) then
						local SocialModel = require'Model/SocialModel'
						SocialModel.SetHeadIcon(msg.from_uuid,photoname,function(UnitImg)
							-- print('333333333333333333333333')
							if not self.root.IsDispose then
								-- print('444444444444444444444444444')
								UIUtil.SetImage(self.ib_target1,UnitImg,false,UILayoutStyle.IMAGE_STYLE_BACK_4)
								self.ib_bgimg2.Visible = true
								self.ib_target1.Visible = true
	        				end
	    				end)
	    			else
						local pro = data.Pro
						local gender = data.Gender
						local img = 'static/target/'..pro..'_'..gender..'.png'
						UIUtil.SetImage(self.ib_target1,img)
						self.ib_bgimg2.Visible = true
						self.ib_target1.Visible = true
    				end
				else
					local pro = data.Pro
					local gender = data.Gender
					local img = 'static/target/'..pro..'_'..gender..'.png'
					UIUtil.SetImage(self.ib_target1,img)
					self.ib_bgimg2.Visible = true
					self.ib_target1.Visible = true
    			end

				local PracticeLv = data.PracticeLv  or 0 
				if PracticeLv > 0 then
					self.lb_step.Visible = true
 					GameUtil.SetPracticeName(self.lb_step,PracticeLv,0)
 					self.lb_step.X = self.lbl_name.X + self.lbl_name.PreferredSize.x + 4
				else
					self.lb_step.Visible = false
				end


				if msg.channel_type == ChatModel.ChannelState.CHANNEL_GUILD then
					local GuildModel = require 'Model/GuildModel'
	  				local position = GuildModel.GetMemberPosition(msg.from_uuid)
	  				if position > 0 and position < 50 then
	  					-- print('2222222222222222222222222222222222222222222222222222222222222222222:',position)
	  					local guildPath = "$static/TL_hud/output/TL_hud.xml|TL_hud|position_" .. position
	  					UIUtil.SetImage(self.lb_post,guildPath)
	  					self.lb_post.Visible = true
	  					if PracticeLv > 0 then
	  						self.lb_post.X = self.lb_step.X + self.lb_step.Width + 4
	  					else
	  						self.lb_post.X = self.lbl_name.X + self.lbl_name.PreferredSize.x + 4
	  					end
	  				else
	  					self.lb_post.Visible = false
	  				end
	  			else 
	  				self.lb_post.Visible = false
				end

				

				-- if data.vip_level > 0 then
				-- 	local vipLv = Util.GetText('Vip_VIPLV',data.vip_level)
				-- 	self.lb_vip.Text = vipLv
				-- 	self.lb_vip.X = self.lb_step.X + self.lb_step.PreferredSize.x + 4
				-- end

			

    		end)

		end
																      -- self.rect_cvs = self.cvs_record1.Bounds2D
		self.root.Height = self.cvs_record1.Height + self.size_default.y - self.recordCvsBounds2D.height + 10

	end
end

local function mirror(node)
	node.X = node.Parent.Width - node.X - node.Width
end

function _M:TurnDirection()
	 
	mirror(self.lbl_name)

	mirror(self.ib_bgimg2)
	mirror(self.ib_target1)
	mirror(self.ib_bgimg3)
	mirror(self.lb_lv)
	-- mirror(self.cvs_record1)
	mirror(self.thb_content)
end

return _M