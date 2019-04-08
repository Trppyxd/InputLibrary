using System;
using System.Runtime.InteropServices;

namespace InputLibrary
{
    public class Mouse
    {
        #region DLL Imports

        /// <summary>
        /// Mouse event used to manipulate the mouse.
        /// </summary>
        /// <param name="dwFlags"> See <see cref="MouseEventFlags"/> for possible flags. </param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="dwData"></param>
        /// <param name="dwExtraInfo"></param>
        /// <seealso cref="LeftClick"/>
        /// <seealso cref="LeftDown"/>
        /// <seealso cref="LeftUp"/>
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, uint dwData, int dwExtraInfo);

        #region Cursor

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out POINT lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int X, int Y)
            {
                this.X = X;
                this.Y = Y;
            }
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetCursorPos(int X, int Y);

        #endregion

        #endregion

        #region Flags
        [Flags]
        public enum MouseEventFlags
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

        [Flags]
        public enum MouseEventDataXButtons
        {
            XBUTTON1 = 0x00000001,
            XBUTTON2 = 0x00000002
        }

        #endregion

        #region Functions

        #region Helpers

        private static void DoMouseAction(MouseEventFlags flag)
        {
            if (!GetCursorPos(out POINT pos))
                throw new InvalidOperationException("Could not retrieve mouse position.");

            mouse_event((int)flag, pos.X, pos.Y, 0, 0);
        }

        #endregion

        #region LeftMouse

        public static void LeftClick()
        {
            DoMouseAction(MouseEventFlags.LEFTDOWN);
            DoMouseAction(MouseEventFlags.LEFTUP);
        }

        public static void LeftDown()
        {
            DoMouseAction(MouseEventFlags.LEFTDOWN);
        }

        public static void LeftUp()
        {
            DoMouseAction(MouseEventFlags.LEFTUP);
        }

        #endregion

        #region RightMouse

        public static void RightClick()
        {
            DoMouseAction(MouseEventFlags.RIGHTDOWN);
            DoMouseAction(MouseEventFlags.RIGHTUP);
        }

        public static void RightDown()
        {
            DoMouseAction(MouseEventFlags.RIGHTDOWN);
        }

        public static void RightUp()
        {
            DoMouseAction(MouseEventFlags.RIGHTUP);
        }

        #endregion

        #region MiddleMouse

        public static void MiddleClick()
        {
            DoMouseAction(MouseEventFlags.MIDDLEDOWN);
            DoMouseAction(MouseEventFlags.MIDDLEUP);
        }

        public static void MiddleDown()
        {
            DoMouseAction(MouseEventFlags.MIDDLEDOWN);
        }

        public static void MiddleUp()
        {
            DoMouseAction(MouseEventFlags.MIDDLEUP);
        }

        #endregion

        #endregion
    }
}