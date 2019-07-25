Shader "Custom/FXAura" {
		Properties{
			_Color("Main Color", Color) = (.5,.5,.5,1)
			_MainTex("Base (RGBA)", 2D) = "white" { }
			_Ramp("Toon Ramp (RGB)", 2D) = "gray" {}
			_Color2("Aura Color", Color) = (0,0,1,1)
			_ColorR("Rim Color", Color) = (0,1,1,1)
			_Outline("Outline width", Range(.002, 0.8)) = .3
			_OutlineZ("Outline Z", Range(-.06, 0)) = -.05
			_NoiseTex("Noise Texture", 2D) = "white" { }
			_Scale("Noise Scale", Range(0.0, 0.2)) = 0.01
			_SpeedX("Speed X", Range(-10, 10)) = 0
			_SpeedY("Speed Y", Range(-10, 10)) = 3.0
			_Opacity("Noise Opacity", Range(0.01, 10.0)) = 10
			_Brightness("Brightness", Range(0.5, 3)) = 2
			_Edge("Rim Edge", Range(0.0, 1)) = 0.1
			_RimPower("Rim Power", Range(0.01, 10.0)) = 1
 
		}
 
		CGINCLUDE
 
		#include "UnityCG.cginc"
 
		struct appdata {
			float4 vertex : POSITION;
			float3 normal : NORMAL;
 
		};
 
		struct v2f {
			float4 pos : SV_POSITION;
			UNITY_FOG_COORDS(0)
			float3 viewDir : TEXCOORD1;
			float3 normalDir : TEXCOORD2;
		};
 
		uniform float _Outline;
		uniform float _OutlineZ;
		uniform float _RimPower;
 
 
		v2f vert(appdata v) {
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			float3 norm = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, v.normal));// set v.normal to v.vertex if you have hard normals
			float2 offset = TransformViewToProjection(norm.xy);
			o.pos.xy += offset * _Outline * o.pos.z;
			o.pos.z += _OutlineZ;// push away from camera
			o.normalDir = normalize(mul(float4(v.normal, 0), unity_WorldToObject).xyz); // normal direction
			o.viewDir = normalize(WorldSpaceViewDir(v.vertex)); // view direction
			UNITY_TRANSFER_FOG(o, o.pos);
			return o;
		}
		ENDCG
 
		SubShader
		{
			Tags{ "RenderType" = "Opaque" }
			UsePass "Custom/ToonLit/FORWARD" // Base shader pass, using the standard ToonLit shader

			Pass
			{
				Name "OUTLINE"
				Tags{ "LightMode" = "Always" }
				Cull Back
				ZWrite Off
				ColorMask RGB
				Blend SrcAlpha OneMinusSrcAlpha // Transparency Blending
 
				CGPROGRAM
					#pragma vertex vert
					#pragma fragment frag
					#pragma multi_compile_fog
 
					sampler2D _NoiseTex;
					float _Scale, _Opacity, _Edge;
					uniform float4 _Color2, _ColorR;
					float _Brightness, _SpeedX, _SpeedY;
 
					fixed4 frag(v2f i) : SV_Target
					{
						float2 uv = float2(i.pos.x* _Scale - (_Time.x * _SpeedX), i.pos.y * _Scale - (_Time.x * _SpeedY)); // float2 based on speed, position and, scale
						float4 text = tex2D(_NoiseTex, uv);
						float4 rim = pow(saturate(dot(i.viewDir, i.normalDir)), _RimPower); // calculate inverted rim based on view and normal
						rim -= text; // subtract noise texture
						float4 texturedRim = saturate(rim.a * _Opacity); // make a harder edge
						float4 extraRim = (saturate((_Edge + rim.a) * _Opacity) - texturedRim) * _Brightness;// extra edge, subtracting the textured rim
						float4 result = (_Color2 * texturedRim) + (_ColorR * extraRim);// combine both with colors
						UNITY_APPLY_FOG(i.fogCoord, result);
						return result;
					}
				ENDCG
			}

		}
	} 
	Fallback "Custom/ToonLit"
}
