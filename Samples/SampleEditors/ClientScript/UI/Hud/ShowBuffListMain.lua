local _M = {}
_M.__index = _M

local UIUtil = require 'UI/UIUtil'
local TimeUtil = require 'Logic/TimeUtil'
local Util = require 'Logic/Util'

local function GetBuffShowData(id)
    return unpack(GlobalHooks.DB.Find('BuffShowData',{buff_id = id}))
end
local function OnGlobalTouchUp( self,  gameobject, point )
    if not UGUIMgr.CheckInRect(self.ui.comps.cvs_buffshow.Transform, point, true) then
        self:Close()
    end
end
local function ClearTime(self)
     if self.timeid ~= nil then
            LuaTimer.Delete(self.timeid)
            self.timeid = nil
        end
end

local function CanScroll(self)
    local is_max = #self.ClientBuffInfoList*self.tempnode.Height > self.sp_buffshow.Height
    self.sp_buffshow.Scrollable.Scroll.vertical = is_max
    return is_max
end
local function ShowBuff(self)
     if #self.ClientBuffInfoList == 0 then
        return
     end
     local function ShowBuffNode(node,index)
        if self.ClientBuffInfoList[index] == null then
           node.Visible = false
            return
        end
        local lb_buffname = UIUtil.FindChild(node,'lb_buffname', true) 
        local lb_bufftime = UIUtil.FindChild(node,'lb_bufftime', true) 
        local ib_bufficon = UIUtil.FindChild(node,'ib_bufficon', true) 
        local tb_bufftips = UIUtil.FindChild(node,'tb_bufftips', true) 
        local data = GetBuffShowData(self.ClientBuffInfoList[index].id)
        if data == nil then
            error("GetBuffShowData error with id",self.ClientBuffInfoList[index].id)
        end
        --print("ShowBuffNode",index)
        tb_bufftips.XmlText = string.format("<f size = '%d'>",tb_bufftips.FontSize) .. Util.GetText(data.buff_desc) .. "</f>"
        lb_buffname.Text = Util.GetText(data.buff_name)
        local time,_ = math.modf((self.ClientBuffInfoList[index].TotalTime - self.ClientBuffInfoList[index].PassTime)/1000)
        lb_bufftime.Text = TimeUtil.FormatToCN(time)
        UIUtil.SetImage(ib_bufficon,self.ClientBuffInfoList[index].res)
        
     end

     --print_r("ClientBuffInfoList",self.ClientBuffInfoList)
     UIUtil.ConfigVScrollPan(self.sp_buffshow,self.tempnode,#self.ClientBuffInfoList,function(node,index)
       ShowBuffNode(node,index)
     end)

     if self.timeid == nil then
         self.timeid = LuaTimer.Add(0,35,function(id) 
            if not self.ClientBuffInfoList or #self.ClientBuffInfoList == 0 then
                ClearTime(self)
                return false
            end
            local curTime = os.clock()

            for i=#self.ClientBuffInfoList, 1, -1  do
               local data = self.ClientBuffInfoList[i]
               if data.PassTime ~= data.TotalTime then
                   local timevalue = data.PassTime + (curTime - data.curTime)*1000
                   timevalue = math.min(timevalue,data.TotalTime)
                   data.PassTime = timevalue
                   data.curTime = os.clock()
                end
                if data.PassTime == data.TotalTime then
                    table.remove(self.ClientBuffInfoList,i)
                end
            end

            if #self.ClientBuffInfoList == 0 then
                self:Close()
                return
            end
            self.sp_buffshow:RefreshShowCell()
           
            if not CanScroll(self) then
                 self.sp_buffshow.Scrollable:LookAt(Vector2(0,0))
            end
            return true
         end)
     end
 
end
function _M.OnEnter( self,params)
    self.rootname = params.Rootname
    self.ClientBuffInfoList = {}
    for v in Slua.iter(params.BuffInfoList) do
        local ClientBuffInfo = v.Value.Buffinfo
        local data = {}
        data.id = ClientBuffInfo.id
        data.PassTime = ClientBuffInfo.PassTime
        data.TotalTime = ClientBuffInfo.TotalTime
        data.res = ClientBuffInfo.Data.IconName
        data.curTime = os.clock()
        
        table.insert(self.ClientBuffInfoList,data)
    end


        if not self.globalTouchKey then
            self.globalTouchKey = GameGlobal.Instance.FGCtrl:AddGlobalTouchDownHandler(params.Rootname, function( obj, point )
                if self.ui.IsRunning then
                    -- if self.IsDrag ~= nil and self.IsDrag then
                    --     return
                    -- end
                    OnGlobalTouchUp(self, obj, point)
                end
            end)
        end
        self.sp_buffshow = self.ui.comps.sp_buffshow
        -- self.sp_buffshow.Scrollable.event_Scrolled = function(sender, e)
        --     print("Scrollable.event_Scrolled")
        --     self.IsDrag = true
        -- end
        -- self.sp_buffshow.Scrollable.event_OnEndDrag = function(sender, e)
        --    print("Scrollable.event_OnEndDrag")
        --    self.IsDrag = false
        -- end
        self.tempnode =  self.ui.comps.cvs_buffinfo
        self.tempnode.Visible = false

        if #self.ClientBuffInfoList > 0 then
            ShowBuff(self)
        end
        
end

function _M.OnExit(self)

        if self.globalTouchKey then
            GameGlobal.Instance.FGCtrl:RemoveGlobalTouchDownHandler(self.globalTouchKey)
            self.globalTouchKey = nil
        end

       ClearTime(self)
end

function _M.OnInit(self)
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
    self.ui.menu.ShowType = UIShowType.Cover
    self.ui:EnableTouchFrameClose(false)
    HudManager.Instance:InitAnchorWithNode(self.ui.comps.cvs_buffshow, bit.bor(HudManager.HUD_TOP))
     
end
local function ShowDebuffList(eventname,params)
    GlobalHooks.UI.OpenUI("ShowBuffListMain",0,params)
end
local function initial()
    EventManager.Subscribe("ShowBuffList",ShowDebuffList)
end

local function fin()
    EventManager.Unsubscribe("ShowBuffList",ShowDebuffList)
end



_M.initial = initial
_M.fin = fin
return _M