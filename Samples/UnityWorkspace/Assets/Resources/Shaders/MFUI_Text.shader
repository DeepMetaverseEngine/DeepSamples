// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "iPhone/MFUI_Text"
{
	Properties
	{
		_MainTex ("MainTex", 2D) = "" {}
		_clrBase ("Color", COLOR) = (1, 1, 1, 1)
	}

	SubShader
	{
		Tags { "Queue" = "Transparent" }

		Pass
		{
			Blend One OneMinusSrcAlpha
			Cull Off
			Lighting Off
			Fog { Mode Off }
			ZTest Off
			AlphaTest GEqual [_Cutoff]
			
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
				float4 c1 = tex2D(_MainTex, input.tex);
				c1.rgb = c1.rgb * _clrBase.a;
				return c1 * _clrBase;
			}
 
			ENDCG
		}
	}
	
	
}

