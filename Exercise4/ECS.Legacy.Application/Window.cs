namespace ECS.Legacy.Application
{
    public class Window : IWindow
    {
        public void OpenWindow()
        {
            System.Console.WriteLine("Window is open");
        }

        public void CloseWindow()
        {
            System.Console.WriteLine("Window is closed");
        }

    }
}