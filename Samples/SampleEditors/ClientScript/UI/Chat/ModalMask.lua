local _M = {}

function _M.Create(parent, node, color)
	local self = {}
	setmetatable(self, {__index=_M})

	self:Init(parent, node, color)

	return self
end

function _M:Init(parent, node, color)
	local canvas = UECanvas()
	canvas.Visible = false
	canvas.Transform.anchorMin = Vector2.zero
	canvas.Transform.anchorMax = Vector2.one

	canvas.Name = "ModelMask"
	parent:AddChild(canvas)

	canvas.Layout = UILayout.CreateUILayoutColor(color or UnityEngine.Color(0,0,0,0),UnityEngine.Color(0,0,0,0))
	canvas.Enable = true
	canvas.IsInteractive = true
	canvas.EnableChildren = true

	canvas.event_PointerClick = function ()
		canvas.Visible = false
	end

	self.root = canvas
	self.node = node
end

function _M:Show()
	self.root.Visible = true

	if self.node.Parent ~= self.root then
		local newLocalPos = self.root:GlobalToLocal(self.node:LocalToGlobal(), true)
		local oldParent = self.node.Parent
		self.root:AddChild(self.node)
		self.node.Position2D = newLocalPos
		self.node.Visible = true
	end
end

return _M