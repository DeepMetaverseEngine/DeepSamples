// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "iPhone/MFUI_ImageGray"
{
	Properties
	{
		_MainTex ("Base (RGB), Alpha (A)", 2D) = "white" {}
		_clrBase ("Color", COLOR) = (1, 1, 1, 1)
	}
	
	SubShader 
	{
		Tags { "Queue" = "Transparent" }
		
		Pass 
		{    
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Off
			Lighting Off
			Fog { Mode Off }
			ZTest Off

			CGPROGRAM
 
			#pragma vertex vert  
			#pragma fragment frag 
 
			uniform sampler2D _MainTex;     
			uniform float4 _clrBase;
 
			struct vertexInput
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct vertexOutput 
			{
				float4 pos : SV_POSITION;
				float2 tex : TEXCOORD0;
			};
 
			vertexOutput vert(vertexInput input) 
			{
				vertexOutput output;
 
				output.tex = input.texcoord;
				output.pos = UnityObjectToClipPos(input.vertex);
				return output;
			}
 
			float4 frag(vertexOutput input) : COLOR
			{
				float4 col = tex2D(_MainTex, input.tex);  
				float alpha = col.a;

				float grey = dot(col.rgb, float3(0.299, 0.587, 0.114));
				col.rgb = float3(grey, grey, grey);
				col.a = alpha * _clrBase.a;

				return col;
			}
 
			ENDCG
		}
	}
}