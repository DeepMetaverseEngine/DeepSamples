function main()

    local cam = Api.Camera.SetArgument({pos = {{x=8,y=-17.5},{x=8.1,y=-17.6},{x=8.2,y=-17.7}}, agl = {15,15,15} ,fov = {45,45,45}},{x=0,y=8,z=-17.5},{x=15,y=0,z=0})

    Api.Task.Wait(cam)
  

end
