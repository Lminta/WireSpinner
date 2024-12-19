using Diese;
using Diese.Events;
using Diese.Rendering;
using Diese.Utils;

class Program
{
    public class Logger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void LogWarning(string message)
        {
            Console.WriteLine(message);
        }

        public void LogError(string message)
        {
            throw new NotImplementedException();
        }

        public void LogException(Exception exception)
        {
            throw new NotImplementedException();
        }
    }

    public class TestTexture : ITexture
    {
        public uint[] Pixels => CreatePixels(800, 600);
        
        static uint[] CreatePixels(uint width, uint height)
        {
            uint[] screen = new uint[width * height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Example: Create a gradient based on x and y
                    uint a = 255; // Fully opaque
                    uint r = (uint)(x * 255 / width);  // Horizontal gradient for red
                    uint g = (uint)(y * 255 / height); // Vertical gradient for green
                    uint b = 128; // Constant blue

                    screen[y * width + x] = (a << 24) | (r << 16) | (g << 8) | b;
                }
            }
            return screen;
        }
    }
    
    static bool running = false;

    static void Main()
    {
        Console.WriteLine("Testing SDL2.dll load...");
        Controller.Instance.Initialize(new Logger());
        Console.WriteLine("SDL2.dll loaded successfully.");
        
        var window = Diese.Controller.Instance.CreateWindow("SDL2 Example", 100, 100, 800, 600);
        var testTexture = new TestTexture();
        Controller.Instance.AddEventHandler<QuitEvent>(QuitEvent);
        running = true;
        while (running)
        {
            window.RenderTexture(testTexture);
            Controller.Instance.Update();
            Thread.Sleep(50);
        }

        Controller.Instance.RemoveEventHandler<QuitEvent>(QuitEvent);
        Controller.Instance.Quit();
    }

    static void QuitEvent(QuitEvent evt)
    {
        running = false;   
    }
}
