local _M = {}
_M.__index = _M


local TargetModel=require 'Model/TargetModel'
local Util   = require 'Logic/Util'
local UIUtil = require 'UI/UIUtil'
local ItemModel = require 'Model/ItemModel'


function _M.OnInit(self)

  	--设置类型，界面打开时其他界面不隐藏
	self.ui.menu.ShowType = UIShowType.Cover
	self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.FadeMoveDown)
	
	self.cvs_detail=self.ui.comps.cvs_detail
	--自适应
	HudManager.Instance:InitAnchorWithNode(self.cvs_detail, bit.bor(HudManager.HUD_CENTER))

	--点击界面其他部分界面关闭，并打开目标面板，以防误操作
	self.ui.menu.event_PointerUp = function()
 		self.ui:Close()
  	end

  	self.ib_pic=self.ui.comps.ib_pic
  	self.lb_tips=self.ui.comps.lb_tips
  	self.cvs_item=self.ui.comps.cvs_item1
  	self.btn_get=self.ui.comps.btn_get

  	self.lb_leftlevel=self.ui.comps.lb_level3
  	self.lb_rightlevel=self.ui.comps.lb_level4

  	--点击领取按钮，关闭界面
  	self.btn_get.TouchClick=function()
  		TargetModel.RequestGetItem(self.target.record_num)
        EventManager.Fire('Event.Target.PlayEffect',{})
  		  self.ui:Close()
  	end
end


function _M.OnEnter(self,params)

	self.target=params
	--判断是否满足条件
	if self.target.target_level <= DataMgr.Instance.UserData.Level then 
		self.lb_leftlevel.Visible=false
		self.lb_rightlevel.Visible=false
		self.btn_get.Visible=true
	else
		self.lb_leftlevel.Visible=true
		self.lb_leftlevel.Text=self.target.target_level
		self.lb_rightlevel.Visible=true
		self.btn_get.Visible=false
	end

	--设置原图以及描述
	local iconId = self.target.picture_res
	UIUtil.SetImage(self.ib_pic,iconId,false, UILayoutStyle.IMAGE_STYLE_BACK_4)
   	self.lb_tips.Text=Util.GetText(self.target.desc) 

   	--设置icon图标
    local itemdetail=ItemModel.GetDetailByTemplateID(self.target.item_id)
    local quality = itemdetail.static.quality
    local icon=itemdetail.static.atlas_id
    local num =self.target.item_num
    local itshow=UIUtil.SetItemShowTo(self.cvs_item,icon,quality,num)
      	itshow.EnableTouch = true
          	itshow.TouchClick = function()      
        local detail = UIUtil.ShowNormalItemDetail({detail = itemdetail,itemShow = itshow,autoHeight = true,autoWeight=true})         
    end
end


return _M