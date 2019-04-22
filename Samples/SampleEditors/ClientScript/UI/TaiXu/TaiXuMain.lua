-- 太虚幻境
-- 作者：任祥建
-- 日期：2018.10.9

local _M = {}
_M.__index = _M

local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'
local ItemModel = require 'Model/ItemModel'

local curselectmain = 1


local function ShowTips( itemid,posX,posY )
    local detail = UIUtil.ShowNormalItemDetail({
        templateID = itemid,
        autoHeight = true,
        autoWidth = true})
    local size = detail.ui.comps.cvs_itemtips.Size2D
    if posX-size[1] < 0 then
        size[1] = posX
    end
    detail:SetPos(posX-size[1],posY-size[2])
    local function OnDetailExit()
        detail.Visible = false
    end
    detail:SubscribOnExit(OnDetailExit)
end


local function GetMainData()
    local detail = GlobalHooks.DB.GetFullTable('taixuillusory')
    return detail
end
local function GetSubData(groupid)
    local detail = GlobalHooks.DB.Find('taixuillusory_sub', {group_id = groupid})
    return detail
end


--获取进入次数  0就返回所有太虚次数 否则返回对应groupid的次数
local function RequestTaiXuEnterCount(groupid,cb)
    local request= {c2s_index=groupid}
    Protocol.RequestHandler.TLClientTaiXuListRequest(request, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end


--查找剩余次数
local function FindEnterCount(self,groupid)
    for i, v in ipairs(self.count) do
        if self.count[i].groupid==groupid then
            return v
        end
    end
end


--扫荡
local function RequestTaiXuSweep(self,groupid,subid,cb)
    local request= {c2s_groupid=groupid,c2s_subid=subid}
    Protocol.RequestHandler.TLClientTaiXuSweepRequest(request, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end


local function InitSubList(self,groupid,index,node,havecount)
    local lb_subname = node:FindChildByEditName('lb_titlename', true)
    local sp_itemlist = node:FindChildByEditName('sp_itemlist', true)
    local cvs_itemicon = node:FindChildByEditName('cvs_itemicon', true)
    local btn_go = node:FindChildByEditName('btn_go', true)
    local btn_mop = node:FindChildByEditName('btn_mop', true)

    lb_subname.Text = Util.GetText(self.subdata[index].sub_name)
    local showitemcount = 0
    for i, v in pairs(self.subdata[index].preview.id) do
        if v ~= 0 then
            showitemcount = showitemcount + 1
        end
    end
    btn_go.Enable = havecount
    btn_go.IsGray = not havecount
    btn_mop.Enable = havecount
    btn_mop.IsGray = not havecount
    
    UIUtil.ConfigHScrollPan(sp_itemlist, cvs_itemicon, showitemcount, function(node,index1)
        local cvs_item = node:FindChildByEditName('cvs_item', true)
        local itemdetail = ItemModel.GetDetailByTemplateID(self.subdata[index].preview.id[index1])
        UIUtil.SetItemShowTo(cvs_item,itemdetail,1)
        cvs_item.TouchClick = function(sender)
            local temp = self.ui.root:GlobalToLocal(sender:LocalToGlobal(),true)
            ShowTips(itemdetail.static.id,temp[1],temp[2])
        end
    end)
    cvs_itemicon.Visible = false

    btn_go.TouchClick = function(sender)
        local result = FunctionUtil.OpenFunction(self.subdata[index].function_id)
        if result then
            self:Close()
        end
    end
    
    --扫荡
    btn_mop.TouchClick = function(sender)
        if GlobalHooks.IsFuncOpen('fairtyland_mop_up',true) then
            RequestTaiXuSweep(self,groupid,self.subdata[index].sub_id,function (rsp)
                for i, v in ipairs(self.count) do
                    if self.count[i].groupid==rsp.s2c_groupid then
                        self.count[i].curTimes=rsp.s2c_times
                        self.lb_surplusnum1.Text=
                        (self.count[i].MaxTimes - self.count[i].curTimes >0 
                                and {self.count[i].MaxTimes - self.count[i].curTimes} or {0})[1]
                        self.uicount[i].Text=self.count[i].MaxTimes - self.count[i].curTimes
                    end
                end
            end)
        end
    end
end


local function InitMainList(self,index,node)
    
    local y=(index% 2 ==1 and {-200} or {0})[1]
    node.Transform.localPosition=Vector2(node.Transform.localPosition.x+70,y) 
    
    local lb_title = node:FindChildByEditName('lb_title', true)
    local ib_design = node:FindChildByEditName('ib_design', true)
    local lb_levelreq = node:FindChildByEditName('lb_levelreq', true)
    local lb_surplusnum = node:FindChildByEditName('lb_surplusnum', true)
    local cvs_locked=node:FindChildByEditName('cvs_locked',true)
    local cvs_count=node:FindChildByEditName('cvs_count',true)
    
    table.insert(self.uicount,lb_surplusnum)

    local info=FindEnterCount(self,self.maindata[index].group_id)
    
    lb_title.Text = Util.GetText(self.maindata[index].illusory_name)
    lb_title.Transform:GetChild(0).gameObject:GetComponent('Text').horizontalOverflow= UnityEngine.HorizontalWrapMode.Wrap
    lb_levelreq.Text = Util.GetText(self.maindata[index].explain)
    lb_surplusnum.Text =(info.MaxTimes - info.curTimes > 0 and {info.MaxTimes - info.curTimes} or {0})[1] 
    
    UIUtil.SetImage(ib_design,self.maindata[index].res,false,UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER)
    local islock = self.maindata[index].level > DataMgr.Instance.UserData.Level
    node.IsGray=islock
    cvs_count.Visible=not islock
    cvs_locked.Visible = islock
    local lb_reqlev =cvs_locked:FindChildByEditName('lb_reqlev',true)
    lb_reqlev.Text=Util.GetText("PlayerNeedLevel",self.maindata[index].level)

    node.TouchClick = function(sender)
        if islock then
            GameAlertManager.Instance:ShowNotify(Util.GetText(Constants.PagodaMain.LevelNotEnoughTips))
        else
            local groupid= self.maindata[index].group_id
            self.subdata = GetSubData(self.maindata[index].group_id)
            UIUtil.ConfigVScrollPan(self.sp_inlist, self.cvs_info, #self.subdata, function(node,index1)
                InitSubList(self,groupid,index1,node,self.maindata[index].curcount ~= 0)
            end)

            curselectmain = index
            self.lb_title.Text = Util.GetText(self.maindata[groupid].illusory_name)
            local time=FindEnterCount(self,self.maindata[index].group_id)
            self.lb_surplusnum1.Text = (time.MaxTimes - time.curTimes >0 and {time.MaxTimes - time.curTimes} or {0})[1]
            self.cvs_info.Visible = false
            self.cvs_chooselevel.Visible = true
        end
    end
end


function _M.OnEnter( self )
    self.maindata = GetMainData()
    self.cvs_chooselevel.Visible = false
    self.cvs_taixu.Visible = false
    self.uicount={}
    RequestTaiXuEnterCount(0,function (rsp)
        self.count=rsp.s2c_data
        UIUtil.ConfigHScrollPan(self.sp_list, self.cvs_taixu, #self.maindata,function(node,index)
            InitMainList(self,index,node)
        end)
    end)
    self.btn_close.TouchClick = function(sender)
        self.cvs_chooselevel.Visible = false
    end
end


function _M.OnExit( self )

end


function _M.OnInit(self)
    self.cvs_taixu = self.root:FindChildByEditName('cvs_taixu', true)
    self.cvs_chooselevel = self.root:FindChildByEditName('cvs_chooselevel', true)
    self.sp_list = self.root:FindChildByEditName('sp_list', true)
    self.lb_title = self.cvs_chooselevel:FindChildByEditName('lb_title', true)
    self.btn_close = self.cvs_chooselevel:FindChildByEditName('btn_close', true)
    self.lb_surplusnum1 = self.cvs_chooselevel:FindChildByEditName('lb_surplusnum1', true)
    self.sp_inlist = self.cvs_chooselevel:FindChildByEditName('sp_list', true)
    self.cvs_info = self.cvs_chooselevel:FindChildByEditName('cvs_info', true)
end


return _M
