local _M = {}
_M.__index = _M 
local UIUtil = require 'UI/UIUtil.lua'
local Util = require 'Logic/Util'

local titleScale = 1

local function showCheckTitle(self,data)
	if self.titleModel then
		UI3DModelAdapter.ReleaseModel(self.titleModel.Key)
		self.titleModel = nil
	end
	self.comps.lb_titletext.Visible = false

	if data == nil then
		return
	end

	if data.title_type == 1 then
		self.comps.lb_titletext.Layout = null
		self.comps.lb_titletext.Visible = true
		-- self.comps.lb_titletext.Layout = null
		self.comps.lb_titletext.Text = Util.GetText(data.word_res)
	elseif data.title_type == 2 then
		local fileName = data.effect_res
		self.comps.lb_titletext.Layout = null
		local parentCvs = self.ui.comps.cvs_title
		local pos2d = self.ui.comps.cvs_anchor.Position2D
		local info = UI3DModelAdapter.AddSingleModel(parentCvs, pos2d, titleScale, self.ui.menu.MenuOrder,fileName)
		info.Callback = function (info2)
			local trans2 = info2.RootTrans
			trans2:Rotate(Vector3.up,180)
		end
		self.titleModel = info
	elseif data.title_type == 3 then
		self.comps.lb_titletext.Text = Util.GetText(data.word_res,self.nameExt or "")
		self.comps.lb_titletext.Visible = true
		if not string.IsNullOrEmpty(data.pic_res) then
			self.comps.lb_titletext.Layout = HZUISystem.CreateLayout(data.pic_res, UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER, 8);
		end
		-- if not string.IsNullOrEmpty(data.effect_res) then
		-- 	local fileName = data.effect_res
		-- 	local  titlenode =  self.comps.lb_titletext
		-- 	local title = UI3DModelAdapter.AddSingleModel(titlenode, Vector2(0,0), 1, self.ui.menu.MenuOrder,fileName)
  --           title.Callback = function (info)
  --               local trans2 = info.RootTrans
  --               trans2:Rotate(Vector3.up,180)
  --               trans2.localPosition = Vector3(titlenode.Size2D[1]/2,-titlenode.Size2D[2], -500)
  --           end
  --           rself.titleModel = info
		-- end 
	end

end


function _M.OnEnter( self, titleId,nameExt)
	self.titleId = titleId
	self.nameExt = nameExt or ""
 	local titleData = GlobalHooks.DB.FindFirst('title',{title_id = self.titleId})
	showCheckTitle(self,titleData)
end

function _M.OnExit( self )
 	if self.titleModel then
		UI3DModelAdapter.ReleaseModel(self.titleModel.Key)
		self.titleModel = nil
	end
end

function _M.OnDestory( self )
 
end

function _M.OnInit( self )

	self.ui.menu.ShowType = UIShowType.Cover
    self.ui.menu:SetCompAnime(self.ui.menu, UIAnimeType.NoAnime)

	self.comps.btn_an.TouchClick = function ( ... )
		GlobalHooks.UI.OpenUI('TitleFrame',0,self.titleId)
		self.ui:Close()
	end
	self.comps.cvs_bg.TouchClick = function ( ... )
 		self.ui:Close()
	end

	self.comps.cvs_getnew.TouchClick = function ( ... )
 		GlobalHooks.UI.OpenUI('TitleFrame',0,self.titleId)
		self.ui:Close()
	end

end

return _M