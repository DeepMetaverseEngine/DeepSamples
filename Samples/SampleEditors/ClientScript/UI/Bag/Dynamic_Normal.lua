local function CreateBegin(con_w)
	return
	
	{
				
		{
			{id = 'qdi',HZImageBox.CreateImageBox,X=1,Y=1,Img=Constants.InternalImg.icon_di,UIStyle=UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER},

			direction='h',
			-- 左边
			{id='item',X=10,Y=6,ItemShow.Create,Icon='79',padding=10,W=74,H=74},
			-- 中间部分
			{
				-- 名称
				{
					direction='h',
					{id='name',HZLabel.CreateLabel,Y=8,FontSize=Constants.FontSize.detail_name,Color=Constants.Color.detail_normal},
					--{id='bind',X=183,HZImageBox.CreateImageBox},
				},
				{direction='h',-- 作用
				{id='able',HZLabel.CreateLabel,FontSize=Constants.FontSize.detail_base,Color=Constants.Color.detail_normal},
						},
				{direction='h',-- 作用
				{id='use_lv',HZLabel.CreateLabel,FontSize=Constants.FontSize.detail_base,Color=Constants.Color.detail_normal},
				},
				
			},
			padding = 6,
		},
	}
end


local function CreateContent(con_w)
	return 
	{
		X=20,
		{id='desc',HZTextBox.CreateTextBox,FontSize=Constants.FontSize.detail_normal,ContentW=con_w-40,padding=20},
		-- {HZImageBox.CreateImageBox,Img='#dynamic/tips/output/tips.xml|tips|24',padding=10,W=con_w,UIStyle=UILayoutStyle.IMAGE_STYLE_BACK_4},
		--line
		{
			id='desc1',
			{id='desc1_text',HZTextBox.CreateTextBox,FontSize=Constants.FontSize.detail_normal,ContentW=con_w-40,padding=20},
		},
		{id='effect_time',HZLabel.CreateLabel,FontSize=Constants.FontSize.detail_normal,Color=Constants.Color.detail_normal,padding=20},
		{
			id='price',
			direction='h',
			{id='price_value',HZLabel.CreateLabel,FontSize=Constants.FontSize.detail_normal,Color=Constants.Color.detail_normal},
			
		}
		
	}	
end

local function CreateEnd()
	return
	{
		
	}
end

return {CreateBegin = CreateBegin, CreateContent = CreateContent, CreateEnd = CreateEnd}