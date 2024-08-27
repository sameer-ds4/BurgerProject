Shader "Pixel Sorcerer/Scroller"
{
    Properties
    {        
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _ScrollSpeed_X ("Scroll Speed X", Range(-10, 10)) = 2.5
        _ScrollSpeed_Y ("Scroll Speed Y", Range(-10, 10)) = 2.5
        _Smoothness ("Smoothness", Range(0,1)) = 0.5
        [Enum(Opaque,0, Cutout,1, Fade,2, Transparent,3)] _RenderMode ("Render Mode", Float) = 0
        _Cutoff ("Alpha Cutoff", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        // sampler2D _MainTex;
        sampler2D _MainTex : register(s0);


        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _Color;
        fixed _ScrollSpeed_X;
        fixed _ScrollSpeed_Y;
        float _Cutoff;
        half _Smoothness;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Adjust UVs and wrap them using frac()
            fixed2 scrolledUV = IN.uv_MainTex;
            scrolledUV += fixed2(_ScrollSpeed_X * _Time.y, _ScrollSpeed_Y * _Time.y);
            scrolledUV = frac(scrolledUV);  // Wrap UVs within 0-1 range

            // Sample the texture with the adjusted UVs
            half4 c = tex2D(_MainTex, scrolledUV) * _Color;
            o.Albedo = c.rgb;
            o.Smoothness = _Smoothness; 

            // Handle Cutout mode with alpha clipping
            if (_Cutoff > 0)
            {
                clip(c.a - _Cutoff);
            }

            o.Alpha = c.a;
        }
        ENDCG
    }

    CustomEditor "ShaderWithRenderingModesGUI"
    FallBack "Diffuse"
}
