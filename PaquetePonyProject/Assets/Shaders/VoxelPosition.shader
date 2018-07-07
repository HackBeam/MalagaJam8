Shader "Custom/VoxelPosition" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_Tex ("Texture", 2D) = "white"{}
		_Color2 ("Secondary Color", Color) = (1,1,1,1)
		_Tex2 ("Secondary Texture", 2D) = "white"{}
		_DisAmount("DisAmount", Range(-0.01, 1)) = 0.01
        _Radius("Radius", Range(0, 100)) = 0 
		_NScale ("Noise Scale", Range(0, 10)) = 1 
		_NoiseTex("Dissolve Noise", 2D) = "white"{} 
        _DisLineWidth("Line Width", Range(0.001, 2)) = 0
        _DisLineColor("Line Tint", Color) = (1,1,1,1)
		_PixelSize("PixelSize", float) = 1
	}
	SubShader {

		Tags { "RenderType" = "Transparent" }
            LOD 200   
            //Cull Off
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert
		
		float3 _Position; // from script
		
		float4 _Color, _Color2, _DisLineColor;
		sampler2D _Tex, _Tex2;
		float _DisAmount;
		float _Radius;
		sampler2D _NoiseTex;
		float _NScale;
		float _DisLineWidth;
		float _PixelSize;


		struct Input {
			float3 worldPos;
			float3 worldNormal;
			float2 uv_Tex;
		};

		void surf (Input IN, inout SurfaceOutput o) {

			// triplanar noise
			float3 blendNormal = saturate(pow(IN.worldNormal * 1.4,4));
			//half4 nSide1 = tex2D(_NoiseTex, (IN.worldPos.xy/* + _Time.x*/) * _NScale);
			//half4 nSide2 = tex2D(_NoiseTex, (IN.worldPos.xz/* + _Time.x*/) * _NScale);
			//half4 nTop = tex2D(_NoiseTex, (IN.worldPos.yz/* + _Time.x*/) * _NScale);
			
			//float3 noisetexture = nSide1;
			//noisetexture = lerp(noisetexture, nTop, blendNormal.x);
			//noisetexture = lerp(noisetexture, nSide2, blendNormal.y);
			float3 noisetexture = tex2D(_NoiseTex, round(IN.worldPos.xz/_PixelSize) * _PixelSize * _NScale);
			float3 dis = distance(round(_Position/_PixelSize)*_PixelSize, round(IN.worldPos/_PixelSize)*_PixelSize);
			dis = round(dis/_PixelSize)*_PixelSize;
			float3 sphere = 1 - saturate(dis / _Radius);
			//sphere= float3(floor(sphere.x/_PixelSize)*_PixelSize, floor(sphere.y/_PixelSize)*_PixelSize, floor(sphere.z/_PixelSize)*_PixelSize);
			//sphere= float3(round(sphere.x), round(sphere.y), round(sphere.z));
			float3 sphereNoise = noisetexture.r * sphere;
			//sphereNoise *= step((sphereNoise.x/_PixelSize)*_PixelSize, sphereNoise.x + (_PixelSize/2)) * step((sphereNoise.z/_PixelSize)*_PixelSize, sphereNoise.z + (_PixelSize/2));

			float3 primaryZone = step(sphereNoise,_DisAmount);
			float3 dissolveLine = step(sphereNoise - _DisLineWidth, _DisAmount) * step(_DisAmount, sphereNoise); // line between two textures
			float3 secondaryZone = step(_DisAmount, sphereNoise - _DisLineWidth);

			//float3 primaryAlpha = primaryZone * _Color.a;
			//float3 secondaryAlpha = secondaryZone * _Color2.a;
			//float3 dissolveLineAlpha = dissolveLine * _DisLineColor.a;

			//clip ((primaryAlpha + secondaryAlpha + dissolveLineAlpha) -.1f);
 

			dissolveLine *= _DisLineColor.rgb; // color the line
			float3 primaryTex = primaryZone * _Color.rgb * tex2D(_Tex, IN.uv_Tex).rgb;
			float3 secondaryTex = secondaryZone * _Color2.rgb * tex2D(_Tex2, IN.uv_Tex).rgb;
			
			float3 resultTex = primaryTex + secondaryTex + dissolveLine;
			
			o.Albedo = resultTex;

			//o.Emission = dissolveLine;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
