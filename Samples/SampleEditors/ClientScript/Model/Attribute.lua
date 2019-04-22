local _M = {}
_M.__index = _M

_M.PropChangeCallback = {}

function _M.Notify(status, userdata, opt, self)
  if userdata:ContainsKey(status, UserData.NotiFyStatus.PROP) then
    for key,val in pairs(_M.PropChangeCallback) do
      val(userdata:GetAttribute(UserData.NotiFyStatus.PROP))
    end
  end
end

function _M.RemovePropChangeListener(key)
  _M.PropChangeCallback[key] = nil
end

function _M.AddPropChangeListener(key, cb)
  _M.PropChangeCallback[key] = cb
end

function _M.InitNetWork()
  -- print('----------Attribute InitNetWork------------')

end

function _M.fin()
  -- print('----------Attribute fin------------')
  DataMgr.Instance.UserData:DetachLuaObserver("AttributeMain")
end

function _M.initial()
  -- print('----------Attribute inital------------')
  DataMgr.Instance.UserData:AttachLuaObserver("AttributeMain", _M)
end

return _M