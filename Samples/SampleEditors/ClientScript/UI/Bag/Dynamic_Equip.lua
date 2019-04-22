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
		{
			id = 'gem_attr',
			{id='gem_attr_title',HZLabel.CreateLabel,FontSize=Constants.FontSize.detail_attr_title,Color=Constants.Color.detail_attr_title,padding = 6},
			{
				direction = 'h',
				{		
					{HZImageBox.CreateImageBox,id='gem_slot1',UIStyle=UILayoutStyle.IMAGE_STYLE_BACK_4,W=27,H=27},
					{HZImageBox.CreateImageBox,id='gem_slotlock1'},
					padding=6,
				},
				{
					{HZImageBox.CreateImageBox,id='gem_slot2',UIStyle=UILayoutStyle.IMAGE_STYLE_BACK_4,W=27,H=27},
					{HZImageBox.CreateImageBox,id='gem_slotlock2'},
					padding=6,
				},
				{
					{HZImageBox.CreateImageBox,id='gem_slot3',UIStyle=UILayoutStyle.IMAGE_STYLE_BACK_4,W=27,H=27},
					{HZImageBox.CreateImageBox,id='gem_slotlock3'},
					padding=26,
				},
				padding=15,
			},
			{
				id='gem_attr_value',
				direction = 'h',
				{sub_id='img',HZImageBox.CreateImageBox,Y=5,padding=8},
				{sub_id='attrdesc',HZLabel.CreateLabel,FontSize=Constants.FontSize.detail_normal,Color=Constants.Color.detail_gem_attr},
				padding = 6, --鉴定属性文字间距	
			},
			{HZImageBox.CreateImageBox,X=-22,Img=Constants.InternalImg.split_line,W=339,H=5,UIStyle=UILayoutStyle.IMAGE_STYLE_BACK_4},
			padding=6,
		},
		{HZLabel.CreateLabel,Text=Constants.Text.base_attr_title,FontSize=Constants.FontSize.detail_attr_title,Color=Constants.Color.detail_attr_title,padding=6},
		{
			id='base_attr',
			{
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
		},
		{HZImageBox.CreateImageBox,X=-22,padding = 6,Img=Constants.InternalImg.split_line,W=339,H=5,UIStyle=UILayoutStyle.IMAGE_STYLE_BACK_4},
		--鉴定属性
		{
			{HZLabel.CreateLabel,Text=Constants.Text.extra_attr_title,FontSize=Constants.FontSize.detail_attr_title,Color=Constants.Color.detail_attr_title,padding=6},
			--line
			id='extra_attr',
			{
				{
					id='extra_attr_array',
					direction = 'h',
					{sub_id='img',HZImageBox.CreateImageBox,Y=5,padding=8},
					{sub_id='attrname',HZLabel.CreateLabel,FontSize=Constants.FontSize.detail_normal,Color=Constants.Color.detail_extra_attr},
					{sub_id='attr',X=130,HZLabel.CreateLabel,FontSize=Constants.FontSize.detail_normal,Color=Constants.Color.detail_extra_attr},	
					padding = 6, --鉴定属性文字间距	
				},
				{id='end_line',HZImageBox.CreateImageBox,X=-22,W=339,H=5,UIStyle=UILayoutStyle.IMAGE_STYLE_BACK_4},
				padding=6,
			}
		},
		{id='desc',HZTextBox.CreateTextBox,ContentW=con_w-40,padding=6},
	}	
end

local function CreateEnd()
	
end

return {CreateBegin = CreateBegin, CreateContent = CreateContent, CreateEnd = CreateEnd}