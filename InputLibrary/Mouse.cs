using System;
using System.Runtime.InteropServices;

namespace InputLibrary
{
    public class Mouse
    {
        #region DLL Imports

        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, int dwExtraInfo);

        #endregion

        #region Flags
        [Flags]
        public enum MouseEventFlags : uint
        {
            ABSOLUTE = 0x8000,
            LEFTDOWN = 0x0002,
            LEFTUP = 0x0004,
            MIDDLEDOWN = 0x0020,
            MIDDLEUP = 0x0040,
            MOVE = 0x0001,
            RIGHTDOWN = 0x0008,
            RIGHTUP = 0x0010,
            XDOWN = 0x0080,
            XUP = 0x0100,
            WHEEL = 0x0800,
            HWHEEL = 0x01000
        }

        public enum MouseEventDataXButtons : uint
        {
            XBUTTON1 = 0x00000001,
            XBUTTON2 = 0x00000002
        }

        #endregion

        #region Functions

        public static void Click()
        {
            mouse_event((int)MouseEventFlags.LEFTDOWN, 0, 0, 0, 0);
            mouse_event((int)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
        }

        public static void LeftDown()
        {
            mouse_event((int)MouseEventFlags.LEFTDOWN, 0, 0, 0, 0);
        }

        public static void LeftUp()
        {
            mouse_event((int)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
        }

        public static void RightDown()
        {
            mouse_event((int)MouseEventFlags.RIGHTDOWN, 0, 0, 0, 0);
        }

        public static void RightUp()
        {
            mouse_event((int)MouseEventFlags.RIGHTUP, 0, 0, 0, 0);
        }

        #endregion
    }
}