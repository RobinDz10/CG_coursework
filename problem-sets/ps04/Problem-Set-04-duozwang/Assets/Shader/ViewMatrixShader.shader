Shader "Unlit/ViewMatrixShader"
{
    Properties { 
        Col ("color", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        cull off

        // Tags { "RenderType"="Opaque" }
        // LOD 100
    
        
        Tags { "RenderType" = "Opaque" "Queue" = "Transparent" }
        
        Pass
        {
            CGPROGRAM

            // this script will provide both a Vertex and a Fragment shader:
            #pragma vertex vert
            #pragma fragment frag

            // include Unity built-in shader helper functions:
            #include "UnityCG.cginc"

            // note: in GLSL the following struct of variables
            //     would have the "attribute" qualifier,
            //     as received by the vertex shader
            //     and unique for each vertex shader instance:
            struct appdata {
                float4 vertex : POSITION;
            };

            // note: in GLSL the following struct of variables
            //     would have the "varying" qualifier,
            //     as passed from the vertex shader
            //     to the fragment shader:
            struct v2f {
                float4 vertex : SV_POSITION;
            };
            
            float4x4 viewMatrix;
            float4x4 modelMatrix;
            float4 Col;


            v2f vert (appdata v) {
                v2f o;
                o.vertex = mul(viewMatrix, mul(modelMatrix, v.vertex));
                o.vertex = UnityObjectToClipPos(o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                return Col;
            }

            ENDCG
        }
    }
}
