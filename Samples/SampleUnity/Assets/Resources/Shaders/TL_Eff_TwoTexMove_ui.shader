Shader "TL/Eff/TwoTexMove_ui" {
Properties {
	[HDR]_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)

	_MainTex ("Main Texture", 2D) = "white" {}

	_Speed("Distort Speed", Float) = 1

		//���� ��¼�ü�����ĸ��߽��ֵ
		_Area("Area", Vector) = (0,0,1,1)
		//----end----

}

Category {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
    Blend One One
	Cull Off Lighting Off ZWrite Off
	Offset -1, -1


	SubShader {

		Pass {

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
				float4 _MainTex_ST;
	
			half4 _TintColor;

			half _Speed;

			//��������Ӧ�����_Area
			float4 _Area;
			//----end----
	

			
			struct appdata_t {
				float4 vertex : POSITION;
				half4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				half4 color : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_FOG_COORDS(2)
					//��������¼�������������
					float2 worldPos : TEXCOORD1;
				//----end----

			};
			
		


			v2f vert (appdata_t v)
			{
				v2f o;

                o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				//���������㶥�����������
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xy;
				//----end----

				return o;
			}
			
			half4 frag (v2f i) : SV_Target
			{				
				half4 tex = tex2D(_MainTex, i.texcoord  + _Speed * _Time.xx);
				half4 tex2 = tex2D(_MainTex, i.texcoord  - _Speed * _Time.xx * 1.4 + float2(0.4, 0.6));

				tex *= tex2;								
				half4 col = 2.0f * i.color.a * _TintColor * tex;
				UNITY_APPLY_FOG(i.fogCoord, col);
				half alpha = saturate(tex.a * _TintColor.a * 2);
				//�������ж϶��������Ƿ��ڲü�����
				bool inArea = i.worldPos.x >= _Area.x && i.worldPos.x <= _Area.z && i.worldPos.y >= _Area.y && i.worldPos.y <= _Area.w;
				//----end----

				//����ڲü�����returnԭ����Ч������������
				return inArea ? half4(col.rgb *alpha, alpha) : half4(col.rgb *0, 0);
			}
			ENDCG 
		}
	}	
}
}
