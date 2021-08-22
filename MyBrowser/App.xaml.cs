using System;
using System.Windows;
using System.Diagnostics;
using System.Threading.Tasks;
using CefSharp;
using CefSharp.Wpf;

namespace MyBrowser
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Propiedades

        public static string[] DesktopUserAgents
        {
            get
            {
                if (_desktopUserAgents == null)
                {
                    _desktopUserAgents = new string[]
                    {
                        @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.116 Safari/537.36",
                    };
                }

                return _desktopUserAgents;
            }
        }
        private static string[] _desktopUserAgents;

        public static Random Random
        {
            get
            {
                if (_random == null)
                {
                    _random = new Random();
                }

                return _random;
            }
        }
        private static Random _random;

        #endregion
        #region Metodos

        public static void CerrarCefSharpProcess()
        {
            var processes = Process.GetProcessesByName("CefSharp.BrowserSubprocess");

            foreach (var process in processes)
            {
                try
                {
                    if (!process.HasExited)
                    {
                        process.Kill();
                    }
                }
                catch
                {
                }
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            DispatcherUnhandledException += (o, e2) =>
            {
                Shutdown();
            };
            AppDomain.CurrentDomain.UnhandledException += (o, e2) =>
            {
                Shutdown();
            };
            Dispatcher.UnhandledException += (o, e2) =>
            {
                Shutdown();
            };
            TaskScheduler.UnobservedTaskException += (o, e2) =>
            {
                Shutdown();
            };
            CerrarCefSharpProcess();

            var cefsettings = new CefSettings
            {
                UserAgent = DesktopUserAgents[Random.Next(0, DesktopUserAgents.Length)]
            };

            _ = Cef.Initialize(cefsettings);
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            if (Cef.IsInitialized)
            {
                Cef.Shutdown();
            }
        }


        #endregion
    }
}
