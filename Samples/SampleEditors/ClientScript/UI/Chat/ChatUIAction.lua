 local _M = {}
_M.__index = _M

local ChatUtil = require "UI/Chat/ChatUtil"
local Util                  = require "Logic/Util"
 
function _M:OnClickBegin(displayNode)
    --关闭
    print('TLChatActionUI OnClickBegin')
    if self.faceCb then
        self.faceCb(nil)
    end
    self.ui.menu:Close()
end

function _M:OnClickFace(index)
    print('ChatUIAction OnClickFace')
    if self.faceCb then
        self.faceCb(self.data[index])
    end
end

function _M:RefreshFace(index, node, data)
    if data then
        local img_face = node:FindChildByEditName("img_face", true)
        local btn_face = node:FindChildByEditName("btn_face", true)
        node.Visible = true
        img_face.Layout = HZUISystem.CreateLayout("#dynamic/action/output/action.xml|action|"..data.pic, UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER, 0)
        --img_face.Transform:SetPivot(Vector2(-0.5, 1.5))
        btn_face.TouchClick = function (displayNode, pos)
            self:OnClickFace(index)
        end
    else
        node.Visible = false
    end
end

local function InitData(self)
    local data = GlobalHooks.DB.GetFullTable('ChatAction')
    self.data = data
    if data ~= nil and #data > 0 then
        local column = 5
        self.ui.comps.sp_facelist:Initialize(self.ui.comps.cvs_biaoqing.Size2D.x,self.ui.comps.cvs_biaoqing.Size2D.y, (#self.data + column - 1) / column, column, self.ui.comps.cvs_biaoqing,
        function(gx,gy,obj)
            local index = (gx+1) + (gy)*column
            self:RefreshFace(index,obj,self.data[index])
        end,
        nil)
    end
end

function _M:OnInit()
    self.defaultPos = self.ui.comps.cvs_emoji.Position2D
    self.Height = self.ui.comps.cvs_emoji.Height

    self.ui.menu.ShowType = UIShowType.Cover
    self.ui.menu.IsInteractive = false
    
    self.ui.menu.event_PointerClick = function() 
        self:OnClickBegin() 
    end
    self.ui.comps.cvs_biaoqing.Visible = false

    self.btn_close = self.ui.comps.btn_close
    self.btn_close.TouchClick = function() 
        self:OnClickBegin() 
    end

    InitData(self)

end

function _M:OnEnter(pos,pos2)
    -- print_r(pos'')
    local position
    if pos then
        position = pos
    elseif pos2 then
        position = Vector2(pos2.x,pos2.y - self.Height)
    else
        position = self.defaultPos
    end

    self.ui.comps.cvs_emoji.Position2D = position
end

return _M