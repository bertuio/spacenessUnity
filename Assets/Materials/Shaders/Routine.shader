Shader "Routine/Posterize"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Bends("Num of bends", Range(2,8)) = 3
        _Smoothing("Smoothing", Range(0,1)) = 0.05
        _IsGrayscale("Grayscale", Int) = 1
        _IsGrayscaleExcludeLight("ExcludeLightGrayscale", Int) = 0
    }
    SubShader
    {
        Cull off
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert fullforwardshadows finalcolor:mycolor

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        int _Bends;
        float _Smoothing;
        int _IsGrayscale;
        int _IsGrayscaleExcludeLight;

        float colorIntensity(float3 col)
        {
            return (col.r + col.g + col.b) / 3 ;
        }

        float Lerp(float a, float b, float t) 
        {
            return a + (b - a) * t;
        }

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void mycolor(Input IN, SurfaceOutput o, inout half4 col) 
        {
            float4 grayscale = max(col.r*0.78, max(col.b*0.89, col.g*0.98));
            float ceiled = ceil(grayscale * _Bends);
            float4 startFadePositionColor = (ceiled - _Smoothing) / _Bends;
            float4 startFadeColor = (ceiled-1) / _Bends;
            float4 endFadeColor = ceiled / _Bends;
            float intensity = colorIntensity(col);
            col = Lerp(startFadeColor, endFadeColor, smoothstep(startFadePositionColor, endFadeColor, grayscale)) * (_IsGrayscale*max(1-_IsGrayscaleExcludeLight,0)? half4(intensity.xxx, col.a): col);
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = (_IsGrayscale * _IsGrayscaleExcludeLight) ? colorIntensity(c).xxx :c.rgb;
            // Metallic and smoothness come from slider variables
            //o.Metallic = _Metallic;
            //o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
