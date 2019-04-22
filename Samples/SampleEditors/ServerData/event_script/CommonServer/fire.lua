function main()
    local ename, param = unpack(string.split(ArgID, '|'))
    local current_args = Api.GetNextArg(arg, {content = param})
    Api.SendMessage(Api.ManagerName, Api.UUID, ename, current_args)
end
