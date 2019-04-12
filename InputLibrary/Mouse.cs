using System;
using System.Runtime.InteropServices;
using System.Threading;

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
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, uint dwExtraInfo);

        #region DLL Cursor

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        /// <see cref="Move"/>
        /// <seealso cref="MoveSmooth"/>
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

        private static void DoMouseAction(MouseEventFlags flag, int x, int y)
        {
            mouse_event((int)flag, x, y, 0, 0);
        }

        private static void DoMouseAction(MouseEventFlags flag, int data)
        {
            if (!GetCursorPos(out POINT pos))
                throw new InvalidOperationException("Could not retrieve mouse position.");

            mouse_event((int)flag, pos.X, pos.Y, 0, 0);
        }

        private static void DoMouseAction(MouseEventFlags flag, MouseEventDataXButtons XButton)
        {
            if (!GetCursorPos(out POINT pos))
                throw new InvalidOperationException("Could not retrieve mouse position.");

            mouse_event((int)flag, pos.X, pos.Y, (int)XButton, 0);
        }

        #endregion

        #region Cursor

        /// <summary>
        /// Sets the cursor to the provided <paramref name="x"/> and <paramref name="y"/>.
        /// The x:0, y:0 point is the current Cursor location.
        /// </summary>
        /// <param name="x">X coordinate relative to current pos.</param>
        /// <param name="y">Y coordinate relative to current pos.</param>
        /// <see cref="SetCursorPos"/>
        /// <seealso cref="MoveSmooth"/>
        public static void Move(int x, int y)
        {
            DoMouseAction(MouseEventFlags.MOVE, x, y);
        }

        public static void MoveSmooth(int x, int y)
        {
            if (!GetCursorPos(out POINT pos))
                throw new InvalidOperationException("Could not retrieve mouse position.");

            var posX = pos.X;
            var posY = pos.Y;
            var destPosX = pos.X + x;
            var destPosY = pos.Y + y;

            while (posX != destPosX && posY != destPosY)
            {
                if (posX < destPosX)
                {
                    posX++;
                }
                if (posY < destPosY)
                {
                    posY++;
                }

                Console.WriteLine($"x:{posX} - y:{posY}");
                SetCursorPos(posX, posY);
                Thread.Sleep(5); // Time between each pixel movement in ms.
            }
        }

        #endregion

        #region LeftMouse

        public static void LeftClick()
        {
            LeftDown();
            LeftUp();
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
            RightDown();
            RightUp();
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
            MiddleDown();
            MiddleUp();
        }

        public static void MiddleDown()
        {
            DoMouseAction(MouseEventFlags.MIDDLEDOWN);
        }

        public static void MiddleUp()
        {
            DoMouseAction(MouseEventFlags.MIDDLEUP);
        }

        /// <summary>
        /// Simulates scrolling from -100 to 100 ticks.
        /// </summary>
        /// <param name="increment">From -100 to 100 ticks.</param>
        public static void Scroll(int increment)
        {
            if (increment > 100 || increment < -100)
                throw new ArgumentOutOfRangeException(nameof(increment), "Scroll increment out of range (100 to -100)");

            DoMouseAction(MouseEventFlags.WHEEL, increment);
        }

        #endregion

        #region SideMouse

        public static void SideFrontClick()
        {
            SideFrontDown();
            SideFrontUp();
        }
        public static void SideFrontDown()
        {
            DoMouseAction(MouseEventFlags.XDOWN, MouseEventDataXButtons.XBUTTON1);
        }
        public static void SideFrontUp()
        {
            DoMouseAction(MouseEventFlags.XUP, MouseEventDataXButtons.XBUTTON1);
        }

        public static void SideBackClick()
        {
            SideBackDown();
            SideBackUp();
        }
        public static void SideBackDown()
        {
            DoMouseAction(MouseEventFlags.XDOWN, MouseEventDataXButtons.XBUTTON2);
        }
        public static void SideBackUp()
        {
            DoMouseAction(MouseEventFlags.XUP, MouseEventDataXButtons.XBUTTON2);
        }

        #endregion

        #endregion
    }
}