// Shader created with Shader Forge Beta 0.17 
// Shader Forge (c) Joachim 'Acegikmo' Holmer
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.17;sub:START;pass:START;ps:lgpr:1,nrmq:1,limd:1,blpr:2,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:False,uamb:True,mssp:True,ufog:True,aust:True,igpj:True,qofs:0,lico:1,qpre:3,flbk:,rntp:2,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,hqsc:True,hqlp:False,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300;n:type:ShaderForge.SFN_Final,id:1,x:32719,y:32712|diff-27-OUT,normal-103-RGB,emission-67-OUT;n:type:ShaderForge.SFN_Fresnel,id:3,x:33459,y:32750|NRM-80-OUT;n:type:ShaderForge.SFN_Power,id:4,x:33229,y:32815|VAL-3-OUT,EXP-6-OUT;n:type:ShaderForge.SFN_Slider,id:6,x:33459,y:32911,ptlb:Rim Power,min:0.1,cur:2.229323,max:6;n:type:ShaderForge.SFN_Tex2d,id:7,x:33229,y:32431,ptlb:Texture Diffuse,tex:3a8c2afc0e345dc4f88b0e3489825077,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:26,x:33229,y:32627,ptlb:Main Color,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:27,x:32981,y:32525|A-7-RGB,B-26-RGB;n:type:ShaderForge.SFN_Tex2d,id:62,x:33459,y:33032,ptlb:Rim Texture,tex:7f51eb5cd50896b4ba3f11f28a9de93c,ntxv:0,isnm:False|UVIN-87-UVOUT;n:type:ShaderForge.SFN_Color,id:63,x:33459,y:33240,ptlb:Rim Color,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:64,x:33272,y:33147|A-62-RGB,B-63-RGB;n:type:ShaderForge.SFN_ValueProperty,id:65,x:33272,y:33318,ptlb:Rim Intensity,v1:3;n:type:ShaderForge.SFN_Multiply,id:66,x:33095,y:33129|A-64-OUT,B-65-OUT;n:type:ShaderForge.SFN_Multiply,id:67,x:32978,y:32888|A-4-OUT,B-66-OUT;n:type:ShaderForge.SFN_NormalVector,id:80,x:33665,y:32701,pt:False;n:type:ShaderForge.SFN_Panner,id:87,x:33650,y:33009,spu:0,spv:1|UVIN-90-UVOUT,DIST-93-OUT;n:type:ShaderForge.SFN_TexCoord,id:90,x:34011,y:32911,uv:0;n:type:ShaderForge.SFN_Multiply,id:93,x:33826,y:33061|A-95-T,B-96-OUT;n:type:ShaderForge.SFN_Time,id:95,x:34011,y:33061;n:type:ShaderForge.SFN_ValueProperty,id:96,x:34011,y:33229,ptlb:Rim Speed,v1:0.5;n:type:ShaderForge.SFN_Tex2d,id:103,x:33054,y:32689,ptlb:Normal,tex:a1a4cd460aad8ac49b9cd9611e0a8095,ntxv:3,isnm:True;proporder:26-7-103-63-62-6-65-96;pass:END;sub:END;*/

Shader "Langvv/Rim_Scroll_V_Additive_Diffuse_Normal" {
    Properties {
        _MainColor ("Main Color", Color) = (0.5,0.5,0.5,1)
        _TextureDiffuse ("Texture Diffuse", 2D) = "white" {}
        _Normal ("Normal", 2D) = "bump" {}
        _RimColor ("Rim Color", Color) = (0.5,0.5,0.5,1)
        _RimTexture ("Rim Texture", 2D) = "white" {}
        _RimPower ("Rim Power", Range(0.1, 6)) = 0.1
        _RimIntensity ("Rim Intensity", Float ) = 3
        _RimSpeed ("Rim Speed", Float ) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform float _RimPower;
            uniform sampler2D _TextureDiffuse; uniform float4 _TextureDiffuse_ST;
            uniform float4 _MainColor;
            uniform sampler2D _RimTexture; uniform float4 _RimTexture_ST;
            uniform float4 _RimColor;
            uniform float _RimIntensity;
            uniform float _RimSpeed;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float4 uv0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.uv0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float2 node_123 = i.uv0;
                float3 normalLocal = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(node_123.rg, _Normal))).rgb;
                float3 normalDirection = normalize( mul( normalLocal, tangentTransform ) );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor + UNITY_LIGHTMODEL_AMBIENT.xyz;
////// Emissive:
                float4 node_95 = _Time + _TimeEditor;
                float3 emissive = (pow((1.0-max(0,dot(i.normalDir, viewDirection))),_RimPower)*((tex2D(_RimTexture,TRANSFORM_TEX((i.uv0.rg+(node_95.g*_RimSpeed)*float2(0,1)), _RimTexture)).rgb*_RimColor.rgb)*_RimIntensity));
                float3 finalColor = diffuse * (tex2D(_TextureDiffuse,TRANSFORM_TEX(node_123.rg, _TextureDiffuse)).rgb*_MainColor.rgb) + emissive;
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform float _RimPower;
            uniform sampler2D _TextureDiffuse; uniform float4 _TextureDiffuse_ST;
            uniform float4 _MainColor;
            uniform sampler2D _RimTexture; uniform float4 _RimTexture_ST;
            uniform float4 _RimColor;
            uniform float _RimIntensity;
            uniform float _RimSpeed;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float4 uv0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.uv0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float2 node_124 = i.uv0;
                float3 normalLocal = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(node_124.rg, _Normal))).rgb;
                float3 normalDirection = normalize( mul( normalLocal, tangentTransform ) );
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor;
                float3 finalColor = diffuse * (tex2D(_TextureDiffuse,TRANSFORM_TEX(node_124.rg, _TextureDiffuse)).rgb*_MainColor.rgb);
/// Final Color:
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
