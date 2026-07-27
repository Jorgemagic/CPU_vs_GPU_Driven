cbuffer ViewParams : register(b0)
{
    float4x4 viewProj;
    float4 gridInfo0; // x: time, y: columns, z: rows, w: cube count
    float4 gridInfo1; // x: scale, y: spacing, z: total width, w: total height
    float4 drawInfo;  // x: vertex count
};

struct InstanceData
{
    float4x4 worldViewProj;
};

StructuredBuffer<InstanceData> Instances : register(t0);

Texture2D DiffuseTexture : register(t1);
SamplerState Sampler : register(s0);

struct VS_IN
{
    float4 pos : POSITION;
    float4 col : COLOR;
    float2 tex : TEXCOORD;
    uint objectIndex : TEXCOORD1;
};

struct PS_IN
{
    float4 pos : SV_POSITION;
    float4 col : COLOR;
    float2 tex : TEXCOORD;
};

PS_IN VS(VS_IN input)
{
    PS_IN output = (PS_IN)0;

    output.pos = mul(input.pos, Instances[input.objectIndex].worldViewProj);
    output.col = input.col;
    output.tex = input.tex;

    return output;
}

float4 PS(PS_IN input) : SV_Target
{
    return DiffuseTexture.Sample(Sampler, input.tex);
}
