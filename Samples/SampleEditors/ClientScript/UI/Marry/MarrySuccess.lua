local _M = {}
_M.__index = _M

local SocialModel = require 'Model/SocialModel'
local UIUtil = require 'UI/UIUtil'
local Util = require 'Logic/Util'

local function Play3DEffect(self, parentCvs, pos2d, scale, menuOrder, fileName)
	local transSet = TransformSet()
	transSet.Pos = Vector3(pos2d.x, pos2d.y, 0)
	transSet.Scale = Vector3(scale, scale, scale)
	transSet.Parent = parentCvs.Transform
	transSet.Layer = Constants.Layer.UI
	transSet.LayerOrder = menuOrder
	self.effect = RenderSystem.Instance:PlayEffect(fileName, transSet)
end

function _M.OnEnter( self, params )
	self.comps.lb_name1.Text = params.husband
	self.comps.lb_name2.Text = params.wife

	local face_husband = self.comps.ib_headpic1
	Util.GetRoleSnap(params.husbandId, function( roleSnap )
		local photoname = roleSnap.Options['Photo0']
		if not string.IsNullOrEmpty(photoname) then
			SocialModel.SetHeadIcon(params.husbandId, photoname, function(UnitImg)
				if not face_husband.IsDispose then
                    UIUtil.SetImage(face_husband,UnitImg,false,UILayoutStyle.IMAGE_STYLE_BACK_4)
				end
			end)
		else
			UIUtil.SetImage(face_husband, 'static/target/'..roleSnap.Pro..'_'..roleSnap.Gender..'.png',false,UILayoutStyle.IMAGE_STYLE_BACK_4)
		end
	end)

	local face_wife = self.comps.ib_headpic2
	Util.GetRoleSnap(params.wifeId, function( roleSnap )
		local photoname = roleSnap.Options['Photo0']
		if not string.IsNullOrEmpty(photoname) then
			SocialModel.SetHeadIcon(params.wifeId, photoname, function(UnitImg)
				if not face_wife.IsDispose then
                    UIUtil.SetImage(face_wife,UnitImg,false,UILayoutStyle.IMAGE_STYLE_BACK_4)
				end
			end)
		else
			UIUtil.SetImage(face_wife, 'static/target/'..roleSnap.Pro..'_'..roleSnap.Gender..'.png',false,UILayoutStyle.IMAGE_STYLE_BACK_4)
		end
	end)

	Play3DEffect(self, self.ui.menu, Vector2(0, 0), 1, self.ui.menu.MenuOrder, '/res/effect/ui/ef_ui_interface_advanced.assetbundles')
end

function _M.OnExit( self )

end

function _M.OnInit( self )

end

return _M