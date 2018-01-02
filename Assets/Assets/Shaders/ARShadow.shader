
Shader "AR/AlhpaShadow"
{ 
	SubShader
	{
		Tags{ "LightMode"="ForwardBase" "RenderType"="Opaque" "Queue"="Geometry+1" "ForceNoShadowCast"="True"}
		LOD 200
		Blend Zero SrcColor
		ZWrite On

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#pragma multi_compile_fwdbase
			#include "AutoLight.cginc"

			struct v2f 
			{
				float4 pos : SV_POSITION;
				LIGHTING_COORDS(0,1)
			}; 

			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				TRANSFER_VERTEX_TO_FRAGMENT(o);
				return o;
			}

			fixed4 frag(v2f i) : COLOR
			{
				float atten = LIGHT_ATTENUATION(i);
				return fixed4(1,1,1,1) * atten;
			}

			ENDCG
		}
	}
	Fallback "VertexLit"
}