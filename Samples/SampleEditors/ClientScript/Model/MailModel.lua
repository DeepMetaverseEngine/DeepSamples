local _M = {}
_M.__index = _M

local MailModel = {
    listData = nil,--列表
    unReadCount = 0,
}

function _M.CheckMailNum(self)
    local unReadCount = 0
    if MailModel.listData ~= nil then
        for i=1,#MailModel.listData do
            if MailModel.listData[i].mail_status==1 or MailModel.listData[i].attachment then 
                unReadCount = unReadCount + 1
            end
        end
        MailModel.unReadCount = unReadCount
    else
        unReadCount = MailModel.unReadCount
    end
    -- print('MailModel.CheckMailNum', unReadCount)
    -- EventManager.Fire("Event.Mail.UnRead", { num = unReadCount })
    GlobalHooks.UI.SetRedTips('mail_receive', unReadCount)
end

function _M.ClientSendMailRequest(title, content, cb)
    local request = {}
    request.c2s_receiver_uuid = DataMgr.Instance.UserData.RoleID
    request.c2s_title = title
    local contentData = {}
    contentData.txt_content = content
    request.c2s_content = contentData
    Protocol.RequestHandler.TLClientSendMailRequest(request, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--邮件列表
function _M.ClientGetMailBoxInfoRequest(cb)
    local msg = {}
    Protocol.RequestHandler.TLClientGetMailBoxInfoRequest(msg,function(ret)
        -- print_r(ret)
        MailModel.listData = ret.s2c_mailsnap_list
        if cb then cb(ret) end
    end)
end
--删除邮件
function _M.ClientDeleteMailRequest(c2s_delete_all,c2s_remove_uuid_list,cb)
    local msg = {c2s_delete_all= c2s_delete_all,c2s_remove_uuid_list=c2s_remove_uuid_list}
    Protocol.RequestHandler.TLClientDeleteMailRequest(msg,function(ret)
        -- print_r(ret)
        if cb  then cb() end
    end)
end
--获取mail详情.
function _M.ClientGetMailDetailRequest(c2s_mailuuid,cb)
    local msg = {c2s_mailuuid = c2s_mailuuid}
    Protocol.RequestHandler.TLClientGetMailDetailRequest(msg,function(ret)
        -- print_r(ret)
        if cb  and ret.s2c_mail_detail~=nil then cb(ret.s2c_mail_detail) end
    end)
end
--
function _M.ClientGetMailAttachmentReqeust(c2s_mailuuid,cb)
    local msg = {c2s_mailuuid = c2s_mailuuid}
    Protocol.RequestHandler.TLClientGetMailAttachmentRequest(msg,function(ret)
        -- print_r(ret)
        if cb then
            cb(rsp)
        end
    end, function(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

-- local function OnTLClientIncomingMailNotify( notify )
--     -- print("ClientIncomingMailNotify---------------------------------")
--     -- print_r(notify)
--     MailModel.unReadCount = notify.s2c_new_mail_count
--     -- print('TLClientIncomingMailNotify', MailModel.unReadCount)
--     EventManager.Fire("Event.Mail.UnRead", {num=notify.s2c_new_mail_count})
-- end

function _M.InitNetWork(initNotify)
    if initNotify then
        -- Protocol.PushHandler.TLClientIncomingMailNotify(OnTLClientIncomingMailNotify)
    end
end

return _M
