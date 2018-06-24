sampler TextureSamplerA;
float Time;
float amount = 1;

float4 main(float4 position : SV_Position, float4 col : COLOR0, float2 uv : TEXCOORD0) : COLOR0{
	float4 base = tex2D(TextureSamplerA,uv);
	float4 test = float4(0,0,0,0);
	test.a = base.a;
	test.rgb = base.rgb * base.a;
	clip(test.a-0.5f);
	return test*col;
}
technique Ambient
{
	pass Pass1
	{
		PixelShader = compile ps_4_0 main();
	}
}