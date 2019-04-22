function main()

    local cam = Api.Camera.SetArgument({pos = {{x=11,y=-12},{x=11.1,y=-12.1},{x=11.2,y=-12.2}}, agl = {42,42,42} ,fov = {45,45,45}},{x=0,y=11,z=-12})

    Api.Task.Wait(cam)
  

end
