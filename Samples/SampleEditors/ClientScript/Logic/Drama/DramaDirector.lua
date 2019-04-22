---------------------------------
--! @file
--! @brief a Doxygen::Lua DramaDirector.lua
---------------------------------
print('-----------------DramaDirector.lua load-----------------')
---------------------------------internal FSM----------------------------------
local FSM = {}
FSM.__index = FSM
function FSM.Change(self,from,to,do_now)
	local next_state 
	if type(from) == 'table' then
		for _,v in ipairs(from) do
			if v == self.state then
				next_state = to
				break
			end
		end
	elseif self.state == from then
		next_state = to
	end
	if not next_state then
		return
	end
	if do_now and self.cb then
		self.cb(self.own,'leave',self.state)
		self.state = next_state
		self.cb(self.own,'enter',self.state)
	else
		table.insert(self.trans,next_state)
		self.state = next_state
	end	
end

--fsm_table
	--name from to
--cb: before leave enter 
function FSM.Create(fsm_table,own,cb)
	local ret = {trans = {},cb = cb,own = own}
	setmetatable(ret,FSM)
	for name,event in pairs(fsm_table) do
		ret[name] = function (fsm,now)
			fsm:Change(event.from,event.to,now)
		end
	end
	return ret
end

function FSM.IsState(self,state)
	return self.state == state
end

function FSM.GetState(self)
	return self.state
end

function FSM.Update(self)
	local pre_state = nil
	local count = 0
	while self.cb and #self.trans > 0 do
		local state = self.trans[1]
		if pre_state then
			self.cb(self.own,'leave',pre_state)
		end
		self.cb(self.own,'enter',state, pre_state)
		pre_state = state
		table.remove(self.trans,1)
		count = count + 1
		if count > 200 then
			error('while true')
		end
	end
end
---------------------------------internal FSM end------------------------------

---------------------------------blackboard------------------------------------
local BlackBoard = {}
BlackBoard.__index = BlackBoard

function BlackBoard.Create()
	return setmetatable({NEXT_ID = 1,data = {}},BlackBoard)
end

-- id有值时，按传入id作为key，id不存在时，使用内部id
function BlackBoard.Add(self,id,obj)
	if not obj then
		obj = id
		id = self.NEXT_ID
		self.NEXT_ID = self.NEXT_ID + 1
	else
		if type(id) == 'number' and not self[id] and self.NEXT_ID < id then
			self.NEXT_ID = id + 1
		end		
	end
	if type(obj) ~= 'table' then
		error('type error')
	end
	self.data[id] = obj
	return id
end

function BlackBoard.Get(self,id)
	return self.data[id]
end

function BlackBoard.Remove(self,id)
	self.data[id] = nil
end

function BlackBoard.Find(self,find_iter)
	local function MatchInTable(a,b)
		for k,v in pairs(b or {}) do
			if type(v) == 'table' then
				if not MatchInTable(a[k],v) then
					return false
				end
			elseif a[k] ~= v then
				return false
			end
		end	
		return true
	end
	local str_type = type(find_iter)
	local ret = {}
	if str_type == 'table' then
		for k,v in pairs(self.data) do
			if MatchInTable(v,find_iter) then
				table.insert(ret,v)
			end
		end
	elseif str_type == 'function' then
		for k,v in pairs(self.data) do
			if find_iter(v) then
				table.insert(ret,v)
			end
		end
	end
end

function BlackBoard.AddOnAddedCB(self,fn)
	self.onAdded = self.onAdded or {}
	table.insert(self.onAdded,fn)
end

function BlackBoard.RemoveOnAddedCB(self,fn)
	for i,f in ipairs(self.onAdded or {}) do
		if f == fn then
			table.remove(self.onAdded,i)
			break
		end
	end
end

function BlackBoard.AddOnRemovedCB(self,fn)
	self.onRemoved = self.onRemoved or {}
	table.insert(self.onRemoved,fn)	
end

function BlackBoard.RemoveOnAddedCB(self,fn)
	for i,f in ipairs(self.onRemoved or {}) do
		if f == fn then
			table.remove(self.onRemoved,i)
			break
		end
	end
end

function BlackBoard.GetAll(self)
	return self.data 
end

---------------------------------blackboard end--------------------------------

---------------------------------drama fsm defined ----------------------------
local STATE = 
{
	INIT = 'init',
	RUNNING = 'running',
	SUSPENDED = 'suspended',
	CLOSE = 'close',
	INVALID = 'invalid',
}

local FSM_DRAMA_TABLE = 
{
	Init = {to = STATE.INIT},
	Run = {from = {STATE.INIT,STATE.SUSPENDED}, to = STATE.RUNNING},
	Suspend = {from = STATE.RUNNING, to = STATE.SUSPENDED},
	Close = {from = {STATE.SUSPENDED,STATE.RUNNING}, to = STATE.CLOSE}, 
	Invalid = {from = STATE.CLOSE, to = STATE.INVALID}
}
---------------------------------drama fsm defined end ------------------------

---------------------------------timer ----------------------------------------
local SimpleTimer = {}
SimpleTimer.__index = Timer
-- fn 返回true表示timer需要停止
function SimpleTimer.Create(fn, t, once)
	return setmetatable({fn=fn, t=t or 0, once=once, ct=0},SimpleTimer)
end

-- Update返回true表示timer需要停止
function SimpleTimer.Update(self,delta)
	if self.stoped then return true end
	if self.paused then return false end
	self.ct = self.ct + delta
	if self.ct >= self.t then
		self.ct = 0
		local pass_time = self.t
		if pass_time < delta then
			pass_time = delta
		end
		local ret = self.func(pass_time,k)
		self.stoped = self.once or ret
	else
		return false
	end	
end

function SimpleTimer.Stop(self)
	self.stoped = true
end

function SimpleTimer.Pause(self)
	self.paused = true
end
---------------------------------timer end ------------------------------------

---------------------------------message --------------------------------------
local DramaMessage = {}
DramaMessage.__index = DramaMessage
function DramaMessage.Create(msgname,fn)
	return setmetatable({name = msgname, fn = function() 
		local ret = fn(msgname)
		if ret == nil then
			return true
		else
			return ret
		end	
	end},DramaMessage)
end

function DramaMessage.Stop(self)
	self.completed = true
end

function DramaMessage.Update()
	return self.completed
end

function DramaMessage.TryRecive(self,msgname,...)
	if msgname == self.name and fn(msgname,...) then
		self.completed = true
	end
end

---------------------------------message end ----------------------------------

---------------------------------drama node -----------------------------------
local DramaNode = {}
DramaNode.__index = DramaNode
local function Resume(self,...)
	local status = coroutine.status(self.co)
	if status ~= 'suspended' then
		return status
	end
	self.mgr:PushRunning(self)
	local ret = {coroutine.resume(self.co,...)}
	-- print('Resume', PrintTable(ret))
	if not ret[1] then	
		if not self.killed then
			-- error(debug.traceback(self.co)..ret[2])
		end
		self:Stop()
	else
		self:SetOutput(unpack(ret,2))
	end
	self.mgr:PopRunning()
	return coroutine.status(self.co)
end

local function fsm_callback(self,actname,state,prestate)
	--print(self,' fsm_callback name:',actname,' state:',state,' pre:',prestate)
	if state == STATE.INVALID and actname == 'enter' then
		self.mgr:OnEventInvaild(self)
		-- TODO 清理逻辑
		if not self.parent then			
			-- root event, clear
			self.mgr:PushRunning(self)
			for k,v in pairs(self.mgr.api) do
				local clear_func
				if k == 'Clear' and type(v) == 'function' then
					clear_func = v
				elseif type(v) == 'table' then
					clear_func = v.Clear
				end
				if clear_func then
					clear_func()
				end
			end
			self.mgr:PopRunning()
		end
	elseif state == STATE.CLOSE and actname == 'enter' then
		-- 停止所有timer及子节点
		local all = self.data_center:GetAll()
		for id, v in pairs(all) do
			if v.Stop then
				v:Stop()
			end
		end
		self.fsm:Invalid(false)
	elseif state == STATE.RUNNING and actname == 'enter' then
		self:ClearWait()
		local params = self.killed and {exit = true} or self.params
		local status = Resume(self, self, unpack(params))
		-- print('resume----',status,self.name)
		if status == 'dead' then
			self.fsm:Close()
		elseif self.killed then
			self.fsm:Run(false)
		else
			self.fsm:Suspend(false)
		end
	end
end

-- create_info {fn = xxx, repeat_data = {count = xxx, sec = xxx}}
-- wait_selector等待选择列表
-- wait_sequence等待序列列表
-- wait_all 等待所有
function DramaNode.Create(create_info,...)
	local self = 
	{
		data_center = BlackBoard.Create(),
		params = {...},
	}
	local typeStr = type(create_info)
	local fn
	if typeStr == 'function' then
		fn = create_info
		self.name = 'undefined'
	elseif typeStr == 'table' and create_info.fn then
		fn = create_info.fn
		self.repeat_data = create_info.repeat_data
		self.name = create_info.name or 'undefined'
	else
		return
	end

	local function reset()
		self.killed = nil
		self:ClearWait()
	end
	
	local function run_func(...)
		::main::
		local ret = {fn(...)}
		local repeat_data = self.repeat_data
		if repeat_data then
			Reset()
			local sec = repeat_data.sec
			if repeat_data.count > 1 then
				repeat_data.count = repeat_data.count - 1
			else
				self.repeat_data = nil
			end
			if sec then
				self.sleep_time = sec
				self:_Yield()
				goto main
			end
		end
		return unpack(ret)
	end
	self.co = coroutine.create(run_func)
	self.fsm = FSM.Create(FSM_DRAMA_TABLE,self,fsm_callback)
	self.fsm:Init(true)
	return setmetatable(self,DramaNode)
end	

function DramaNode.SetManager(self,mgr)
	self.mgr = mgr
end

function DramaNode.SetOutput(self,...)
	self.output = {...}
end

function DramaNode.GetOutput(self)
	return unpack(self.output or {})
end

function DramaNode.ClearWait(self)
	self.sleep_time = nil
	self.wait_selector = nil
	self.wait_all = nil
	self.wait_sequence = nil
end

function DramaNode.AddNode(self,e)
	if self.killed or self.fsm:IsState(STATE.INVALID) then
		return nil
	else
		e.parent = self
		e:SetManager(self.mgr)
		e.fsm:Run(false)
		-- parent等待子节点running成功
		local id = self.data_center:Add(e)
		e.id = id
		return id
	end
end

function DramaNode.GetRootEvent(self)
	if not self.parent then
		return self
	end
	if not self.root then
		self.root = self.parent:GetRootEvent()
	end
	return self.root
end

function DramaNode._Yield(self,...)
	self.fsm:Suspend(false)
	local ret = coroutine.yield(...)
	if ret.exit then
		error('force exit')
	end
end

function DramaNode.Await(self,second)
	local s = self.fsm:GetState()
	if not (s == STATE.RUNNING or s == STATE.SUSPENDED) then return end
	self.sleep_time = second or -1
	self:_Yield()
end

-- wait_params{timeout=xxx,sequence=true/false}
function DramaNode.Wait(self,id,wait_params)
	wait_params = wait_params or {}
	local function SetWait(wait_id)
		local e = self:GetAnything(wait_id)
		if not e then 
			return nil 
		end
		if not wait_params or not wait_params.sequence then
			self.wait_selector = self.wait_selector or {}
			self.wait_selector[wait_id] = wait_params
		else
			self.wait_sequence = self.wait_sequence or {}
			self.wait_sequence[wait_id] = wait_params
		end
		return e
	end
	
	local need_check_node
	if not id then 
		self.wait_all = wait_params 
	elseif type(id) == 'table' then
		for _,v in pairs(id) do
			SetWait(v)
		end
	else
		need_check_node = SetWait(id)
	end
	self:_Yield()
	print('DramaNode.Wait',self,self.name)
	if need_check_node  then
		return need_check_node:GetOutput()
	end
end

function DramaNode.Stop(self,id)
	local function StopWithID(v)
		local e = self.data_center:Get(v)
		if e then 
			e:Stop()
		end	
	end
	if not id then
		if self.fsm:IsState(STATE.INVALID) then
			return
		end
		self.killed = true
		self.fsm:Run(false)
	elseif type(id) == 'table' then
		for _,v in pairs(id) do
			StopWithID(v)
		end
	else
		StopWithID(v)
	end
end

function DramaNode.AddTimer(self, update_func, t, once)
	local timer = SimpleTimer.Create(update_func,t,once)
	return self.data_center:Add(timer)
end

function DramaNode.GetAnything(self, id, meta)
	local e = self.data_center:Get(id)
	if not meta or getmetatable(e) == meta then
		return e
	end	
end

function DramaNode.FindAnything(self,find_iter)
	return self.data_center:Find(find_iter)
end

function DramaNode.AddAnything(self,key,data)
	return self.data_center:Add(key,data)
end

function DramaNode.RemoveTimer(self, timeId)
	local e = self:GetAnything(timeId,SimpleTimer)
	if e then
		e:Stop()
	end
end

function DramaNode.UnsubscribeMessage(self,msgname,fn)
	local e = self:GetAnything(timeId,DramaMessage)
	if e then
		e:Stop()
	end	
end

function DramaNode.SubscribMessage(self, msgname, fn)
	local msg = DramaMessage.Create(msgname,fn)
	return self.data_center:Add(msg)
end

function DramaNode.OnReciveMessage(self, msgname, ...)
	local all = self.data_center:Find(function(v) return getmetatable(v) == DramaMessage end)
	for _,v in ipairs(all) do
		v:TryRecive(msgname,...)
	end
end

function DramaNode.Update(self,delta)
	if self.fsm:IsState(STATE.INIT) or self.fsm:IsState(STATE.INVALID) then return end
	self.fsm:Update()
	self.mgr:PushRunning(self)
	local resume_waitall = true
	local check_wait = self.wait_all or self.wait_selector or self.wait_sequence 
	local all = self.data_center:GetAll()
	local check_empty = true
	for id,v in pairs(all) do
		local meta = getmetatable(v)
		if meta == DramaNode or meta == SimpleTimer or meta == DramaMessage then
			check_empty = false
			if v:Update(delta) then
				print('remove---',id,v.name,self.fsm.state,check_wait)
				self.data_center:Remove(id)
				if self.wait_selector and self.wait_selector[id] then
					self.wait_selector[id] = nil
					-- 结束其他的
					for sub_id,vv in pairs(self.wait_selector) do
						local sub_e = self.data_center:Get(sub_id)
						sub_e:Stop()
					end
					self.wait_selector = nil
				end
				if self.wait_sequence then
					self.wait_sequence[id] = nil
				end
			else
				resume_waitall = false
			end
		end
	end
	-- 计算timeout
	local function calc_is_timeout(params)
		if not params or not params.timeout then return false end
		if params and params.timeout > 0 then
			params.timeout = params.timeout - delta
		end
		return params.timeout <= 0 
	end
	
	if calc_is_timeout(self.wait_all) then
		self.wait_all = nil
	end

	if resume_waitall then
		self.wait_all = nil
	end
	for id,p in pairs(self.wait_selector or {}) do
		if calc_is_timeout(p) then
			self.wait_selector[id] = nil
		end
	end
	for id,p in pairs(self.wait_selector or {}) do
		if calc_is_timeout(p) then
			self.wait_selector[id] = nil
		end
	end
	if self.wait_sequence and not next(self.wait_sequence) then
		self.wait_sequence = nil
	end
	
	local resume_conditon = {}
	if check_wait then
		-- print(not self.wait_all,not self.wait_selector,not self.wait_sequence)
		table.insert(resume_conditon,not self.wait_all and not self.wait_selector and not self.wait_sequence)
	end
	
	if check_empty then
		table.insert(resume_conditon,true)
	end
	-- print('checkresum_contidon')
	-- print_r(resume_conditon)
	if self.sleep_time then
		local con = false
		if self.sleep_time >= 0 then
			self.sleep_time = self.sleep_time - delta
			if self.sleep_time <= 0 then
				con = true			
			end
		end
		table.insert(resume_conditon,con)
	end
	
	if #resume_conditon > 0 then
		local ok = true
		for _,v in ipairs(resume_conditon) do
			if not v then
				ok = false
				break
			end
		end
		if ok then
			self.fsm:Run(false)
		end	
	end
	self.mgr:PopRunning()
	return self.fsm:IsState(STATE.INVALID)
end

function DramaNode.AddInvalidCB(self,cb)
	self.invaild_cbs = self.invaild_cbs or {}
	table.insert(self.invaild_cbs,cb)
end
---------------------------------drama node end -------------------------------

---------------------------------basic api ------------------------------------
local _basicApi = {}
function _basicApi.Sleep(parent,second)
	return parent:Await(second)
end
function _basicApi.Wait(parent,id,params)
	return parent:Wait(id,params)
end
function _basicApi._asyncTimeout(self, sec)
	self:AddTimer(function() 
		
	end,sec, true)
	self:Await()
end

function _basicApi.Stop(parent,id)
	parent:Stop(id)
end
---------------------------------basic api end --------------------------------

---------------------------------manager --------------------------------------
local Director = {}
Director.__index = Director
function Director.Create(load_fn)
	local ret = 
	{
		running_stack = {},
		root_ids = {},
		data_center = BlackBoard.Create(),
		load_fn = load_fn,
		api = {},
	}
	setmetatable(ret,Director)
	ret:RegisterApi(_basicApi)
	return ret
end
local async_prefix = '_async'
function Director.RegisterApi(self, api_table, api_name)
	local cur_api
	if not api_name or api_name == '' then
		cur_api = self.api
	else
		cur_api = self.api[api_name] or {}
		self.api[api_name] = cur_api
	end
	
	local function CreateSyncApi(fn,name)
		return function (...)
			local running_event = self:PeekRunning()
			running_event:Await(0)
			return fn(running_event, ...)
		end
	end
	
	local function CreateAsyncApi(fn,name)
		return function (...)
			local running_event = self:PeekRunning()
			local e = DramaNode.Create({name=name,fn=fn},...)
			return running_event:AddNode(e)
		end
	end
	
	for k,v in pairs(api_table) do
		if type(v) == 'function' then
			if string.find(k,async_prefix) then				
				local fun_name = string.sub(k,string.len(async_prefix) + 1)
				cur_api[fun_name] = CreateAsyncApi(v,fun_name)
			else
				cur_api[k] = CreateSyncApi(v,k)
			end
		elseif k ~= '__index' then
			cur_api[k] = v
		end
	end	
end

function Director.PushRunning(self,e)
	table.insert(self.running_stack, e)
end

function Director.PeekRunning(self)
	return self.running_stack[#self.running_stack]
end

function Director.PopRunning(self)
	table.remove(self.running_stack)
end

function Director.AddToRoot(self,node)
	node:SetManager(self)
	node.fsm:Run(false)
	local id = self.data_center:Add(node)
	node.id = id
	table.insert(self.root_ids,id)
	return id
end

function Director.SendMessage(self,msgname,...)
	for i=#self.root_ids,1,-1 do
		local root = self.data_center:Get(self.root_ids[i])
		if root then
			root:OnReciveMessage(msgname,...)
		end
	end	
end

function Director.GetScriptID(self,script_name)
	local ret = {}
	-- print('------get id')
	-- print_r(self.root_ids)
	-- print_r(self.data_center)
	-- for k,v in pairs(self.data_center.data) do
	-- 	print('kkkkk',k,v.name)
	-- end
	for i=#self.root_ids,1,-1 do
		local id = self.root_ids[i]
		local root = self.data_center:Get(id)
		-- print('root name ',root.name)
		if root.name == script_name then
			table.insert(ret, id)
		end
	end
	return unpack(ret)
end

function Director.Update(self,delta)
	local cur_clock = os.clock()
	self._lastclock = self._lastclock or cur_clock
	delta = delta or (cur_clock - self._lastclock)
	-- print('update---',delta,self._lastclock,cur_clock)
	self._lastclock = cur_clock
	for i=#self.root_ids,1,-1 do
		local id = self.root_ids[i]
		local root = self.data_center:Get(id)
		if root.fsm:IsState(STATE.INVALID) then
			self.data_center:Remove(id)
			table.remove(self.root_ids,i)
		else
			root:Update(delta)
		end
	end
end

function Director.StartScript(self,script_name,...)
 	if not self.load_fn then
 		error('load function nil')
 	end	
	local ok,ret = pcall(self.load_fn,script_name)
	if not ok then
		--UnityEngine.Debug.LogError('[Drama] load failed '..ret)
		if not string.find(ret,'design/test') then
			print('[Drama] load failed',ret)
		end
		ret = nil
	end
	if not ret then
		return nil
	else
		ret.api = self.api
	end
	local function node_func(...)
		ret.main(...)
	end
	local script = DramaNode.Create({fn=node_func,name = script_name},...)	
	print('script name',script.name,script_name)
	return self:AddToRoot(script)
end

function Director.StopScript(self,id)
	local e = self.data_center:Get(id)
	if e and not e.parent then
		e:Stop()
	end
end

function Director.StopAllScript(self)
	for i=#self.root_ids,1,-1 do
		local root = self.data_center:Get(self.root_ids[i])
		if root then
			root:Stop()
		end
	end
	self:Update(0)
end

function Director.OnEventInvaild(self,script)
	if self.invaild_cbs then
		for _,v in ipairs(self.invaild_cbs) do
			v(script)
		end
	end	
end

function Director.AddInvalidCB(self,cb)
	self.invaild_cbs = self.invaild_cbs or {}
	table.insert(self.invaild_cbs,cb)
end

return Director
---------------------------------manager end-----------------------------------















































