---------------------------------
--! @file
--! @brief a Doxygen::Lua ApiUnit.lua
---------------------------------
local _M = {}


function _M.AddUnit(self,templateid,x,y,dir)
	-- print("AddUnit")
	-- print_r(self)
	local objId = TLBattleScene.Instance:AddStoryUnit(templateid,Vector2(x,y),dir,function()
		--self:Stop()
	end)
	--self:Await()
	return objId
end

function _M.RemoveUnit(self,objid)
	TLBattleScene.Instance:RemoveStoryObject(objid)
end

function _M.RemoveLastAddUnit(self)
	TLBattleScene.Instance:RemoveLastUnit()
end

function _M.PlayAnim(self,objid,name,delay,speed,wrapMode)
	TLBattleScene.Instance:PlayAnimation(objid,name,
	function()
	end,delay,speed,wrapMode)

end

function _M.GetPro()
	return DataMgr.Instance.UserData.Pro
end

function _M.GetSex()
	return DataMgr.Instance.UserData.Gender
end


--修改场景中npc动作

function _M.ChangeUnitPlayAnim(self,TemplateID,actonlist,isCross,cb,delay,speed,wrapMode)
	local pathtype = type(actonlist)
	local name = ""
	local _wrapMode = wrapMode or 0
	local _isCross = isCross or true
    if pathtype == 'string' then
    	name = actonlist
    	if name == "n_idle" then
    		name = ""
    	end
    elseif pathtype == 'table' then 
    	local _actionlist = actonlist[1]
    	name = _actionlist[1]
    	_wrapMode = _actionlist[2] or 1
    	table.remove(actonlist,1)
    	if name == "n_idle" and #actonlist == 0 then
    		name = ""
    	end
    end

	TLBattleScene.Instance:ChangeUnitAnimation(TemplateID,name,_isCross,function()
   		if #actonlist == 0 or pathtype == 'string' then
    		if cb then
    			cb()
    		end
    	else
			_M.ChangeUnitPlayAnim(self,TemplateID,actonlist,_isCross,cb,delay,speed,_wrapMode)
    	end
    end,delay or 0,speed or 1,_wrapMode)
end

function _M.AddBubbleTalk(self,templateID,context,talktype,keeptime)
	context = "<f color='ffffffff'>"..context..'</f>'
	TLBattleScene.Instance:AddBubbleTalk(templateID,context,talktype,keeptime)
end

function _M.AddCGBubbleTalk(self,objid,context,talktype,keeptime)
	context = "<f color='ffffffff'>"..context..'</f>'
	TLBattleScene.Instance:AddCGBubbleTalk(objid,context,talktype,keeptime)
end

function _M.ChangeDirection(self,templateID,dir,issmooth)
	TLBattleScene.Instance:UnitChangeDirection(templateID,dir,issmooth or false)
end

function _M.FaceToPlayer(self,templateID)
	TLBattleScene.Instance:FaceToPlayer(templateID)
end

function _M.UnitStopTurnDirect(self,templateID,isStop)
	TLBattleScene.Instance:UnitStopTurnDirect(templateID,isStop)
end

function _M.GetDirFromPlayer(self,templateID)
	return TLBattleScene.Instance:GetDirFromPlayer(templateID)
end
function _M.Clear()

end
-- 启动一个事件
-- 具体逻辑
-- api.Wait()
-- 结束时清理-- 未播放完毕的动作停止、特效清除
return _M