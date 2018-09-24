using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace NWLClient
{
    class ExtraFun
    {
        const int LVM_GETITEMCOUNT = 0x1004;
        const int LVM_SETITEMPOSITION = 0x100F;
        const int LVM_GETITEMPOSITION = 0x1010;

        public static void HideTaskbar()
        {
            IntPtr tb = Helper.GetTaskbarHandle();
            NativeMethods.MoveWindow(tb, 0, 0, 0, 0, true);
        }

        public static void HideStartButton()
        {
            throw new NotImplementedException();
        }

        public static void WindowFun()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                int xd = 2;
                int yd = 2;
                while (true)
                {
                    IntPtr hWnd = NativeMethods.GetForegroundWindow();
                    NativeMethods.RECT winrekt = Helper.GetRekt(hWnd);
                    NativeMethods.MoveWindow(hWnd, winrekt.Left + xd, winrekt.Top + yd, winrekt.Right - winrekt.Left, winrekt.Bottom - winrekt.Top, false);
                    if (winrekt.Right >= SystemParameters.WorkArea.Right)
                        xd = -2;
                    if (winrekt.Left <= SystemParameters.WorkArea.Left)
                        xd = 2;
                    if (winrekt.Bottom >= SystemParameters.WorkArea.Bottom)
                        yd = -2;
                    if (winrekt.Top <= SystemParameters.WorkArea.Top)
                        yd = 2;
                    Thread.Sleep(30);
                }
            }).Start();
        }

        public static void DesktopFun()
        {
            /*IntPtr progman = NativeMethods.FindWindow("Progman", "Program Manager");
            IntPtr defView = NativeMethods.FindWindowEx(progman, IntPtr.Zero, "SHELLDLL_DefView", null);
            IntPtr dListView = NativeMethods.FindWindowEx(defView, IntPtr.Zero, "SysListView32", "FolderView");
            int count = (int)NativeMethods.SendMessage(dListView, LVM_GETITEMCOUNT, IntPtr.Zero, IntPtr.Zero);
            uint dPID = 0;
            NativeMethods.GetWindowThreadProcessId(dListView, ref dPID);
            IntPtr vProcess = NativeMethods.OpenProcess(PROCESS_VM_OPERATION | PROCESS_VM_READ | PROCESS_VM_WRITE, false, vProcessId);
            IntPtr foo = IntPtr.Zero;
            IntPtr vPointer = NativeMethods.VirtualAllocEx(vProcess, IntPtr.Zero, sizeof(uint), MEM_RESERVE | MEM_COMMIT, PAGE_READWRITE);

            for (int j = 0; j < vItemCount; j++)
            {
                byte[] vBuffer = new byte[256];
                LVITEM[] vItem = new LVITEM[1];
                vItem[0].mask = LVIF_TEXT;
                vItem[0].iItem = j;
                vItem[0].iSubItem = 0;
                vItem[0].cchTextMax = vBuffer.Length;
                vItem[0].pszText = (IntPtr)((int)vPointer + Marshal.SizeOf(typeof(LVITEM)));
                uint vNumberOfBytesRead = 0;
                WriteProcessMemory(vProcess, vPointer, Marshal.UnsafeAddrOfPinnedArrayElement(vItem, 0), Marshal.SizeOf(typeof(LVITEM)), ref vNumberOfBytesRead);
                SendMessage(hwndIcon, LVM_GETITEMW, j, vPointer.ToInt32());
                ReadProcessMemory(vProcess, (IntPtr)((int)vPointer + Marshal.SizeOf(typeof(LVITEM))), Marshal.UnsafeAddrOfPinnedArrayElement(vBuffer, 0), vBuffer.Length, out vNumberOfBytesRead);

                // Get the name of the Icon
                vText = Encoding.Unicode.GetString(vBuffer, 0, (int)vNumberOfBytesRead);

                // Get  Icon location
                SendMessage(hwndIcon, LVM_GETITEMPOSITION, j, vPointer.ToInt32());
                Point[] vPoint = new Point[1];
                foo = Marshal.UnsafeAddrOfPinnedArrayElement(vPoint, 0);
                ReadProcessMemory(vProcess, vPointer, Marshal.UnsafeAddrOfPinnedArrayElement(vPoint, 0), Marshal.SizeOf(typeof(Point)), out vNumberOfBytesRead);

                //and ultimaely move icon.
                SendMessage(hwndIcon, LVM_SETITEMPOSITION, j, lParam[0]);*/
                // FUCK TH15 5H1T 1'M OUT
            }
    }
}
