using Evergine.Common.Graphics;

namespace CPUDriven
{
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args = null)
        {
            uint width = 1280;
            uint height = 720;
            bool Windowed = true;
            bool VSync = false;

            using (var test = new CPUDriven())
            {
                GraphicsBackend preferredBackend = GraphicsBackend.DirectX12;
                if (args?.Length > 1)
                {
                    bool parsed = Enum.TryParse(args[1], out preferredBackend);
                }

                test.Initialize(preferredBackend);

                // Create Window
                string windowsTitle = $"{typeof(CPUDriven).Name}";
                var windowSystem = test.WindowSystem;
                var window = windowSystem.CreateWindow(windowsTitle, width, height);
                test.Surface = window;
                test.FPSUpdateCallback = (fpsString) =>
                {
                    window.Title = $"{windowsTitle}  {fpsString}";
                };

                // Managers
                var swapChainDescriptor = test.CreateSwapChainDescription(window.Width, window.Height, test.SwapChainPixelFormat, Windowed);
                swapChainDescriptor.SurfaceInfo = window.SurfaceInfo;
                

                var graphicsContext = test.CreateGraphicsContext(swapChainDescriptor, preferredBackend, VSync);
                windowsTitle = $"{windowsTitle} [{graphicsContext.BackendType}]";

                test.Run();
            }
        }
    }
}
