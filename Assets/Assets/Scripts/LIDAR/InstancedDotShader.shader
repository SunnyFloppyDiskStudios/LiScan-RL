Shader "Custom/InstancedDotShader"
{
    Properties {
        _BaseColor ("Color", Color) = (1,1,1,1)
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        Pass {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes {
                float4 positionOS : POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings {
                float4 positionHCS : SV_POSITION;
                float4 color : COLOR;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            UNITY_INSTANCING_BUFFER_START(Props)
                UNITY_DEFINE_INSTANCED_PROP(float4, _BaseColor)
            UNITY_INSTANCING_BUFFER_END(Props)

            Varyings vert(Attributes IN) {
                Varyings OUT;
                UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_TRANSFER_INSTANCE_ID(IN, OUT);

                float4 worldPosition = mul(UNITY_MATRIX_M, IN.positionOS);
                OUT.positionHCS = TransformWorldToHClip(worldPosition);
                OUT.color = UNITY_ACCESS_INSTANCED_PROP(Props, _BaseColor);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target {
                return IN.color;
            }
            ENDHLSL
        }
    }
}
