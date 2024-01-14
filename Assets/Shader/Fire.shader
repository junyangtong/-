Shader "Fire"
{
    Properties
    {
        [Header(Texture)]
        _Mask            ("R:内焰 G:外焰 B:透贴",2D)="blue"{}
        _Noise           ("R:噪声1 G:噪声2",2D)="gray"{}
        _Noise1Params    ("噪声1 X:大小 Y:流速 Z:强度",vector)=(1.0,1.0,1.0,1.0)
        _Noise2Params    ("噪声2 X:大小 Y:流速 Z:强度",vector)=(1.0,1.0,1.0,1.0)
        [HDR]_color1     ("內焰颜色",color)=(1.0,1.0,1.0,1.0)
        [HDR]_color2     ("外焰颜色",color)=(1.0,1.0,1.0,1.0)
    }

    SubShader{
        Tags {
            "Queue"="Transparent"               // 调整渲染顺序
            "RenderPipeline"="UniversalRenderPipeline" 
            "RenderType"="Transparent"          // 对应改为Cutout
            "ForceNoShadowCasting"="True"       // 关闭阴影投射
            "IgnoreProjector"="True"            // 不响应投射器
        }
        
         HLSLINCLUDE
             
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
          
            
            
            CBUFFER_START(UnityPerMaterial)
            uniform float3 _Noise1Params;
            uniform float3 _Noise2Params;
            uniform float3 _color1;
            uniform float3 _color2;
            CBUFFER_END
            
        uniform TEXTURE2D (_Mask);
         SAMPLER (sampler_Mask); 
        uniform TEXTURE2D (_Noise);
         SAMPLER(sampler_Noise);
        
        


        struct Attributes{
            float4 positionOS   : POSITION;
            float2 uv          : TEXCOORD0;
            
        };

        struct Varyings{
            float4 positionCS   : SV_POSITION;
            float2 uv0          : TEXCOORD0;
            float2 uv1          : TEXCOORD1;
            float2 uv2          : TEXCOORD2;    

        };
         ENDHLSL
        Pass {
           
//            Tags {
//                "LightMode"="UniversalForward"
//            }
//            
           Blend one OneMinusSrcAlpha  

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
        Varyings vert (Attributes v){
            Varyings o = (Varyings)0;
            o.positionCS = TransformObjectToHClip(v.positionOS.xyz);
            o.uv0 = v.uv;
            o.uv1 =v.uv*_Noise1Params.x-float2(0.0,frac(_Time.x*_Noise1Params.y));
            o.uv2 =v.uv*_Noise2Params.x-float2(0.0,frac(_Time.x*_Noise2Params.y));
            return o;
        }

        float4 frag (Varyings i): SV_TARGET{
            half warpMask = SAMPLE_TEXTURE2D(_Mask,sampler_Mask,i.uv0).b;

            half war_Noise1 = SAMPLE_TEXTURE2D(_Noise,sampler_Noise,i.uv1).r;
            half war_Noise2 = SAMPLE_TEXTURE2D(_Noise,sampler_Noise,i.uv2).g;
            half noise = war_Noise1*_Noise1Params.z+war_Noise2*_Noise2Params.z;
            float2 WarpUV = i.uv0 + float2 (0.0,noise)*warpMask;
            
            half3 war_Mask = SAMPLE_TEXTURE2D(_Mask,sampler_Mask,WarpUV).rgb;
            half3 finallyRGB = war_Mask.r*_color1+war_Mask.g*_color2;
            half opacity = war_Mask.r+war_Mask.g;
            return  float4(finallyRGB,opacity);
        
        }
            ENDHLSL
        }
    }
    

}