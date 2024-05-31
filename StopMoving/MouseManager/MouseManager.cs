using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StopMoving.MouseManager.Interop;

namespace StopMoving.MouseManager
{
    public class MouseManager
    {
        public MouseInteropManager.POINT MousePosition { get; private set; }

        public bool KeepMouseFreeze { get; set; }

        public void RegisterMousePositon()
        {
            MousePosition = MouseInteropManager.GetCursorPosition();
        }

        private void ChangeMousePosition()
        {
            MouseInteropManager.SetCursorPos(MousePosition.x, MousePosition.y);
        }

        public void StopFreezingMouse()
        {
            KeepMouseFreeze = false;
        }

        public void ResetMousePosition(Task task)
        {
            task.Dispose();

            KeepMouseFreeze = true;

            ChangeMousePosition();

            if (KeepMouseFreeze)
            {
                Task.Delay(100).ContinueWith(ResetMousePosition);
            }
        }
    }
}
