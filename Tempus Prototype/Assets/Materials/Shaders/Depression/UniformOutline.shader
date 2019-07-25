Shader "Custom/UniformOutline" 
{
	Properties
	{
		_Color("Main Color", Color) = (0.5,0.5,0.5,1)
		_MainTex ("Texture", 2D) = "white" {}
		_OutlineColor ("Outline color", Color) = (0,0,0,1)
		_OutlineWidth ("Outlines width", float) = 0.001
	}

	CGINCLUDE
	#include "UnityCG.cginc"

	struct vertexInput
	{
		float4 vertex : POSITION;
	};

	struct vertexOutput
	{
		float4 pos : POSITION;
	};

	uniform float _OutlineWidth;
	uniform float4 _OutlineColor;
	uniform sampler2D _MainTex;
	uniform float4 _Color;

	ENDCG

	SubShader
	{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" }

		Pass //Outline
		{
			ZWrite Off
			Cull Back
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			vertexOutput vert(vertexInput input)
			{
				vertexInput original = input;
				input.vertex.xyz += _OutlineWidth * normalize(input.vertex.xyz);

				vertexOutput output;
				output.pos = UnityObjectToClipPos(input.vertex);
				return output;

			}

			half4 frag(vertexOutput i) : COLOR
			{
				return _OutlineColor;
			}

			ENDCG
		}

		Tags{ "Queue" = "Geometry"}

		CGPROGRAM
		#pragma surface surf Lambert
		 
		struct Input {
			float2 uv_MainTex;
		};
		 
		void surf (Input IN, inout SurfaceOutput output) {
			fixed4 colour = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			output.Albedo = colour.rgb;
			output.Alpha = colour.a;
		}
		ENDCG
	}
	Fallback "Diffuse"
}

