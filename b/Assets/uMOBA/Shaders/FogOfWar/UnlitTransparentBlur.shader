// Unity's Unlit/Transparent shader + blur effect
Shader "Unlit/TransparentBlur" {

Properties {
    _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
    size ("size", Range(0, 0.1)) = 0.004
}

SubShader {
    Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
    LOD 100

    ZWrite Off
    Blend SrcAlpha OneMinusSrcAlpha 

    Pass {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                half2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            uniform float size;
            
            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }
            
            fixed4 frag(v2f i) : SV_Target
            {
                float2 t = i.texcoord;
                float4 sum = float4(0.0, 0.0, 0.0, 0.0);

                // 8+8+1 tap filter with predefined gaussian weights
                // samples 8 values horiontally and 8 values vertically, not diagonally though
                // => looks better than 8 neighborhood
                // note: tex2D(tex, coord) -> second param is uv coord [0..1]
                sum += tex2D(_MainTex, float2(t.x - 4.0*size, t.y))            * 0.0162162162 / 2;
                sum += tex2D(_MainTex, float2(t.x - 3.0*size, t.y))            * 0.0540540541 / 2;
                sum += tex2D(_MainTex, float2(t.x - 2.0*size, t.y))            * 0.1216216216 / 2;
                sum += tex2D(_MainTex, float2(t.x - 1.0*size, t.y))            * 0.1945945946 / 2;
                sum += tex2D(_MainTex, float2(t.x + 1.0*size, t.y))            * 0.1945945946 / 2;
                sum += tex2D(_MainTex, float2(t.x + 2.0*size, t.y))            * 0.1216216216 / 2;
                sum += tex2D(_MainTex, float2(t.x + 3.0*size, t.y))            * 0.0540540541 / 2;
                sum += tex2D(_MainTex, float2(t.x + 4.0*size, t.y))            * 0.0162162162 / 2;
                sum += tex2D(_MainTex, float2(t.x,            t.y))            * 0.2270270270;
                sum += tex2D(_MainTex, float2(t.x,            t.y - 4.0*size)) * 0.0162162162 / 2;
                sum += tex2D(_MainTex, float2(t.x,            t.y - 3.0*size)) * 0.0540540541 / 2;
                sum += tex2D(_MainTex, float2(t.x,            t.y - 2.0*size)) * 0.1216216216 / 2;
                sum += tex2D(_MainTex, float2(t.x,            t.y - 1.0*size)) * 0.1945945946 / 2;
                sum += tex2D(_MainTex, float2(t.x,            t.y + 1.0*size)) * 0.1945945946 / 2;
                sum += tex2D(_MainTex, float2(t.x,            t.y + 2.0*size)) * 0.1216216216 / 2;
                sum += tex2D(_MainTex, float2(t.x,            t.y + 3.0*size)) * 0.0540540541 / 2;
                sum += tex2D(_MainTex, float2(t.x,            t.y + 4.0*size)) * 0.0162162162 / 2;
                return sum;
            }
        ENDCG
    }
}

}