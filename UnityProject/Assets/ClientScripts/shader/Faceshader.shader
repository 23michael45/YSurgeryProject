Shader "Unlit/Faceshader"
{

    Properties
    {
        _MainTex ("_MainTex", 2D) = "white" {}
		_BaseTex("_BaseTex", 2D) = "white" {}
		_Hair ("_Hair", 2D) = "white" {}
		_Foundation("_Foundation", 2D) = "white" {}
		_EyeBrow ("_EyeBrow", 2D) = "white" {}
		_Eyelash("_Eyelash", 2D) = "white" {}
		_EyeShadow ("_EyeShadow", 2D) = "white" {}		
		_Pupil ("_Pupil", 2D) = "white" {}		
		_Shadow ("_Shadow", 2D) = "white" {}
		_Lip ("_Lip", 2D) = "white" {}
		_FaceTatoo ("_FaceTatoo", 2D) = "white" {}	

		_DoubleEye("_DoubleEye", 2D) = "white" {}


		_AreaTex ("_AreaTex", 2D) = "white" {}
		_NoseHoleMask("_NoseHoleMask", 2D) = "white" {}

		_Hue("Hue",Range(0,359)) = 0
		_Saturation("Saturation", Range(-1.0,1.0)) = 0.0
		_Value("Value", Range(-1.0,1.0)) = 0.0

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
            //#pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
				float4 type : COLOR;
                //UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;

			sampler2D _Hair;

			sampler2D _Foundation;
			sampler2D _EyeBrow;
			sampler2D _EyeShadow;
			sampler2D _Eyelash;
			sampler2D _Pupil;	
			sampler2D _Lip;
			sampler2D _Shadow;
			sampler2D _FaceTatoo;					

			sampler2D _AreaTex;

			sampler2D _DoubleEye;

			sampler2D _NoseHoleMask;
			sampler2D _BaseTex;
			
            float4 _MainTex_ST;



			uniform	half _Hue;
			uniform	half _Saturation;
			uniform half _Value;



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


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);	
				o.uv2 = v.uv2;
                //UNITY_TRANSFER_FOG(o,o.vertex);

				//if (v.uv2.x == 1 && v.uv2.y == 1)
				//{
				//	//o.type = float4(1, 1, 1, 1);
				//	o.type = float4(1, 0, 0, 0);
				//}
				//else
				//{

				//	//o.type = float4(0, 0, 0, 0);
				//}
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 Main = tex2D(_MainTex, i.uv);	
				fixed4  Hair = tex2D(_Hair, i.uv);


				fixed4 Foundation = tex2D(_Foundation, i.uv);
				fixed4 EyeBrow	= tex2D(_EyeBrow , i.uv);
				fixed4 EyeShadow = tex2D(_EyeShadow, i.uv);				
				fixed4 Pupil = tex2D(_Pupil, i.uv);		
				fixed4 Lip = tex2D(_Lip, i.uv);
				fixed4 Shadow = tex2D(_Shadow, i.uv);
				fixed4 FaceTatoo = tex2D(_FaceTatoo, i.uv);
				fixed4 Eyelash = tex2D(_Eyelash, i.uv);

				fixed4 DoubleEye = tex2D(_DoubleEye, i.uv);
				

				fixed4 NoseHoleMask = tex2D(_NoseHoleMask, i.uv);
				fixed4 BaseTex = tex2D(_BaseTex, i.uv);


				fixed4 Mix_DoubleEye = lerp(Main, DoubleEye, DoubleEye.a);

				fixed4 Mix_Hair = lerp(Mix_DoubleEye, Hair, Hair.a);

				fixed4 Mix_Foundation = lerp(Mix_Hair, Foundation, Foundation.a);
				fixed4 Mix_EyeBrow = lerp(Mix_Foundation, EyeBrow, EyeBrow.a);
				fixed4 Mix_EyeShadow = lerp(Mix_EyeBrow, EyeShadow, EyeShadow.a);
				fixed4 Mix_Eyelash = lerp(Mix_EyeShadow, Eyelash, Eyelash.a);				
				fixed4 Mix_Pupil = lerp(Mix_Eyelash, Pupil, Pupil.a);
				fixed4 Mix_Lip = lerp(Mix_Pupil, Lip, Lip.a);
				fixed4 Mix_Shadow = lerp(Mix_Lip, Shadow, Shadow.a);
				fixed4 Mix_FaceTatoo = lerp(Mix_Shadow, FaceTatoo, FaceTatoo.a);

			    fixed4 Mix_all = Mix_FaceTatoo;


				fixed4 Area = tex2D(_AreaTex, i.uv);	
							   


				


				float3 colorHSV;
				colorHSV.xyz = RGBConvertToHSV(BaseTex.xyz);   //转换为HSV
				colorHSV.x += _Hue; //调整偏移Hue值
				colorHSV.x = colorHSV.x % 359;    //超过360的值从0开始

				colorHSV.y += _Saturation;  //调整饱和度
				colorHSV.z += _Value;

				BaseTex.xyz = HSVConvertToRGB(colorHSV.xyz);



				

				fixed4 Mix_final = lerp( BaseTex, Mix_all, NoseHoleMask);

				fixed4 col = lerp(Mix_final, Area, Area.a / 2);

				//fixed v = i.uv2.y;
				//fixed mv = (1 - v);
				//col = col * (1 - v) + fixed4(v, v, v, 1);
				////point in face and point is nose hole
				//if (i.uv2.y == 0.0)
				//{
				//	//col = fixed4(1, 0, 0, 1);
				//}
				//else
				//{
				//	col = col * mv;

				//}

                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
