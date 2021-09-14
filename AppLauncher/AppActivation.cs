using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static AppLauncher.NativeApi.InputApi;

namespace AppLauncher
{
    //TODO: dispose
    public class AppActivation
    {
        #region Private Fields

        private static IntPtr mouseHookID = IntPtr.Zero;
        private static IntPtr keyboardHookID = IntPtr.Zero;
        private readonly MouseHookHandlerDelegate mouseHookHandler;
        private readonly KeyboardHookHandlerDelegate keyboardHookHandler;

        private List<Keys> pressedKeys = new List<Keys>();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public AppActivation()
        {
            mouseHookHandler = MouseHookCallback;
            keyboardHookHandler = KeyboardHookCallback;
        }

        #endregion

        #region Public Methods

        public void SetHooks()
        {
            mouseHookID = SetWindowsHookEx(WH_MOUSE_LL, mouseHookHandler, IntPtr.Zero, 0);
            keyboardHookID = SetWindowsHookEx(WH_KEYBOARD_LL, keyboardHookHandler, IntPtr.Zero, 0);
        }

        public void RemoveHooks()
        {
            UnhookWindowsHookEx(mouseHookID);
            UnhookWindowsHookEx(keyboardHookID);
        }

        #endregion

        #region Hook Callbacks

        private IntPtr MouseHookCallback(int nCode, IntPtr wParam, ref MOUSEINPUT lParam)
        {
            if (nCode >= 0 & wParam == (IntPtr)WM_MOUSEMOVE)
            {
                if (lParam.X == 0 && lParam.Y == 0)
                {
                    App.Current.MainWindow.Show();
                }
            }

            return CallNextHookEx(mouseHookID, nCode, wParam, ref lParam);
        }

        private IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, ref KEYBDINPUT lParam)
        {
            if (nCode >= 0 & wParam == (IntPtr)WM_KEYDOWN)
            {
                if (!pressedKeys.Contains((Keys)lParam.Vk))
                {
                    pressedKeys.Add((Keys)lParam.Vk);
                }

                if (pressedKeys.Count < 3)
                {
                    return CallNextHookEx(keyboardHookID, nCode, wParam, ref lParam); ;
                }

                if (pressedKeys.Contains(Keys.LShiftKey) & pressedKeys.Contains(Keys.LControlKey) & pressedKeys[2] == Keys.L)
                {
                    App.Current.MainWindow.Show();
                }
            }
            else if (wParam == (IntPtr)WM_KEYUP)
            {
                pressedKeys.Clear();
            }

            return CallNextHookEx(keyboardHookID, nCode, wParam, ref lParam);
        }

        #endregion
    }
}
