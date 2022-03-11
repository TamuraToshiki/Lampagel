Shader "Unlit/Ground"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
				// �J�����̑O���x�N�g��
				float3 forward = -UNITY_MATRIX_V._m20_m21_m22; 

				// �J�����̃��[���h�ʒu
				float3 campos = _WorldSpaceCameraPos; 

				// near �� far ���王����̒����܂ł̋������擾
				float center_distance = abs(_ProjectionParams.z - _ProjectionParams.y) * 0.5; 

				// ���ʂ��ړ����ׂ����S�_
				float3 center = campos + forward * (center_distance + abs(_ProjectionParams.y)); 

				// �ړ���̒��_
				float3 pos = float3(v.vertex.x * center_distance * 0.5 + center.x, 0, v.vertex.z * center_distance * 0.5 + center.z);

				 // �N���b�v���W��
				v2f o;
				o.vertex = UnityWorldToClipPos(pos);

				// uv���W��pos����K�p(1/16�����Ȃ��Ƒ����T�C�Y�I�ɂ��������Ȃ邩���H)
				o.uv = TRANSFORM_TEX(pos.xz*float2(1.0 / 16.0, 1.0 / 16.0), _MainTex);
				UNITY_TRANSFER_FOG(o, o.vertex);
				return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				 float4 col = tex2D(_MainTex, i.uv);
				 UNITY_APPLY_FOG(i.fogCoord, col);
				 return col;
				 
            }
            ENDCG
        }
    }
}
