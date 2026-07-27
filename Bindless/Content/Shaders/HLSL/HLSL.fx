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
    uint materialIndex;
};

StructuredBuffer<InstanceData> Instances : register(t0);

Texture2D DiffuseAtlas : register(t1);
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
    nointerpolation uint materialIndex : TEXCOORD1;
};

PS_IN VS(VS_IN input)
{
    PS_IN output = (PS_IN)0;

    InstanceData instance = Instances[input.objectIndex];
    output.pos = mul(input.pos, instance.worldViewProj);
    output.col = input.col;
    output.tex = input.tex;
    output.materialIndex = instance.materialIndex;

    return output;
}

float4 PS(PS_IN input) : SV_Target
{
    uint tileIndex = input.materialIndex & 3;
    float2 tileOffset = float2(tileIndex & 1, tileIndex >> 1) * 0.5f;
    float2 atlasUV = input.tex * 0.5f + tileOffset;

    float4 materialTint = 1.0f;
    if (tileIndex == 1)
    {
        materialTint = float4(1.0f, 0.82f, 0.72f, 1.0f);
    }
    else if (tileIndex == 2)
    {
        materialTint = float4(0.72f, 0.9f, 1.0f, 1.0f);
    }
    else if (tileIndex == 3)
    {
        materialTint = float4(0.86f, 1.0f, 0.72f, 1.0f);
    }

    return DiffuseAtlas.Sample(Sampler, atlasUV) * materialTint;
}
