function main()
    print('HotTest AreaManager ')
    -- Api.ReStart()
    -- do return end

    local function Printtest(name)
        print('test ------11111', os.clock(), name)
    end
    -- local p = Api.Task.CreateParallel()
    -- print('create ----', p, os.clock())
    -- Api.CallTo(p, Api.Task.AddEvent, Printtest)
    -- Api.CallTo(p, Api.Task.DelaySec, 5)
    -- Api.CallTo(p, Api.Task.AddEvent, Printtest)
    -- Api.CallTo(p, Api.Task.DelaySec, 1)
    -- Api.CallTo(p, Api.Task.AddEvent, Printtest)
    -- Api.CallTo(p, Api.Task.DelaySec, 3)
    -- Api.CallTo(p, Api.Task.AddEvent, Printtest)
    -- Api.Task.Wait(p)
    -- print('end---------', os.clock())
    local sample = {
        'Parallel',
        {
            'Sequence',
            -- Arg = 5,
            {Api.Task.DelaySec, 5},
            {Api.Task.AddEvent, Printtest, 'Sequence'},
            {Api.Task.DelaySec, 5},
            {Api.Task.AddEvent, Printtest, 'Sequence'}
        },
        {
            'Selector',
            {Api.Task.DelaySec, 1},
            {Api.Task.AddEvent, Printtest, 'Selector'},
            {Api.Task.DelaySec, 2},
            {Api.Task.AddEvent, Printtest, 'Selector'}
        },
        {
            'Parallel',
            {Api.Task.DelaySec, 1},
            {Api.Task.AddEvent, Printtest, 'Parallel'},
            {Api.Task.DelaySec, 2},
            {Api.Task.AddEvent, Printtest, 'Parallel'}
        },
        {
            {Api.Task.DelaySec, 1},
            {Api.Task.AddEvent, Printtest, 'Sequence111'},
            {Api.Task.DelaySec, 2},
            {
                'Parallel',
                {Api.Task.DelaySec, 1},
                {Api.Task.AddEvent, Printtest, 'Parallel222'},
                {Api.Task.DelaySec, 10},
                {Api.Task.AddEvent, Printtest, 'Parallel222'},
            },
            {Api.Task.AddEvent, Printtest, 'Sequence111'}
        }
    }


    local sample2 = {
        'Sequence',
        -- Arg = 5,
        {Api.Task.DelaySec, 5},
        {Api.Task.AddEvent, Printtest, 'Sequence'},
        {Api.Task.DelaySec, 5},
        {Api.Task.AddEvent, Printtest, 'Sequence'}
    }

    local zones = Api.GetAllZones()
    for _,v in ipairs(zones) do
        Api.SendMessage('Zone',v.ZoneUUID, 'hllerwe',{hellp='world',1,2,3})
    end
    -- local p = Api.Task.RunEvents(sample)
    print(' parse ok-----------',p)
    -- Api.Task.Wait(p)

    Api.Task.Wait()
    local dd = '你好'
    print('end -----------你好-----------',dd)

    Api.Listen.AddPeriodicSec(
        0.01,
        10,
        function()
            local xx = Api.RandomPercent(50)
            print(xx)
        end
    )
    Api.Task.Wait()

    


end
