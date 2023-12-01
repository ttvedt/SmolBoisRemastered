// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/SwapPallits"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_boiBirdPallet("Look Up Pallet", 2D) = "white" {}
		_PallitCount("PallitCount", float) = 2.0  //Initialized?  Why did I set this to 8?
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
			Blend SrcAlpha OneMinusSrcAlpha //Alpha???

			Pass
			{
			CGPROGRAM
				#pragma glsl
			//#pragma surface surf Standard fullforwardshadows alpha  //Thought this might fix the alpha :(
			#pragma target 3.0
			#pragma vertex vert 
			#pragma fragment frag 
			#pragma multi_compile DUMMY PIXELSNAP_ON
			#include "UnityCG.cginc"

			struct appdata_t  //Manages Input Data
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f        //Returned by the vertex Shader
			{
				float4 vertex   : SV_POSITION;
				//fixed4 color    : COLOR;
				float4 color : COLOR;  //No different?
				//half2 texcoord  : TEXCOORD0;
				float2 texcoord  : TEXCOORD0;  //Also no different?
			};

			fixed4 _Color;

			v2f vert(appdata_t IN)  //function that will take care of vertex shading. The function will return a struct v2f and accept an appdata
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap(OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _boiBirdPallet;
			float _PallitCount;

			fixed4 frag(v2f IN) : COLOR
			{
				float4 pCoord = tex2Dlod(_MainTex, float4(IN.texcoord.xy, 0.0, 0.0));
				float palSize = 1.0 / _PallitCount;   //determines how long a pallet is based on how many pallets there are based on length -Not hight :(
				float palShift = IN.color.r * 255.0;
				//float4 c = tex2Dlod(_boiBirdPallet, float4(pCoord.r * palSize + palSize * (palShift - 1)), float4(pCoor.g -_PallitCount), 0.0, 0.0);  //didn't work
				//float4 c = tex2Dlod(_boiBirdPallet, float4(pCoord.r + palShift - 1), (pCoord.y * palSize + palSize * (palShift - 1)), 0.0, 0.0));  //didn't work
				float4 c = tex2Dlod(_boiBirdPallet, float4(pCoord.r * palSize + palSize * (palShift - 1), 0.5, 0.0, 0.0));  //changed our LUT to MAKE IT work
				return c * _Color;                 //(r=x coord g=y coord * pallet size) + (pallet size * shift-1) //Trying to understand it's process
			}
		ENDCG
		}
		}
}

