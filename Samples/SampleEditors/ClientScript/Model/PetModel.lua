

local PetModel = {
    petList = {}
}
--获得宠物列表请求
function PetModel.TLClientGetPetListRequest(cb)
    local msg = {}
    Protocol.RequestHandler.TLClientGetPetListRequest(msg,function(ret)
        print_r(ret)
        petList = ret.petList
        if cb and ret.s2c_code == 200 and petList~=nil then cb(petList) end
    end)
end
--获得宠物列表请求
function PetModel.TLClientPetDetailRequest(petId,cb)
    local msg = {petId=petId}
    Protocol.RequestHandler.TLClientPetDetailRequest(msg,function(ret)
        print_r(ret)
        if cb and ret.s2c_code == 200 then cb( ret.petDetail) end
    end)
end
--宠物布阵详情请求
function PetModel.TLClientPetPatternRequest(cb)
    local msg = {}
    Protocol.RequestHandler.TLClientPetPatternRequest(msg,function(ret)
        print_r(ret)
        if cb and ret.s2c_code == 200 then cb( ret) end
    end)
end
--宠物主战请求
function PetModel.TLClientPetMainRequest(petId,cb)
    local msg = {petId=petId}
    Protocol.RequestHandler.TLClientPetMainRequest(msg,function(ret)
        print_r(ret)
        if cb and ret.s2c_code == 200 then cb( ret) end
    end)
end
--宠物进阶详情请求
function PetModel.TLClientPetUpgradeInfoRequest(petId,cb)
    local msg = {petId=petId}
    Protocol.RequestHandler.TLClientPetUpgradeInfoRequest(msg,function(ret)
        print_r(ret)
        if cb and ret.s2c_code == 200 then cb( ret.upgradeInfo) end
    end)
end
--宠物技能详情请求
function PetModel.TLClientPetSkillInfoRequest(petId,cb)
    local msg = {petId=petId}
    Protocol.RequestHandler.TLClientPetSkillInfoRequest(msg,function(ret)
        print_r(ret)
        if cb and ret.s2c_code == 200 then cb( ret) end
    end)
end
--宠物培养请求
function PetModel.TLClientPetDevelopRequest(petId,itemId,cb)
    local msg = {petId=petId,itemId=itemId}
    Protocol.RequestHandler.TLClientPetDevelopRequest(msg,function(ret)
        print_r(ret)
        if cb and ret.s2c_code == 200 then cb( ret) end
    end)
end
--宠物进阶请求
function PetModel.TLClientPetUpgradeRequest(petId,cb)
    local msg = {petId=petId}
    Protocol.RequestHandler.TLClientPetUpgradeRequest(msg,function(ret)
        print_r(ret)
        if cb and ret.s2c_code == 200 then cb( ret) end
    end)
end
--宠物技能详情请求
function PetModel.TLClientPetSkillInfoRequest(petId,cb)
    local msg = {petId=petId}
    Protocol.RequestHandler.TLClientPetSkillInfoRequest(msg,function(ret)
        print_r(ret)
        if cb and ret.s2c_code == 200 then cb( ret.skillInfo) end
    end)
end
--宠物技能培养请求
function PetModel.TLClientPetSkillUpgradeRequest(petId,skillId,cb)
    local msg = {petId=petId,skillId=skillId}
    Protocol.RequestHandler.TLClientPetSkillUpgradeRequest(msg,function(ret)
        print_r(ret)
        if cb and ret.s2c_code == 200 then cb( ret.skillInfo) end
    end)
end
return PetModel
