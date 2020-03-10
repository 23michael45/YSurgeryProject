Shader "Unlit/Faceshader"
{

    Properties
    {
        _MainTex ("_MainTex", 2D) = "white" {}
		_BaseTex("_BaseTex", 2D) = "white" {}
		_Hair ("_Hair", 2D) = "white" {}

		_Foundation("_Foundation", 2D) = "white" {}
		_Foundation_H("Foundation_H",Range(0,359)) = 0
		_Foundation_S("Foundation_S", Range(0,1.0)) = 0.25
		_Foundation_V("Foundation_S", Range(-1.0,1.0)) = 0

		_EyeBrow ("_EyeBrow", 2D) = "white" {}
		_EyeBrow_H("_EyeBrow_H",Range(0,359)) = 0
		_EyeBrow_S("_EyeBrow_S", Range(0,1.0)) = 0.1
		_EyeBrow_V("Foundation_S", Range(-1.0,1.0)) = 0

		_Eyelash("_Eyelash", 2D) = "white" {}
		_Eyelash_H("_Eyelash_H",Range(0,359)) = 0
		_Eyelash_S("_Eyelash_S", Range(0,1.0)) = 0.1
		_Eyelash_V("Foundation_S", Range(-1.0,1.0)) = 0

		_EyeShadow ("_EyeShadow", 2D) = "white" {}	
		_EyeShadow_H("_EyeShadow_H",Range(0,359)) = 0
		_EyeShadow_S("_EyeShadow_S", Range(0,1.0)) = 0.5
		_EyeShadow_V("Foundation_S", Range(-1.0,1.0)) = 0

		_Pupil ("_Pupil", 2D) = "white" {}	
		_Pupil_H("_Pupil_H",Range(0,359)) = 0
		_Pupil_S("_Pupil_S", Range(0,1.0)) = 0.5
		_Pupil_V("Foundation_S", Range(-1.0,1.0)) = 0

		_Shadow ("_Shadow", 2D) = "white" {}
		_Shadow_H("_Shadow_H",Range(0,359)) = 0
		_Shadow_S("_Shadow_S", Range(0,1.0)) = 0.5
		_Shadow_V("Foundation_S", Range(-1.0,1.0)) = 0

		_Lip ("_Lip", 2D) = "white" {}
		_Lip_H("_Lip_H",Range(0,359)) = 0
		_Lip_S("_Lip_S", Range(0,1.0)) = 0.6
		_Lip_V("Foundation_S", Range(-1.0,1.0)) = 0

		_FaceTatoo ("_FaceTatoo", 2D) = "white" {}	
		_FaceTatoo_H("_FaceTatoo_H",Range(0,359)) = 0
		_FaceTatoo_S("_FaceTatoo_S", Range(0,1.0)) = 0.5
		_FaceTatoo_V("Foundation_S", Range(-1.0,1.0)) = 0

		_DoubleEye("_DoubleEye", 2D) = "white" {}

		_AreaTex ("_AreaTex", 2D) = "white" {}
		_NoseHoleMask("_NoseHoleMask", 2D) = "white" {}


		_R("R",Range(-1.0,1.0)) = 0.0
		_G("G", Range(-1.0,1.0)) = 0.0
		_B("B", Range(-1.0,1.0)) = 0.0
					   

		/*_H("H",Range(0,359)) = 0
		_S("S", Range(-1.0,1.0)) = 0.0
		_V("V", Range(-1.0,1.0)) = 0.0*/

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 1000

        Pass
        {
            CGPROGRAM
			#include "Assets/ClientScripts/shader/Aub_Blends.cginc"
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
			uniform	half _Foundation_H;
			uniform	half _Foundation_S;			
			uniform	half _Foundation_V;

			sampler2D _EyeBrow;
			uniform	half _EyeBrow_H	;
			uniform	half _EyeBrow_S	;
			uniform	half _EyeBrow_V;

			sampler2D _EyeShadow;
			uniform	half _EyeShadow_H  ;
			uniform	half _EyeShadow_S  ;
			uniform	half _EyeShadow_V;

			sampler2D _Eyelash;
			uniform	half _Eyelash_H;
			uniform	half _Eyelash_S;
			uniform	half _Eyelash_V;

			sampler2D _Pupil;	
			uniform	half _Pupil_H;
			uniform	half _Pupil_S;
			uniform	half _Pupil_V;

			sampler2D _Lip;
			uniform	half _Lip_H;
			uniform	half _Lip_S;
			uniform	half _Lip_V;
						

			sampler2D _Shadow;
			uniform	half _Shadow_H;
			uniform	half _Shadow_S;
			uniform	half _Shadow_V;

			sampler2D _FaceTatoo;					
			uniform	half _FaceTatoo_H;
			uniform	half _FaceTatoo_S;
			uniform	half _FaceTatoo_V;

			sampler2D _AreaTex;

			sampler2D _DoubleEye;

			sampler2D _NoseHoleMask;
			sampler2D _BaseTex;
			
            float4 _MainTex_ST;





			uniform	half _R;
			uniform	half _G;
			uniform half _B;

			//uniform	half _Hue;
			//uniform	half _Saturation;
			//uniform half _Value;



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

			fixed4 ColorBlend(fixed4 base, uniform half h, uniform half s, uniform half v) {

				float3 baseHSV;
				baseHSV.xyz = RGBConvertToHSV(base.xyz);   //转换为HSV
				baseHSV.x = h; //调整偏移Hue值
				baseHSV.y = s;    //超过360的值从0开始	
				baseHSV.z += v;

				base.xyz = HSVConvertToRGB(baseHSV.xyz);
				return base;
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
				


				fixed4 Mix_DoubleEye = lerp(Main, DoubleEye, DoubleEye.a);

				fixed4 Mix_Hair = lerp(Mix_DoubleEye, Hair, Hair.a);

				//fixed4 Foundation_Blend = Screen(Mix_Hair, Foundation);

				//Foundation hsv
				//float3 FoundationHSV;
				//FoundationHSV.xyz = RGBConvertToHSV(Foundation.xyz);   //转换为HSV
				//FoundationHSV.x = _Foundation_H; //调整偏移Hue值
				//FoundationHSV.y = _Foundation_S;    //超过360的值从0开始	
				//Foundation.xyz = HSVConvertToRGB(FoundationHSV.xyz);
				
				Foundation = ColorBlend(Foundation, _Foundation_H, _Foundation_S, _Foundation_V);
				fixed4 Mix_Foundation = lerp(Mix_Hair, Foundation, Foundation.a);

				EyeBrow = ColorBlend(EyeBrow, _EyeBrow_H, _EyeBrow_S, _EyeBrow_V);
				fixed4 EyeBrow_Blend = Multiply(EyeBrow, Mix_Foundation);
				fixed4 Mix_EyeBrow = lerp(Mix_Foundation, EyeBrow_Blend, EyeBrow.a);

				EyeShadow = ColorBlend(EyeShadow, _EyeShadow_H, _EyeShadow_S, _EyeShadow_V);
				fixed4 Mix_EyeShadow = lerp(Mix_EyeBrow, EyeShadow, EyeShadow.a);

				Eyelash = ColorBlend(Eyelash, _Eyelash_H, _Eyelash_S, _Eyelash_V);
				fixed4 Mix_Eyelash = lerp(Mix_EyeShadow, Eyelash, Eyelash.a);	

				Pupil = ColorBlend(Pupil, _Pupil_H, _Pupil_S, _Pupil_V);
				fixed4 Mix_Pupil = lerp(Mix_Eyelash, Pupil, Pupil.a);

				
				Lip = ColorBlend(Lip, _Lip_H, _Lip_S, _Lip_V);
				fixed4 Mix_Lip = lerp(Mix_Pupil, Lip, Lip.a);

				Shadow = ColorBlend(Shadow, _Shadow_H, _Shadow_S, _Shadow_V);
				fixed4 Shadow_Blend = Multiply(Shadow, Mix_Lip);
				fixed4 Mix_Shadow = lerp(Mix_Lip, Shadow_Blend, Shadow.a);

				FaceTatoo = ColorBlend(FaceTatoo, _FaceTatoo_H, _FaceTatoo_S, _FaceTatoo_V);
				fixed4 Mix_FaceTatoo = lerp(Mix_Shadow, FaceTatoo, FaceTatoo.a);

			    fixed4 Mix_all = Mix_FaceTatoo;


				fixed4 Area = tex2D(_AreaTex, i.uv);	
							   
							   				

				


				fixed4 BaseTex = tex2D(_BaseTex, i.uv);

				BaseTex.x += _R * curve(BaseTex.x);
				BaseTex.y += _G * curve(BaseTex.y);
				BaseTex.z += _B * curve(BaseTex.z);
				

				fixed4 Mix_final = lerp( BaseTex, Mix_all, NoseHoleMask);

				fixed4 col = lerp(Mix_final, Area, Area.a / 2);
				//fixed4 col = EyeBrow_Blend;


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
