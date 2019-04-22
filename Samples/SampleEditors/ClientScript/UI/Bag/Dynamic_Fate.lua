local function CreateBegin(con_w)
	return
	{
	
		{
			direction='h',
			-- 左边

			{id = 'qdi',HZImageBox.CreateImageBox,X=1,Y=1,Img=Constants.InternalImg.icon_di,UIStyle=UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER},

			{id='item',X=10,Y=6,ItemShow.Create,Icon='79',padding=10,W=74,H=74},
			-- 中间部分
			{
				-- 名称sa
				{

					direction='h',
					{id='name',HZLabel.CreateLabel,Y=8,FontSize=Constants.FontSize.detail_name,Color=Constants.Color.detail_normal},
					{id='star',HZLabel.CreateLabel,Y=8,FontSize=Constants.FontSize.detail_name,TextAnchor=CommonUI.TextAnchor.R_T,X = con_w-120},
					--{id='bind',X=183,HZImageBox.CreateImageBox},
				},
				{
					direction='h',
					{			
						-- 装备职业、部位		
						{id='part',HZLabel.CreateLabel,FontSize=Constants.FontSize.detail_base,Color=Constants.Color.detail_normal},
						{
							direction='h',
							{id='lv',HZLabel.CreateLabel,FontSize=Constants.FontSize.detail_base,Color=Constants.Color.detail_normal},
						},
					},
					{
						X=100,
						{id='pro',HZLabel.CreateLabel,FontSize=Constants.FontSize.detail_base,Color=Constants.Color.detail_normal},
						{
							direction='h',
						{id='score',HZLabel.CreateLabel,FontSize=Constants.FontSize.detail_base,Color=Constants.Color.detail_normal,padding=5},
							{id='arrow',HZImageBox.CreateImageBox,Y=-3},
							},
					}
				},
				
			},
			padding = 8,
		},
	}
end

local color_random = 0x479dedff
local function CreateContent(con_w)
	return 
	{
		X=20,
		{id='strengthen',HZLabel.CreateLabel,FontSize=Constants.FontSize.detail_attr_title,Color=Constants.Color.detail_attr_title,padding=4},
		--宝石属性
		
													--'无'
		-- {id='nobase_attr',HZLabel.CreateLabel,Text= Constants.Text.no_attr ,FontSize=Constants.FontSize.detail_attr_title,Color=Constants.Color.detail_attr_title},

		{
			id='base_attr',
			{
				direction='h',
				{HZLabel.CreateLabel,Text=Constants.Text.base_attr_title,FontSize=Constants.FontSize.detail_attr_title,Color=Constants.Color.detail_attr_title,padding=6},
				{
					X=250,
					{
						id = 'lock',{HZImageBox.CreateImageBox,Img=Constants.InternalImg.fate_lock,UIStyle=UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER},
					},
				},
			},
			{
				id='base_attr_array',
				direction='h',
				{sub_id='img',HZImageBox.CreateImageBox,Y=5,padding=8},
				{sub_id='attrname',HZLabel.CreateLabel,FontSize=Constants.FontSize.detail_normal,Color=Constants.Color.detail_normal,padding=5},
				{sub_id='attr',X=130,HZLabel.CreateLabel,FontSize=Constants.FontSize.detail_normal,Color=Constants.Color.detail_normal,padding=5},
				{sub_id='arrow',HZImageBox.CreateImageBox,Y=0},
				{sub_id='appendvalue',X=210,HZLabel.CreateLabel,FontSize=Constants.FontSize.Green,Color=Constants.Color.Green},
				padding = 6, --基础属性文字间距
			},
		},
		
		--'无'
		-- {id='nonext_attr',HZLabel.CreateLabel,Text=Constants.Text.no_attr,FontSize=Constants.FontSize.detail_attr_title,Color=Constants.Color.detail_attr_title},
		
		{
			id='next_attr',
			{
				{
					direction='h',
					{HZLabel.CreateLabel,Text = Constants.Text.nonext_attr_title,FontSize=Constants.FontSize.detail_attr_title,Color=Constants.Color.detail_attr_title},
					{
						X=120,
						{
							direction='h',
							id = 'next_cost',
							{HZLabel.CreateLabel,Text=Constants.Text.next_attr_cost ,FontSize=Constants.FontSize.detail_attr_title,Color=Constants.Color.detail_attr_title},
							{HZImageBox.CreateImageBox,X=45,Img=Constants.InternalImg.fate_cost,UIStyle=UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER},
						},
					},
					{id ='costnum',HZLabel.CreateLabel,X=185,FontSize=Constants.FontSize.Green,Color=Constants.Color.Green},
				},
				{
					id='next_attr_array',
					direction='h',
					{sub_id='img',HZImageBox.CreateImageBox,Y=5,padding=8},
					{sub_id='attrname',HZLabel.CreateLabel,FontSize=Constants.FontSize.detail_normal,Color=Constants.Color.detail_normal,padding=5},
					{sub_id='attr',X=130,HZLabel.CreateLabel,FontSize=Constants.FontSize.detail_normal,Color=Constants.Color.detail_normal,padding=5},
					{sub_id='arrow',HZImageBox.CreateImageBox,Y=0},
					{sub_id='appendvalue',X=210,HZLabel.CreateLabel,FontSize=Constants.FontSize.Green,Color=Constants.Color.Green},
					padding = 6, --基础属性文字间距
				},
			},
		},


		
		{HZImageBox.CreateImageBox,X=-22,padding = 6,Img=Constants.InternalImg.split_line,W=339,H=5,UIStyle=UILayoutStyle.IMAGE_STYLE_BACK_4},
		
		{id='desc',HZTextBox.CreateTextBox,ContentW=con_w-40,padding=6},
	}	
end

local function CreateEnd()
	
end

return {CreateBegin = CreateBegin, CreateContent = CreateContent, CreateEnd = CreateEnd}