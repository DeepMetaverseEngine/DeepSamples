Shader "iPhone/MFUI_Image"
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
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Off
			Lighting Off
			Fog { Mode Off }
			ZTest Off
			Color [_clrBase]
			AlphaTest GEqual [_Cutoff]
			SetTexture [_MainTex] { combine texture * primary }
		}
	}
	
}

