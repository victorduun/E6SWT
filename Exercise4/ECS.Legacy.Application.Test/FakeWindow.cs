using System;
using System.Collections.Generic;
using System.Text;

namespace ECS.Legacy.Application.Test
{
    public class FakeWindow : IWindow
    {
        public bool IsOpen = false;
        public void OpenWindow()
        {
            IsOpen = true;
        }

        public void CloseWindow()
        {
            IsOpen = false;
        }
    }
}
