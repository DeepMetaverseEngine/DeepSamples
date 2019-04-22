function main(scriptName,...)
    local id = Api.Task.StartDramaScript(scriptName, ...)
    return Api.Task.Wait(id)
end