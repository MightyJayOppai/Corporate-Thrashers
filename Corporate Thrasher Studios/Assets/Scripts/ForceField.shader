Shader "Unlit/ForceField"
{
    Properties
    {
        _MainColor ("MainColor", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _Fresnel("Fresnel Intensity", Range(0,200)) = 3.0
		_FresnelWidth("Fresnel Width", Range(0,2)) = 3.0
		_Distort("Distort", Range(0, 100)) = 1.0
		_IntersectionThreshold("Highlight of intersection threshold", range(0,1)) = .1 //Max difference for intersections
		_ScrollSpeedU("Scroll U Speed",float) = 2
		_ScrollSpeedV("Scroll V Speed",float) = 0
    }
    SubShader
    {
        Tags { "Queue" = "Overlay" "IgnoreProjector" = "True" "RenderType" = "Transparent"  }
        //LOD 100
        GrabPass{ "_GrabTexture" }
        Pass
        {
            Lighting Off ZWrite On
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            
            // make fog work
            //#pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                //(CHECK WHEN USING FLOAT INSTEAD OF FIXED)
                // having all float in vertex on mobile is probably faster than fixed, but not in frag.
                // You should use fixed in frag but only if you can avoid the conversion cost.
                // To do this you convert to fixed at the final part of your vert shader ie one conversion at the end.
                // Added normal
                fixed4 vertex : POSITION;
                fixed4 normal : NORMAL;
                fixed3 uv : TEXCOORD0;
            };

            struct v2f
            {
                fixed2 uv : TEXCOORD0;

                //UNITY_FOG_COORDS(1)
                //Added rimColor and screenPos

                fixed4 vertex : SV_POSITION;
                fixed3 rimColor : TEXCOORD1;
                fixed4 screenPos : TEXCOORD2;
            };

            //Added _CameraDepthTexture & _GrabTexture
            sampler2D _MainTex, _CameraDepthTexture, _GrabTexture;
            //Added _MainColor, _GrabTexture_ST & _GrabTexture_TexelSize
            fixed4 _MainTex_ST, _MainColor, _GrabTexture_ST, _GrabTexture_TexelSize;
            //Added this entire line
            fixed _Fresnel, _FresnelWidth, _Distort, _IntersectionThreshold, _ScrollSpeedU, _ScrollSpeedV;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                
                //Everything from hereon is added
                //scroll uv
				o.uv.x += _Time * _ScrollSpeedU;
				o.uv.y += _Time * _ScrollSpeedV;

				//fresnel 
				fixed3 viewDir = normalize(ObjSpaceViewDir(v.vertex));
				fixed dotProduct = 1 - saturate(dot(v.normal, viewDir));
				o.rimColor = smoothstep(1 - _FresnelWidth, 1.0, dotProduct) * .5f;
				o.screenPos = ComputeScreenPos(o.vertex);
				COMPUTE_EYEDEPTH(o.screenPos.z);//eye space depth of the vertex 
                return o;
            }

            fixed4 frag (v2f i, fixed face : VFace) : SV_Target
            {
                //added intersection
				fixed intersect = saturate((abs(LinearEyeDepth(tex2Dproj(_CameraDepthTexture,i.screenPos).r)
                - i.screenPos.z)) / _IntersectionThreshold);
                
                // Changed from fixed4 col to fixed3 main
                fixed3 main = tex2D(_MainTex, i.uv);
                
                // Everything from hereon is added
                    
                //distortion
				i.screenPos.xy += (main.rg * 2 - 1) * _Distort * _GrabTexture_TexelSize.xy;
				fixed3 distortColor = tex2Dproj(_GrabTexture, i.screenPos);
				distortColor *= _MainColor * _MainColor.a + 1;

				//intersect hightlight
				i.rimColor *= intersect * clamp(0,1,face);
				main *= _MainColor * pow(_Fresnel,i.rimColor) ;
				
				//lerp distort color & fresnel color
				main = lerp(distortColor, main, i.rimColor.r);
				main += (1 - intersect) * (face > 0 ? .03:.3) * _MainColor * _Fresnel;
				return fixed4(main,.9);
                //return col;
            }
            ENDCG
        }
    }
}
