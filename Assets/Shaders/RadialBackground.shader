Shader "Custom/RadialBackground"
{
    Properties
    {
        _Color ("Center Color", Color) = (0.2, 0.4, 1, 1)
        _Radius ("Radius", Range(0, 1)) = 0.5
        _Softness ("Edge Softness", Range(0.01, 1)) = 0.3
        _PulseStrength ("Pulse Strength", Range(0, 1)) = 0.0
        _PulseSpeed ("Pulse Speed", Range(0, 10)) = 2.0
    }
    SubShader
    {
        Tags { "Queue"="Background" "RenderType"="Opaque" }
        ZWrite Off
        Cull Off
        Lighting Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            fixed4 _Color;
            float _Radius;
            float _Softness;
            float _PulseStrength;
            float _PulseSpeed;

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 centeredUV = i.uv - 0.5;
                float dist = length(centeredUV);
                float pulse = sin(_Time.y * _PulseSpeed) * _PulseStrength;
                float radius = _Radius + pulse;

                float gradient = smoothstep(radius, radius - _Softness, dist);
                fixed4 col = _Color * gradient;

                return col;
            }
            ENDCG
        }
    }
}
