sampler TextureSamplerA;
float Time;
float amount = 1;
Texture2D Palette : register (t1);
sampler TextureSamplerB : register (s1) {
	Texture = (Palette);
};
float4 when_eq(float4 x, float4 y) {
  return 1.0 - abs(sign(x - y));
}
float4 main(float4 position : SV_Position, float4 col : COLOR0, float2 uv : TEXCOORD0) : COLOR0{
	float4 base = tex2D(TextureSamplerA,uv);
	int iPal = col.a * 255;
	float offset = ((iPal >> 1)& 3)/3.0f * sign((iPal & 1)-0.5f);
	float pal = (iPal>>4) / 16.0f;

	amount = when_eq(col.a,1.0f);
	float fleeb = clamp((1-(base.r))*0.75f+offset,0.01f,0.99f);
	float4 test = tex2D(TextureSamplerB,float2(fleeb,pal))* (1-amount) + base * (amount);
	test.a = base.a;
	test.rgb = test.rgb * base.a;
	clip(test.a-0.5f);
	return test;
}
technique Ambient
{
	pass Pass1
	{
		PixelShader = compile ps_4_0 main();
	}
}