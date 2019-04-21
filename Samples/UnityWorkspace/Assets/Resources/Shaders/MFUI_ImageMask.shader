// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "iPhone/MFUI_ImageMask"
{
	Properties
	{
		_MainTex ("MainTex", 2D) = "" {}
		_MaskTex ("MaskTex", 2D) = "" {}
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
			uniform sampler2D _MaskTex; 
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
				float4 c1 = tex2D(_MainTex, (input.tex));  
				float4 c2 = tex2D(_MaskTex, (input.tex));  
				c1.a = c2.r * _clrBase.a;
				return c1;
			}
 
			ENDCG
		}
	}
	
}

