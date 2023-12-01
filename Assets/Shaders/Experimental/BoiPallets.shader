Shader "CustomURP/BoiPallets"
{
    Properties
    {
        _MainTex("Diffuse", 2D) = "white" {}
        _MaskTex("Mask", 2D) = "white" {}
        _NormalMap("Normal Map", 2D) = "bump" {}

        _boiBirdPallet("Look Up Pallet", 2D) = "white" {}
        _PallitCount("PallitCount", float) = 2.0

        // Legacy properties. They're here so that materials using this shader can gracefully fallback to the legacy sprite shader.
        [HideInInspector] _Color("Tint", Color) = (1,1,1,1)
        [HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
        [HideInInspector] _AlphaTex("External Alpha", 2D) = "white" {}
        [HideInInspector] _EnableExternalAlpha("Enable External Alpha", Float) = 0
    }

        SubShader
        {
            Tags {"Queue" = "Transparent" "RenderType" = "Transparent" "RenderPipeline" = "UniversalPipeline" }

            Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            //------------------ Light & Color Calculation
            Pass
            {
                Tags { "LightMode" = "Universal2D" }

                HLSLPROGRAM
                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

                #pragma vertex CombinedShapeLightVertex
                #pragma fragment CombinedShapeLightFragment

                #pragma multi_compile USE_SHAPE_LIGHT_TYPE_0 __
                #pragma multi_compile USE_SHAPE_LIGHT_TYPE_1 __
                #pragma multi_compile USE_SHAPE_LIGHT_TYPE_2 __
                #pragma multi_compile USE_SHAPE_LIGHT_TYPE_3 __
                #pragma multi_compile _ DEBUG_DISPLAY

                struct Attributes
                {
                    float3 positionOS   : POSITION;
                    float4 color        : COLOR;
                    float2  uv          : TEXCOORD0;
                    UNITY_VERTEX_INPUT_INSTANCE_ID
                };

                struct Varyings
                {
                    float4  positionCS  : SV_POSITION;
                    half4   color       : COLOR;
                    float2  uv          : TEXCOORD0;
                    half2   lightingUV  : TEXCOORD1;
                    #if defined(DEBUG_DISPLAY)
                    float3  positionWS  : TEXCOORD2;
                    #endif
                    UNITY_VERTEX_OUTPUT_STEREO
                };

                #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/LightingUtility.hlsl"

                TEXTURE2D(_MainTex);
                SAMPLER(sampler_MainTex);
                TEXTURE2D(_MaskTex);
                SAMPLER(sampler_MaskTex);
                half4 _MainTex_ST;

                #if USE_SHAPE_LIGHT_TYPE_0
                SHAPE_LIGHT(0)
                #endif

                #if USE_SHAPE_LIGHT_TYPE_1
                SHAPE_LIGHT(1)
                #endif

                #if USE_SHAPE_LIGHT_TYPE_2
                SHAPE_LIGHT(2)
                #endif

                #if USE_SHAPE_LIGHT_TYPE_3
                SHAPE_LIGHT(3)
                #endif

                Varyings CombinedShapeLightVertex(Attributes v)  //Vert OUT
                {
                    Varyings o = (Varyings)0;
                    UNITY_SETUP_INSTANCE_ID(v);
                    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                    o.positionCS = TransformObjectToHClip(v.positionOS);
                    #if defined(DEBUG_DISPLAY)
                    o.positionWS = TransformObjectToWorld(v.positionOS);
                    #endif
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    o.lightingUV = half2(ComputeScreenPos(o.positionCS / o.positionCS.w).xy);

                    o.color = v.color;
                    return o;
                }
                
                #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/CombinedShapeLightShared.hlsl"

                half4 CombinedShapeLightFragment(Varyings i) : SV_Target  //Frag IN
                {
                    const half4 main = i.color * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                    const half4 mask = SAMPLE_TEXTURE2D(_MaskTex, sampler_MaskTex, i.uv);
                    SurfaceData2D surfaceData;
                    InputData2D inputData;

                    InitializeSurfaceData(main.rgb, main.a, mask, surfaceData);
                    InitializeInputData(i.uv, i.lightingUV, inputData);

                    ////----------------------------
                    //float4 pCoord = tex2Dlod(_MainTex, float4(texcoord.xy, 0.0, 0.0));
                    //float palSize = 1.0 / _PallitCount;   //determines how long a pallet is based on how many pallets there are based on length -Not hight :(
                    //float palShift = color.r * 255.0;
                    ////float4 c = tex2Dlod(_boiBirdPallet, float4(pCoord.r * palSize + palSize * (palShift - 1)), float4(pCoor.g -_PallitCount), 0.0, 0.0);  //didn't work
                    ////float4 c = tex2Dlod(_boiBirdPallet, float4(pCoord.r + palShift - 1), (pCoord.y * palSize + palSize * (palShift - 1)), 0.0, 0.0));  //didn't work
                    //float4 c = tex2Dlod(_boiBirdPallet, float4(pCoord.r * palSize + palSize * (palShift - 1), 0.5, 0.0, 0.0));  //changed our LUT to MAKE IT work
                    //return c * _Color;                 //(r=x coord g=y coord * pallet size) + (pallet size * shift-1) //Trying to understand it's process
                    ////-----------------------------

                    return CombinedShapeLightShared(surfaceData, inputData);
                }

                    ////----------------------------
                    //float4 pCoord = tex2Dlod(_MainTex, float4(texcoord.xy, 0.0, 0.0));
                    //float palSize = 1.0 / _PallitCount;   //determines how long a pallet is based on how many pallets there are based on length -Not hight :(
                    //float palShift = color.r * 255.0;
                    ////float4 c = tex2Dlod(_boiBirdPallet, float4(pCoord.r * palSize + palSize * (palShift - 1)), float4(pCoor.g -_PallitCount), 0.0, 0.0);  //didn't work
                    ////float4 c = tex2Dlod(_boiBirdPallet, float4(pCoord.r + palShift - 1), (pCoord.y * palSize + palSize * (palShift - 1)), 0.0, 0.0));  //didn't work
                    //float4 c = tex2Dlod(_boiBirdPallet, float4(pCoord.r * palSize + palSize * (palShift - 1), 0.5, 0.0, 0.0));  //changed our LUT to MAKE IT work
                    //return c * _Color;                 //(r=x coord g=y coord * pallet size) + (pallet size * shift-1) //Trying to understand it's process
                    ////-----------------------------

                ENDHLSL
            }
                //------------------Normal Calculation
            Pass
            {
                Tags { "LightMode" = "NormalsRendering"}

                HLSLPROGRAM
                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

                #pragma vertex NormalsRenderingVertex
                #pragma fragment NormalsRenderingFragment

                struct Attributes
                {
                    float3 positionOS   : POSITION;
                    float4 color        : COLOR;
                    float2 uv           : TEXCOORD0;
                    float4 tangent      : TANGENT;
                    UNITY_VERTEX_INPUT_INSTANCE_ID
                };

                struct Varyings
                {
                    float4  positionCS      : SV_POSITION;
                    half4   color           : COLOR;
                    float2  uv              : TEXCOORD0;
                    half3   normalWS        : TEXCOORD1;
                    half3   tangentWS       : TEXCOORD2;
                    half3   bitangentWS     : TEXCOORD3;
                    UNITY_VERTEX_OUTPUT_STEREO
                };

                TEXTURE2D(_MainTex);
                SAMPLER(sampler_MainTex);
                TEXTURE2D(_NormalMap);
                SAMPLER(sampler_NormalMap);
                half4 _NormalMap_ST;  // Is this the right way to do this?

                Varyings NormalsRenderingVertex(Attributes attributes)
                {
                    Varyings o = (Varyings)0;
                    UNITY_SETUP_INSTANCE_ID(attributes);
                    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                    o.positionCS = TransformObjectToHClip(attributes.positionOS);
                    o.uv = TRANSFORM_TEX(attributes.uv, _NormalMap);
                    o.color = attributes.color;
                    o.normalWS = -GetViewForwardDir();
                    o.tangentWS = TransformObjectToWorldDir(attributes.tangent.xyz);
                    o.bitangentWS = cross(o.normalWS, o.tangentWS) * attributes.tangent.w;
                    return o;
                }

                #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/NormalsRenderingShared.hlsl"

                half4 NormalsRenderingFragment(Varyings i) : SV_Target
                {
                    const half4 mainTex = i.color * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                    const half3 normalTS = UnpackNormal(SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, i.uv));

                    return NormalsRenderingShared(mainTex, normalTS, i.tangentWS.xyz, i.bitangentWS.xyz, i.normalWS.xyz);
                }
                ENDHLSL
            }

            Pass
            {
                Tags { "LightMode" = "UniversalForward" "Queue" = "Transparent" "RenderType" = "Transparent"}

                HLSLPROGRAM
                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

                #pragma vertex UnlitVertex
                #pragma fragment UnlitFragment

                struct Attributes
                {
                    float3 positionOS   : POSITION;
                    float4 color        : COLOR;
                    float2 uv           : TEXCOORD0;
                    UNITY_VERTEX_INPUT_INSTANCE_ID
                };

                struct Varyings
                {
                    float4  positionCS      : SV_POSITION;
                    float4  color           : COLOR;
                    float2  uv              : TEXCOORD0;
                    #if defined(DEBUG_DISPLAY)
                    float3  positionWS  : TEXCOORD2;
                    #endif
                    UNITY_VERTEX_OUTPUT_STEREO
                };

                TEXTURE2D(_MainTex);
                SAMPLER(sampler_MainTex);
                float4 _MainTex_ST;

                Varyings UnlitVertex(Attributes attributes)
                {
                    Varyings o = (Varyings)0;
                    UNITY_SETUP_INSTANCE_ID(attributes);
                    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                    o.positionCS = TransformObjectToHClip(attributes.positionOS);
                    #if defined(DEBUG_DISPLAY)
                    o.positionWS = TransformObjectToWorld(v.positionOS);
                    #endif
                    o.uv = TRANSFORM_TEX(attributes.uv, _MainTex);
                    o.color = attributes.color;
                    return o;
                }

                

                float4 UnlitFragment(Varyings i) : SV_Target
                {
                   
                    float4 mainTex = i.color * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);

                    

                    #if defined(DEBUG_DISPLAY)
                    SurfaceData2D surfaceData;
                    InputData2D inputData;
                    half4 debugColor = 0;

                    ////----------------------------
                    //float4 pCoord = tex2Dlod(_MainTex, float4(texcoord.xy, 0.0, 0.0));
                    //float palSize = 1.0 / _PallitCount;   //determines how long a pallet is based on how many pallets there are based on length -Not hight :(
                    //float palShift = color.r * 255.0;
                    ////float4 c = tex2Dlod(_boiBirdPallet, float4(pCoord.r * palSize + palSize * (palShift - 1)), float4(pCoor.g -_PallitCount), 0.0, 0.0);  //didn't work
                    ////float4 c = tex2Dlod(_boiBirdPallet, float4(pCoord.r + palShift - 1), (pCoord.y * palSize + palSize * (palShift - 1)), 0.0, 0.0));  //didn't work
                    //float4 c = tex2Dlod(_boiBirdPallet, float4(pCoord.r * palSize + palSize * (palShift - 1), 0.5, 0.0, 0.0));  //changed our LUT to MAKE IT work
                    //return c * _Color;                 //(r=x coord g=y coord * pallet size) + (pallet size * shift-1) //Trying to understand it's process
                    ////-----------------------------

                    InitializeSurfaceData(mainTex.rgb, mainTex.a, surfaceData);
                    InitializeInputData(i.uv, inputData);
                    SETUP_DEBUG_DATA_2D(inputData, i.positionWS);

                    

                    if (CanDebugOverrideOutputColor(surfaceData, inputData, debugColor))
                    {
                        return debugColor;
                    }
                    #endif

                    return mainTex;
                }
                ENDHLSL
            }
                //Pass
                //{
                //HLSLPROGRAM
                //#pragma glsl
                ////#pragma surface surf Standard fullforwardshadows alpha  //Thought this might fix the alpha :(
                //#pragma target 3.0
                //#pragma vertex vert 
                //#pragma fragment frag 
                //#pragma multi_compile DUMMY PIXELSNAP_ON
                //#include "UnityCG.cginc"

                //struct appdata_t  //Manages Input Data
                //{
                //    float4 vertex   : POSITION;
                //    float4 color    : COLOR;
                //    float2 texcoord : TEXCOORD0;
                //};

                //struct v2f        //Returned by the vertex Shader
                //{
                //    float4 vertex   : SV_POSITION;
                //    //fixed4 color    : COLOR;
                //    float4 color : COLOR;  //No different?
                //    //half2 texcoord  : TEXCOORD0;
                //    float2 texcoord  : TEXCOORD0;  //Also no different?
                //};

                //fixed4 _Color;

                //v2f vert(appdata_t IN)  //function that will take care of vertex shading. The function will return a struct v2f and accept an appdata
                //{
                //    v2f OUT;
                //    OUT.vertex = UnityObjectToClipPos(IN.vertex);
                //    OUT.texcoord = IN.texcoord;
                //    OUT.color = IN.color;
                //    #ifdef PIXELSNAP_ON
                //    OUT.vertex = UnityPixelSnap(OUT.vertex);
                //    #endif

                //    return OUT;
                //}

                //sampler2D _MainTex;
                //sampler2D _boiBirdPallet;
                //float _PallitCount;

                //fixed4 frag(v2f IN) : COLOR
                //{
                //    float4 pCoord = tex2Dlod(_MainTex, float4(IN.texcoord.xy, 0.0, 0.0));
                //    float palSize = 1.0 / _PallitCount;   //determines how long a pallet is based on how many pallets there are based on length -Not hight :(
                //    float palShift = IN.color.r * 255.0;
                //    //float4 c = tex2Dlod(_boiBirdPallet, float4(pCoord.r * palSize + palSize * (palShift - 1)), float4(pCoor.g -_PallitCount), 0.0, 0.0);  //didn't work
                //    //float4 c = tex2Dlod(_boiBirdPallet, float4(pCoord.r + palShift - 1), (pCoord.y * palSize + palSize * (palShift - 1)), 0.0, 0.0));  //didn't work
                //    float4 c = tex2Dlod(_boiBirdPallet, float4(pCoord.r * palSize + palSize * (palShift - 1), 0.5, 0.0, 0.0));  //changed our LUT to MAKE IT work
                //    return c * _Color;                 //(r=x coord g=y coord * pallet size) + (pallet size * shift-1) //Trying to understand it's process
                //}
                //    ENDHLSL
                //}
        }

            Fallback "Sprites/Default"
}
