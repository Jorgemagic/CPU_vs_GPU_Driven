using Common;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Common.Images;
using Evergine.Common.Graphics;
using Evergine.Common.Graphics.VertexFormats;
using Evergine.Mathematics;
using Buffer = Evergine.Common.Graphics.Buffer;

namespace GPUDriven
{
    public class GPUDriven : VisualTestDefinition
    {
        public const int CubeColumns = 100;
        public const int CubeRows = 100;
        private const float CubeScale = 0.35f;
        private const float CubeSpacing = 1.1f;
        private const float FarPlane = 1000f;

        private VertexPositionColorTexture[] vertexData = new VertexPositionColorTexture[]
        {
            new VertexPositionColorTexture(new Vector3(-1.0f, -1.0f, -1.0f), new Color(1.0f, 0.0f, 0.0f, 1.0f), new Vector2(1, 0)),
            new VertexPositionColorTexture(new Vector3(1.0f,   1.0f, -1.0f), new Color(1.0f, 0.0f, 0.0f, 1.0f), new Vector2(0, 1)),
            new VertexPositionColorTexture(new Vector3(-1.0f,  1.0f, -1.0f), new Color(1.0f, 0.0f, 0.0f, 1.0f), new Vector2(0, 0)),
            new VertexPositionColorTexture(new Vector3(-1.0f, -1.0f, -1.0f), new Color(1.0f, 0.0f, 0.0f, 1.0f), new Vector2(1, 0)),
            new VertexPositionColorTexture(new Vector3(1.0f,  -1.0f, -1.0f), new Color(1.0f, 0.0f, 0.0f, 1.0f), new Vector2(1, 1)),
            new VertexPositionColorTexture(new Vector3(1.0f,   1.0f, -1.0f), new Color(1.0f, 0.0f, 0.0f, 1.0f), new Vector2(0, 1)),

            new VertexPositionColorTexture(new Vector3(-1.0f, -1.0f,  1.0f), new Color(0.0f, 1.0f, 0.0f, 1.0f), new Vector2(1, 0)),
            new VertexPositionColorTexture(new Vector3(-1.0f,  1.0f,  1.0f), new Color(0.0f, 1.0f, 0.0f, 1.0f), new Vector2(0, 0)),
            new VertexPositionColorTexture(new Vector3(1.0f,   1.0f,  1.0f), new Color(0.0f, 1.0f, 0.0f, 1.0f), new Vector2(0, 1)),
            new VertexPositionColorTexture(new Vector3(-1.0f, -1.0f,  1.0f), new Color(0.0f, 1.0f, 0.0f, 1.0f), new Vector2(1, 0)),
            new VertexPositionColorTexture(new Vector3(1.0f,   1.0f,  1.0f), new Color(0.0f, 1.0f, 0.0f, 1.0f), new Vector2(0, 1)),
            new VertexPositionColorTexture(new Vector3(1.0f,  -1.0f,  1.0f), new Color(0.0f, 1.0f, 0.0f, 1.0f), new Vector2(1, 1)),

            new VertexPositionColorTexture(new Vector3(-1.0f, 1.0f, -1.0f), new Color(0.0f, 0.0f, 1.0f, 1.0f), new Vector2(1, 0)),
            new VertexPositionColorTexture(new Vector3(1.0f,  1.0f,  1.0f), new Color(0.0f, 0.0f, 1.0f, 1.0f), new Vector2(0, 1)),
            new VertexPositionColorTexture(new Vector3(-1.0f, 1.0f,  1.0f), new Color(0.0f, 0.0f, 1.0f, 1.0f), new Vector2(0, 0)),
            new VertexPositionColorTexture(new Vector3(-1.0f, 1.0f, -1.0f), new Color(0.0f, 0.0f, 1.0f, 1.0f), new Vector2(1, 0)),
            new VertexPositionColorTexture(new Vector3(1.0f,  1.0f, -1.0f), new Color(0.0f, 0.0f, 1.0f, 1.0f), new Vector2(1, 1)),
            new VertexPositionColorTexture(new Vector3(1.0f,  1.0f,  1.0f), new Color(0.0f, 0.0f, 1.0f, 1.0f), new Vector2(0, 1)),

            new VertexPositionColorTexture(new Vector3(-1.0f, -1.0f, -1.0f), new Color(1.0f, 1.0f, 0.0f, 1.0f), new Vector2(1, 0)),
            new VertexPositionColorTexture(new Vector3(-1.0f, -1.0f,  1.0f), new Color(1.0f, 1.0f, 0.0f, 1.0f), new Vector2(0, 0)),
            new VertexPositionColorTexture(new Vector3(1.0f,  -1.0f,  1.0f), new Color(1.0f, 1.0f, 0.0f, 1.0f), new Vector2(0, 1)),
            new VertexPositionColorTexture(new Vector3(-1.0f, -1.0f, -1.0f), new Color(1.0f, 1.0f, 0.0f, 1.0f), new Vector2(1, 0)),
            new VertexPositionColorTexture(new Vector3(1.0f,  -1.0f,  1.0f), new Color(1.0f, 1.0f, 0.0f, 1.0f), new Vector2(0, 1)),
            new VertexPositionColorTexture(new Vector3(1.0f,  -1.0f, -1.0f), new Color(1.0f, 1.0f, 0.0f, 1.0f), new Vector2(1, 1)),

            new VertexPositionColorTexture(new Vector3(-1.0f, -1.0f, -1.0f), new Color(1.0f, 0.0f, 1.0f, 1.0f), new Vector2(1, 0)),
            new VertexPositionColorTexture(new Vector3(-1.0f,  1.0f,  1.0f), new Color(1.0f, 0.0f, 1.0f, 1.0f), new Vector2(0, 1)),
            new VertexPositionColorTexture(new Vector3(-1.0f, -1.0f,  1.0f), new Color(1.0f, 0.0f, 1.0f, 1.0f), new Vector2(0, 0)),
            new VertexPositionColorTexture(new Vector3(-1.0f, -1.0f, -1.0f), new Color(1.0f, 0.0f, 1.0f, 1.0f), new Vector2(1, 0)),
            new VertexPositionColorTexture(new Vector3(-1.0f,  1.0f, -1.0f), new Color(1.0f, 0.0f, 1.0f, 1.0f), new Vector2(1, 1)),
            new VertexPositionColorTexture(new Vector3(-1.0f,  1.0f,  1.0f), new Color(1.0f, 0.0f, 1.0f, 1.0f), new Vector2(0, 1)),

            new VertexPositionColorTexture(new Vector3(1.0f, -1.0f, -1.0f), new Color(0.0f, 1.0f, 1.0f, 1.0f), new Vector2(1, 0)),
            new VertexPositionColorTexture(new Vector3(1.0f, -1.0f,  1.0f), new Color(0.0f, 1.0f, 1.0f, 1.0f), new Vector2(0, 0)),
            new VertexPositionColorTexture(new Vector3(1.0f,  1.0f,  1.0f), new Color(0.0f, 1.0f, 1.0f, 1.0f), new Vector2(0, 1)),
            new VertexPositionColorTexture(new Vector3(1.0f, -1.0f, -1.0f), new Color(0.0f, 1.0f, 1.0f, 1.0f), new Vector2(1, 0)),
            new VertexPositionColorTexture(new Vector3(1.0f,  1.0f,  1.0f), new Color(0.0f, 1.0f, 1.0f, 1.0f), new Vector2(0, 1)),
            new VertexPositionColorTexture(new Vector3(1.0f,  1.0f, -1.0f), new Color(0.0f, 1.0f, 1.0f, 1.0f), new Vector2(1, 1)),
        };

        private Viewport[] viewports;
        private Rectangle[] scissors;
        private CommandQueue commandQueue;
        private GraphicsPipelineState pipelineState;
        private ComputePipelineState computePipelineState;
        private Buffer[] vertexBuffers;
        private ResourceLayout graphicsResourceLayout;
        private ResourceLayout computeResourceLayout;
        private ResourceSet graphicsResourceSet;
        private ResourceSet computeResourceSet;
        private Buffer viewParamsBuffer;
        private Buffer instanceDataBuffer;
        private Buffer indirectArgsBuffer;
        private int cubeCount;

        private Matrix4x4 view;
        private Matrix4x4 proj;
        private float time;
        private ViewParams viewParams;

        protected override void OnResized(uint width, uint height)
        {
            this.viewports[0] = new Viewport(0, 0, width, height);
            this.scissors[0] = new Rectangle(0, 0, (int)width, (int)height);
            this.proj = Matrix4x4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)width / height, 0.1f, FarPlane, reverseDepthBuffer: true);
        }

        protected override async void InternalLoad()
        {
            var vertexShaderDescription = await this.assetsDirectory.ReadAndCompileShader(this.graphicsContext, "HLSL", "VertexShader", ShaderStages.Vertex, "VS");
            var pixelShaderDescription = await this.assetsDirectory.ReadAndCompileShader(this.graphicsContext, "HLSL", "FragmentShader", ShaderStages.Pixel, "PS");
            var computeShaderDescription = await this.assetsDirectory.ReadAndCompileShader(this.graphicsContext, "CS", "ComputeShader", ShaderStages.Compute, "CS");

            var vertexShader = this.graphicsContext.Factory.CreateShader(ref vertexShaderDescription);
            var pixelShader = this.graphicsContext.Factory.CreateShader(ref pixelShaderDescription);
            var computeShader = this.graphicsContext.Factory.CreateShader(ref computeShaderDescription);

            var expandedVertexData = new VertexData[this.vertexData.Length * CubeColumns * CubeRows];
            for (uint objectIndex = 0; objectIndex < CubeColumns * CubeRows; objectIndex++)
            {
                int objectVertexOffset = (int)objectIndex * this.vertexData.Length;
                for (int vertexIndex = 0; vertexIndex < this.vertexData.Length; vertexIndex++)
                {
                    VertexPositionColorTexture source = this.vertexData[vertexIndex];
                    expandedVertexData[objectVertexOffset + vertexIndex] = new VertexData()
                    {
                        Position = source.Position,
                        Color = source.Color,
                        TexCoord = source.TexCoord,
                        ObjectIndex = objectIndex,
                    };
                }
            }

            var vertexBufferDescription = new BufferDescription((uint)(Unsafe.SizeOf<VertexData>() * expandedVertexData.Length), BufferFlags.VertexBuffer, ResourceUsage.Default);
            var vertexBuffer = this.graphicsContext.Factory.CreateBuffer(expandedVertexData, ref vertexBufferDescription);

            Texture texture2D = null;
            using (var stream = this.assetsDirectory.Open("crate.ktx"))
            {
                if (stream != null)
                {
                    Image image = Image.Load(stream);
                    var textureDescription = image.TextureDescription;
                    texture2D = this.graphicsContext.Factory.CreateTexture(image.DataBoxes, ref textureDescription);
                }
            }

            SamplerStateDescription samplerDescription = SamplerStates.LinearClamp;
            var sampler = this.graphicsContext.Factory.CreateSamplerState(ref samplerDescription);

            this.cubeCount = CubeColumns * CubeRows;

            float cameraDistance = Math.Max(CubeColumns, CubeRows) * CubeSpacing * 1.4f;
            this.view = Matrix4x4.CreateLookAt(new Vector3(0, 0, cameraDistance), new Vector3(0, 0, 0), Vector3.UnitY);
            this.proj = Matrix4x4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)this.frameBuffer.Width / (float)this.frameBuffer.Height, 0.1f, FarPlane, reverseDepthBuffer: true);

            var viewParamsBufferDescription = new BufferDescription((uint)Unsafe.SizeOf<ViewParams>(), BufferFlags.ConstantBuffer, ResourceUsage.Default);
            this.viewParamsBuffer = this.graphicsContext.Factory.CreateBuffer(ref viewParamsBufferDescription);

            var instanceDataBufferDescription = new BufferDescription(
                (uint)(Unsafe.SizeOf<InstanceData>() * this.cubeCount),
                BufferFlags.UnorderedAccess | BufferFlags.BufferStructured | BufferFlags.ShaderResource,
                ResourceUsage.Default,
                ResourceCpuAccess.None,
                Unsafe.SizeOf<InstanceData>());
            this.instanceDataBuffer = this.graphicsContext.Factory.CreateBuffer(ref instanceDataBufferDescription);

            var indirectArgsBufferDescription = new BufferDescription(
                (uint)(Unsafe.SizeOf<IndirectDrawArgs>() * this.cubeCount),
                BufferFlags.IndirectBuffer | BufferFlags.UnorderedAccess | BufferFlags.BufferStructured | BufferFlags.ShaderResource,
                ResourceUsage.Default,
                ResourceCpuAccess.None,
                Unsafe.SizeOf<IndirectDrawArgs>());
            this.indirectArgsBuffer = this.graphicsContext.Factory.CreateBuffer(ref indirectArgsBufferDescription);

            var vertexLayouts = new InputLayouts()
                  .Add(new LayoutDescription()
                      .Add(new ElementDescription(ElementFormat.Float3, ElementSemanticType.Position))
                      .Add(new ElementDescription(ElementFormat.UByte4Normalized, ElementSemanticType.Color))
                      .Add(new ElementDescription(ElementFormat.Float2, ElementSemanticType.TexCoord, 0))
                      .Add(new ElementDescription(ElementFormat.UInt, ElementSemanticType.TexCoord, 1)));

            var graphicsResourceLayoutDescription = new ResourceLayoutDescription(
                    new LayoutElementDescription(0, ResourceType.ConstantBuffer, ShaderStages.Vertex),
                    new LayoutElementDescription(0, ResourceType.StructuredBuffer, ShaderStages.Vertex),
                    new LayoutElementDescription(1, ResourceType.TextureView, ShaderStages.Pixel),
                    new LayoutElementDescription(0, ResourceType.Sampler, ShaderStages.Pixel));
            this.graphicsResourceLayout = this.graphicsContext.Factory.CreateResourceLayout(ref graphicsResourceLayoutDescription);

            var computeResourceLayoutDescription = new ResourceLayoutDescription(
                new LayoutElementDescription(0, ResourceType.ConstantBuffer, ShaderStages.Compute),
                new LayoutElementDescription(0, ResourceType.StructuredBufferReadWrite, ShaderStages.Compute),
                new LayoutElementDescription(1, ResourceType.StructuredBufferReadWrite, ShaderStages.Compute));
            this.computeResourceLayout = this.graphicsContext.Factory.CreateResourceLayout(ref computeResourceLayoutDescription);

            var computePipelineDescription = new ComputePipelineDescription()
            {
                shaderDescription = new ComputeShaderStateDescription()
                {
                    ComputeShader = computeShader
                },
                ResourceLayouts = new[] { this.computeResourceLayout },
                ThreadGroupSizeX = 64,
                ThreadGroupSizeY = 1,
                ThreadGroupSizeZ = 1,
            };
            this.computePipelineState = this.graphicsContext.Factory.CreateComputePipeline(ref computePipelineDescription);

            var pipelineDescription = new GraphicsPipelineDescription()
            {
                PrimitiveTopology = PrimitiveTopology.TriangleList,
                InputLayouts = vertexLayouts,
                ResourceLayouts = new[] { this.graphicsResourceLayout },
                Shaders = new GraphicsShaderStateDescription()
                {
                    VertexShader = vertexShader,
                    PixelShader = pixelShader,
                },
                RenderStates = new RenderStateDescription()
                {
                    RasterizerState = RasterizerStates.CullBack,
                    BlendState = BlendStates.Opaque,
                    DepthStencilState = DepthStencilStates.ReadWrite,
                },
                Outputs = this.frameBuffer.OutputDescription,
            };

            this.pipelineState = this.graphicsContext.Factory.CreateGraphicsPipeline(ref pipelineDescription);
            this.commandQueue = this.graphicsContext.Factory.CreateCommandQueue();

            var swapChainDescription = this.swapChain?.SwapChainDescription;
            var width = swapChainDescription.HasValue ? swapChainDescription.Value.Width : this.surface.Width;
            var height = swapChainDescription.HasValue ? swapChainDescription.Value.Height : this.surface.Height;

            this.viewports = new Viewport[1];
            this.viewports[0] = new Viewport(0, 0, width, height);
            this.scissors = new Rectangle[1];
            this.scissors[0] = new Rectangle(0, 0, (int)width, (int)height);

            this.vertexBuffers = new Buffer[1];
            this.vertexBuffers[0] = vertexBuffer;

            var graphicsResourceSetDescription = new ResourceSetDescription(this.graphicsResourceLayout, this.viewParamsBuffer, this.instanceDataBuffer, texture2D, sampler);
            this.graphicsResourceSet = this.graphicsContext.Factory.CreateResourceSet(ref graphicsResourceSetDescription);

            var computeResourceSetDescription = new ResourceSetDescription(this.computeResourceLayout, this.viewParamsBuffer, this.instanceDataBuffer, this.indirectArgsBuffer);
            this.computeResourceSet = this.graphicsContext.Factory.CreateResourceSet(ref computeResourceSetDescription);

            this.MarkAsLoaded();
        }

        protected override void InternalDrawCallback(TimeSpan gameTime)
        {
            this.time += (float)gameTime.TotalSeconds;
            this.viewParams.ViewProj = Matrix4x4.Multiply(this.view, this.proj);
            this.viewParams.GridInfo0 = new Vector4(this.time, CubeColumns, CubeRows, this.cubeCount);
            this.viewParams.GridInfo1 = new Vector4(CubeScale, CubeSpacing, (CubeColumns - 1) * CubeSpacing, (CubeRows - 1) * CubeSpacing);
            this.viewParams.DrawInfo = new Vector4(this.vertexData.Length, 0, 0, 0);

            var commandBuffer = this.commandQueue.CommandBuffer();

            commandBuffer.Begin();

            commandBuffer.UpdateBufferData(this.viewParamsBuffer, ref this.viewParams);

            commandBuffer.SetComputePipelineState(this.computePipelineState);
            commandBuffer.SetResourceSet(this.computeResourceSet);
            commandBuffer.Dispatch((uint)((this.cubeCount + 63) / 64), 1, 1);
            commandBuffer.ResourceBarrierUnorderedAccessView(this.instanceDataBuffer);
            commandBuffer.ResourceBarrierUnorderedAccessView(this.indirectArgsBuffer);

            RenderPassDescription renderPassDescription = new RenderPassDescription(this.frameBuffer, new ClearValue(ClearFlags.All, 0, 0, Color.CornflowerBlue));
            commandBuffer.BeginRenderPass(ref renderPassDescription);

            commandBuffer.SetViewports(this.viewports);
            commandBuffer.SetScissorRectangles(this.scissors);
            commandBuffer.SetGraphicsPipelineState(this.pipelineState);
            commandBuffer.SetVertexBuffers(this.vertexBuffers);            
                commandBuffer.SetResourceSet(this.graphicsResourceSet);
            uint indirectDrawStride = (uint)Unsafe.SizeOf<IndirectDrawArgs>();
            for (int i = 0; i < this.cubeCount; i++)
            {
                commandBuffer.DrawInstancedIndirect(this.indirectArgsBuffer, indirectDrawStride * (uint)i, 1, indirectDrawStride);
            }

            commandBuffer.EndRenderPass();
            commandBuffer.End();

            commandBuffer.Commit();

            this.commandQueue.Submit();
            this.commandQueue.WaitIdle();
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct ViewParams
        {
            public Matrix4x4 ViewProj;
            public Vector4 GridInfo0;
            public Vector4 GridInfo1;
            public Vector4 DrawInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct VertexData
        {
            public Vector3 Position;
            public Color Color;
            public Vector2 TexCoord;
            public uint ObjectIndex;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct InstanceData
        {
            public Matrix4x4 WorldViewProj;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct IndirectDrawArgs
        {
            public uint VertexCountPerInstance;
            public uint InstanceCount;
            public uint StartVertexLocation;
            public uint StartInstanceLocation;
        }
    }
}
