using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace THelper
{
    public class WinAPI
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        internal static extern void mouse_event(MouseEvent dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

        [DllImport("User32.dll")]
        internal static extern int FindWindow(string ClassName, string WindowName);

        /// <summary>
        ///     "Поднимает" окно на передний план, или делает Focus.
        /// </summary>
        /// <param name="hWnd">HWND окна</param>
        /// <returns></returns>
        [DllImport("User32.dll")]
        public static extern IntPtr SetForegroundWindow(int hWnd);

        /// <summary>
        ///     Выполняет клик мышкой
        /// </summary>
        public static void MouseClick()
        {
            mouse_event(MouseEvent.MOUSEEVENTF_RIGHTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, 0);
            mouse_event(MouseEvent.MOUSEEVENTF_RIGHTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
        }

        /// <summary>
        ///     Проверка на существование окна
        /// </summary>
        /// <param name="name">Имя окна</param>
        /// <returns>Да/Нет</returns>
        public static bool IsWindowOpen(string name)
        {
            var hWnd = FindWindow(null, name);
            if (hWnd > 0)
                return true;
            return false;
        }

        /// <summary>
        ///     "Поднимает" окно на передний план, или делает Focus.
        /// </summary>
        /// <param name="name">Имя окна</param>
        public static void SetForegroundWindow(string name)
        {
            var hWnd = FindWindow(null, name);
            SetForegroundWindow(hWnd);
        }

        internal enum MouseEvent
        {
            MOUSEEVENTF_LEFTDOWN = 0x02,
            MOUSEEVENTF_LEFTUP = 0x04,
            MOUSEEVENTF_RIGHTDOWN = 0x08,
            MOUSEEVENTF_RIGHTUP = 0x10
        }
    }
}