-- 事件测试用例
function main()
    --客户端事件
end

-- lua Player Api.StartCommand('lua Player while true do Api.GetSession(Api.UUID) Api.Task.Sleep(0.01) end')
-- lua Player Api.Task.TransportPlayer({MapTemplateID = 100050})
-- lua Player Api.Task.TransportPlayer({MapTemplateID = 504010})
-- lua Player Api.Task.TransportPlayer({MapTemplateID = 100030})

-- 仙盟押镖
-- lua Player Api.Task.TransportPlayer({MapTemplateID = 501000})
-- lua Player Api.Task.TransportPlayer({MapTemplateID = 500050})
-- lua Player Api.Task.TransportPlayer({MapTemplateID = 100040})
-- lua Player Api.Task.TransportPlayer({MapTemplateID = 100010})
    
-- 据点战主战场
-- lua Player Api.Task.TransportPlayer({MapTemplateID = 502000})
-- 据点战副战场
-- lua Player Api.Task.TransportPlayer({MapTemplateID = 502001})
-- npc跟随玩家
-- lua Player Api.Task.StartEventByKey('follow.5')
-- 炼丹
-- lua Player Api.Task.StartEventByKey('alchemy.80001')
-- 刮刮卡
-- lua Player Api.Task.StartEventByKey('scratch.1')
-- lua Player Api.Task.StartEventByKey('treasure.51001')
-- 翻牌
-- lua Player Api.Task.StartEventByKey('supperzzle.1')
-- 九宫格
-- lua Player Api.Task.StartEventByKey('ninth_palace.1')
-- 奖励
-- lua Player Api.Task.StartEventByKey('reward.2')
-- lua Player for i=1,30 do for ii=2,10 do Api.Task.StartEventByKey('follow.'..ii) end end
-- lua Zone Api.Task.StartEventByKey('rift_pub.70001')
-- lua Zone Api.Task.StartEventByKey('rift_pub.70007')
-- lua Player Api.Task.StartEventByKey('turntable.1')
-- lua Player Api.Task.StartEventByKey('turntable.2')
-- lua Player Api.Task.StartEventByKey('reward.2')
-- lua Player Api.Task.StartEventByKey('reward.1')
-- lua Player Api.Task.StartEventByKey('reward.7000000')
-- lua Player Api.Task.StartEventByKey('group.10007')
-- lua Player Api.Task.StartEventByKey('horse.50001')
-- lua Client Api.Task.StartEventByKey('message.1')
-- lua Player Api.Task.StartEventByKey('monster_per.7030310')
-- lua Player Api.Task.StartEventByKey('item_per.6200120')
-- lua Player Api.Task.StartEventByKey('item_per.7000430')
-- lua Player Api.Task.StartEventByKey('item_pub.23040',Api.GetArg({Test=true}))
-- lua Api.ReStart()
-- lua Player start test,map
-- lua Player stop test
-- Lgc
--[[  自动测试

--测试大文件加载
lua Player pprint(Api.GetClientApi(Api.UUID).FindExcelData('item/item.xlsx/item',1))
lua Player pprint(Api.GetClientApi(Api.UUID).FindExcelData('reward/common_reward.xlsx/common_reward',1))
lua Player pprint(Api.FindExcelData('reward/common_reward.xlsx/common_reward',1))

-- 重启所有lua虚拟机
lua Api.ReStart()
-- 停止自动测试脚本
lua Player Api.Task.StopEvent('Tester')
-- message 
lua Player Api.Task.StartEvent('Tester','message', 0, 5)
-- 刮刮卡 
lua Player Api.Task.StartEvent('Tester','scratch')
-- 奖励 
lua Player Api.Task.StartEvent('Tester','reward')
-- 炼丹 
lua Player Api.Task.StartEvent('Tester','alchemy')
-- 翻牌 
lua Player Api.Task.StartEvent('Tester','supperzzle')
-- 拼图 
lua Player Api.Task.StartEvent('Tester','turntable')
-- 拼图 
lua Player Api.Task.StartEvent('Tester','treasure')
-- 驯马 
lua Player Api.Task.StartEvent('Tester','horse')
-- 个人道具 
lua Player Api.Task.StartEvent('Tester','item_per')
-- 公共道具 
lua Player Api.Task.StartEvent('Tester','item_pub')
-- 个人秘境
lua Player Api.Task.StartEvent('Tester','rift_per')
-- 公共秘境
lua Player Api.Task.StartEvent('Tester','rift_pub')
-- 地图初始化脚本测试
lua Player Api.Task.StartEvent('Tester','map',5)
-- 传送
lua Player Api.Task.StartEvent('Tester','transfer')
-- 私人怪
lua Player Api.Task.StartEvent('Tester','monster_per')
-- 公共怪
lua Player Api.Task.StartEvent('Tester','monster_pub')
-- 跟随
lua Player Api.Task.StartEvent('Tester','follow',5)
-- 尾随
lua Player Api.Task.StartEvent('Tester','stalker')


lua AreaManager pprint(Api.FindExcelData('item/item.xlsx/item',1000).item_key)
reload item/item.xlsx item
reload functions/open_time.xlsx open_time
组队自动战斗状态跨地图
]]
