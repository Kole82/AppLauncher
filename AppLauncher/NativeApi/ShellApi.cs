using System;
using System.Runtime.InteropServices;

namespace AppLauncher.NativeApi
{
    internal class ShellApi
    {
        #region Enums

        [Flags]
        public enum SIIGBF
        {
            SIIGBF_RESIZETOFIT = 00,
            SIIGBF_BIGGERSIZEOK = 01,
            SIIGBF_MEMORYONLY = 02,
            SIIGBF_ICONONLY = 04,
            SIIGBF_THUMBNAILONLY = 08,
            SIIGBF_INCACHEONLY = 10,
            SIIGBF_CROPTOSQUARE = 20,
            SIIGBF_SCALEUP = 100
        }

        #endregion

        #region Structs

        [StructLayout(LayoutKind.Sequential)]
        public struct SIZE
        {
            public int cx;
            public int cy;

            public SIZE(int x, int y)
            {
                cx = x;
                cy = y;
            }
        }

        #endregion

        #region Interfaces

        [ComImportAttribute()]
        [GuidAttribute("bcc18b79-ba16-442f-80c4-8a59c30c463b")]
        [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IShellItemImageFactory
        {
            void GetImage([In, MarshalAs(UnmanagedType.Struct)] SIZE size,
                          [In] SIIGBF flags,
                          [Out] out IntPtr phbm);
        }

        #endregion

        #region Extern Methods

        [DllImport("shell32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        [return: MarshalAs(UnmanagedType.Interface)]
        public static extern IShellItemImageFactory SHCreateItemFromParsingName([In][MarshalAs(UnmanagedType.LPWStr)] string pszPath,
                                                                                [In] IntPtr pbc,
                                                                                [In][MarshalAs(UnmanagedType.LPStruct)] Guid riid);

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        #endregion
    }
}
