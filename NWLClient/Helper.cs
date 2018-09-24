using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace NWLClient
{
    class Helper
    {
        public static NativeMethods.RECT GetRekt(IntPtr hWnd)
        {
            NativeMethods.RECT rekt = new NativeMethods.RECT();
            NativeMethods.GetWindowRect(hWnd, ref rekt);
            return rekt;
        }

        public static IntPtr GetTaskbarHandle()
        {
            return NativeMethods.FindWindow("Shell_traywnd", "");
        }

        /// <summary>
        /// Returns a list of child windows
        /// </summary>
        /// <param name="parent">Parent of the windows to return</param>
        /// <returns>List of child windows</returns>
        public static List<IntPtr> GetChildWindows(IntPtr parent)
        {
            List<IntPtr> result = new List<IntPtr>();
            GCHandle listHandle = GCHandle.Alloc(result);
            try
            {
                NativeMethods.EnumWindowsProc childProc = new NativeMethods.EnumWindowsProc(EnumWindow);
                NativeMethods.EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }
            return result;
        }

        /// <summary>
        /// Callback method to be used when enumerating windows.
        /// </summary>
        /// <param name="handle">Handle of the next window</param>
        /// <param name="pointer">Pointer to a GCHandle that holds a reference to the list to fill</param>
        /// <returns>True to continue the enumeration, false to bail</returns>
        private static bool EnumWindow(IntPtr handle, IntPtr pointer)
        {
            GCHandle gch = GCHandle.FromIntPtr(pointer);
            List<IntPtr> list = gch.Target as List<IntPtr>;
            if (list == null)
            {
                throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
            }
            list.Add(handle);
            //  You can modify this to check to see if you want to cancel the operation, then return a null here
            return true;
        }

        public static string GetWindowTitle(IntPtr hWnd)
        {
            int capacity = NativeMethods.GetWindowTextLength(hWnd) * 2;
            StringBuilder stringBuilder = new StringBuilder(capacity);
            NativeMethods.GetWindowText(hWnd, stringBuilder, stringBuilder.Capacity);
            return stringBuilder.ToString();
        }

        public static string GetWindowClass(IntPtr hWnd)
        {
            StringBuilder stringBuilder = new StringBuilder(256);
            NativeMethods.GetClassName(hWnd, stringBuilder, stringBuilder.Capacity);
            return stringBuilder.ToString();
        }
    }
}
