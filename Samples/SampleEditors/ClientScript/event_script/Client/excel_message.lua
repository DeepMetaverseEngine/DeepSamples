function main(ele, ...)
    local Convert_Texts = {
        '{PlayerName}',
        '{GuildName}'
    }
    local pos
    local text = Api.GetText(ele.text)
    ele.auto_convert = 1
    if ele.auto_convert == 1 and arg.special_index then
        local index = arg.special_index
        for i, v in ipairs(Convert_Texts) do
            text = text:gsub(v, '{' .. (index + i - 1) .. '}')
        end
    end

    local time_sec = arg.TimeSec or ele.time

    if ele.send_channel == 1 then
        Api.UI.ShowMeesageInChannel(Api.GetText(text, time_sec, ...), ele.channel_id[1])
    end
    -- print('text',text,arg.special_index)
    if ele.type == 1 then
        -- top message
        Api.Task.Wait(Api.Task.AddEvent('Client/countdown_ui', time_sec, Api.GetText(text), pos, ele.fade_out, ...))
    elseif ele.type == 2 then
        -- bottom message
        pos = {y = 426}
        Api.Task.Wait(Api.Task.AddEvent('Client/countdown_ui', time_sec, Api.GetText(text), pos, ele.fade_out, ...))
    elseif ele.type == 3 then
        -- go round message
        Api.UI.ShowGoround(Api.GetText(text))
    elseif ele.type == 4 then
        Api.UI.ShowFloatingTips(Api.GetText(text, time_sec, ...))
    end
end
