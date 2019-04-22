// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'


Shader "TL/Char/CharToon" {
	Properties{
		_color_map("color_map", 2D) = "white" {}
		_diff_power("diff_power", Range(0, 1)) = 0.9
		_fresnel_color("fresnel_color", Color) = (0,0,0,1)
		_fresnel_area("fresnel_area", Range(0, 3)) = 3
		_alpha("alpha", Range(0, 1)) = 1
	}

		SubShader{
			Tags{
					"IgnoreProjector" = "True"
					"Queue" = "Transparent"
					"RenderType" = "Transparent"
				}
			Blend SrcAlpha OneMinusSrcAlpha
			LOD 100

		Pass{
			Tags{ "LightMode" = "ForwardBase" }

			CGPROGRAM
			#pragma fragmentoption ARB_precision_hint_fastest  
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "AutoLight.cginc"


			uniform fixed3 _LightColor0;
			sampler2D _color_map;
			fixed _diff_power;
			fixed4 _fresnel_color;
			fixed _fresnel_area;
			fixed _alpha;




			struct V2f {
				fixed4 pos : SV_POSITION;
				fixed2 uv0 : TEXCOORD0;
				fixed4 posWorld : TEXCOORD1;
				fixed3 normalDir : TEXCOORD2;
				fixed3 fres : TEXCOORD3;
				fixed3 col : COLOR; 
			};

			V2f vert(appdata_full v) {

				V2f  o;

				o.uv0 = v.texcoord.xy;

				o.normalDir = UnityObjectToWorldNormal(v.normal);

				o.posWorld = mul(unity_ObjectToWorld, v.vertex);

				o.pos = UnityObjectToClipPos(v.vertex);

				fixed3 lightDirection = _WorldSpaceLightPos0.xyz;

				fixed3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - o.posWorld.xyz);

				o.fres = pow(1.0 - max(0, dot(o.normalDir, viewDirection)), _fresnel_area)* _fresnel_color.rgb;

				o.col =  lerp(max(0,dot(lightDirection, o.normalDir))*_LightColor0,UNITY_LIGHTMODEL_AMBIENT.rgb,0.2 );

				return o;
			}

			fixed4 frag(V2f i) : SV_Target{

			

				fixed4 diff = tex2D(_color_map, i.uv0) ;

				fixed3 finalColor = ( diff * _diff_power + diff * i.col) + i.fres;

				return fixed4(finalColor,_alpha);
			}
			ENDCG
	   }
    }
		FallBack "VertexLit"
}
