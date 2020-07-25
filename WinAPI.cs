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
        ///     Выполняет клик мышкой в текущих координатах
        /// </summary>
        public static void MouseClick()
        {
            mouse_event(MouseEvent.MOUSEEVENTF_RIGHTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, 0);
            mouse_event(MouseEvent.MOUSEEVENTF_RIGHTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
        }

        /// <summary>
        ///     Проверка на существование открытого окна
        /// </summary>
        /// <param name="tittle">Заголовок окна</param>
        /// <returns>Да/Нет</returns>
        public static bool IsWindowOpen(string tittle)
        {
            var hWnd = FindWindow(null, tittle);
            if (hWnd > 0)
                return true;
            return false;
        }

        /// <summary>
        ///     "Поднимает" окно на передний план (делает Focus).
        /// </summary>
        /// <param name="tittle">Заголовок окна</param>
        public static void SetForegroundWindow(string tittle)
        {
            var hWnd = FindWindow(null, tittle);
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