Shader "water"
{
    Properties
    {
        [Header(Texture)]
        _Color ("Color",Color) = (1,1,1,1)  
        _WaterCol("WaterColor",Color)=(1,1,1,1)
        _Main ("Texture", 2D) = "white" {} 
        _NoiseTex("Noise Texture", 2D) = "white" {} 
        _NoiseTex2("Noise Texture2", 2D) = "white" {} 
        _GradientTex("Gradient Texture", 2D) = "white" {}
        _RippleData("x：大小，y：速度 ",vector) = (1.0,1.0,1.0,1.0)
        _RippleData2("x：大小，y：速度 ",vector) = (1.0,1.0,1.0,1.0)
        _WaterSpeed("WaterSpeed" ,Range(0.0,1)) = 0.5
        
    }

    SubShader{
        Tags {
            "Queue"="Transparent"               // 调整渲染顺序
            "RenderPipeline"="UniversalRenderPipeline" 
//            "RenderType"="Transparent"          // 对应改为Cutout
            "RenderType"="TransparentCutout"
            "ForceNoShadowCasting"="True"       // 关闭阴影投射
            "IgnoreProjector"="True"            // 不响应投射器
        }
        
         HLSLINCLUDE
             
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
         
            CBUFFER_START(UnityPerMaterial)
            uniform float4 _Color;
            uniform float _WaterSpeed;
            uniform float4 _RippleData;
            uniform float4 _RippleData2;
            uniform float4 _WaterCol; 
            CBUFFER_END
            
        uniform TEXTURE2D (_Main);
         SAMPLER (sampler_Main); 
        uniform TEXTURE2D (_NoiseTex);
         SAMPLER(sampler_NoiseTex);
         float4 _Noise_ST;
         uniform  TEXTURE2D(_GradientTex);
         SAMPLER(sampler_GradientTex);
         uniform TEXTURE2D(_NoiseTex2);
         SAMPLER(sampler_NoiseTex2);
         
         
        struct Attributes{
            float4 positionOS   : POSITION;
            float2 uv           : TEXCOORD0;
         
            
        };

        struct Varyings{
            float4 positionCS   : SV_POSITION;
            float2 uv0          : TEXCOORD0;
            float2 uv1          : TEXCOORD1;
            float2 uv2          :TEXCOORD2;

        };
         ENDHLSL
        Pass {
            Blend one OneMinusSrcAlpha  

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

        Varyings vert (Attributes v){
            Varyings o = (Varyings)0;
            
                
            o.positionCS = TransformObjectToHClip(v.positionOS.xyz);
            
             o.uv0 = v.uv;
             
            o.uv0.y = (1-o.uv0.y);
            o.uv1 = o.uv0*_RippleData.x-float2(frac(_Time.x*_RippleData.y),0.0);
             o.uv2 = o.uv0*_RippleData2.x-float2(frac(_Time.x*_RippleData2.y),0.0);
            return o;
        }

        float4 frag (Varyings i): SV_TARGET{
            
            
                half grad = SAMPLE_TEXTURE2D(_GradientTex,sampler_GradientTex, i.uv0).x ;
                i.uv0.x += (i.uv0.y * 0.8 + 0.2) *( 0.15 * SAMPLE_TEXTURE2D(_NoiseTex2,sampler_NoiseTex2,half2(_Time.y * _WaterSpeed,i.uv0.y)).x -0.1);
                i.uv0.y += (i.uv0.y * 0.8 + 0.2) *( 0.08 * (SAMPLE_TEXTURE2D(_NoiseTex2,sampler_NoiseTex2,half2(_Time.y * _WaterSpeed,i.uv0.x)).x -0.5));
                // i.uv0 =SAMPLE_TEXTURE2D(_NoiseTex,sampler_NoiseTex,i.uv2).x;
                float4 war_Ripple = SAMPLE_TEXTURE2D(_NoiseTex,sampler_NoiseTex,i.uv1).x;
                // war_Ripple = smoothstep(0.5,0.6,war_Ripple);
                float4 col =SAMPLE_TEXTURE2D(_Main,sampler_Main, i.uv0);
                return col*grad *_Color*0.8+war_Ripple*0.4*_WaterCol*grad;
           
        
        }
            ENDHLSL
        }
    }
    

}