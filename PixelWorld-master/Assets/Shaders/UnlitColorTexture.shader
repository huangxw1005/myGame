Shader "Craft/Unlit/ColorTexture"
{
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
	}

	SubShader {
		Tags {  "RenderType"="Opaque" "Queue" = "Transparent" "IgnoreProjector"="True"}
		LOD 100
		
		// rim
		Pass {
			Blend SrcAlpha One 
            ZTest Greater
            Lighting Off
            ZWrite Off
			
            CGPROGRAM  
            #pragma vertex vert  
            #pragma fragment frag  
            #include "UnityCG.cginc"  
              
			float4 _RimColor;

            struct appdata {  
                float4 vertex : POSITION;  
                float2 texcoord : TEXCOORD0;
                float4 normal:NORMAL;  
            };
            struct v2f {  
                float4 pos : SV_POSITION;  
                float4 color:COLOR;  
            };  
			
            v2f vert (appdata v)  
            {  
                v2f o;  
                o.pos = mul(UNITY_MATRIX_MVP,v.vertex);  
                float3 viewDir = normalize(ObjSpaceViewDir(v.vertex));  
                float rim = 1 - saturate(dot(viewDir,v.normal));  
                o.color = _RimColor*pow(rim,1);
                return o;  
            }
            float4 frag (v2f i) : COLOR  
            {  
                return i.color;   
            }  
            ENDCG  
        }
		
		Pass {
			ZWrite On  
            ZTest LEqual
			
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
				float2 uv : TEXCOORD0;
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
