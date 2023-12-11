Shader "Custom/SwapPallits"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        _boiBirdPallet("Look Up Pallet", 2D) = "white" {}
        _NormalMap("Normal Map", 2D) = "bump" {}  // New normal map property
        _PallitCount("PallitCount", float) = 2.0
        _Color("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap("Pixel snap", Float) = 1
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Fog { Mode Off }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma glsl
            #pragma target 3.0
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile DUMMY PIXELSNAP_ON
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                float3 normal   : NORMAL; // New normal map input
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                float4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
                float3 normal   : TEXCOORD1; // Pass normal to fragment shader
            };

            fixed4 _Color;
            sampler2D _MainTex;
            sampler2D _boiBirdPallet;
            sampler2D _NormalMap; // New normal map sampler
            float _PallitCount;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color;
                OUT.normal = UnityObjectToWorldNormal(IN.normal); // Transform normal to world space
                #ifdef PIXELSNAP_ON
                OUT.vertex = UnityPixelSnap(OUT.vertex);
                #endif
                return OUT;
            }

            fixed4 frag(v2f IN) : COLOR
            {
                float4 pCoord = tex2Dlod(_MainTex, float4(IN.texcoord.xy, 0.0, 0.0));
                float palSize = 1.0 / _PallitCount;
                float palShift = IN.color.r * 255.0;
                float4 c = tex2Dlod(_boiBirdPallet, float4(pCoord.r * palSize + palSize * (palShift - 1), 0.5, 0.0, 0.0));
                
                // Normal map lighting calculation
                float3 normal = UnpackNormal(tex2D(_NormalMap, IN.texcoord.xy));
                float3 lightDir = normalize(float3(1, 1, 1)); // Adjust the light direction as needed
                float NdotL = max(0, dot(normal, lightDir));
                c.rgb *= NdotL; // Apply lighting to the color
                
                return c * _Color;
            }
            ENDCG
        }
    }
}
