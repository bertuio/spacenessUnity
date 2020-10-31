Shader "Routine/FogShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float Lerp(float a, float b, float t)
            {
                return a + (b - a) * t;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _CameraDepthTexture;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 tex = tex2D(_MainTex, i.uv);
                float depth = tex2D(_CameraDepthTexture, i.uv);
                
                depth = max(0,pow(depth + 0.95, 10) - 0.7);
                float lum = (tex.r * 0.3 + tex.g * 0.59 + tex.b * 0.11)*depth;
                return float4(tex.rgb*depth,tex.a);
            }
            ENDCG
        }
    }
}
