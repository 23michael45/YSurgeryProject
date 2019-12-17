Shader "Unlit/Faceshader"
{

    Properties
    {
        _MainTex ("_MainTex", 2D) = "white" {}
		_Hair ("_Hair", 2D) = "white" {}
		_Foundation("_Foundation", 2D) = "white" {}
		_EyeBrow ("_EyeBrow", 2D) = "white" {}
		_EyeShadow ("_EyeShadow", 2D) = "white" {}		
		_Pupil ("_Pupil", 2D) = "white" {}		
		_Shadow ("_Shadow", 2D) = "white" {}
		_Lip ("_Lip", 2D) = "white" {}
		_FaceTatoo ("_FaceTatoo", 2D) = "white" {}	

		_AreaTex ("_AreaTex", 2D) = "white" {}
		
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
			sampler2D _Pupil;	
			sampler2D _Lip;
			sampler2D _Shadow;
			sampler2D _FaceTatoo;
			
			
			sampler2D _AreaTex;
			
            float4 _MainTex_ST;

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


				fixed4 Mix_Hair = lerp(Main, Hair, Hair.a);
				fixed4 Mix_Foundation = lerp(Mix_Hair, Foundation, Foundation.a);
				fixed4 Mix_EyeBrow = lerp(Mix_Foundation, EyeBrow, EyeBrow.a);
				fixed4 Mix_EyeShadow = lerp(Mix_EyeBrow, EyeShadow, EyeShadow.a);
				fixed4 Mix_Pupil = lerp(Mix_EyeShadow, Pupil, Pupil.a);
				fixed4 Mix_Lip = lerp(Mix_Pupil, Lip, Lip.a);
				fixed4 Mix_Shadow = lerp(Mix_Lip, Shadow, Shadow.a);
				fixed4 Mix_FaceTatoo = lerp(Mix_Shadow, FaceTatoo, FaceTatoo.a);

			    fixed4 Mix_all = Mix_FaceTatoo;
				fixed4 Area = tex2D(_AreaTex, i.uv);		

				fixed4 col = lerp(Mix_all, Area, Area.a/3);
				
				fixed v = i.uv2.y;
				fixed mv = (1 - v);
				col = col * (1 - v) + fixed4(v, v, v, 1);
				//point in face and point is nose hole
				if (i.uv2.y == 0.0)
				{
					//col = fixed4(1, 0, 0, 1);
				}
				else
				{
					col = col * mv;

				}

                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
