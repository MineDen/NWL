using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace NWLClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            if (FunBeginsHere.ShouldCopyToAppData())
            {
                FunBeginsHere.CopyToAppData();
                Thread.Sleep(500);
                Process.Start(Environment.GetEnvironmentVariable("localappdata") + @"\nwl.exe");
                Current.Shutdown();
            }
        }
    }
}
