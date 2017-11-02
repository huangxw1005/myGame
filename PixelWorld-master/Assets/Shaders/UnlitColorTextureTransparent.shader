Shader "Craft/Unlit/ColorTextureTransparent"
{
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}

	SubShader {
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent" "IgnoreProjector"="True"}
		LOD 100

		Pass {
			ZWrite On  
            ZTest Less
			Blend SrcAlpha OneMinusSrcAlpha
			
			CGPROGRAM
			#pragma vertex vert  
			#pragma fragment frag
			#pragma target 3.0
            #include "UnityCG.cginc"
			
			sampler2D _MainTex;
			float4	_Color;
			
			struct appdata {
				float4 vertex : POSITION;
				float3 texcoord : TEXCOORD0;
			};
			struct v2f {
				float4 pos : SV_POSITION;
				float3 uv : TEXCOORD0;
			};

			v2f vert(appdata v) 
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.texcoord;
				return o;
			}

			fixed4 frag(v2f i) : COLOR
			{
				fixed4 color = tex2D(_MainTex, i.uv);
				return color * _Color;
			}
			ENDCG
		}
	}
}
