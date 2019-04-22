local _M = {}
_M.__index = _M

local Util = require 'Logic/Util.lua'
local UIUtil = require 'UI/UIUtil.lua'
local ItemModel = require 'Model/ItemModel'
local ShopModel = require 'Model/ShopModel'
local ShopUIUtil = require 'UI/Store/ShopUIUtil'

function _M.OnEnter( self, storeType, ...)
    --SoundManager.Instance:PlaySoundByKey('uiopen',false)
end

function _M.OnLoad(self, callBack, storeType, OpenStoreType, SeleteItemId,...)
    if type(storeType) == 'table' then
        storeType = tonumber(GlobalHooks.DB.GetGlobalConfig(storeType.shopKey))
    end

    print('Shop storeType:',storeType)
    self.currentTag = nil
    self.enterParams = {...}

    local tableData = GlobalHooks.DB.Find('Store_Type',{store_id = storeType})
 
    local default
    local tbts = {}
    for i=1,10 do
        local comp = self.ui.comps['tbt_an' .. i]
        if comp == nil then
            break
        end

        local ShopType = tableData[i]
        if ShopType ~= nil then
            comp.UserTag = ShopType.store_type
            if OpenStoreType ~=nil and OpenStoreType == ShopType.store_type then
                default = comp
            end
            comp.Text = Util.GetText(ShopType.type_name)
            table.insert(tbts, comp)
            comp.Visible = true
        else
            comp.Visible = false
        end
    end
 
    self.tbts = tbts

    default = default or self.ui.comps['tbt_an1']
    local function ToggleFunc(sender)   
        local tag = sender.UserTag
        self.storeType = tag
 

        ShopModel.RequestGetStoreBoughtInfo(self.storeType,function(resp)
            if resp:IsSuccess() then
                local boughtMap = resp.s2c_data
              
                if boughtMap == nil then
                    print('boughtMap is nil')
                    return
                end

                -- local salelist = resp.salelist

                ShopUIUtil.updateBoughtMap(self.storeType,boughtMap)
              -- GameAlertManager.Instance:ShowNotify('商店类型'..self.storeType)
              -- self.defaultIndex = 1
                ShopUIUtil.showScrollPan(self,tag,boughtMap,SeleteItemId)
                SeleteItemId = nil

                if callBack then
                    callBack:Invoke(true)
                    callBack = nil
                end
            else 
                if callBack then
                    callBack:Invoke(true)
                    callBack = nil
                end
              -- self.boughtMap = nil
                self.ui.menu:Close()
            end
        end)
    end
	UIUtil.ConfigToggleButton(self.tbts, default, false, ToggleFunc)
end

function _M.OnExit( self )

	  print('Shop OnExit')

end

function _M.OnDestory( self )

	  print('Shop  OnDestory')

end

function _M.OnInit( self )
    
    -- self.ui.menu.ShowType = UIShowType.Cover
    -- self.ui.comps.cvs_background.Enable = false

    -- self.ui:EnableTouchFrame(false)
    -- self.ui:EnableChildren(false)


    ShopUIUtil.InitCompmont(self)

end

return _M