// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "niuqu" {
    Properties {
        _normal ("normal", 2D) = "bump" {}
		_MainTex("Main Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		_twistPower("twistPower", Range(0, 1)) = 0.2


	}
		SubShader{
			Tags {
				"IgnoreProjector" = "True"
				"Queue" = "Transparent"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "True"

			}
			
			  Pass {

				  Blend SrcAlpha OneMinusSrcAlpha
				  Cull Off
				  Lighting Off
				  ZWrite Off
				  ZTest[unity_GUIZTestMode]

				  CGPROGRAM
				  #pragma fragmentoption ARB_precision_hint_fastest
				  #pragma vertex vert
				  #pragma fragment frag
				  #include "UnityCG.cginc"
			      #include "UnityUI.cginc"
				  #pragma multi_compile __ UNITY_UI_ALPHACLIP
				  #pragma exclude_renderers d3d11 gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
				  #pragma target 2.0


			 sampler2D _MainTex;
             sampler2D _normal;
             fixed _twistPower;
			 fixed4 _Color;


            struct appdata {
				fixed4 vertex : POSITION;
				fixed2 uv : TEXCOORD0;
            };

            struct v2f {
				fixed4 pos : SV_POSITION;
				fixed2 uv : TEXCOORD0;
				fixed4 screenPos : TEXCOORD1;
            };

            v2f vert (appdata v) {
                v2f o;
                o.uv = v.uv;

                o.pos = UnityObjectToClipPos(v.vertex );

                o.screenPos = ComputeGrabScreenPos(o.pos);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
              
                fixed3 bump = UnpackNormal(tex2D(_normal,i.uv));

				fixed2 offset = bump.xy *  _twistPower;

				i.screenPos.xy = offset * i.screenPos.z + i.screenPos.xy;

#if UNITY_UV_STARTS_AT_TOP
				i.screenPos.y = -i.screenPos.y;
#else
				i.screenPos.y = i.screenPos.y;
#endif

				fixed3 refrCol = tex2D(_MainTex, i.screenPos.xy /i.screenPos.w).rgb;
			
				fixed3 col = refrCol * _Color;

                return fixed4(col, _Color.a);
            }
            ENDCG
        }
    }
}
