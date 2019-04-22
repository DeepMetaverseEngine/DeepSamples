local _M = {}
_M.__index = _M


--获取商品列表
function _M.ClientGetAuctionItemListRequest(itemType, secType, pro, level, quality, star_level, tpltList, part, lastId, cb)
    print("----------ClientGetAuctionItemListRequest----------", itemType, secType, pro, level, quality, star_level, tpltList, part, lastId)
    local msg = { c2s_item_type = itemType, c2s_sec_type = secType, c2s_pro = pro, c2s_level = level, 
    c2s_quality = quality, c2s_star_level = star_level, c2s_templateIds = tpltList, c2s_part = part, c2s_lastId = lastId }
    Protocol.RequestHandler.ClientGetAuctionItemListRequest(msg, function(rsp)
        -- print_r(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--购买物品
function _M.ClientAuctionBuyRequest(auctionId, price, num, cb)
    -- print("----------ClientAuctionBuyRequest----------")
    local msg = { c2s_auctionId = auctionId, c2s_price = price, c2s_num = num }
    Protocol.RequestHandler.ClientAuctionBuyRequest(msg, function(rsp)
        -- print_r(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--物品上架
function _M.ClientPutOnShelvesRequest(slotIndex, num, price, cb)
    -- print("----------ClientPutOnShelvesRequest----------")
    local msg = { c2s_slotIndex = slotIndex, c2s_num = num, c2s_price = price }
    Protocol.RequestHandler.ClientPutOnShelvesRequest(msg, function(rsp)
        -- print_r(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--物品下架
function _M.ClientPutOffShelvesRequest(auctionId, cb)
    print("----------ClientPutOffShelvesRequest----------", auctionId)
    local msg = { c2s_auctionId = auctionId }
    Protocol.RequestHandler.ClientPutOffShelvesRequest(msg, function(rsp)
        -- print_r(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--修改上架物品价格
function _M.ClientAuctionChangePriceRequest(auctionId, price, cb)
    -- print("----------ClientAuctionChangePriceRequest----------")
    local msg = { c2s_auctionId = auctionId, c2s_price = price }
    Protocol.RequestHandler.ClientAuctionChangePriceRequest(msg, function(rsp)
        -- print_r(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--获取出售列表
function _M.ClientGetSaleItemListRequest(cb)
    -- print("----------ClientGetSaleItemListRequest----------")
    local msg = { }
    Protocol.RequestHandler.ClientGetSaleItemListRequest(msg, function(rsp)
        -- print_r(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--获取正在出售的同类物品
function _M.ClientGetOtherItemListRequest(templateId, cb)
    -- print("----------ClientGetOtherItemListRequest----------")
    local msg = { c2s_templateId = templateId }
    Protocol.RequestHandler.ClientGetOtherItemListRequest(msg, function(rsp)
        -- print_r(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--获取收益信息
function _M.ClientGetSalesRecordRequest(cb)
    -- print("----------ClientGetSalesRecordRequest----------")
    local msg = { }
    Protocol.RequestHandler.ClientGetSalesRecordRequest(msg, function(rsp)
        -- print_r(rsp)
        if cb then
            cb(rsp)
        end
    end)
end

--提取收益
function _M.ClientAuctionExtractRequest(cb)
    -- print("----------ClientAuctionExtractRequest----------")
    local msg = { }
    Protocol.RequestHandler.ClientAuctionExtractRequest(msg, function(rsp)
        -- print_r(rsp)
        if cb then
            cb(rsp)
        end
    end)
end


function _M.InitNetWork(initNotify)
    if initNotify then
        
    end
end


return _M