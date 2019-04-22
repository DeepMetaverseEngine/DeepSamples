local _M = {}
_M.__index = _M

local MedicineModel = require 'Model/MedicineModel'
local ItemModel = require 'Model/ItemModel'
local UIUtil = require 'UI/UIUtil'

local function InitInfo( self)
   
end



function _M.OnEnter( self )

	local d = DataMgr.Instance.UserData.GameOptionsData
	--自动吃药
	self.ui.comps.tbt_autoheal.IsChecked = d.AutoUseItem
	--自动填充
	self.ui.comps.tbt_autoput.IsChecked = d.AutoRecharge
	--血池次数及上限
	self.ui.comps.gg_pool:SetGaugeMinMax(0, 100)
	--TODO
	self.ui.comps.gg_pool.Value = 60 --1~99
	--阀值
	self.ui.comps.lb_num = d.UseThreshold

	--当前回复量
	self.ui.comps.lb_huinum.Text = "998"
	--血池可使用次数/血池总次数
	self.ui.comps.lb_count.Text = "1/1234"
	--充能金额
	self.ui.comps.lb_costnum.Text = "555"
end


function _M.OnExit( self )
	print('Medicine OnExit')
end

function _M.OnDestory( self )
	print('Medicine OnDestory')
end

function _M.OnInit( self )
	print('Medicine OnInit')
	--设置类型，界面打开时其他界面不隐藏.
	self.ui.menu.ShowType = UIShowType.Cover
	--点击界面其他部分界面关闭.
	self.ui.menu.event_PointerUp = function( ... )
		self.ui:Close()
	end
	self.itemID  = 0

	self.ui.comps.btn_more.TouchClick = function ( sender )
		local v =  DataMgr.Instance.UserData.GameOptionsData.UseThreshold
		v = v + 1
		if v>100 then
			v = 100
		end
		self.ui.comps.lb_num.Text = v
	end

	self.ui.comps.btn_less.TouchClick = function ( sender )

		local v = DataMgr.Instance.UserData.GameOptionsData.UseThreshold
		v = v - 1
		if v<0 then
			v = 0
		end
		self.ui.comps.lb_num.Text = v
	end

	self.ui.comps.btn_ok.TouchClick = function ( sender )
		MedicineModel.SaveOptions(self.ui.comps.tbt_autoheal.IsChecked,
				self.ui.comps.tbt_autoput.IsChecked,
				DataMgr.Instance.UserData.GameOptionsData.UseThreshold,
				function(msg)
					self.ui:Close()
				end)
	end

	self.ui.comps.btn_one.TouchClick = function ( sender )
		MedicineModel.DoRechargeMedicinePool(function(msg)
			self.ui:Close()
		end)
	end
end
	

return _M
