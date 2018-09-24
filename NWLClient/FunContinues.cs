using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWLClient
{
    class FunContinues
    {
        public enum BlockedApplications
        {
            RegistryEditor,
            TaskManager,
            AddRemovePrograms,
            CommandPrompt,
            ControlPanel
        }

        public enum BlockedFeatures
        {
            ChangePassword,
            LockComputer,
            Logoff,
            RunOnce,
            MSI,
            Desktop,
            AddFromCDorFloppy,
            ManageMyComputerVerb,
            DriveTypeAutoRun
        }

        public static void BlockApplication(BlockedApplications app, bool unblock)
        {
            RegistryKey SoftwareKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser,
                Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32).OpenSubKey("Software", true);
            RegistryKey blockKey;
            switch (app)
            {
                case BlockedApplications.RegistryEditor:
                    blockKey = SoftwareKey.CreateSubKey(@"Microsoft\Windows\CurrentVersion\Policies\System", true);
                    blockKey.SetValue("DisableRegistryTools", unblock ? 0 : 1, RegistryValueKind.DWord);
                    break;
                case BlockedApplications.TaskManager:
                    blockKey = SoftwareKey.CreateSubKey(@"Microsoft\Windows\CurrentVersion\Policies\System", true);
                    blockKey.SetValue("DisableTaskMgr", unblock ? 0 : 1, RegistryValueKind.DWord);
                    break;
                case BlockedApplications.AddRemovePrograms:
                    blockKey = SoftwareKey.CreateSubKey(@"Microsoft\Windows\CurrentVersion\Policies\System", true);
                    blockKey.SetValue("NoAddRemovePrograms", unblock ? 0 : 1, RegistryValueKind.DWord);
                    break;
                case BlockedApplications.CommandPrompt:
                    blockKey = SoftwareKey.CreateSubKey(@"Policies\Microsoft\Windows\System", true);
                    blockKey.SetValue("DisableCMD", unblock ? 0 : 1, RegistryValueKind.DWord);
                    break;
                case BlockedApplications.ControlPanel:
                    blockKey = SoftwareKey.CreateSubKey(@"Microsoft\Windows\CurrentVersion\Policies\Explorer", true);
                    blockKey.SetValue("NoControlPanel", unblock ? 0 : 1, RegistryValueKind.DWord);
                    break;
            }
        }

        public static void BlockFeature(BlockedFeatures feature, bool unblock)
        {
            RegistryKey SoftwareKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser,
                Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32).OpenSubKey("Software", true);
            RegistryKey LMSoftwareKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
                Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32).OpenSubKey("SOFTWARE", true);
            RegistryKey blockKey;
            switch (feature)
            {
                case BlockedFeatures.ChangePassword:
                    blockKey = SoftwareKey.CreateSubKey(@"Microsoft\Windows\CurrentVersion\Policies\System", true);
                    blockKey.SetValue("DisableChangePassword", unblock ? 0 : 1, RegistryValueKind.DWord);
                    break;
                case BlockedFeatures.LockComputer:
                    blockKey = SoftwareKey.CreateSubKey(@"Microsoft\Windows\CurrentVersion\Policies\System", true);
                    blockKey.SetValue("DisableLockWorkstation", unblock ? 0 : 1, RegistryValueKind.DWord);
                    break;
                case BlockedFeatures.Logoff:
                    blockKey = SoftwareKey.CreateSubKey(@"Microsoft\Windows\CurrentVersion\Policies\Explorer", true);
                    blockKey.SetValue("NoLogoff", unblock ? 0 : 1, RegistryValueKind.DWord);
                    break;
                case BlockedFeatures.RunOnce:
                    blockKey = SoftwareKey.CreateSubKey(@"Microsoft\Windows\CurrentVersion\Policies\Explorer", true);
                    blockKey.SetValue("DisableLocalMachineRunOnce", unblock ? 0 : 1, RegistryValueKind.DWord);
                    break;
                case BlockedFeatures.MSI:
                    blockKey = LMSoftwareKey.CreateSubKey(@"Policies\Microsoft\Windows\Installer", true);
                    blockKey.SetValue("DisableMSI", unblock ? 0 : 1, RegistryValueKind.DWord);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
