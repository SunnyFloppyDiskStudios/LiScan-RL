Shader "Custom/AlwaysBehind"
{
    Properties { }
    SubShader
    {
        Tags { 
            "RenderPipeline"="UniversalPipeline" 
            "Queue"="Geometry-1"
            "RenderType"="Opaque"
            "LightMode"="UniversalForward"
        }
        LOD 100

        Cull Off
        ZWrite Off
        ZTest LEqual
        
        Pass
        {
            ColorMask RGBA
            Blend Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes { float4 positionOS : POSITION; };
            struct Varyings  { float4 positionCS : SV_POSITION; };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionCS = TransformObjectToHClip(IN.positionOS);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                return half4(0,0,0,1); // solid black
            }
            ENDHLSL
        }
    }
    Fallback Off
}
