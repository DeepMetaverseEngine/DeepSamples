Shader "iPhone/Back" {
	Properties {
        _NotVisibleColor ("NotVisibleColor (RGB)", Color) = (0,0.16,0.16,0)
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader {
        Tags { "Queue" = "Geometry+500" "RenderType"="Opaque"}
        LOD 200

        Pass {
            ZTest Greater
			Cull OFF
            Lighting Off
            ZWrite Off
			Blend One OneMinusSrcAlpha

			
			Color [_NotVisibleColor]
			SetTexture [_MainTex] { combine texture * primary }
        }

        Pass {
            ZTest LEqual
			Cull OFF
            Material {
                Diffuse (1,1,1,1)
                Ambient (1,1,1,1)
            }
            Lighting On
            SetTexture [_MainTex] { combine texture } 
        }

    } 
    FallBack "Diffuse"
}
