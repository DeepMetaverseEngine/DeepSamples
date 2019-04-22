function main(ele, arg)
    Api.Listen.PlayerDead(nil,
        function(uuid)
            local allPlayer = Api.GetAllPlayers()
            local isAllDead = true
            for _, v in ipairs(allPlayer) do
                if not Api.IsPlayerDead(v) then
                    isAllDead = false
                end
            end

            if isAllDead then
                --gameover
                Api.SetGameOver(1, "")
                Api.Task.StopEvent(ID)
            end
        end
    )
    Api.Task.WaitAlways()
end

function clean(reason)

end
