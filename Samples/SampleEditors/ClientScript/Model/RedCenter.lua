local _M = {}
_M.__index = _M

--以uitag形式保存的红点数据
local RedInfo = {
	-- 以key value形式保存
	-- key是uitag,结构如下
	--
	-- uitag1|
	--       |comp1|
	--       |     |redkey1|
	--       |     |       |{subKey1 = count}
	--       |     |       |{subKey2 = count}
	--       |     |redkey2|
	--       |     |       |{subKey1 = count}
	--       |     |       |{subKey2 = count}
	--       |
	--       |comp2|
	--       |     |redkey1|
	--       |     |       |{subKey1 = count}
	--       |     |       |{subKey2 = count}
	--       |     |redkey2|
	--       |     |       |{subKey1 = count}
	--       |     |       |{subKey2 = count}
	--       |
	-- uitag2|
	--       |
}

--以redKey形式保存的红点数据
local RedKeys = {
	--key:redKey，结构如下
	--
	-- redKey1|
	--        |{subKey1 = count}
	--        |{subKey2 = count}
	--        |
	-- redKey2|
	--        |
}

--以控件名形式保存的红点数据
local NodeData = {
	--key:fullpath(控件路径)，结构如下
	--
	-- fullpath1|
	--          |showinfo|
	--          |        |{sourceKey = showNum}
	--          |        |{sourceKey = showNum}
	--          |
	--          |{relation = path}
	--          |
	-- fullpath2|
	--          |
}

--根据路径解析出控件
--路径示例：FunctionHud/cvs_guild/lb_tip
local function ParseNodeByParse( path )
	local list = string.split(path, '/')
	local rootNode = HudManager.Instance:GetHudUI(list[1])
	if rootNode then
		local index = 2
		local node = rootNode
		while index <= #list do
			node = node:FindChildByEditName(list[index], false)
			if node == nil then
				return nil
			end
			index = index + 1
		end
		return node
	end
	return nil
end

local function GetNodePath( node )
	local HudRoot = nil
	local path = node.EditName
	local parent = node.Parent
	while parent and parent.Name ~= 'HudRoot' and parent.Name ~= 'UILayer' and parent.Name ~= 'HudMenuRoot' do
		local name = parent.Name
		local index = string.find(name, ' - ', 1)
		if index then
			name = string.sub(name, 1, index - 1)
		end
		path = name..'/'..path
		parent = parent.Parent
	end
	return path
end

--设置单个控件上的红点显示
--label    红点label控件
--showNum  红点显示开关，0 消除红点，1 显示红点，大于1表示既显示红点又显示对应数字
--redKey   红点对应的功能标识，可自定义，也可是表里的 red_key 等
--subKey   redKey的子key，表示同一个redKey下可叠加多个红点。没有的可不填
local function ShowRedPoint( label, showNum, redKey, subKey )
	local fullpath = GetNodePath(label)
	local data = NodeData[fullpath] or {}
	data.showinfo = data.showinfo or {}
	local key = (redKey or 'commonred')..'.'..(subKey or 'default')
	data.showinfo[key] = showNum
	NodeData[fullpath] = data
	local count = 0
	for _, v in pairs(data.showinfo) do
		if v > 0 then
			count = count + v
		end
	end
	label.Visible = count > 0
	label.Text = (label.UserData == 'ShowNum' and count > 1) and tostring(count) or ''
	-- print('------ShowRedPoint----', fullpath, showNum, count, key)

	--处理关联显示控件
	if not string.IsNullOrEmpty(data.relation) then
		-- print('red center relation:', label.EditName, data.relation)
		local relationNode = ParseNodeByParse(data.relation)
		if relationNode then
			ShowRedPoint(relationNode, showNum, fullpath..'.'..key)
		else
			print('RedCenter Relation Node not exist: ', fullpath, data.relation)
		end
	end
end

local function CheckUIRedPoint( uitag, ui )
	local comps = RedInfo[uitag]
 	if comps then
 		for compname, reds in pairs(comps) do
 			local comp = ui.comps[compname]
 			if comp then
	 			for redkey, subkeys in pairs(reds) do
	 				for subkey, count in pairs(subkeys) do
						ShowRedPoint(comp, count, redkey, subkey)
	 				end
	 			end
 			end
 		end
 	end
end

local function CheckShowUITips( redKey, subKey )
	local db = GlobalHooks.DB.Find('advance_ui', redKey)
	if not string.IsNullOrEmpty(db.key) then
		local showKeys = {}
		for k, subkeys in pairs(RedKeys) do
			db = GlobalHooks.DB.Find('advance_ui', k)
			if not db then
				print('db is nil',k)
			end
			if db and not string.IsNullOrEmpty(db.key) and GlobalHooks.IsFuncOpen(db.UI_flag) then
 				for subkey, count in pairs(subkeys) do
					if count > 0 then
						showKeys[db.key] = db.key
						break
					end
 				end
			end
		end
		-- print_r('-----------------CheckShowUITips', redKey, subKey, showKeys)
		local ui = GlobalHooks.UI.FindUI('AbilityUpMain')
		if ui then
			ui.Reset(ui, showKeys)
		else
			GlobalHooks.UI.OpenHud('AbilityUpMain', 0, showKeys)
		end
	end
end

--获取指定redKey,subKey的红点数量信息
--redKey xls表里的red_key字段
--subKey redKey的子key，表示同一个redKey下可叠加多个红点。没有的可不填
--return 对应的红点数量 0表示没有
local function GetRedData( redKey, subKey )
	if RedKeys[redKey] == nil then
		error('red_key not found!!! '..redKey)
		return 0
	end
	local subKey = subKey or 'default'
	return RedKeys[redKey][subKey]
end

--红点中心通用接口，跟服务端协议参数相对应
--redKey xls表里的red_key字段
--count  红点显示数量，0 消除红点，1 显示红点，大于1表示既显示红点又显示对应数字
--subKey redKey的子key，表示同一个redKey下可叠加多个红点。没有的可不填
local function SetRedTips( redKey, count, subKey, internal )
	if RedKeys[redKey] == nil then
		error('red_key not found!!! '..redKey)
		return
	end

	local subKey = subKey or 'default'
	if RedKeys[redKey][subKey] == count and not internal then
		return
	end

	RedKeys[redKey][subKey] = count

	local db = GlobalHooks.DB.Find('advance_ui', redKey)
	if db then
		if GlobalHooks.IsFuncOpen(db.UI_flag) then
			--hud
			local rootNode
			if db.hud_type == 1 then
				local ui = HudManager.Instance:GetHudUI("MainHud")
				rootNode = ui:FindChildByEditName('cvs_menuhud', true)
			elseif db.hud_type == 2 then
				rootNode = HudManager.Instance:GetHudUI("FunctionHud")
			elseif db.hud_type == 3 then
				local ui = HudManager.Instance:GetHudUI("MainHud")
				rootNode = ui:FindChildByEditName('cvs_chat', true)
			else
				rootNode = HudManager.Instance:GetHudUI("MainHud")
			end
			if rootNode ~= nil then
				local cvs = rootNode:FindChildByEditName(db.hud_comps, true)
				local lb_red = cvs:FindChildByEditName('lb_tip', true)
				if lb_red then
					ShowRedPoint(lb_red, count, redKey, subKey)
				end
			end

			--ui
			if #db.ui.tag > 0 then
				for i = 1, #db.ui.tag do
					local uitag = db.ui.tag[i]
					if not string.IsNullOrEmpty(uitag) then
						local compname = db.ui.red[i]
						RedInfo[uitag][compname][redKey][subKey] = count

						local ui = GlobalHooks.UI.FindUI(uitag)
						if ui and type(ui) == 'table' then
							CheckUIRedPoint(uitag, ui)
						end
					end
				end
			end

			local firekey = 'Event.RedTips.'..redKey
			EventManager.Fire(firekey, {count = count,redKey = redKey, subKey = subKey})
			CheckShowUITips(redKey, subKey)
		else
			-- print('------RedCenter-------- function not open:', db.UI_flag, redKey, subKey, count)
		end
	end
end

--设置红点控件关系数据，即让关系红点控件跟着本红点控件一起显示隐藏
--node     红点控件
--relation 关系控件的完整路径,如：MainHud/cvs_topright/cvs_shanzi/lb_shanzired
local function SetNodeRelation( node, relation )
	local fullpath = GetNodePath(node)
	local data = NodeData[fullpath] or {}
	data.relation = relation
	NodeData[fullpath] = data
end

local function OnRedTipsNotify(notify)
	print("---------OnRedTipsNotify------------", notify.s2c_key, notify.s2c_count)
	SetRedTips(notify.s2c_key, notify.s2c_count)
end

local function OnFunctionOpen( eventname, params )
	if params.val == 1 then
		local isShow = false
		for k, subkeys in pairs(RedKeys) do
			db = GlobalHooks.DB.Find('advance_ui', k)
			if not db then
				print('db is nil',k)
			end
			if db and db.UI_flag == params.name then
				for subkey, count in pairs(subkeys) do
					if count > 0 then
						SetRedTips(k, count, subkey, true)
						break
					end
				end
			end
		end
	end
end

local function InitRedData()
	print("------------RedCenter InitRedData")
	local info = {}
	local db = GlobalHooks.DB.Find('advance_ui', {})
	for _, v in ipairs(db) do
		if #v.ui.tag > 0 then
			for i = 1, #v.ui.tag do
				local uitag = v.ui.tag[i]
				if not string.IsNullOrEmpty(uitag) then
					local compname = v.ui.red[i]
					local comps = info[uitag] or {}
					local red = comps[compname] or {}
					red[v.red_key] = { default = 0 }
					comps[compname] = red
					info[uitag] = comps
				end
			end
		end
		RedKeys[v.red_key] = {}
		RedKeys[v.red_key].default = 0
	end
	RedInfo = info
	NodeData = {}
end

--退出场景时调用，参数：是否断线重连触发的切场景
function _M.OnExitScene(reconnect)

end

--进入场景时调用
function _M.OnEnterScene()
	
end

function _M.fin()
	EventManager.Unsubscribe("Event.FunctionOpen.FuncOpen", OnFunctionOpen)
end

function _M.initial()
	print("RedCenter initial")
	EventManager.Subscribe("Event.FunctionOpen.FuncOpen", OnFunctionOpen)
end

function _M.InitNetWork(initNotify)
	-- print("------------RedCenter InitNetWork", initNotify)
	if initNotify then
		InitRedData()
		Protocol.PushHandler.ClientRedTipsNotify(OnRedTipsNotify)
	end
end

GlobalHooks.UI.ShowRedPoint = ShowRedPoint
GlobalHooks.UI.SetRedTips = SetRedTips
GlobalHooks.UI.CheckUIRedPoint = CheckUIRedPoint
GlobalHooks.UI.SetNodeRelation = SetNodeRelation
GlobalHooks.UI.GetRedData = GetRedData

return _M