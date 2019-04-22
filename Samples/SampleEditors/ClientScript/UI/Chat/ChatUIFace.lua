local _M = {}
_M.__index = _M

local ChatUtil = require "UI/Chat/ChatUtil"
local ChatFace = ChatUtil.ChatFace
local Util  = require "Logic/Util"

function _M:OnClickClose(displayNode)
    --关闭
    self.faceCb(nil)
    self.ui.menu:Close()
end

function _M:OnClickFace(index)
    -- body
    --ChatMain.addFace(index)
    if self.faceCb then
        self.faceCb(index)
    end
end

function _M:InitFace(node)
    local img_face = node:FindChildByEditName("img_face", true)

    -- 调整ImageBox的锚点, 使精灵动画的居中显示
    img_face.Scale = Vector2(0.7, 0.7)
    img_face.Position2D = Vector2(8, 8)
    img_face.Transform:SetPivot(Vector2(-0.5, 1.5))
end

function _M:RefreshFace(index, node, data)
    if data then
        local img_face = node:FindChildByEditName("img_face", true)
        local btn_face = node:FindChildByEditName("btn_face", true)
        node.Visible = true
        img_face.Layout = HZUISystem.CreateLayoutFromCpj("dynamic/emotion/output/emotion.xml", data.e_code, 0)
        btn_face.TouchClick = function (displayNode, pos)
            self:OnClickFace(index)
        end
    else
        node.Visible = false
    end
end

function _M:OnEnter(pos,pos2)
    -- self.ui.comps.cvs_emoji.Position2D = pos or self.defaultPos
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
 

function _M:OnExit()
end

function _M:OnInit()
    self.defaultPos = self.ui.comps.cvs_emoji.Position2D
    self.Height = self.ui.comps.cvs_emoji.Height
 
 
    self.ui.menu.ShowType = UIShowType.Cover
    self.ui.menu.IsInteractive = false

    self.ui.menu.event_PointerClick = function() self:OnClickClose() end
    self.ui.comps.cvs_biaoqing.Visible = false

    self.btn_close = self.ui.menu:GetComponent("btn_close")
    self.btn_close.TouchClick = function() 
        self:OnClickClose() 
    end

    local column = 7
    self.ui.comps.sp_facelist:Initialize(self.ui.comps.cvs_biaoqing.Size2D.x,self.ui.comps.cvs_biaoqing.Size2D.y, (#ChatFace + column - 1) / column, column, self.ui.comps.cvs_biaoqing,
        function(gx,gy,obj)
            local index = (gx+1) + (gy)*column
            self:RefreshFace(index,obj,ChatFace[index])
        end,
        function(node)
            self:InitFace(node)
        end)
end

return _M