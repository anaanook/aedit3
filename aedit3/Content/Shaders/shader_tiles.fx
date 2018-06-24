sampler TextureSamplerA;
float Time;
float amount = 1;
Texture2D Palette : register (t1);
sampler TextureSamplerB : register (s1) {
	Texture = (Palette);
};

float4 main(float4 position : SV_Position, float4 col : COLOR0, float2 uv : TEXCOORD0) : COLOR0{
	float4 base = tex2D(TextureSamplerA,uv);
	float pal = 7.0f / 16.0f;
	float offset = Time;
	float4 test = tex2D(TextureSamplerB,float2((1-(base.r))*0.75f+position.z*100,pal))* amount;
	test.a = base.a;
	test.rgb = base.rgb* base.a;
	return test*col;
}
technique Ambient
{
	pass Pass1
	{
		PixelShader = compile ps_4_0 main();
	}
}