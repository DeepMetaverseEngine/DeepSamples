local _M = {}
_M.__index = _M
print('-------------load testui---------------------')
local UIUtil = require 'UI/UIUtil.lua'
local testdetail
-- self.ui BaseUI实例例
-- self.ui.comps 节点访问器
-- self.ui.menu csharp MenuBase
function _M.OnEnter( self, ...)
	--print('OnEnter',self,...)
	---- 测试查找模版数据
	---- 整表
	--local all = GlobalHooks.DB.Find('Item',{})
	--print('items all count:',#all)
	--
    --
	---- 按键
	--local test1 = GlobalHooks.DB.Find('Item',1000)
	--print(test1  and test1.name)
    --
	---- 按函数
	--local testfun = GlobalHooks.DB.Find('Item',function(item) return item.name:find('物攻宝石') ~= nil end)
	--print('items function count:',#testfun)
    --
    --
	--local e = GlobalHooks.DB.Find('Equip',114430)
	--print('Equip name :',e and e.name or '')
	--
	--local e1 = unpack(GlobalHooks.DB.Find('Equip',{id=11440}))
	--print('Equip 111name :',e1 and e1.name or '')
	---- print_r({id=11440})
    --
    --
	--self.testwatch = {aaa = 3}
	--
	--self.comps.bt_out.TouchClick = function()
	--	self.testwatch.aaa = math.random(0,1000000)
	--end
	--Watch(self.testwatch,function()
	--	self.comps.bt_goon.Text = self.testwatch.aaa
	--end)
    --
	--for k,v in pairs(self) do
	--	print('pairs after',k,v)
	--end
	-- GlobalHooks.Drama.Start('test/hello_world.lua')
	--stram
	-- local output = (require 'Protocol/OutputStream.lua').Create()
	-- MenuMgr.Instance:RemoveCacheUIByTag('ItemDetail',200)
	-- package.loaded['/ui_edit/lua/UI/Bag/ItemDetail.lua'] = nil
	-- testdetail = GlobalHooks.UI.OpenUI('ItemDetail')
	-------------------------------Start-------------------------
	-- Protocol.RequestHandler.TestProtocolRequest(msg,function(rpdata)
	-- 	print_r(rpdata)
	-- end)
	------------------------------END----------------------------
	local BusinessModel = require 'Model/BusinessModel'
	BusinessModel.FirstRequire()
	
end

function _M.OnExit( self )
	
	-- testdetail:Close()
	
	print('OnExit')
end

function _M.OnDestory( self )
	
	print('OnDestory')
end

function _M.OnInit( self )
	print('OnInit')
end

return _M