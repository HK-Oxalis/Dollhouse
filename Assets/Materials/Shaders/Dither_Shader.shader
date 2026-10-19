Shader "Custom/Dither_Shader"
{
    Properties
    {
        _Colour_Depth_Hue("Hue Colour depth", Int) = 5
        //_Colour_Depth_Saturation("Saturation Colour depth", Int) = 64
        _Colour_Depth_Value("Value Colour depth", Int) = 10
        _Value_Brightness_Offset("Value brightness multiplier", float) = 1.5
        
        _Dither_Threshold("Dither threshold", float) = 0.5
        _Dither_Strength("Dither pattern strength", float) = 10
        
    }
    
    SubShader
    {
        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
        ENDHLSL

        Tags { "RenderType"="Opaque" }
        LOD 100
        ZWrite Off Cull Off
        Pass
        {
            Name "Dither_Shader"

            HLSLPROGRAM
            
            #pragma vertex Vert
            #pragma fragment Frag
            
            uniform int _Colour_Depth_Hue;
            uniform int _Colour_Depth_Saturation;
            uniform int _Colour_Depth_Value;
            uniform float _Value_Brightness_Offset;
            
            uniform float _Dither_Threshold;
            uniform float _Dither_Strength;
            
            float Quantise(float value, int _Colour_Depth, float offset =1)
            {
                return (floor(value * (_Colour_Depth * offset)) / _Colour_Depth);
            }
            
            float Quantise_Dithered(float value, int _Colour_Depth, float dither_Value, float offset =1)
            {
                float dither_Depth_Threshold = _Dither_Threshold / _Colour_Depth;
                float dither_Step = ((dither_Value / _Colour_Depth) * _Dither_Strength) * step(value, dither_Depth_Threshold);
                
                return ((floor(value * (_Colour_Depth * offset)) - dither_Step) / _Colour_Depth);
            }
            
            float2 Cartesian_To_Polar(float2 coords)
            {
                float radius = sqrt(Sq(coords.x) + Sq(coords.y));
                float theta = atan(coords.y / coords.x);
                
                return float2(radius, theta);
            }
            
            float2 Polar_To_Cartesian(float2 coords)
            {
                float x = coords.x * cos(coords.y);
                float y = coords.x * sin(coords.y);
                
                return float2(x, y);
            }
            
            float flower_Tex(float2 coords)
            {
                
                float2 circle_Coords =(sin((coords * 10) - float2(0.5, 0.5)) / float2(1.0, 2));
                
                float2 polar_Coords = Cartesian_To_Polar(circle_Coords);
                float petal_Width = 5;
                float petal_Height = 12;
                
                polar_Coords.x -= abs(sin(polar_Coords.y * petal_Width)) / petal_Height;
                circle_Coords = Polar_To_Cartesian(polar_Coords);
                
                //Clamp circle alpha to avoid subpixel circles
                float circle_Alpha = clamp(length(circle_Coords), 0.21, 1);
                
                float circles = sin(circle_Alpha * 50) + 0.5;
                
                return  circles;
            }
            
            float2 Cylinder_Project(float2 coords, float3 direction)
            {
                float u = 0.5 + 0.5 * atan2(direction.x, - direction.z);
                
                return coords + (float2(u * -1, 0.5f + direction.y));
            }
            
            float4 Frag (Varyings input) : SV_Target
            {
                float4 colour = SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearClamp, input.texcoord).rgba;
                
                float3 hsv_Colour = RgbToHsv(colour.rgb);
                
                float2 look_Dir = Cylinder_Project(input.texcoord, GetViewForwardDir());
                
                hsv_Colour.r = Quantise(hsv_Colour.r, _Colour_Depth_Hue);
                //hsv_Colour.g = Quantise(hsv_Colour.g, _Colour_Depth_Saturation);
                hsv_Colour.b = Quantise_Dithered(hsv_Colour.b, _Colour_Depth_Value, flower_Tex(look_Dir), _Value_Brightness_Offset);
                
                colour = float4(HsvToRgb(hsv_Colour).rgb, 1);
                
                //colour.r = look_Dir.y;
                
                return colour;
            }
            
            ENDHLSL
        }
    }
}
