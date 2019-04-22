local _M = {}
_M.__index = _M
local ItemModel = require 'Model/ItemModel'
_M.ItemsTable = {}

_M.Recharging=false
_M.MedicineLimitCount = 50

function _M.SaveOptions(autoUseItem,autoput,useThreshold,cb)
  print('---------MedicineModel.SaveOptions----------')
   local d = DataMgr.Instance.UserData.GameOptionsData
        d.AutoUseItem = autoUseItem
        d.AutoRecharge = autoput
        d.UseThreshold = useThreshold
        DataMgr.Instance.UserData.GameOptionsData = d
  local request = 
  { 
  
  	c2s_AutoUseItem = autoUseItem,
  	c2s_AutoRecharge = autoput,
  	c2s_UseThreshold = useThreshold
  }
  Protocol.RequestHandler.ClientSaveOptionsRequest(request, function(rsp)
    if cb then
      if rsp.s2c_code == 200 then
      end
      cb(rsp)
    end
  end)
end

function _M.SaveMedicineItemID(itemID)
    print('---------SaveMedicineItemID----------',itemID)
     local d = DataMgr.Instance.UserData.GameOptionsData
        d.itemID = itemID 
        DataMgr.Instance.UserData.GameOptionsData = d
     local request = 
     { 
        c2s_itemID = itemID
     }
   
    Protocol.RequestHandler.ClientSaveMedicineItemRequest(request, function(rsp)
      if cb then
          if rsp.s2c_code == 200 then
          end
      end
    end)
end

function _M.DoRechargeMedicinePool(cb)
    local request = {}
    Protocol.RequestHandler.ClientRechargeMedicinePoolRequest(request,function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

  local function DoSmartSelectItem(eventname,params)
--血瓶对应背包内的数量.
  local itemCountInBag = {}
  --排序
  local itemIndex = {}
  --获取所有种类的血拼及对应数量.
  local itemMapLength = 0
  for i,v in pairs(_M.ItemsTable) do
    itemMapLength = itemMapLength + 1
    local itemCount =  DataMgr.Instance.UserData.Bag:Count(v.item_id)
    itemCountInBag[v.item_id] = itemCount
    itemIndex[itemMapLength] = v.item_id 
  end

   local d = DataMgr.Instance.UserData.GameOptionsData
   --当前选取的物品ID.
   local curSelectID = d.itemID
   local curIndex = #itemIndex
  --print('MedicineModel DoSmartSelectItem =========================curSelectID = '..curSelectID)
  for i=#itemIndex,1,-1 do
    curIndex = i
    if curSelectID ==  itemIndex[i] then
      break
    end
    
  end
--print('MedicineModel DoSmartSelectItem =========================当前药品INDEX = '..curIndex)

  local selectID = 0
  for i=curIndex,1,-1 do
     selectID = itemIndex[i]
      if itemCountInBag[selectID] > 0 then

      local itemdetail = ItemModel.GetDetailByTemplateID(selectID)    
          if itemdetail.static.level_limit <= DataMgr.Instance.UserData.Level then
             _M.SaveOptions( d.AutoUseItem,d.SmartSelect,d.UseThreshold,selectID,function(sender) end)
          end
      break
      end
  end
      --  print('MedicineModel DoSmartSelectItem ========================='..selectID)
end

local function OnInitFirst( ... )
    print('---------MedicineModel.OnInitFirst----------')
    _M.ItemsTable = {}
    --获取血瓶表.
    local MedicineTelmpalteData = GlobalHooks.DB.Find('MedicineItemData', {})
    
    --物品ID表.
    for k, v in pairs(MedicineTelmpalteData) do
        table.insert( _M.ItemsTable,v)
    end
    --根据权重升序排序.
    table.sort( _M.ItemsTable,function (a,b)
                  if a.item_array < b.item_array then return true
                  else return false   
                  end   
    end)
end


function _M.GetOneDataByPlayerLv(lv)
    local temp =unpack(GlobalHooks.DB.Find('MedicinePool', {level=lv}))
    return temp
end
--通过修行之道获取额外血量加成
function _M.GetAdditionByPracticeLv(lv)
    local temp =unpack(GlobalHooks.DB.Find('MedicinePoolAddition',{artifact_stage = lv}))
    if temp then
        return temp.add
    end
end


local function initial()
    EventManager.Subscribe('Event.Scene.FirstInitFinish', OnInitFirst)
    EventManager.Subscribe("DoSmartSelectItem",DoSmartSelectItem)
end

local function fin()
    _M.ItemsTable = nil
    EventManager.Unsubscribe("DoSmartSelectItem",DoSmartSelectItem)
    EventManager.Unsubscribe('Event.Scene.FirstInitFinish', OnInitFirst)
end

_M.fin = fin
_M.initial = initial
return _M