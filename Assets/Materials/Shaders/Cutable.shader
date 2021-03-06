Shader "Unlit/Cutable" {
    Properties{
        _Color("Color", Color) = (1,1,1,1)
    }

    SubShader{
        Tags { "RenderType" = "Opaque" "Queue" = "Geometry+1"}
        Pass {
            Stencil {
                Ref 2
                Comp equal
                Pass keep
                ZFail decrWrap
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            struct appdata {
                float4 vertex : POSITION;
            };
            struct v2f {
                float4 pos : SV_POSITION;
            };
            v2f vert(appdata v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            float4 _Color;
            half4 frag(v2f i) : SV_Target {
                return _Color;
            }
            ENDCG
        }
    }
}