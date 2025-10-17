Shader "Custom/AlwaysBehind"
{
    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalRenderPipeline"
            "Queue" = "Geometry-1"
        }

        Pass
        {
            Name "AlwaysBehindPass"
            Cull Off
            ZWrite Off
            ZTest LEqual
            ColorMask RGBA
            Blend Off

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
            };

            Varyings Vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionCS = TransformObjectToHClip(IN.positionOS);
                return OUT;
            }

            half4 Frag(Varyings IN) : SV_Target
            {
                return half4(0, 0, 0, 1);
            }
            ENDHLSL
        }
    }
}
