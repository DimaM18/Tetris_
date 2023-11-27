// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// Unlit shader. Simplest possible textured shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "Gamepie/Ui/StencilMask" {
Properties {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    [HideInInspector] _Stencil ("Stencil", int) = 0
    [HideInInspector] _StencilOp ("StencilOP", int) = 0
    [HideInInspector] _StencilComp ("StencilComp", int) = 0
    [HideInInspector] _StencilReadMask ("StencilReadMask", int) = 0
    [HideInInspector] _StencilWriteMask ("StencilWriteMask", int) = 0
    [HideInInspector] _ColorMask ("ColorMask", int) = 15
}

SubShader {
    Tags { "Queue" = "Geometry-1" }  // Write to the stencil buffer before drawing any geometry to the screen
    ColorMask 0 // Don't write to any colour channels
    ZWrite Off // Don't write to the Depth buffer
    
    

    Pass {
            // Write the value 1 to the stencil buffer
    Stencil
    {
        Ref 1
        Comp Always
        Pass Replace
    }
        CGPROGRAM
        

            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata_t v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.texcoord);
                UNITY_APPLY_FOG(i.fogCoord, col);
                UNITY_OPAQUE_ALPHA(col.a);
                return col;
            }
        ENDCG
    }
}

}
