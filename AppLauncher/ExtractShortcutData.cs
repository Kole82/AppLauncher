using IWshRuntimeLibrary;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static AppLauncher.NativeApi.ShellApi;

namespace AppLauncher
{
    public class ExtractShortcutData
    {
        #region Public Methods

        public static string GetName(string path)
        {
            string extension = Path.GetExtension(path);

            return extension != string.Empty ? Path.GetFileName(path).Replace(extension, string.Empty) : Path.GetFileName(path);
        }

        public static string GetFullPath(string path)
        {
            string extension = Path.GetExtension(path);

            return extension == ".lnk" ? GetShortcutTargetPath(path) : Path.GetFullPath(path);
        }

        public static byte[] GetIcon(string path)
        {
            IShellItemImageFactory isiif = null;
            Guid iid = typeof(IShellItemImageFactory).GUID;

            try
            {
                isiif = SHCreateItemFromParsingName(path, IntPtr.Zero, iid);
                if (isiif == null) return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to obtain the IShellItemImageFactory interface for " + path, e);
                return null;
            }

            SIIGBF flags = SIIGBF.SIIGBF_BIGGERSIZEOK;
            IntPtr hBitmap = IntPtr.Zero;

            try
            {
                isiif.GetImage(new SIZE(64, 64), flags, out hBitmap);
            }
            catch (Exception e)
            {
                Console.WriteLine("GetImage call failed for " + path, e);
            }

            Marshal.FinalReleaseComObject(isiif);

            if (hBitmap == IntPtr.Zero) return null;

            ImageSource imgSource = null;

            try
            {
                imgSource = Imaging.CreateBitmapSourceFromHBitmap(hBitmap,
                                                                  IntPtr.Zero,
                                                                  System.Windows.Int32Rect.Empty,
                                                                  BitmapSizeOptions.FromEmptyOptions());

                imgSource.Freeze();
            }

            catch (Exception ex)
            {
                Console.WriteLine("GetIcon Failed", ex);
            }

            DeleteObject(hBitmap);

            return GetByteArray(imgSource);
        }

        #endregion

        #region Private Helpers

        private static string GetShortcutTargetPath(string shortcutPath)
        {
            WshShell shell = new WshShell();
            IWshShortcut shortcut = null;
            shortcut = shell.CreateShortcut(shortcutPath);

            return shortcut.TargetPath;
        }

        private static byte[] GetByteArray(ImageSource imageSource)
        {
            byte[] bytes = null;
            var bitmapSource = imageSource as BitmapSource;
            BitmapEncoder encoder = new PngBitmapEncoder();

            if (bitmapSource != null)
            {
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    bytes = stream.ToArray();
                }
            }

            return bytes;
        }

        #endregion
    }
}
