local _M = {}

local cjson = require "cjson"
local Util	  = require "Logic/Util"
local ChatModel = require "Model/ChatModel"
local ItemModel = require "Model/ItemModel"

_M.ChatFace = GlobalHooks.DB.Find('ChatFace', {})

_M.m_StrTemp = nil			   --中间转换码
 
_M.LinkType = {
	LinkTypeItem = 1,		  --物品超链接
	LinkTypeVoice = 2,		 --语音超链接
	LinkTypePerson = 3,		--人物超练级
	LinkTypeSendPlace = 4,	 --传送到某人身边超链接
	LinkTypeTeamShout = 5,	   --队伍消息超练级
	LinkTypeMapMsg = 6,		--根据地图id和x，y坐标传送信息处理超练级
	LinkTypeMonster = 7,	   --怪物超链接
	LinkTypePet = 8,		   --宠物超链接
	LinkTypeSkill = 9,		 --技能介绍超链接
	LinkTypeRecruit = 10,	  --队伍招募信息超链接
	LinkTypeGoto = 11,		 --everything in goto
	LinkTypeGuildRecruit = 12, --公会招募信息超链接
	LinkTypeMultiVSReport = 13,--5v5战报超链接
	LinkTypeRedEnvelope = 14,  --红包超链接
	LinkTypeSysItem = 15,		  --物品超链接 没有下划线
	LinkTypeGuildShout = 16,
}

-- _M.PrivateList = {}
_M.UnionData = {}



local function DealItemEncode(msg, type)
	return "|<" .. type .. " " .. msg .. "></" .. type .. ">|"
end

 
function _M.StartsWith(str,Start)
	return string.starts(str, Start)
end
 
function _M.EndsWith(str, End)
	return string.ends(str, End)
end

function _M.MsgConvertToStr(data, type)
	-- body
	if data == nil then
		return ""
	end

	local input = {}
	input.MsgType = type
	input.data = data
	return cjson.encode(input)
end

function _M.AddFaceByIndex(index)
	local input = {}
	input.index = index
	local msg = cjson.encode(input)
	return DealItemEncode(msg, "q")
end

function _M.GetFaceEmotion(index)
	if _M.ChatFace[index] then
		return _M.ChatFace[index].e_code
	end
	return ""
end

function _M.GetFaceEmotionLang(index)
  return _M.ChatFace[index].e_text
end

function _M.Input2Str(input)
	return cjson.encode(input)
end

function _M.GuildRecruitShout(mainChannel,showChannels)
	local input = {}
	input.MsgType = _M.LinkType.LinkTypeGuildShout
	input.GuildId = DataMgr.Instance.UserData.GuildId
	input.GuildName = DataMgr.Instance.UserData.GuildName
	input.ClickApply = Util.GetText('common_click_apply')
	local linkdata = _M.Input2Str(input)

	local showData = Util.GetText('guild_yelling',input.GuildName)
	local message = showData .. DealItemEncode(linkdata, "a")
	ChatModel.ChatShout(message,mainChannel,showChannels)
end

 
-- showData显示的消息
-- mainChannle 消息发送频道 默认是ChatModel.ChannelState.CHANNEL_WORLD
-- showChannels 消息另外显示的频道  是一个{} 额外要显示的聊天频道都放到这里，mainChannle
function _M.TeamShout(showData,mainChannel,showChannels,cb)
	local input = {}
	input.MsgType = _M.LinkType.LinkTypeTeamShout
	input.playerId = DataMgr.Instance.UserData.RoleID
	input.ClickApply = Util.GetText('common_click_apply')
	local linkdata =  _M.Input2Str(input)
	
	local message = showData .. DealItemEncode(linkdata, "a")
	ChatModel.ChatShout(message,mainChannel,showChannels,cb)
end


function _M.SendBeastsTeam()
    
    local input = {}
    input.MsgType = ChatUtil.LinkType.LinkTypeTeamShout
    input.GuildId = DataMgr.Instance.UserData.GuildId
    input.playerId = DataMgr.Instance.UserData.RoleID
    input.GuildName = DataMgr.Instance.UserData.GuildName
    input.ClickApply = Util.GetText('common_click_apply')
    local linkdata = cjson.encode(input)

    local showData = Util.GetText('guild_monster_yelling')
    local messsage = showData .. DealItemEncode(linkdata, "a")

    local send = {}
    send.show_type = 0
    send.show_channel = ChatModel.ChannelState.CHANNEL_GUILD
    send.channel_type = ChatModel.ChannelState.CHANNEL_GUILD
    send.from_name = DataMgr.Instance.UserData.Name
    send.from_uuid = DataMgr.Instance.UserData.RoleID
    send.content = messsage
    send.func_type = 0

    Protocol.RequestHandler.ClientChatRequest(send, function(ret)
        if cb ~= nil then
           cb(ret)
        end
    end)


end

function _M.AddItemByData(detail)
	--print("_____________________________________")
	--print(PrintTable(detail))
	local input = {}
	input.ID = detail.ID
	input.TemplateId = detail.TemplateID
	input.MsgType = _M.LinkType.LinkTypeItem						  --为其它类型提供扩展
	input.Quality = detail.Quality
	input.Name = Util.GetText(detail.static.name)
	local msg = cjson.encode(input)
	return DealItemEncode(msg, "a")
end


function _M.AddItemByTemplateId(templateId)
	local detail = ItemModel.GetDetailByTemplateID(templateId)
	detail.TemplateID = detail.static.id
	detail.ID = detail.static.id
	detail.Quality = detail.static.quality
	local input = {}
	input.Id = detail.ID
	input.TemplateId = detail.TemplateID
	-- 不要链接
	input.MsgType = _M.LinkType.LinkTypeSysItem						  --为其它类型提供扩展
	input.Quality = detail.Quality
	input.Name = Util.GetText(detail.static.name)
	local msg = cjson.encode(input)
	return DealItemEncode(msg, "a")
end

-- function _M.ShowPlayerGetItem(templateId,count)
-- 	local RoChatModel = require "Model/ChatModel"
--     local content = _M.AddItemByTemplateId(templateId)
--     local get_text = Constants.Text.chat_getItem    -- '获得{0}x{1}'
--     local message = Util.GetText(get_text,content,count)
-- 	RoChatModel.AddClientMsg(ChatModel.ChannelState.CHANNEL_SYSTEM,message)
-- end

function _M.AddMapMsg(data)
	--地图传送信息处理
	return _M.MsgConvertToStr(data, _M.LinkType.LinkTypeMapMsg)
end

function _M.AddVoiceByData(data)
	data.MsgType = _M.LinkType.LinkTypeVoice
	local msg = cjson.encode(data)
	return DealItemEncode(msg, "v")
end

function _M.AddPersonByData(data)
	-- body
	if data == nil then
		return ""
	end

	local input = {}
	input.s2c_playerId = data.s2c_playerId
	input.MsgType = _M.LinkType.LinkTypePerson
	input.s2c_name = data.s2c_name
	input.s2c_level = data.s2c_level
	input.Quality = data.Quality
	local msg = cjson.encode(input)
	return DealItemEncode(msg, "a")
end

function _M.AddSendPlace(data)
	--传送信息处理
	if data == nil then
		return ""
	end

	local input = {}
	input.MsgType = _M.LinkType.LinkTypeSendPlace
	input.id = data.id
	return cjson.encode(input)
end

 
--玩家广播消息里的表情解析 by cywu
function _M.HandleChatClientDecodeFace(msg)
	local message = ""
	local retArray = msg:split("|")
	for i, ement in ipairs(retArray) do
		local item = ement
		if _M.StartsWith(item, "<q ") and _M.EndsWith(item, "></q>") then
			local ok,msg = pcall(cjson.decode,ChatModel.GetContent(item))
			if ok then 
				local temp = '{' .. Util.GetText(_M.GetFaceEmotionLang(msg.index)) .. '}'
				message = message .. temp
			end
		else
			message = message .. ement
		end
	end
	return message
end

local function FormatNormalMessage(item,strColor,strokeColor,fontSize,SysMsg)
	if strColor == nil then
		strColor = 0x000000
	end
	if fontSize == nil then
		fontSize = 16
	end
	if not SysMsg then
		item = Util.FilterBlackWord(item)
		item = '<![CDATA['.. item ..']]>'
	end
	local color = GameUtil.RGB_To_ARGBString(strColor)
	local str
	if strokeColor ~= nil then
		str = "<font size= '" .. fontSize .. "' border= '8' bcolor='" .. GameUtil.RGB_To_ARGBString(strokeColor) .. "' color='" .. string.format("%08X", color) .. "'>"
	else
		str = "<font size= '" .. fontSize .. "' color='" .. color .. "'>"
	end

	local message = UIFactory.Instance:DecodeAttributedString(str .. item .. "</font>", nil)
	if message == nil then
		message = UIFactory.Instance:DecodeAttributedString(str .. "error" .. "</font>", nil)
	end
	return message
end


function _M.HandleChatClientDecode(msg, strColor, title, strokeColor, fontSize,SysMessage)
	--富文本解析
	if strColor == nil then
		strColor = 0x000000
	end
	
	if fontSize == nil then
		fontSize = 16
	end
	local linkdata = AttributedString()
	if title ~= nil then
		for i = 1, #title do
			linkdata:Append(title[i])
		end
	end

	local retArray = msg:split("|")
 
	for i, ement in ipairs(retArray) do
		local item = ement
		local temptext = ""
		local abs = AttributedString()
		if _M.StartsWith(item, "<q ") and _M.EndsWith(item, "></q>") then
			local itemData = GameUtil.createTextAttribute(strColor,fontSize)
			temptext = "a"
			local ok,msg = pcall(cjson.decode,ChatModel.GetContent(item))
			if not ok or msg == nil or type(msg) ~= "table" then
				if msg and  type(msg) ~= "table" then
					local message = FormatNormalMessage(item,strColor,strokeColor,fontSize)
					if message ~= nil then
						abs:Append(message)
					end	
				end
				print('1gv5871b')
			else
				local e_code = _M.GetFaceEmotion(msg.index)
				if not string.IsNullOrEmpty(e_code) then
					itemData.resSprite = "/dynamic/emotion/output/emotion.xml," .. e_code
					abs:Append(temptext, itemData)
				end
			end
		elseif _M.StartsWith(item, "<a ") and _M.EndsWith(item, "></a>") then
			local itemData = GameUtil.createTextAttribute(strColor,fontSize)
			local curcontent = ChatModel.GetContent(item)
			local ok,msg = pcall(cjson.decode,curcontent)
			if not ok or  msg == nil or type(msg) ~= "table" or msg.MsgType == nil then
				if msg and  type(msg) ~= "table" then
					local message = FormatNormalMessage(item,strColor,strokeColor,fontSize)
					if message ~= nil then
						abs:Append(message)
					end	
				end
				print('1gv5871b')
			elseif msg.MsgType == _M.LinkType.LinkTypeItem then			   --物品
				temptext = msg.Name
				GameUtil.SetTextAttributeFontColorRGB(itemData,Constants.QualityColor[msg.Quality or 0])
				if strokeColor ~= nil then
					itemData.borderColor = strokeColor
				end
				itemData.underline = true
				itemData.borderCount = TextBorderCount.Border
			elseif msg.MsgType == _M.LinkType.LinkTypeSysItem then
				temptext = msg.Name
				GameUtil.SetTextAttributeFontColorRGB(itemData,Constants.QualityColor[msg.Quality or 0])
				if strokeColor ~= nil then
					itemData.borderColor = strokeColor
				end 
				itemData.borderCount = TextBorderCount.Border
			elseif msg.MsgType == _M.LinkType.LinkTypePerson then		  --人物
				temptext = msg.s2c_name
				GameUtil.SetTextAttributeFontColorRGB(itemData,Constants.QualityColor[msg.Quality or 0])
				if strokeColor ~= nil then
					itemData.borderColor = strokeColor
				end
				itemData.borderCount = TextBorderCount.Border
			elseif msg.MsgType == _M.LinkType.LinkTypeTeamShout then
				temptext = msg.ClickApply
				GameUtil.SetTextAttributeFontColorRGB(itemData,Constants.Color.Green3)
				itemData.underline = true
				if strokeColor ~= nil then
					itemData.borderColor = strokeColor
				end
			elseif msg.MsgType == _M.LinkType.LinkTypeGuildShout then
				-- print_r('msg:',msg)
				temptext = msg.ClickApply
				GameUtil.SetTextAttributeFontColorRGB(itemData,Constants.Color.Green3)
				itemData.underline = true
				if strokeColor ~= nil then
					itemData.borderColor = strokeColor
				end
			end
			--itemData.fontStyle = FontStyle.STYLE_BOLD
			itemData.link = curcontent
			abs:Append(temptext, itemData)
			
		--语音先注释掉
		--elseif _M.StartsWith(item, "<v ") and _M.EndsWith(item, "></v>") then
		--	local itemData = GameUtil.createTextAttribute(strColor,fontSize)
		--	local curcontent = ChatModel.GetContent(item)
		--	local msg = cjson.decode(curcontent)
		--	local viocetext = "v"
		--	local vioceabs = AttributedString()
		--	local vioceItemData = GameUtil.createTextAttribute(strColor,fontSize)
		--	vioceItemData.resSprite = "/dynamic/effects/chat_voice/chat_voice.xml,chatvoice2"
		--	--itemData.fontStyle = FontStyle.STYLE_BOLD
		--	vioceItemData.link = curcontent
		--	vioceabs:Append(viocetext, vioceItemData)
		--	linkdata:Append(vioceabs)
        --
		--	temptext = msg.Time .. "s"
		--	GameUtil.SetTextAttributeFontColorRGB(itemData,strColor)
		--	abs:Append(temptext, itemData);
			--print("-------------------------------")
		elseif _M.StartsWith(item, "<p ") and _M.EndsWith(item, "></p>") then
			local itemData = GameUtil.createTextAttribute(strColor,fontSize)
			local curcontent = ChatModel.GetContent(item)
			local message = FormatNormalMessage(curcontent,strColor,strokeColor,fontSize,true)
			if message ~= nil then
				abs:Append(message)
			end		
		else
			local message = FormatNormalMessage(item,strColor,strokeColor,fontSize,SysMessage)
			if message ~= nil then
				abs:Append(message)
			end			 
		end
		linkdata:Append(abs)
	end

	return linkdata
end

function _M.HandleOriginalToInput(msg)
	--文字转换成输入
	local retArray = string.split(msg, "|")
	_M.m_StrTemp = {}
	local str = ""
	for i, ement in ipairs(retArray) do
		local item = ement
		if (_M.StartsWith(item, "<q ") and _M.EndsWith(item, "></q>")) 
			or (_M.StartsWith(item, "<a ") and _M.EndsWith(item, "></a>")) then
			str = str .. "|" .. i .. "|"
			_M.m_StrTemp[i] = item
			i = i + 1
		else
			local startpos = string.find(item, "<f")
			local endpos = string.find(item, "</f>")
			local endpos2 = string.find(item, "</font>")
			if startpos and (endpos or endpos2) then
				str = str .. "|" .. i .. "|"
				_M.m_StrTemp[i] = item
				i = i + 1
			else
				str = str .. item
			end
		end
	end
	return str
end

function _M.HandleInputToOriginal(msg)
	--输入转换文字
	local retArray = string.split(msg, "|")
 
	local str = ""
	for i, ement in ipairs(retArray) do
		local item = tonumber(ement)
		 
		if (_M.m_StrTemp ~= nil and _M.m_StrTemp[item] ~= nil) then
			str = str .. "|" .. _M.m_StrTemp[item] .. "|"
		else
			str = str .. ement
		end
	end
	 
	return str
end
 
function _M.HandleString(msg, dest)
	--文字转换成输入
	local retArray = string.split(msg, "|")
	_M.m_StrTemp = {}
	local str = ""
	for i, ement in ipairs(retArray) do
		local item = tonumber(ement)
		if item ~= nil then
			if dest[item] ~= nil then
				str = str .. dest[item]
			end
		else
			str = str .. ement
		end
	end
	return str
end

function _M.GetActorPos()
 
	local posData = GameUtil.GetActorPos() 

	local linkData = {}
	linkData.MapTemplateId = DataMgr.Instance.UserData.MapTemplateId
	linkData.ZoneUUID = DataMgr.Instance.UserData.ZoneUUID

	linkData.targetX = math.floor(posData.x)
	linkData.targetY = math.floor(posData.y)
 	local linkMsg = _M.AddMapMsg(linkData)
 	local mapName = DataMgr.Instance.UserData.MapName
 	local posInfo = "<p " .. Util.GetText(Constants.ChatFomatText.ShowPos,linkMsg,mapName,linkData.targetX,linkData.targetY) .. "></p>"
	return posInfo
end
 
GlobalHooks.ChatUtil = GlobalHooks.ChatUtil or {}
GlobalHooks.ChatUtil.HandleChatClientDecode = _M.HandleChatClientDecode

return _M

