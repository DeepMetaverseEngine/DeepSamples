Shader "iPhone/MFUI_Shape" 
{
	
	Properties
	{
		_clrBase ("Color", COLOR) =  (0,0,0,0.5)
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
			ZWrite Off
			ZTest Off
			Color [_clrBase]
			AlphaTest GEqual [_Cutoff]
        }
	}
}
