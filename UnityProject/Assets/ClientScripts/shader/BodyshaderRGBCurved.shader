Shader "Unlit/BodyshaderRGBCurved"
{

    Properties
    {
        _MainTex ("_MainTex", 2D) = "white" {}		
		_BodyTatoo ("_BodyTatoo", 2D) = "white" {}	
		_AreaTex ("_AreaTex", 2D) = "white" {}

		_R("R",Range(-1.0,1.0)) = 0
		_G("G", Range(-1.0,1.0)) = 0.0
		_B("B", Range(-1.0,1.0)) = 0.0
		
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 1000

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;			
			sampler2D _BodyTatoo;
			sampler2D _AreaTex;
			
			uniform	half _R;
			uniform	half _G;
			uniform half _B;

            float4 _MainTex_ST;

			float curve(float t)
			{
				float p1x = 0;
				float p1y = 0;
				float tp1 = 3.95;
				float p2x = 1;
				float p2y = 0;
				float tp2 = -3.95;


				float a = (p1x * tp1 + p1x * tp2 - p2x * tp1 - p2x * tp2 - 2 * p1y + 2 * p2y) / (p1x * p1x * p1x - p2x * p2x * p2x + 3 * p1x * p2x * p2x - 3 * p1x * p1x * p2x);
				float b = ((-p1x * p1x * tp1 - 2 * p1x * p1x * tp2 + 2 * p2x * p2x * tp1 + p2x * p2x * tp2 - p1x * p2x * tp1 + p1x * p2x * tp2 + 3 * p1x * p1y - 3 * p1x * p2y + 3 * p1y * p2x - 3 * p2x * p2y) / (p1x * p1x * p1x - p2x * p2x * p2x + 3 * p1x * p2x * p2x - 3 * p1x * p1x * p2x));
				float c = ((p1x * p1x * p1x * tp2 - p2x * p2x * p2x * tp1 - p1x * p2x * p2x * tp1 - 2 * p1x * p2x * p2x * tp2 + 2 * p1x * p1x * p2x * tp1 + p1x * p1x * p2x * tp2 - 6 * p1x * p1y * p2x + 6 * p1x * p2x * p2y) / (p1x * p1x * p1x - p2x * p2x * p2x + 3 * p1x * p2x * p2x - 3 * p1x * p1x * p2x));
				float d = ((p1x * p2x * p2x * p2x * tp1 - p1x * p1x * p2x * p2x * tp1 + p1x * p1x * p2x * p2x * tp2 - p1x * p1x * p1x * p2x * tp2 - p1y * p2x * p2x * p2x + p1x * p1x * p1x * p2y + 3 * p1x * p1y * p2x * p2x - 3 * p1x * p1x * p2x * p2y) / (p1x * p1x * p1x - p2x * p2x * p2x + 3 * p1x * p2x * p2x - 3 * p1x * p1x * p2x));

				return a * t * t * t + b * t * t + c * t + d;
			}


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);				
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }




            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 color = tex2D(_MainTex, i.uv);	

				color.x += _R * curve(color.x);
				color.y += _G * curve(color.y);
				color.z += _B * curve(color.z);
				
								
				fixed4 BodyTatoo = tex2D(_BodyTatoo, i.uv);

				
				fixed4 Mix_BodyTatoo = lerp(color, BodyTatoo, BodyTatoo.a);

			    fixed4 Mix_all = Mix_BodyTatoo;
				fixed4 Area = tex2D(_AreaTex, i.uv);		

				fixed4 col = lerp(Mix_all, Area, Area.a);
				
                return col;
            }
            ENDCG
        }
    }
}
