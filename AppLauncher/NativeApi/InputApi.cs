using System;
using System.Runtime.InteropServices;

namespace AppLauncher.NativeApi
{
    internal class InputApi
    {
        #region Constants

        internal const int WH_KEYBOARD_LL = 13;
        internal const int WH_MOUSE_LL = 14;

        internal const int WM_KEYUP = 0x0101;
        internal const int WM_KEYDOWN = 0x0100;
        internal const int WM_MOUSEMOVE = 0x0200;

        #endregion

        #region Delegates

        internal delegate IntPtr MouseHookHandlerDelegate(int nCode, IntPtr wParam, ref MOUSEINPUT lParam);
        internal delegate IntPtr KeyboardHookHandlerDelegate(int nCode, IntPtr wParam, ref KEYBDINPUT lParam);

        #endregion

        #region Extern Methods

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr SetWindowsHookEx(int idHook, MouseHookHandlerDelegate lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr SetWindowsHookEx(int idHook, KeyboardHookHandlerDelegate lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, ref MOUSEINPUT lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, ref KEYBDINPUT lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnhookWindowsHookEx(IntPtr hhk);

        #endregion

        #region Structs

        [StructLayout(LayoutKind.Sequential)]
        internal struct KEYBDINPUT
        {
            public ushort Vk;
            public ushort Scan;
            public uint Flags;
            public uint Time;
            public IntPtr ExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct MOUSEINPUT
        {
            public int X;
            public int Y;
            public uint MouseData;
            public uint Flags;
            public uint Time;
            public IntPtr ExtraInfo;
        }

        #endregion
    }
}
