Shader "Simple2DShape/Triangle" {

	Properties{
		_Color("Color", Color) = (0.9375, 0.5859, 0.2343, 1)
		_Smooth("Smooth", Range(0.0, 1)) = 0.01
		_Fade("Fade", Range(0.0, 1.0)) = 1.0
		_FadeType("FadeType", Int) = 0
		_TexScale("TexScale", Float) = 1
		_MainTex("MainTex", 2D) = "white" {}
		_MainTex_ST("MainTex_ST", Float) = (1.0, 1.0, 0.0, 0.0)
	}

	SubShader{

		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}

		Pass {

			Blend SrcAlpha OneMinusSrcAlpha // Alpha blending
			Cull Off
			ZWrite Off

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			fixed4 _Color; // low precision type is usually enough for colors
			float _Smooth;
			float _Fade; 
			int _FadeType;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _TexScale;


			struct fragmentInput {
				float4 pos : SV_POSITION;
				float2 uv : TEXTCOORD0;
			};


			fragmentInput vert(appdata_base v)
			{
				fragmentInput o;

				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.texcoord.xy - fixed2(0.5,0.5);

				return o;
			}


				
			fixed4 Antialias(float distance) {
				return fixed4(
					_Color.r,
					_Color.g,
					_Color.b,
					_Color.a * ((distance > -_Smooth) ?
						1.0 - pow(distance + _Smooth, 2) / pow(_Smooth, 2)
						: 1)
				);
			}



			fixed4 Culling(float x, float y) {
				float a = 1;
				switch (_FadeType) {
					default:
					case 0: // L -> R
						return Antialias(x + 0.5 - _Fade);
					case 1: // R -> L
						return Antialias(0.5 - _Fade - x);
					case 2: // D -> U
						return Antialias(y + 0.5 - _Fade);
					case 3: // U -> D
						return Antialias(0.5 - _Fade - y);
					case 4: // Clockwise
						return Antialias(atan2(x, y) + (1 - _Fade) * (2 * (3.14159 + _Smooth)) - 3.14159 - _Smooth);
					case 5: // AntiClockwise
						return Antialias(-atan2(x, y) + (1 - _Fade) * (2 * (3.14159 + _Smooth)) - 3.14159 - _Smooth);
				}
			}



			fixed4 frag(fragmentInput i) : SV_Target{

				float distance = max(i.uv.y + abs(i.uv.x) * 2 - 0.5f, -i.uv.y - 0.5);
				
				float4 _MainTex_var = tex2D(_MainTex, TRANSFORM_TEX(i.uv * _TexScale - fixed2(0.5, 0.5), _MainTex));

				return min(_MainTex_var * Antialias(distance), Culling(i.uv.x, i.uv.y));

			}


			ENDCG
		}
	}
}