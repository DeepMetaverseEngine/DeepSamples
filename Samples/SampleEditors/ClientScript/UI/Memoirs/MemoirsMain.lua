local _M = {}
_M.__index = _M

local Util  = require "Logic/Util"
local UIUtil = require 'UI/UIUtil'
local DisplayUtil = require("Logic/DisplayUtil")
local MemoirsModel = require 'Model/MemoirsModel'
local MemoirsUtil = require 'UI/Memoirs/MemoirsUtil'

local ContentType = {
	None = 0,
	Chaptername = 1, --章节名字
	Content = 2,--内容
	Legend = 3,--外传
}
--三生录


function _M.initPageOne(self)
	local node = self.ui.comps.cvs_page1
	local sp_cp =  UIUtil.FindChild(node, 'sp_cp', true)
	local cvs_cpinfo1 =  UIUtil.FindChild(node, 'cvs_cpinfo1', true)
 	cvs_cpinfo1.Visible = false

	local function ShowThreeLivesBook(node,index)
		local curData = self.BookLib[index]
		if curData == nil then
			node.Visible = false
			return 
		end
		local ib_lock = UIUtil.FindChild(node,'ib_lock', true)
		local IsUnlocked = MemoirsUtil.ChapterIsUnlocked(curData.id)
		if IsUnlocked then
			ib_lock.Visible = false
			node.IsGray = false
		else
			ib_lock.Visible = true
			node.IsGray = true
		end
		local lb_cpname = UIUtil.FindChild(node, 'lb_cpname', true)
		lb_cpname.Text = Util.GetText(curData.chapter_number)
		local ib_cppic = UIUtil.FindChild(node, 'ib_cppic', true)
		UIUtil.SetImage(ib_cppic, curData.chapter_small_pic)
		local tbt_cp = UIUtil.FindChild(node, 'tbt_cp', true)
		tbt_cp.IsChecked = index == self.curBookIndex
		if tbt_cp.IsChecked then
			self:initPageOutline()
		end
		tbt_cp.TouchClick = function(sender)
			self.curBookIndex = index
			sp_cp:RefreshShowCell()
		end

	end

 	UIUtil.ConfigGridVScrollPan(sp_cp,cvs_cpinfo1,3,#self.BookLib,function(node, index)
    	ShowThreeLivesBook(node,index)
   	end)
end

function _M.initPageOutline(self)
	self.curBookData = self.BookLib[self.curBookIndex]
	local node = self.ui.comps.cvs_page2
	local lb_name =  UIUtil.FindChild(node, 'lb_name', true)
	lb_name.Text = Util.GetText(self.curBookData.chapter_name)
	local stars = MemoirsUtil.GetStar(self.curBookData.id)
	for i = 1,5 do
		local starnode = UIUtil.FindChild(node, 'ib_star'..i, true)
		if i <= stars then
			starnode.Enable = true
		else
			starnode.Enable = false
		end
	end

	local ib_bg =  UIUtil.FindChild(node, 'ib_bg', true)
	local img = "dynamic/TL_sanshenglubg/"..self.curBookData.chapter_pic
	UIUtil.SetImage(ib_bg,img)
end
function _M.initPageStoryReview(self,isStory)
	local node = self.ui.comps.cvs_page3
	local sp_des =  UIUtil.FindChild(node, 'sp_des', true)
	local lb_name1 =  UIUtil.FindChild(node, 'lb_name1', true)
	local lb_name2 =  UIUtil.FindChild(node, 'lb_name2', true)

	if isStory then
		lb_name1.Visible = true
		lb_name2.Visible = false
	else
		lb_name1.Visible = false
		lb_name2.Visible = true
	end
	
	local lb_destop =  UIUtil.FindChild(node, 'lb_destop', true)
	lb_destop.Visible = false
	local lb_title1 =  UIUtil.FindChild(node, 'lb_title1', true)
	lb_title1.Visible = false
	local lb_title2 =  UIUtil.FindChild(node, 'lb_title2', true)
	lb_title2.Visible = false
	local cvs_des1 = UIUtil.FindChild(node, 'cvs_des1', true)
	cvs_des1.Visible = false
	local lb_destop =  UIUtil.FindChild(node, 'lb_destop', true)
	lb_destop.Visible = false
	--sp_des.:ClearGrid()
	sp_des.Scrollable.Container:RemoveAllChildren(true)
	--print("self.curBookData.story_introduction",self.curBookData.story_introduction)

	local content = nil
	if isStory then
		content = MemoirsUtil.GetContent(tonumber(self.curBookData.story_introduction))
	else
		content = MemoirsUtil.GetContent(tonumber(self.curBookData.role_introduction))
	end
	--print_r("content",content)
	local nodey = 0
	for i,v in ipairs(content.title_type) do
		local contenttype = v
		local contenttext = Util.GetText(content.content[i]) 
		if contenttype == ContentType.None then
			break
		end
		if contenttype == ContentType.Chaptername then
				local title1node = lb_title1:Clone()
				title1node.Visible = true
				title1node.Y = nodey
				title1node.Text = contenttext
				sp_des:AddNormalChild(title1node)
				nodey = nodey + title1node.Height + 5 
		elseif contenttype == ContentType.Legend then

				local title2node = lb_title2:Clone()
				title2node.Visible = true
				title2node.Y = nodey
				sp_des:AddNormalChild(title2node)
				nodey = nodey + title2node.Height + 5

		elseif contenttype == ContentType.Content then
			local cvs_des1node = cvs_des1:Clone()
			cvs_des1node.Visible = true
			local tb_des1node =  UIUtil.FindChild(cvs_des1node, 'tb_des1', true)
			--print("contenttext",contenttext)
			local start,_,contentname = string.find(contenttext,"#(.-)#")
			--print("contentname",start,contentname)
			local tempheight = 12
			if start ~= nil then
				local lb_destopnode = lb_destop:Clone()
				lb_destopnode.Visible = true
				lb_destopnode.Text = contentname
				cvs_des1node:AddChild(lb_destopnode)
				tempheight = lb_destopnode.Height
				contenttext = string.gsub(contenttext,"#"..contentname.."#","")
				--print("contenttext",contenttext)
			end
			tb_des1node.Size2D = Vector2(tb_des1node.Size2D.x - 10, tb_des1node.Size2D.y)
			sp_des:AddNormalChild(cvs_des1node)
			tb_des1node.Scrollable = false
			tb_des1node.XmlText = string.format("<f>%s</f>",contenttext)
			--print("size",tb_des1node.TextComponent.RichTextLayer.ContentHeight)
			local w, h = tb_des1node.TextComponent.PreferredSize.x, tb_des1node.TextComponent.PreferredSize.y
			--print("w, h",w,h)
			tb_des1node.Size2D = Vector2(tb_des1node.Size2D.x, h + 20)
		 	cvs_des1node.Size2D = Vector2(cvs_des1node.Size2D.x - 10, tb_des1node.Height + 15 + tempheight)
			tb_des1node.X = 10
			tb_des1node.Y = tempheight
			cvs_des1node.X = 0
			cvs_des1node.Y = nodey
			nodey = nodey + cvs_des1node.Height + 15
		end
	end
	sp_des.Scrollable:LookAt(Vector2(0, 0))
end
function _M.initPageRoleReview(self) 
	self:initPageStoryReview(false)
end


function _M.initPageChapterProg(self)

	local node = self.ui.comps.cvs_page4
	local sp_quest =  UIUtil.FindChild(node, 'sp_quest', true)
	local cvs_questinfonode =  UIUtil.FindChild(node,'cvs_questinfo', true)
	cvs_questinfonode.Visible = false
	self.Questlist = {}
	for i,v in ipairs(self.curBookData.quest_desc) do
		if v ~= nil then
			table.insert(self.Questlist,v)
		end
	end
	--print("self.Questlist",#self.Questlist)
 	
 	local function ShowQuestProg(node,index)
		
		local curQuest = self.Questlist[index]
		local lb_questname = UIUtil.FindChild(node, 'lb_questname', true)
		lb_questname.Text = Util.GetText(curQuest)
		local ib_stars = {}
		for i = 1,3 do
			local ib_star = UIUtil.FindChild(node, 'ib_star'..i, true)
			table.insert(ib_stars,ib_star)
			ib_star.Visible = false
			if i <= self.curBookData.quest_star[index] then
				ib_star.Visible = true
				ib_star.Enable = true
			end
		end

		local hasdone = MemoirsUtil.GetTaskHasDone(self.curBookData.id,self.curBookData.quest_id[index])
		local ib_checkdi = UIUtil.FindChild(node, 'ib_checkdi', true)
		local ib_check = UIUtil.FindChild(node, 'ib_check', true)
		if hasdone then
			lb_questname.IsGray = true
			ib_check.Enable = true
			for i = 1,3 do
				ib_stars[i].IsGray = false
			end
		else
			lb_questname.IsGray = false
			ib_check.Enable = false
			for i = 1,3 do
				ib_stars[i].IsGray = true
			end
		end
		
		local tbt_select = UIUtil.FindChild(node, 'tbt_select', true)
		tbt_select.IsChecked = index == self.QuestlistIndex
		tbt_select.TouchClick = function(sender)
			self.QuestlistIndex = index
			sp_quest:RefreshShowCell()
		end
	end
	UIUtil.ConfigVScrollPan(sp_quest,cvs_questinfonode,#self.Questlist,function(node,index)
		ShowQuestProg(node,index)
	end)
 
end

function _M.OpenPage(self,Page)
	for i,v in ipairs(self.tbts) do
		v.Visible = true
	end
	self.QuestlistIndex = nil
	if Page == 0 then --第一页
		self.ui.comps.cvs_page1.Visible = true
		self.ui.comps.cvs_page2.Visible = true
		self.ui.comps.cvs_page3.Visible = false
		self.ui.comps.cvs_page4.Visible = false
		self.ui.comps.btn_read.Visible = true
		self.ui.comps.btn_back.Visible = false
		self.ui.comps.cvs_page2.Position2D = self.righhtpos
		for i,v in ipairs(self.tbts) do
			v.Visible = false
		end
		self:initPageOne()
	elseif Page == 1 then --第二页
		self.ui.comps.cvs_page1.Visible = false
		self.ui.comps.cvs_page2.Visible = true
		self.ui.comps.cvs_page3.Visible = true
		self.ui.comps.cvs_page4.Visible = false
		self.ui.comps.btn_read.Visible = false
		self.ui.comps.btn_back.Visible = true
		self.ui.comps.cvs_page2.Position2D = self.leftpos
		self:initPageStoryReview(true)
	elseif Page == 2 then
		self.ui.comps.cvs_page1.Visible = false
		self.ui.comps.cvs_page2.Visible = true
		self.ui.comps.cvs_page3.Visible = true
		self.ui.comps.cvs_page4.Visible = false
		self.ui.comps.btn_read.Visible = false
		self.ui.comps.btn_back.Visible = true
		self.ui.comps.cvs_page2.Position2D = self.leftpos
		self:initPageRoleReview()
	elseif Page == 3 then
		self.ui.comps.cvs_page1.Visible = false
		self.ui.comps.cvs_page2.Visible = true
		self.ui.comps.cvs_page3.Visible = false
		self.ui.comps.cvs_page4.Visible = true
		self.ui.comps.btn_read.Visible = false
		self.ui.comps.btn_back.Visible = true
		self.ui.comps.cvs_page2.Position2D = self.leftpos
		self:initPageChapterProg()
	end
	
end

function _M.OnEnter(self)
	self:OpenPage(0)
	local num = MemoirsUtil.GetUnlockNum()
	self.ui.comps.lb_jindu.Text =  num.."/"..#self.BookLib
	--self.ui.menu.Visible = false
	-- local node = HZUISystem.Instance:GetUILayer()
	-- DisplayUtil.loadEffect("/res/effect/ui/item_shu.assetbundles", node, {autoRemove = false})
end

function _M.OnExit( self )
	print('MemoirsMainOnExit')
end

function _M.OnDestory( self )
	print('MemoirsMainOnDestory')
end

function _M.OnInit( self )
	print('MemoirsMainInit')
	self.leftpos = self.ui.comps.cvs_left.Position2D
	self.righhtpos = self.ui.comps.cvs_right.Position2D
	self.BookLib = GlobalHooks.DB.Find('MemoirsData',{})
	if self.curBookIndex == nil then
		self.curBookIndex = 1
	end
	--print_r(self.BookLib)
	table.sort(self.BookLib, function(booka,bookb)
        return booka.chapter_id < bookb.chapter_id
    end)
	--print_r(self.BookLib)
	self.ui.comps.btn_read.TouchClick = function(sender)
		local function ToggleFunc(sender)     
			local tag = tonumber(sender.UserData)
			self:OpenPage(tag)
		end	
		UIUtil.ConfigToggleButton(self.tbts, self.tbts[1], false, ToggleFunc)
		
	end
	self.ui.comps.btn_back.TouchClick = function(sender)
		self:OpenPage(0)
	end

	self.tbts = {}
	for i = 1,3 do
		local tbt = self.ui.comps['tbt_tab'..i]
		tbt.UserData = i
		table.insert(self.tbts,tbt)
	end
	
end


return _M