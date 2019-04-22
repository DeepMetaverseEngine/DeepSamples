// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "TL/Eff/EasyAB+A_ui" {
	Properties{
		_TintColor("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex("Groud Texture", 2D) = "white" {}
	_TintColor01("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex01("Add Texture", 2D) = "white" {}
	_addPow("add power",Range(0,10)) = 1.0
		_alpPow("alpha power",Range(0,15)) = 1.0
		_InvFade("Soft Particles Factor", Range(0.01,3.0)) = 1.0


		//新增 记录裁剪框的四个边界的值
		_Area("Area", Vector) = (0,0,1,1)
		//----end----

	}

		Category{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "PreviewType" = "Plane" }
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask RGB
		Cull Off Lighting Off ZWrite Off

		SubShader{
		Pass{

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_particles
#pragma multi_compile_fog

#include "UnityCG.cginc"

		sampler2D _MainTex;
	fixed4 _TintColor;
	fixed4 _TintColor01;
	sampler2D _MainTex01;
	float4 _MainTex01_ST;
	fixed _addPow;
	fixed _alpPow;
	//新增，对应上面的_Area
	float4 _Area;
	//----end----


	struct appdata_t {
		float4 vertex : POSITION;
		fixed4 color : COLOR;
		float2 texcoord : TEXCOORD0;
		UNITY_VERTEX_INPUT_INSTANCE_ID
	};

	struct v2f {
		float4 vertex : SV_POSITION;
		fixed4 color : COLOR;
		float4 texcoord : TEXCOORD0;
		UNITY_FOG_COORDS(1)

		UNITY_VERTEX_OUTPUT_STEREO
			//新增，记录顶点的世界坐标
			float2 worldPos : TEXCOORD2;
		//----end----
	};

	float4 _MainTex_ST;

	v2f vert(appdata_t v)
	{
		v2f o;
		UNITY_SETUP_INSTANCE_ID(v);
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
		o.vertex = UnityObjectToClipPos(v.vertex);
		//#ifdef SOFTPARTICLES_ON
		//		o.projPos = ComputeScreenPos(o.vertex);
		//		COMPUTE_EYEDEPTH(o.projPos.z);
		//#endif
		o.color = v.color;
		o.texcoord.xy = TRANSFORM_TEX(v.texcoord,_MainTex);
		o.texcoord.zw = TRANSFORM_TEX(v.texcoord,_MainTex01);
		UNITY_TRANSFER_FOG(o,o.vertex);
		//新增，计算顶点的世界坐标
		o.worldPos = mul(unity_ObjectToWorld, v.vertex).xy;
		//----end----
		return o;
	}

	sampler2D_float _CameraDepthTexture;
	float _InvFade;

	fixed4 frag(v2f i) : SV_Target
	{
//#ifdef SOFTPARTICLES_ON
//		float sceneZ = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)));
//	float partZ = i.projPos.z;
//	float fade = saturate(_InvFade * (sceneZ - partZ));
//	i.color.a *= fade;
//#endif

	fixed4 gc = tex2D(_MainTex, i.texcoord.xy);

	fixed4 addc = tex2D(_MainTex01, i.texcoord.zw);

	fixed4 col = (2.0f * i.color *gc* _TintColor);

	fixed4 coladd = (_addPow *i.color *addc * _TintColor01);

	//	fixed aa = 2.0f * i.color *addc.r * _TintColor.a;

	fixed alpha = _alpPow * i.color *gc.a;

	UNITY_APPLY_FOG(i.fogCoord, col);

	//	return fixed4(col.rgb+coladd.rgb,col.a*_alpPow);

	//新增，判断顶点坐标是否在裁剪框内
	bool inArea = i.worldPos.x >= _Area.x && i.worldPos.x <= _Area.z && i.worldPos.y >= _Area.y && i.worldPos.y <= _Area.w;
	//----end----

	//如果在裁剪框内return原本的效果，否则即隐藏
	return inArea ? fixed4(col.rgb + coladd.rgb, col.a*_alpPow) : fixed4(0,0,0,0);



	}
		ENDCG
	}
	}
	}
}
