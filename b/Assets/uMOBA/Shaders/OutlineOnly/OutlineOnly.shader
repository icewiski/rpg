// Only draws the outline. Add this as a second material to the mesh if the main
// mesh should be drawn too.
//
// Note: only drawing the outline is useful for cases where we really do want to
// use Unity's standard shader as main shader to be effected by water, shadows,
// etc.
//
// Note: Unity sometimes removes the second material for static objects when
// combining meshes, so don't make them static.
Shader "Custom/OutlineOnly" {

Properties {
    _OutlineColor ("Outline Color", Color) = (0,0,0,1)
    _Outline ("Outline width", Range (0.0, 0.03)) = .003
}
 
SubShader {
    Tags { "Queue" = "Transparent" }
    Blend SrcAlpha OneMinusSrcAlpha // strong color and supports alpha
    //Blend One OneMinusDstColor // softer color, doesn't support alpha
 
    // base
    Pass {
        Cull Back
        Blend Zero One

        // put outline behind the main mesh to not outline the insides
        Offset -1000, -1000

        SetTexture [_OutlineColor] {
            ConstantColor (0,0,0,0)
            Combine constant
        }
    }
 
    // outline
    Pass {
        Cull Off // Front also gives an interesting result
        ZWrite Off
        
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"
         
        struct appdata {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
        };
         
        struct v2f {
            float4 vertex : POSITION;
            float4 color : COLOR;
        };

        uniform float _Outline;
        uniform float4 _OutlineColor;
         
        // the vertex shader scales the vertices depending on the normal, so 
        // that the edges are wider
        v2f vert(appdata v) {
            v2f o;
            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
         
            // the following code extends the vertices
            float3 norm   = mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal);
            float2 offset = TransformViewToProjection(norm.xy);
            o.vertex.xy += offset * o.vertex.z * _Outline;
            
            o.color = _OutlineColor;
            return o;
        }
         
        half4 frag(v2f i) :COLOR {
            return i.color;
        }
        ENDCG
    }
}

}