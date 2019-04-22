local DisplayUtil = require("Logic/DisplayUtil")
local Util = require("Logic/Util")

local ItemUseUI = {}
DisplayUtil.warpOOPSelf(ItemUseUI)

function ItemUseUI:OnInit()
    self.ui.comps.btn_close.TouchClick = self._self_Close
    self.ui.comps.btn_use.TouchClick = self._self_onUseBtnClick
    self.ui.menu.ShowType = UIShowType.Cover

    self.awardIcons = { self.ui.comps.cvs_item }
end

function ItemUseUI:onUseBtnClick()

    local itemindex = DataMgr.Instance.UserData.Bag:FindFirstTemplateItemIndex(self.templateId)
    if itemindex ~= 0 then
        DataMgr.Instance.UserData.Bag:Use(itemindex, 1, function(ret)
            if ret then
                print("Useitem success")
            end
        end)
    end
    

    -- local msg = {c2s_templateID = self.templateId, c2s_count = 1}
    -- Protocol.RequestHandler.ClientUseItemByTemplateIdRequest(msg, function(ret)
    --     print_r(ret)
    --     if ret.s2c_code == 200 then
    --     end
    -- end)
    self:Close()
end

function ItemUseUI:OnEnter(templateId)
    self.templateId = templateId
    DisplayUtil.fillAwards(self.awardIcons, {{ templateId, 1}})

    HudManager.Instance.SkillBar.Visible = false;
end

function ItemUseUI:OnExit()
    local uis = { GlobalHooks.UI.FindUI("ItemUseUI") }
    if #uis == 0 or (#uis == 1 and uis[1] == self) then
        HudManager.Instance.SkillBar.Visible = true;
    end
end
function ItemUseUI:OnDestory()
end

return ItemUseUI
