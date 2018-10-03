// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/VoidTransparencyShader"
{
Properties {
    _Color ("Main Color (A=Opacity)", Color) = (1,1,1,1)
    _MainTex ("Base (A=Opacity)", 2D) = "white" {}
}
Category {
    Tags {"IgnoreProjector"="True" "RenderType"="Opaque"}
    ZWrite On
    Blend SrcAlpha OneMinusSrcAlpha
    SubShader {
        Pass {
            GLSLPROGRAM
 
            #ifdef VERTEX
            varying mediump vec2 uv;
            uniform mediump vec4 _MainTex_ST;
            void main() {
                gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
                uv = gl_MultiTexCoord0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
            }
            #endif
         
            #ifdef FRAGMENT
            varying mediump vec2 uv;
            uniform lowp sampler2D _MainTex;
            uniform lowp vec4 _Color;
            void main() {
                gl_FragColor = texture2D(_MainTex, uv) * _Color;
            }
            #endif    
            ENDGLSL
        }
        LOD 200
        Pass {
            ZWrite On
            ColorMask 0
        }   
    }
 
    SubShader {
        Pass {
            SetTexture[_MainTex] {Combine texture * constant ConstantColor[_Color]}
        }
    }
}
}