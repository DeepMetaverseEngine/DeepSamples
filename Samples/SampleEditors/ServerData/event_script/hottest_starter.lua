function main(sec)
    local config = Api.GetCurrentConfig()
    if config.TestScript then
        Api.Task.HotReload(config.TestScript, sec or 2)
        Api.Task.Wait()
    end
end
