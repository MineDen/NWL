using System;
using System.Diagnostics;
namespace NWLRunChecker
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            if (Process.GetProcessesByName("NWLClient").Length <= 0)
                try
                {
                    Process.Start(Environment.GetEnvironmentVariable("localappdata") + @"\nwl.exe");
                } catch (Exception) { }
        }
    }
}
