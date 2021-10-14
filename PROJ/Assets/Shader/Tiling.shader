Shader "Custom/Tiling"
{
    Properties
    {
        _texture ("Texture", 2D) = "white" {}
        _horizontalScroll ("Horizontal Scroll", Range(-10, 10)) = 1
    }
    SubShader
    {
        CGPROGRAM

        #pragma surface surf Lambert
        sampler2D _texture;
        float _horizontalScroll;
        
        struct Input
        {
            float2 uv_texture;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            _horizontalScroll *= _Time;
            float2 newUV = IN.uv_texture + float2(_horizontalScroll, 0.5);
            o.Albedo = tex2D(_texture, newUV).rgb;
           
        }
        ENDCG
    }
    FallBack "Diffuse"
}
