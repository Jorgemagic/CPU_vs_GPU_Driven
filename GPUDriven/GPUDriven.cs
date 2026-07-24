using Common;
using System;
using System.Runtime.CompilerServices;
using Evergine.Common.Graphics;
using Evergine.Common.Graphics.VertexFormats;
using Evergine.Mathematics;
using Buffer = Evergine.Common.Graphics.Buffer;
using Common.Images;

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
            new VertexPositionColorTexture(new Vector3(-1.0f, -1.0f, -1.0f), new Color(1.0f, 0.0f, 0.0f, 1.0f), new Vector2(1, 0)), // Front
            new VertexPositionColorTexture(new Vector3(1.0f,   1.0f, -1.0f), new Color(1.0f, 0.0f, 0.0f, 1.0f), new Vector2(0, 1)),
            new VertexPositionColorTexture(new Vector3(-1.0f,  1.0f, -1.0f), new Color(1.0f, 0.0f, 0.0f, 1.0f), new Vector2(0, 0)),
            new VertexPositionColorTexture(new Vector3(-1.0f, -1.0f, -1.0f), new Color(1.0f, 0.0f, 0.0f, 1.0f), new Vector2(1, 0)),
            new VertexPositionColorTexture(new Vector3(1.0f,  -1.0f, -1.0f), new Color(1.0f, 0.0f, 0.0f, 1.0f), new Vector2(1, 1)),
            new VertexPositionColorTexture(new Vector3(1.0f,   1.0f, -1.0f), new Color(1.0f, 0.0f, 0.0f, 1.0f), new Vector2(0, 1)),

            new VertexPositionColorTexture(new Vector3(-1.0f, -1.0f,  1.0f), new Color(0.0f, 1.0f, 0.0f, 1.0f), new Vector2(1, 0)), // BACK
            new VertexPositionColorTexture(new Vector3(-1.0f,  1.0f,  1.0f), new Color(0.0f, 1.0f, 0.0f, 1.0f), new Vector2(0, 0)),
            new VertexPositionColorTexture(new Vector3(1.0f,   1.0f,  1.0f), new Color(0.0f, 1.0f, 0.0f, 1.0f), new Vector2(0, 1)),
            new VertexPositionColorTexture(new Vector3(-1.0f, -1.0f,  1.0f), new Color(0.0f, 1.0f, 0.0f, 1.0f), new Vector2(1, 0)),
            new VertexPositionColorTexture(new Vector3(1.0f,   1.0f,  1.0f), new Color(0.0f, 1.0f, 0.0f, 1.0f), new Vector2(0, 1)),
            new VertexPositionColorTexture(new Vector3(1.0f,  -1.0f,  1.0f), new Color(0.0f, 1.0f, 0.0f, 1.0f), new Vector2(1, 1)),

            new VertexPositionColorTexture(new Vector3(-1.0f, 1.0f, -1.0f), new Color(0.0f, 0.0f, 1.0f, 1.0f), new Vector2(1, 0)), // Top
            new VertexPositionColorTexture(new Vector3(1.0f,  1.0f,  1.0f), new Color(0.0f, 0.0f, 1.0f, 1.0f), new Vector2(0, 1)),
            new VertexPositionColorTexture(new Vector3(-1.0f, 1.0f,  1.0f), new Color(0.0f, 0.0f, 1.0f, 1.0f), new Vector2(0, 0)),
            new VertexPositionColorTexture(new Vector3(-1.0f, 1.0f, -1.0f), new Color(0.0f, 0.0f, 1.0f, 1.0f), new Vector2(1, 0)),
            new VertexPositionColorTexture(new Vector3(1.0f,  1.0f, -1.0f), new Color(0.0f, 0.0f, 1.0f, 1.0f), new Vector2(1, 1)),
            new VertexPositionColorTexture(new Vector3(1.0f,  1.0f,  1.0f), new Color(0.0f, 0.0f, 1.0f, 1.0f), new Vector2(0, 1)),

            new VertexPositionColorTexture(new Vector3(-1.0f, -1.0f, -1.0f), new Color(1.0f, 1.0f, 0.0f, 1.0f), new Vector2(1, 0)), // Bottom
            new VertexPositionColorTexture(new Vector3(-1.0f, -1.0f,  1.0f), new Color(1.0f, 1.0f, 0.0f, 1.0f), new Vector2(0, 0)),
            new VertexPositionColorTexture(new Vector3(1.0f,  -1.0f,  1.0f), new Color(1.0f, 1.0f, 0.0f, 1.0f), new Vector2(0, 1)),
            new VertexPositionColorTexture(new Vector3(-1.0f, -1.0f, -1.0f), new Color(1.0f, 1.0f, 0.0f, 1.0f), new Vector2(1, 0)),
            new VertexPositionColorTexture(new Vector3(1.0f,  -1.0f,  1.0f), new Color(1.0f, 1.0f, 0.0f, 1.0f), new Vector2(0, 1)),
            new VertexPositionColorTexture(new Vector3(1.0f,  -1.0f, -1.0f), new Color(1.0f, 1.0f, 0.0f, 1.0f), new Vector2(1, 1)),

            new VertexPositionColorTexture(new Vector3(-1.0f, -1.0f, -1.0f), new Color(1.0f, 0.0f, 1.0f, 1.0f), new Vector2(1, 0)), // Left
            new VertexPositionColorTexture(new Vector3(-1.0f,  1.0f,  1.0f), new Color(1.0f, 0.0f, 1.0f, 1.0f), new Vector2(0, 1)),
            new VertexPositionColorTexture(new Vector3(-1.0f, -1.0f,  1.0f), new Color(1.0f, 0.0f, 1.0f, 1.0f), new Vector2(0, 0)),
            new VertexPositionColorTexture(new Vector3(-1.0f, -1.0f, -1.0f), new Color(1.0f, 0.0f, 1.0f, 1.0f), new Vector2(1, 0)),
            new VertexPositionColorTexture(new Vector3(-1.0f,  1.0f, -1.0f), new Color(1.0f, 0.0f, 1.0f, 1.0f), new Vector2(1, 1)),
            new VertexPositionColorTexture(new Vector3(-1.0f,  1.0f,  1.0f), new Color(1.0f, 0.0f, 1.0f, 1.0f), new Vector2(0, 1)),

            new VertexPositionColorTexture(new Vector3(1.0f, -1.0f, -1.0f), new Color(0.0f, 1.0f, 1.0f, 1.0f), new Vector2(1, 0)), // Right
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
        private Buffer[] vertexBuffers;
        private ResourceLayout resourceLayout;
        private ResourceSet resourceSet;
        private Buffer constantBuffer;
        private Matrix4x4[] worldViewProjMatrices;
        private uint constantBufferElementSize;
        private uint constantBufferStride;
        private int cubeCount;

        private Matrix4x4 view;
        private Matrix4x4 proj;
        private float time;

        public GPUDriven()
        {
        }

        protected override void OnResized(uint width, uint height)
        {
            this.viewports[0] = new Viewport(0, 0, width, height);
            this.scissors[0] = new Rectangle(0, 0, (int)width, (int)height);
            this.proj = Matrix4x4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)width / height, 0.1f, FarPlane, reverseDepthBuffer: true);
        }

        protected override async void InternalLoad()
        {
            // Compile Vertex and Pixel shaders
            var vertexShaderDescription = await this.assetsDirectory.ReadAndCompileShader(this.graphicsContext, "HLSL", "VertexShader", ShaderStages.Vertex, "VS");
            var pixelShaderDescription = await this.assetsDirectory.ReadAndCompileShader(this.graphicsContext, "HLSL", "FragmentShader", ShaderStages.Pixel, "PS");

            var vertexShader = this.graphicsContext.Factory.CreateShader(ref vertexShaderDescription);
            var pixelShader = this.graphicsContext.Factory.CreateShader(ref pixelShaderDescription);

            var vertexBufferDescription = new BufferDescription((uint)(Unsafe.SizeOf<VertexPositionColorTexture>() * this.vertexData.Length), BufferFlags.VertexBuffer, ResourceUsage.Default);
            var vertexBuffer = this.graphicsContext.Factory.CreateBuffer(this.vertexData, ref vertexBufferDescription);

            // Create Texture from file
            Texture texture2D = null;
            using (var stream = this.assetsDirectory.Open("crate.ktx"))
            {
                if (stream != null)
                {
                    Image image = Image.Load(stream);
                    var textureDescription = image.TextureDescription;
                    texture2D = graphicsContext.Factory.CreateTexture(image.DataBoxes, ref textureDescription);
                }
            }

            SamplerStateDescription samplerDescription = SamplerStates.LinearClamp;
            var sampler = this.graphicsContext.Factory.CreateSamplerState(ref samplerDescription);

            this.cubeCount = CubeColumns * CubeRows;
            this.worldViewProjMatrices = new Matrix4x4[this.cubeCount];

            float cameraDistance = Math.Max(CubeColumns, CubeRows) * CubeSpacing * 1.4f;
            this.view = Matrix4x4.CreateLookAt(new Vector3(0, 0, cameraDistance), new Vector3(0, 0, 0), Vector3.UnitY);
            this.proj = Matrix4x4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)this.frameBuffer.Width / (float)this.frameBuffer.Height, 0.1f, FarPlane, reverseDepthBuffer: true);

            // Constant Buffer
            this.constantBufferElementSize = (uint)Unsafe.SizeOf<Matrix4x4>();
            uint constantBufferAlignment = 256;
            this.constantBufferStride = ((this.constantBufferElementSize + (constantBufferAlignment - 1)) / constantBufferAlignment) * constantBufferAlignment;
            var constantBufferDescription = new BufferDescription(
                this.constantBufferStride * (uint)this.cubeCount,
                BufferFlags.ConstantBuffer,
                ResourceUsage.Dynamic,
                ResourceCpuAccess.Write,
                0);
            this.constantBuffer = this.graphicsContext.Factory.CreateBuffer(ref constantBufferDescription);

            // Prepare Pipeline
            var vertexLayouts = new InputLayouts()
                  .Add(VertexPositionColorTexture.VertexFormat);

            var resourceLayoutDescription = new ResourceLayoutDescription(
                    new LayoutElementDescription(0, ResourceType.ConstantBuffer, ShaderStages.Vertex, true, this.constantBufferElementSize),
                    new LayoutElementDescription(0, ResourceType.TextureView, ShaderStages.Pixel),
                    new LayoutElementDescription(0, ResourceType.Sampler, ShaderStages.Pixel));

            this.resourceLayout = this.graphicsContext.Factory.CreateResourceLayout(ref resourceLayoutDescription);

            var pipelineDescription = new GraphicsPipelineDescription()
            {
                PrimitiveTopology = PrimitiveTopology.TriangleList,
                InputLayouts = vertexLayouts,
                ResourceLayouts = new[] { this.resourceLayout },
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

            var resourceSetDescription = new ResourceSetDescription(this.resourceLayout, this.constantBuffer, texture2D, sampler);
            this.resourceSet = this.graphicsContext.Factory.CreateResourceSet(ref resourceSetDescription);

            this.MarkAsLoaded();
        }

        protected override void InternalDrawCallback(TimeSpan gameTime)
        {
            // Update
            this.time += (float)gameTime.TotalSeconds;
            var viewProj = Matrix4x4.Multiply(this.view, this.proj);
            this.UpdateWorldViewProjMatrices(viewProj);

            // Draw
            var commandBuffer = this.commandQueue.CommandBuffer();

            commandBuffer.Begin();

            for (int i = 0; i < this.cubeCount; i++)
            {
                commandBuffer.UpdateBufferData(this.constantBuffer, ref this.worldViewProjMatrices[i], this.constantBufferStride * (uint)i);
            }

            RenderPassDescription renderPassDescription = new RenderPassDescription(this.frameBuffer, new ClearValue(ClearFlags.All, 0, 0, Color.CornflowerBlue));
            commandBuffer.BeginRenderPass(ref renderPassDescription);

            commandBuffer.SetViewports(this.viewports);
            commandBuffer.SetScissorRectangles(this.scissors);

            commandBuffer.SetGraphicsPipelineState(this.pipelineState);
            commandBuffer.SetVertexBuffers(this.vertexBuffers);

            for (int i = 0; i < this.cubeCount; i++)
            {
                commandBuffer.SetResourceSet(this.resourceSet, 0, new uint[] { this.constantBufferStride * (uint)i });
                commandBuffer.Draw((uint)this.vertexData.Length);
            }

            commandBuffer.EndRenderPass();
            commandBuffer.End();

            commandBuffer.Commit();

            this.commandQueue.Submit();
            this.commandQueue.WaitIdle();
        }

        private void UpdateWorldViewProjMatrices(Matrix4x4 viewProj)
        {
            float totalWidth = (CubeColumns - 1) * CubeSpacing;
            float totalHeight = (CubeRows - 1) * CubeSpacing;
            Matrix4x4 rotation = Matrix4x4.CreateRotationX(this.time) *
                                 Matrix4x4.CreateRotationY(this.time * 2) *
                                 Matrix4x4.CreateRotationZ(this.time * .7f);

            int index = 0;
            for (int y = 0; y < CubeRows; y++)
            {
                float positionY = (y * CubeSpacing) - (totalHeight * 0.5f);

                for (int x = 0; x < CubeColumns; x++)
                {
                    float positionX = (x * CubeSpacing) - (totalWidth * 0.5f);
                    Matrix4x4 world = Matrix4x4.CreateScale(CubeScale) *
                                      rotation *
                                      Matrix4x4.CreateTranslation(positionX, positionY, 0);

                    this.worldViewProjMatrices[index++] = world * viewProj;
                }
            }
        }
    }
}
