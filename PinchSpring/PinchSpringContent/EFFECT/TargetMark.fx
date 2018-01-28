//作成日：　2017.02.27
//クラス内容：　ターゲットシェーダー
//修正内容リスト：
//

float4x4 WorldViewProjection;

// TODO: add effect parameters here.

texture tex;
float size;

sampler texSampler = sampler_state {
	Texture = <tex>;
};



struct VertexShaderInput
{
    float4 Position : POSITION0;

    // TODO: add input channels such as texture
    // coordinates and vertex colors here.
	float2 TexCoord : TEXCOORD;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;

    // TODO: add vertex shader outputs such as colors and texture
    // coordinates here. These values will automatically be interpolated
    // over the triangle, and provided as input to your pixel shader.
	float2 TexCoord : TEXCOORD;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

	output.Position = mul(input.Position, WorldViewProjection);
	output.TexCoord = input.TexCoord;

    // TODO: add your vertex shader code here.
    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    // TODO: add your pixel shader code here.
	float4 color = tex2D(texSampler, input.TexCoord);

    return float4(1,1,1,1);
}

float4 PixelShaderFunction_2(VertexShaderOutput input) : COLOR0
{
	// TODO: add your pixel shader code here.
	float2 texCoord = input.TexCoord;
	texCoord.x = texCoord.x * size * 1.2f + (1 - size) * 0.3f;
	texCoord.y = texCoord.y * size * 1.2f + (1 - size) * 0.3f;
	float4 color = tex2D(texSampler, texCoord);

	return color;
}

technique Technique1
{
    pass Pass1
    {
        // TODO: set renderstates here.

        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}

technique Technique2
{
	pass Pass1
	{
		// TODO: set renderstates here.

		VertexShader = compile vs_2_0 VertexShaderFunction();
		PixelShader = compile ps_2_0 PixelShaderFunction_2();
	}
}