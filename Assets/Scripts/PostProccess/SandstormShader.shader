Shader "Hidden/SandstormShader"
{
	Properties
	{
		//_MainTex ("Texture", 2D) = "white" {}
		_MainTex("Base (RGB)", 2D) = "" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			//float flipY;
			float vignetteRadius;
			float ns;
			float4 sc;

			float distFromCenter(float2 pt)
			{
				pt = pt - float2(0.5, 0.5);
				return sqrt(dot(pt, pt));
			}

			float rand(float3 co)
			{
				return frac(sin(dot(co.xyz, float3(12.9898, 78.233, 45.5432))) * 43758.5453);
			}

			float prand(float3 co, float3 rep)
			{
				float3 pco = co%rep;
				return frac(sin(dot(pco.xyz, float3(12.9898, 78.233, 45.5432))) * 43758.5453);
			}

			float4 mod289(float4 x)
			{
				return x - floor(x * (1.0 / 289.0)) * 289.0;
			}

			float4 permute(float4 x)
			{
				return mod289(((x*34.0) + 1.0)*x);
			}

			float4 taylorInvSqrt(float4 r)
			{
				return 1.79284291400159 - 0.85373472095314 * r;
			}

			float2 fade(float2 t) {
				return t*t*t*(t*(t*6.0 - 15.0) + 10.0);
			}

			float pnoise(float2 P, float2 rep)
			{
				float4 Pi = floor(P.xyxy) + float4(0.0, 0.0, 1.0, 1.0);
				float4 Pf = frac(P.xyxy) - float4(0.0, 0.0, 1.0, 1.0);
				Pi = Pi%rep.xyxy;//fmod(Pi, rep.xyxy); // To create noise with explicit period
				Pi = mod289(Pi);        // To avoid truncation effects in permutation
				float4 ix = Pi.xzxz;
				float4 iy = Pi.yyww;
				float4 fx = Pf.xzxz;
				float4 fy = Pf.yyww;

				float4 i = permute(permute(ix) + iy);

				float4 gx = frac(i * (1.0 / 41.0)) * 2.0 - 1.0;
				float4 gy = abs(gx) - 0.5;
				float4 tx = floor(gx + 0.5);
				gx = gx - tx;

				float2 g00 = float2(gx.x, gy.x);
				float2 g10 = float2(gx.y, gy.y);
				float2 g01 = float2(gx.z, gy.z);
				float2 g11 = float2(gx.w, gy.w);

				float4 norm = taylorInvSqrt(float4(dot(g00, g00), dot(g01, g01), dot(g10, g10), dot(g11, g11)));
				g00 *= norm.x;
				g01 *= norm.y;
				g10 *= norm.z;
				g11 *= norm.w;

				float n00 = dot(g00, float2(fx.x, fy.x));
				float n10 = dot(g10, float2(fx.y, fy.y));
				float n01 = dot(g01, float2(fx.z, fy.z));
				float n11 = dot(g11, float2(fx.w, fy.w));

				float2 fade_xy = fade(Pf.xy);
				float2 n_x = lerp(float2(n00, n01), float2(n10, n11), fade_xy.x);
				float n_xy = lerp(n_x.x, n_x.y, fade_xy.y);
				return 2.3 * n_xy;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				float2 coords = i.uv;
				//if (flipY > 0) coords.y = 1.0 - coords.y;

				float d = distFromCenter(coords);

				fixed4 col = tex2D(_MainTex, coords);
				// vignette the color with a texture
				float2 unit = float2(1., 0.);
				float tex = pnoise(coords*ns, float2(ns, ns));
				tex = 
					clamp(
						((2*tex)-(tex*tex))*
						(0.87+pnoise(coords.yx*3.2*ns, 3.2*float2(ns, ns)))
					, 0.0, 1.0);
				float tex2 = pnoise(coords.yx*ns, float2(ns, ns));
				tex2 =
					clamp(
					((2 * tex2) - (tex2*tex2))*
						(0.87 + pnoise(coords.yx*2.7*ns, 2.7*float2(ns, ns)))
					, 0.0, 1.0);

				tex = (tex + tex2)*.5;
				tex = tex *
					(
					(
						prand(float3(coords + unit.xy, 0.0), float3(1., 1., 1.)) +
						prand(float3(coords - unit.xy, 0.0), float3(1., 1., 1.)) +
						prand(float3(coords + unit.yx, 0.0), float3(1., 1., 1.)) +
						prand(float3(coords - unit.yx, 0.0), float3(1., 1., 1.)) +
						prand(float3(coords + unit.xy + unit.yx, 0.0), float3(1., 1., 1.)) +
						prand(float3(coords - unit.xy + unit.yx, 0.0), float3(1., 1., 1.)) +
						prand(float3(coords + unit.xy - unit.yx, 0.0), float3(1., 1., 1.)) +
						prand(float3(coords - unit.xy - unit.yx, 0.0), float3(1., 1., 1.))
						)
						*0.125);
				//tex = 1.0 - tex;
				/**/
				col = lerp(col, fixed4(tex*col.rgb, 1.0)+sc, clamp(d-vignetteRadius, 0.0, 1.0));
				return col;
			}
			ENDCG
		}
	}
}
