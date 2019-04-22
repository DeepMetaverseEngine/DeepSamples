function main()


	local cam = Api.Camera.SetArgument({pos = {{x=5.5,y=-14},{x=5.6,y=-14.1},{x=5.7,y=-14.2}}, agl = {7,7,7} ,fov = {45,45,45}},{x=0,y=5.5,z=-14},{x=7,y=0,z=0})
   
   Api.Task.Wait(cam)
  

end
