Shader "Custom/FadeFromBottom"
{
    Properties
    {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _FadeHeight ("Fade Height", Range(0, 1)) = 0.5
        _MainTex ("Main Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _Color;
            float _FadeHeight;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float alpha = saturate((i.uv.y - _FadeHeight) / (1 - _FadeHeight)); // Calculate alpha based on the fade height

                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                col.a = 1.0 - alpha; // Invert alpha for transparency

                return col;
            }
            ENDCG
        }
    }
}
