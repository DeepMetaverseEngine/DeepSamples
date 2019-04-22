local _M = {}
_M.__index = _M

local Util = require 'Logic/Util.lua'
local UIUtil = require 'UI/UIUtil.lua'
local ItemModel = require 'Model/ItemModel'
local ShopModel = require 'Model/ShopModel'


local noLimit = 999


local storeBoughtMap = {}

local function updateBoughtMap(storeType,boughtMap)
    -- body
    storeBoughtMap[storeType] = boughtMap
end

--购买物品成功之后更新数据
local function updateBoughtInfo(storeType,templateId,itemNum)
    -- body
    if storeBoughtMap[storeType] == nil then
        local boughtMap = {}
        storeBoughtMap[storeType] = boughtMap
    end

    local boughtMap = storeBoughtMap[storeType]
    if boughtMap[templateId] == nil then
        boughtMap[templateId] = {}
        boughtMap[templateId].boughtNum = itemNum
    else
        boughtMap[templateId].boughtNum = boughtMap[templateId].boughtNum + itemNum
    end
end

local function getBoughtNum(storeType,templateId)
    local boughtMap = storeBoughtMap[storeType] 
    if boughtMap == nil then
        return 0
    else
        if boughtMap[templateId] then
            return boughtMap[templateId].boughtNum,boughtMap[templateId].vipTimes or 0
        end
        return 0
    end
end
 
local function getCanByNum(self ,itemData)
    -- body
    local limitType = itemData.restriction_type
    if limitType == 0 then
        return 0,noLimit
    end

    -- TODO 减去已经购买的个数
    local boughtNum,vipTimes = getBoughtNum(self.storeType,itemData.item_id)
    local canBuyNum = itemData.restriction_num - boughtNum
    if vipTimes then
        canBuyNum = canBuyNum + vipTimes 
    end
    return limitType,canBuyNum,vipTimes
end


local function addCallBack(self, ... )
  -- body
  -- print('addCallBack')
 
    local item = self.storeData[self.defaultIndex]

    local limitType,canByNum = getCanByNum(self,item)
    if self.buyNum >= canByNum then
        return
    end

    self.buyNum = self.buyNum + 1

    -- print('addCallBack self.buyNum :',self.buyNum)
    self.coutLabel.Text = self.buyNum
    local total_cost = item.cost_num * self.buyNum
    self.buyNumLabel.Text = total_cost
end

local function subCallBack(self, ... )
  -- print('subCallBack')
    if self.buyNum > self.initBuyNum then
        self.buyNum = self.buyNum - 1
    else
        return
    end
    
    self.coutLabel.Text = self.buyNum
    
    local item = self.storeData[self.defaultIndex]
    local total_cost = item.cost_num * self.buyNum
    self.buyNumLabel.Text = total_cost
end

local function showLimit(self,itemData)
  -- body
    local limitType,canByNum,vipTimes = getCanByNum(self,itemData)
    if canByNum == noLimit then
        self.limitLabel.Visible = false
        self.limitnumLabel.Visible = false
    else
        self.limitLabel.Text = Constants.StroeLimit[limitType]
        local showText = canByNum
        if vipTimes and vipTimes > 0 then
            local vip = DataMgr.Instance.UserData.VipLv
            showText = canByNum .. Util.GetText(Constants.Text.show_VipTimes,vip,vipTimes)
        end
        self.limitnumLabel.Text = showText
        self.limitLabel.Visible = true
        self.limitnumLabel.Visible = true
    end
end

local function getDiscount(itemData)
  -- body
    local discount = false
    if itemData.cost_num == itemData.money_base then
        return discount
    else
        _integer,_decimal = math.modf(itemData.cost_num / itemData.money_base) 
        --local value =  _decimal * 10 .. Constants.Text.shop_Discount
        local x = math.floor(_decimal * 100) / 10
        local a,b = math.modf(x)
        local value 
        if b > 0 then
            value = a .. b*10 .. Constants.Text.shop_Discount
        else
            value = a  .. Constants.Text.shop_Discount
        end
        discount = true
        return discount,value
    end
end

local function showPrice(self,itemData)
  -- body
    self.moneybaseLabel.Text = itemData.money_base
    self.moneycostLabel.Text = itemData.cost_num

    local discount,value = getDiscount(itemData)
    if discount then
        self.saleImg.Visible = true
        self.saleLabel.Visible = true
        self.lineLabel.Visible = true
        self.saleLabel.Text = value

        self.costInitialCvs.Visible = true
        self.costNowCvs.Visible = true
 
    else
        self.saleImg.Visible = false
        self.saleLabel.Visible = false
        self.lineLabel.Visible = false

        -- 老王说  不打折的时候不显示原价现价
        self.costInitialCvs.Visible = false
        self.costNowCvs.Visible = false

    end

    local detail = ItemModel.GetDetailByTemplateID(itemData.cost_id)
    local itemIcon = 'static/item/' .. detail.static.atlas_id
    
    UIUtil.SetImage(self.huobi1,itemIcon)
    UIUtil.SetImage(self.huobi2,itemIcon)
    UIUtil.SetImage(self.huobi3,itemIcon)
    UIUtil.SetImage(self.huobi4,itemIcon)

    self.buyNum = self.initBuyNum
    self.coutLabel.Text = self.initBuyNum

     
    -- 售价
    self.buyNumLabel.Text = itemData.cost_num

end

local function showDetialPanel(self,itemData,itemDetial,itemName)

    self.itemNameLabel.Text = itemName
    self.itemDescLabel.XmlText = '<f>'..Util.GetText(itemDetial.static.desc)..'</f>'

    showLimit(self,itemData)

    showPrice(self,itemData)

	  -- TODO 拥有到时候从背包里取，现在做个假的
    -- local boughtNum = getBoughtNum(self.storeType,item.item_id)
    
    local  value = ItemModel.CountItemByTemplateID(itemData.cost_id)
    -- print('owerValue:',value)
    self.ownNumLabel.Text = value
end
 
local function itemTouchClick(self,togButton,index,itemData,itemDetial,itemName)
    if index == self.defaultIndex then
        togButton.IsChecked = true
    elseif  self.selectedTogButton  then 
        self.selectedTogButton.IsChecked = false
        togButton.IsChecked = true
        self.selectedTogButton = togButton
        self.defaultNode = node
        self.defaultIndex = index
        showDetialPanel(self,itemData,itemDetial,itemName)
    end
end

local function showItemShow(self,node,index,itemData,itemDetial,itemName,togButton)
  -- body
    local itemIcon = UIUtil.FindChild(node,'cvs_itemicon')
    local itShow = UIUtil.SetItemShowTo(itemIcon,itemDetial.static.atlas_id, itemDetial.static.quality)
    itShow.EnableTouch = true
    itShow.TouchClick = function ( ... )
 
        itemTouchClick(self,togButton,index,itemData,itemDetial,itemName,togButton)
    -- body
        -- local detail = UIUtil.ShowNormalItemDetail({detail=itemDetial,itemShow=itShow,autoHeight=true})
      
        -- local togButton = UIUtil.FindChild(node,'tbt_opt')
        -- itemTouchClick(self,togButton,index,itemData,itemDetial,itemName)
    end
end

local function showTogButton(self,node,index,itemData,itemDetial,itemName)
  -- body
	local togButton = UIUtil.FindChild(node,'tbt_opt')
	togButton.IsChecked = false

	if self.defaultIndex == index then
 
		togButton.IsChecked = true
		self.selectedTogButton = togButton
        self.defaultNode = node

		showDetialPanel(self,itemData,itemDetial,itemName)
	else
		togButton.IsChecked = false
	end

	togButton.TouchClick = function ( ... )
        itemTouchClick(self,togButton,index,itemData,itemDetial,itemName)
	end

    showItemShow(self,node,index,itemData,itemDetial,itemName,togButton)
end




local function showItemPrice(self,node,itemData)
  -- body
    local moneyImg = UIUtil.FindChild(node,'ib_huobi')
    local detail = ItemModel.GetDetailByTemplateID(itemData.cost_id)

    local itemIcon = 'static/item/' .. detail.static.atlas_id
    UIUtil.SetImage(moneyImg,itemIcon)

    local priceLabel = UIUtil.FindChild(node,'lb_itemprice')
    priceLabel.Text = itemData.cost_num

    local saleImg  = UIUtil.FindChild(node,'ib_sale')
    local saleLabel = UIUtil.FindChild(node,'lb_salenum1')
    local oldV3 = saleLabel.Transform.localEulerAngles
    local v3 = Vector3(oldV3.x,oldV3.y,-30)
    saleLabel.Transform.localEulerAngles = v3
 

    local discount,value = getDiscount(itemData)
    if discount then
        saleImg.Visible = true
        saleLabel.Visible = true
        saleLabel.Text = value
    else
        saleImg.Visible = false
        saleLabel.Visible = false
    end

end 


local function showItemlimitDay(self,node,itemData)
  -- body

    local timeLabel = UIUtil.FindChild(node,'lb_time')
    timeLabel.Visible = false

    local text = ShopModel.GetTimeText(itemData)
    if not string.IsNullOrEmpty(text) then
        timeLabel.Visible = true
        timeLabel.Text = text
    end
end 


local function showItem(self,node,index)
    local itemData = self.storeData[index]
 
	    if itemData == nil then
		    node.Visible = false
        return
    end

    local detail = ItemModel.GetDetailByTemplateID(itemData.item_id)
 
    local itemName = Util.GetText(detail.static.name)
    local itemNameLabel = UIUtil.FindChild(node,'lb_itemname')
    itemNameLabel.Text = itemName
 
    showTogButton(self,node,index,itemData,detail,itemName)

   

    showItemPrice(self,node,itemData)
	
    showItemlimitDay(self,node,itemData)

end


local function updateItem(self,node,index)
    local itemData = self.storeData[index]
    if itemData == nil then
        node.Visible = false
        return
    end
end

local function updateDetialPanel(self,index)
  
    local itemData = self.storeData[index]

    local limitType,canByNum = getCanByNum(self,itemData)
    self.limitnumLabel.Text = canByNum
    -- local detail = ItemModel.GetDetailByTemplateID(item.item_id)

    -- TODO 拥有到时候从背包里取，现在做个假的
    -- local boughtNum = getBoughtNum(self.storeType,item.item_id)
    -- local  value = DataMgr.Instance.UserData:GetAttribute(UserData.NotiFyStatus.DIAMOND)
    local  value = ItemModel.CountItemByTemplateID(itemData.cost_id)
    self.ownNumLabel.Text = value
end


local function buy(self)
    local itemData = self.storeData[self.defaultIndex]
 
    if itemData == nil then
        GameAlertManager.Instance:ShowNotify(Constants.Text.shop_noItem)
        return
    end

    if DataMgr.Instance.UserData.Bag.EmptySlotCount == 0 then
        GameAlertManager.Instance:ShowNotify(Util.GetText('common_bagfull'))
        return
    end
	
    ShopModel.RequestBuyItem(self.storeType,itemData.item_id,self.buyNum,function(resp)
        local storeType = self.storeType
        local itemId = itemData.item_id
        local itemNum = self.buyNum
 
        updateBoughtInfo(storeType,itemId,itemNum)

        updateDetialPanel(self,self.defaultIndex)
        updateItem(self,self.defaultNode,self.defaultIndex)
        GameAlertManager.Instance:ShowNotify(Constants.Text.shop_buysucc)
	end)
end


local function SelectByIndex(self, index)
    UIUtil.MoveToScrollCell(
        self.pan,
        index,
        function(node)
            
        end
    )
end

local function showScrollPan(self,storeType,salelist,SeleteItemId)

    self.initBuyNum = 1 
    self.buyNum = 1
    self.selectedTogButton = nil

    self.storeData = ShopModel.GetStoreData(storeType,salelist)

    self.defaultIndex = 1
    if SeleteItemId ~= nil then
        for k,item in pairs(self.storeData) do
            if item.item_id == SeleteItemId then
                self.defaultIndex = k
            end
        end
    end

    local function eachUpdateCb(node, index)
        showItem(self,node,index)
    end
  
    local col = 2
    UIUtil.ConfigGridVScrollPan(self.pan,self.tempnode,col,#self.storeData,eachUpdateCb)
    SelectByIndex(self,self.defaultIndex)

end


local function InitCompmont(self)
    -- body
    --名称
    self.itemNameLabel = self.ui.comps.lb_name
    --描述
    self.itemDescLabel = self.ui.comps.tb_tips

    --限购类型
    self.limitLabel = self.ui.comps.lb_limit
    --限购数量
    self.limitnumLabel = self.ui.comps.lb_limitnum
  

    self.costInitialCvs = self.ui.comps.cvs_initial
    self.costNowCvs = self.ui.comps.cvs_now

    --原价
    self.moneybaseLabel = self.ui.comps.lb_initialnum
  
    --现价
    self.moneycostLabel = self.ui.comps.lb_nownum
  

    self.huobi1 = self.ui.comps.ib_huobi1
    self.huobi2 = self.ui.comps.ib_huobi2
    self.huobi3 = self.ui.comps.ib_huobi3
    self.huobi4 = self.ui.comps.ib_huobi4

    --折扣图标
    self.saleImg = self.ui.comps.ib_sale
    self.lineLabel =  self.ui.comps.lb_text2
    self.saleLabel = self.ui.comps.lb_salenum


    local oldV3 = self.saleLabel.Transform.localEulerAngles
    -- comp.Transform.pivot = Vector2(0.5,0.5)
    local v3 = Vector3(oldV3.x,oldV3.y,-30)
    self.saleLabel.Transform.localEulerAngles = v3
 
        -- 售价
    self.buyNumLabel = self.ui.comps.lb_pricenum
    -- 拥有
    self.ownNumLabel = self.ui.comps.lb_ownnum

          -- 购买按钮
    self.buyButton = self.ui.comps.btn_begin
    self.buyButton.TouchClick = function ( ... )
        print('buy sth ~~')
        buy(self)
    end

    self.addButton = self.ui.comps.btn_plus
    self.addButton.TouchClick = function ( ... )
        addCallBack(self)
    end

    self.subButton = self.ui.comps.btn_minus
    self.subButton.TouchClick  = function ( ... )
        subCallBack(self)
    end

    -- 数量
    self.coutLabel = self.ui.comps.lb_num
    self.cvsNum = self.ui.comps.cvs_num
    self.cvsNum.TouchClick = function ( sender )
      -- print('hello count click')
        local item = self.storeData[self.defaultIndex]
        if item == nil then 
            return
        end
        local limitType,canByNum = getCanByNum(self,item)
        local maxNum = canByNum
        -- maxNum = 999
        local minNum = 1
        if canByNum == 0 then
            minNum = canByNum
        end

        GlobalHooks.UI.OpenUI('NumInput', 0, minNum, maxNum, {pos=nil, anchor=nil}, function(value)
            self.buyNum = value
            self.coutLabel.Text = self.buyNum
            local total_cost = item.cost_num * self.buyNum;
            self.buyNumLabel.Text = total_cost
        end)
    end


    self.tempnode = self.ui.comps.cav_goods
    self.tempnode.Visible = false 
    self.pan = self.ui.comps.sp_oar
end


_M.updateBoughtMap = updateBoughtMap
_M.showScrollPan = showScrollPan
_M.InitCompmont = InitCompmont

return _M