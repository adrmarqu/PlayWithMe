Shader "PlayWithMe/Water"
{
    Properties
    {
        _MainTex ("Textura", 2D) = "white" {}
        _HeightTex ("MapaAltura", 2D) = "white" {}
		_HeightMax("HeightMax", float) = 20
		_DireccionLuz ("Direccion luz", Vector) = (0, 1, 0, 0)
		_ColorDifuso ("Color difuso", Color) = (0.5, 0.5, 0.5, 1)
		_Placement ("Placement", float) = 0
		
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex VShader
            #pragma fragment PShader


            #include "UnityCG.cginc"

            struct VSInput
            {
                float4 position : POSITION;
                float2 uv : TEXCOORD0;
				float4 normal : NORMAL;
            };

            struct VSOutput
            {
                float2 uv : TEXCOORD0;
                float4 position : SV_POSITION;
				float4 color : COLOR0;
				float3 normalW : NORMAL;
				float height : TEXCOORD1;
				
            };

            sampler2D _MainTex;
            sampler2D _HeightTex;
			float _HeightMax;
			float _Placement;
			float4 _DireccionLuz;
			float4 _ColorDifuso;


            VSOutput VShader(VSInput input)
            {
                VSOutput output;
				
                float4 heightColor = tex2Dlod(_HeightTex, float4(input.uv.x, input.uv.y + _Placement, 0, 0));

                float height = (heightColor.x + heightColor.y + heightColor.z) / 3;

                output.height = height;
                height = height * _HeightMax;

				float4 normalW = normalize(mul(UNITY_MATRIX_M, float4(input.normal.xyz, 0)));
				float4 positionW = mul(UNITY_MATRIX_M, float4(input.position.xyz, 1.0));	

				positionW += float4(0, height, 0, 0);

                float4 positionC = mul(UNITY_MATRIX_V, float4(positionW.xyz, 1.0));
				
                
								
				float4 positionS = mul(UNITY_MATRIX_P, float4(positionC.xyz, 1.0));
				
				output.position = positionS;				
								
				output.uv = input.uv;		
			
				output.color = float4(_ColorDifuso.xyz * (1 - dot(normalW, -_DireccionLuz)), 1);
				
                return output;
            }

            fixed4 PShader(VSOutput input) : SV_Target
            {		
                fixed4 heightColor = tex2D(_HeightTex, float2(0, input.height));
				fixed4 texColor = tex2D(_MainTex, input.uv);

                return input.color * texColor;
				
            }
			
            ENDCG
        }
    }
}
