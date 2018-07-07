Shader "Custom/Hologram" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Lines("Lines", float) = 5
	}
	SubShader {
		Tags { "RenderType"="Transparent" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert alpha:fade //vertex:vert

		sampler2D _MainTex;
		fixed4 _Color;
		float _Lines;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		//void vert(inout appdata_full v)
    	//{
		//	v.vertex.xz -= v.normal * frac(_Time)/10;
		//	v.vertex.y += v.normal * frac(_Time)/20;
		//
    	//}

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Emission = _Color.rgb;
			o.Alpha = _Color.a * frac((IN.worldPos.y - _Time)*_Lines);
		}

		fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten) {
			return fixed4(0,0,0,0);
		}
		ENDCG
	}
	FallBack "Unlit"
}
