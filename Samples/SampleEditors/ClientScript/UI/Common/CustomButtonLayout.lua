local _M = {}
_M.__index = _M

local Util = require("Logic/Util")


local function SetCompents(self,col,btntxts,pos)
    
    self.ui.comps.cvs_menu.Width = (col+1)*self.gapX+col*self.btnW
    self.ui.comps.cvs_menu.Height = 2*self.borederY+(math.ceil(#btntxts/col))*(self.gapY+self.btnH) +self.gapY
    for i, v in ipairs(self.btns) do
        v.X = (self.gapX + self.btnW) * ((i-1) % col) + self.gapX
        v.Y = (self.gapY + self.btnH) * (math.ceil(i/col)-1) + self.gapY +self.borederY
    end
    if pos then
        self.ui.comps.cvs_menu.X = pos[1]
        self.ui.comps.cvs_menu.Y = pos[2]
    else
        self.ui.comps.cvs_menu.X = (self.ui.menu.Transform.rect.width - self.ui.comps.cvs_menu.Width) * 0.5
        self.ui.comps.cvs_menu.Y = (self.ui.menu.Transform.rect.height - self.ui.comps.cvs_menu.Height) * 0.5
    end

    self.ui.comps.cvs_lb.X = 0
    self.ui.comps.cvs_lb.Y = self.ui.comps.cvs_menu.Height - self.ui.comps.cvs_lb.Height
    self.ui.comps.cvs_rb.X = self.ui.comps.cvs_menu.Width - self.ui.comps.cvs_rb.Width
    self.ui.comps.cvs_rb.Y = self.ui.comps.cvs_menu.Height - self.ui.comps.cvs_rb.Height
end

function _M.OnEnter( self,params)
    params.cbs = params.cbs or {}
    for i = 1, #params.btntxts do
        self.btns[i] = self.ui.comps.btn_an:Clone()
        self.btns[i].Text = params.btntxts[i]
        self.ui.comps.cvs_menu:AddChild(self.btns[i])
        self.btns[i].TouchClick = function(sender)
            if params.cbs[i] then
                params.cbs[i](sender,params.extras)
            end
            self:Close()
        end
    end
    params.col = params.col or 1
    SetCompents(self,params.col,params.btntxts,params.pos)
    self.ui:EnableTouchFrameClose(true)
end

function _M.OnExit( self )
    for i=1,#self.btns do
        self.btns[i]:RemoveFromParent(true)
        self.btns[i]=nil
    end
    self.btns = {}
end

function _M.OnInit( self )
    self.originW = self.ui.comps.cvs_menu.Width
    self.originH = self.ui.comps.cvs_menu.Height
    self.btns = {}
    self.borederY = 7
    self.gapX = 15
    self.gapY = 7
    self.btnW = self.ui.comps.btn_an.Width
    self.btnH = self.ui.comps.btn_an.Height
    self.ui.comps.btn_an.Visible = false
    self.ui.menu.ShowType = UIShowType.Cover
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)
end


return _M