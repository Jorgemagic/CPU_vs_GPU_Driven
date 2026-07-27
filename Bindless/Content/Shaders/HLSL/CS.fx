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

struct IndirectDrawArgs
{
    uint vertexCountPerInstance;
    uint instanceCount;
    uint startVertexLocation;
    uint startInstanceLocation;
};

RWStructuredBuffer<InstanceData> RWInstances : register(u0);
RWStructuredBuffer<IndirectDrawArgs> IndirectArgs : register(u1);

float4x4 CreateScale(float scale)
{
    return float4x4(
        scale, 0,     0,     0,
        0,     scale, 0,     0,
        0,     0,     scale, 0,
        0,     0,     0,     1);
}

float4x4 CreateTranslation(float x, float y, float z)
{
    return float4x4(
        1, 0, 0, 0,
        0, 1, 0, 0,
        0, 0, 1, 0,
        x, y, z, 1);
}

float4x4 CreateRotationX(float angle)
{
    float s = sin(angle);
    float c = cos(angle);

    return float4x4(
        1, 0,  0, 0,
        0, c,  s, 0,
        0, -s, c, 0,
        0, 0,  0, 1);
}

float4x4 CreateRotationY(float angle)
{
    float s = sin(angle);
    float c = cos(angle);

    return float4x4(
        c, 0, -s, 0,
        0, 1,  0, 0,
        s, 0,  c, 0,
        0, 0,  0, 1);
}

float4x4 CreateRotationZ(float angle)
{
    float s = sin(angle);
    float c = cos(angle);

    return float4x4(
        c,  s, 0, 0,
        -s, c, 0, 0,
        0,  0, 1, 0,
        0,  0, 0, 1);
}

[numthreads(64, 1, 1)]
void CS(uint3 dispatchThreadId : SV_DispatchThreadID)
{
    uint index = dispatchThreadId.x;
    uint cubeCount = (uint)gridInfo0.w;

    if (index >= cubeCount)
    {
        return;
    }

    IndirectArgs[index].vertexCountPerInstance = (uint)drawInfo.x;
    IndirectArgs[index].instanceCount = 1;
    IndirectArgs[index].startVertexLocation = 0;
    IndirectArgs[index].startInstanceLocation = index;

    uint columns = (uint)gridInfo0.y;
    uint x = index % columns;
    uint y = index / columns;

    float positionX = (x * gridInfo1.y) - (gridInfo1.z * 0.5f);
    float positionY = (y * gridInfo1.y) - (gridInfo1.w * 0.5f);

    float time = gridInfo0.x;
    float4x4 world = mul(CreateScale(gridInfo1.x), CreateRotationX(time));
    world = mul(world, CreateRotationY(time * 2.0f));
    world = mul(world, CreateRotationZ(time * 0.7f));
    world = mul(world, CreateTranslation(positionX, positionY, 0));

    RWInstances[index].worldViewProj = mul(world, viewProj);
    RWInstances[index].materialIndex = index & 3;
}
