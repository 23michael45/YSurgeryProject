Shader "Unlit/UnlitRegionUV"
{
    Properties
    {
        _RawTex ("Raw Texture", 2D) = "white" {}
        _UserTex ("User Texture", 2D) = "white" {}
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
                float4 vertex : SV_POSITION;
            };

            sampler2D _RawTex;            
            sampler2D _UserTex;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.uv2 = v.uv2;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                 fixed4 col;
                if(i.uv2.x == 0 && i.uv2.y ==0)
                {
                    col = tex2D(_RawTex, i.uv);
                }  
                else if(i.uv2.x == 1 && i.uv2.y == 1)
                {
                    col = tex2D(_UserTex, i.uv);
                }
                else{
                    col = (tex2D(_UserTex, i.uv) + tex2D(_RawTex, i.uv)) /2;
                }
                
                //    col = tex2D(_UserTex, i.uv);
                // col = tex2D(_RawTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
