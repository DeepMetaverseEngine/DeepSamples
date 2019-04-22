-----------------适用于普通副本结算界面------------------
local _M = {}
_M.__index = _M

local UIUtil = require 'UI/UIUtil'
local ItemModel = require 'Model/ItemModel'
local Dungeon= require 'Model/DungeonModel'
local TimeUtil=require 'Logic/TimeUtil'


--结算界面掉落列表
local function SetDropList(self,params,node,index)

    local itemdetail=ItemModel.GetDetailByTemplateID(tonumber(params[index].TemplateID))
--[[    local quality = itemdetail.static.quality
    local icon=itemdetail.static.atlas_id]]
    local num =params[index].Qty
    local itshow=UIUtil.SetItemShowTo(node,itemdetail,num)
      itshow.EnableTouch = true
          itshow.TouchClick = function() 
          SoundManager.Instance:PlaySoundByKey('button',false)     
        local detail = UIUtil.ShowNormalItemDetail({x=node.X+290,y=node.Y+35,detail = itemdetail,itemShow = itshow,autoHeight = true,autoWeight=true})         
    end	

end


function _M.OnInit(self)
    
    --设置动画
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.Scale)
    
    self.lb_exp=self.ui.comps.lb_exp
    self.lb_money=self.ui.comps.lb_money

    self.sp_drops=self.ui.comps.sp_drops
    self.cvs_dpitem=self.ui.comps.cvs_dpitem 
    self.cvs_dpitem.Visible=false

    --通关时间
    self.lb_timenum=self.ui.comps.lb_timenum

    --离开时间
    self.lb_betime=self.ui.comps.lb_betime
    self.btn_fbleave=self.ui.comps.btn_fbleave

    self.lb_none=self.ui.comps.lb_none
    self.cvs_rewardlist=self.ui.comps.cvs_rewardlist
    
end


function _M.OnEnter(self,params)

    --获取地图类型
    local mapType=Dungeon.GetMapType(DataMgr.Instance.UserData.MapTemplateId)
    
    --如果是镇妖塔，不需要离开按钮
    if mapType==4 then 
        self.btn_fbleave.Visible=false
        self.lb_betime.Visible=false
    else
        self.btn_fbleave.Visible=true
        self.lb_betime.Visible=true
    end

    --退出副本倒计时
    self._time=math.abs(TimeUtil.TimeLeftSec(params.counttime)) 
    self.myTimer = LuaTimer.Add(
        0,
        1000,
        function()
            if self._time <= 0 then
                self._time = nil
                self.lb_betime.Text = 0
                self.ui:Close()
                return false
            else
                self._time=self._time-1
                self.lb_betime.Text = self._time
                return true              
            end
        end)

    --如果有掉落，设置掉落列表
    if not params.noAward then
        self.cvs_rewardlist.Visible=true
        self.lb_none.Visible=false

        local exp=0
        local gold=0
        --把掉落列表里的钱和金币移除，分开计算
        local drop=params.itemList
        for i = #drop, 1, -1 do
            if drop[i].TemplateID==1 or drop[i].TemplateID==4 then
                if drop[i].TemplateID==4 then 
                   exp=exp+drop[i].Qty
                end

                if drop[i].TemplateID==1 then
                    gold=gold+drop[i].Qty
                end
                table.remove(drop, i)
            elseif drop[i].Qty==0 then 
                table.remove(drop, i)
            end
        end

        --设置经验和金币
        self.lb_exp.Text=params.exp+exp
        self.lb_money.Text=params.gold+gold

        --设置掉落列表
        local function SetDrop(node,index)      
            SetDropList(self,drop,node,index)
        end    
        UIUtil.ConfigHScrollPanWithOffset(self.sp_drops,self.cvs_dpitem,#drop,10,SetDrop)    

    else--否则显示无掉落cvs
        self.cvs_rewardlist.Visible=false
        self.lb_none.Visible=true
    end

    --通关时间
    self.lb_timenum.Text=TimeUtil.SecToTimeformatToMS(params.finishtime)

    --离开副本请求，回调参数里的函数
    self.btn_fbleave.TouchClick=function()
       params.cb()
    end 

end


function _M.OnExit(self)

    --释放计时器
    if self.myTimer then
        LuaTimer.Delete(self.myTimer)
    end    
end


return _M