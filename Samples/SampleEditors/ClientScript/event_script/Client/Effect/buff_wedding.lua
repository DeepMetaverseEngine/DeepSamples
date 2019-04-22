function main()

	--args: path, duration, size
	--duration为0表示不指定播放时长，使用动画默认时长
	--print("effectskill_30010")
	--api.PlayUI3DEffect('/res/effect/ui/EF_UI_ChunJie01.assetbundles', 0, 0.9)
	Api.UI.CloseAll()
	Api.PlayEffect('/res/effect/ui/EF_UI_ChunJie01.assetbundles', {UILayer=true,Pos={x = -30, y = 0, z = -333}},5)
	Api.PlayEffect('/res/effect/ui/EF_UI_ChunJie01.assetbundles', {UILayer=true,Pos={x = -30, y = 0, z = -333}},5)
	Api.PlayEffect('/res/effect/ui/EF_UI_ChunJie01.assetbundles', {UILayer=true,Pos={x = -30, y = 0, z = -333}},5)
end