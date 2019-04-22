local _M = {}
_M.__index = _M

-- self.ui BaseUI实例例
-- self.ui.comps 节点访问器
-- self.ui.menu csharp MenuBase

local function ResetPos( self, param )
	if param ~= nil then
	    -- pos offset, anchor
	    self.pos = param.pos or {}
	    self.anchor = param.anchor or {}
	    
	    if self.pos.x ~= nil then
	        self.ui.comps.keypad.X = self.pos.x

	        -- anchor x
	        if self.anchor.x ~= nil then
	            self.ui.comps.keypad.X = self.ui.comps.keypad.X - self.ui.comps.keypad.Width * self.anchor.x
	        end
	    else
	        self.ui.comps.keypad.X = (self.ui.menu.Transform.rect.width - self.ui.comps.keypad.Width) * 0.5
	    end

	    if self.pos.y ~= nil then
	        self.ui.comps.keypad.Y = self.pos.y

	        -- anchor y
	        if self.anchor.y ~= nil then
	            self.ui.comps.keypad.Y = self.ui.comps.keypad.Y - self.ui.comps.keypad.Height * self.anchor.y
	        end
	    else
	        self.ui.comps.keypad.Y = (self.ui.menu.Transform.rect.height - self.ui.comps.keypad.Height) * 0.5
	    end
	else
	    self.ui.comps.keypad.X = (self.ui.menu.Transform.rect.width - self.ui.comps.keypad.Width) * 0.5
	    self.ui.comps.keypad.Y = (self.ui.menu.Transform.rect.height - self.ui.comps.keypad.Height) * 0.5
	end
end

function _M.OnEnter( self, ...)
	print('NumInput  OnEnter ', #self.ui.menu.ExtParam)
	local params = {...}
 	-- print_r('aaaaaaaaaaaaaa', params)

 	self.minValue =  params[1]
	self.maxValue =  params[2]

	self.showValue = self.minValue

	self.InputOverCB = params[4]
	self.CloseCB = params[5]

	--参数例子 {pos = {x = 100, y = 500}, anchor = {x = 0, y = 1}}
	ResetPos(self, params[3])

	
	self.firstEnter = true

 	for i=0,9,1 do
 		(self.ui.comps['btn_num' .. i]).IsPlaySound=false
     	(self.ui.comps['btn_num' .. i]).TouchClick = function ( sender )
     		-- print('button num:',sender.Text)

			if self.showValue >= self.maxValue then
     			return
     		end

     		if self.firstEnter then
				self.showValue = tonumber(sender.UserTag)
				self.firstEnter = false
     		else
     			self.showValue = self.showValue * 10 +  tonumber(sender.UserTag)
     		end
     		 
     		if self.showValue >= self.maxValue then
     			self.showValue = self.maxValue
     		end

     		-- print(self.showValue)

     		self.InputOverCB(self.showValue)		
     	end
	end

	self.ui.comps.btn_del.TouchClick = function ( sender )
		 
		if self.showValue <= self.minValue then
			return
		end
 
	 	self.showValue,_ = math.modf(self.showValue / 10) 
     			
		if self.showValue <= self.minValue then
			self.showValue = self.minValue
			self.firstEnter = true
		end

		-- print('del:',self.showValue)
		self.InputOverCB(self.showValue)
	end
end

function _M.OnExit( self )
	print('OnExit')
	if self.CloseCB then
		if self.showValue < self.minValue then
			self.showValue = self.minValue
		end
		if self.showValue > self.maxValue then
			self.showValue = self.maxValue
		end
		self.CloseCB(self.showValue)
		self.CloseCB = nil
	end
end

function _M.OnDestory( self )
	print('OnDestory')
end

function _M.OnInit( self )
	print('OnInit')

	self.ui.menu.ShowType = UIShowType.Cover
	self.ui.comps.btn_ok.TouchClick = function ( sender )
     	 self.ui:Close()
	end

	self.ui.menu.event_PointerUp = function( sender, e )
		self.ui:Close()
	end
end

return _M