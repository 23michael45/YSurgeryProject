Shader "Unlit/Bodyshader"
{

    Properties
    {
        _MainTex ("_MainTex", 2D) = "white" {}		
		_BodyTatoo ("_BodyTatoo", 2D) = "white" {}	
		_AreaTex ("_AreaTex", 2D) = "white" {}

		_Hue("Hue",Range(0,359)) = 0
		_Saturation("Saturation", Range(-1.0,1.0)) = 0.0
		_Value("Value", Range(-1.0,1.0)) = 0.0
		
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;			
			sampler2D _BodyTatoo;
			sampler2D _AreaTex;
			
			uniform	half _Hue;
			uniform	half _Saturation;
			uniform half _Value;

            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);				
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }


			float3 RGBConvertToHSV(float3 rgb)
			{
				float R = rgb.x, G = rgb.y, B = rgb.z;
				float3 hsv;
				float max1 = max(R, max(G, B));
				float min1 = min(R, min(G, B));
				if (R == max1)
				{
					hsv.x = (G - B) / (max1 - min1);
				}
				if (G == max1)
				{
					hsv.x = 2 + (B - R) / (max1 - min1);
				}
				if (B == max1)
				{
					hsv.x = 4 + (R - G) / (max1 - min1);
				}
				hsv.x = hsv.x * 60.0;
				if (hsv.x < 0)
					hsv.x = hsv.x + 360;
				hsv.z = max1;
				hsv.y = (max1 - min1) / max1;
				return hsv;
			}
			//HSV to RGB
			float3 HSVConvertToRGB(float3 hsv)
			{
				float R, G, B;
				//float3 rgb;
				if (hsv.y == 0)
				{
					R = G = B = hsv.z;
				}
				else
				{
					hsv.x = hsv.x / 60.0;
					int i = (int)hsv.x;
					float f = hsv.x - (float)i;
					float a = hsv.z * (1 - hsv.y);
					float b = hsv.z * (1 - hsv.y * f);
					float c = hsv.z * (1 - hsv.y * (1 - f));

					if (i == 0) {
						R = hsv.z; G = c; B = a;
					}
					else if (i == 1) {
						R = b; G = hsv.z; B = a;
					}
					else if (i == 2) {
						R = a; G = hsv.z; B = c;
					}
					else if (i == 3) {
						R = a; G = b; B = hsv.z;
					}
					else if (i == 4) {
						R = c; G = a; B = hsv.z;
					}
					else {
						R = hsv.z; G = a; B = b;
					}

				}
				return float3(R, G, B);
			}


            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 Main = tex2D(_MainTex, i.uv);	

				float3 colorHSV;
				colorHSV.xyz = RGBConvertToHSV(Main.xyz);   //转换为HSV
				colorHSV.x += _Hue; //调整偏移Hue值
				colorHSV.x = colorHSV.x % 359;    //超过360的值从0开始

				colorHSV.y += _Saturation;  //调整饱和度
				colorHSV.z += _Value;
				
				Main.xyz = HSVConvertToRGB(colorHSV.xyz);
								
				fixed4 BodyTatoo = tex2D(_BodyTatoo, i.uv);

				
				fixed4 Mix_BodyTatoo = lerp(Main, BodyTatoo, BodyTatoo.a);

			    fixed4 Mix_all = Mix_BodyTatoo;
				fixed4 Area = tex2D(_AreaTex, i.uv);		

				fixed4 col = lerp(Mix_all, Area, Area.a);
				

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
