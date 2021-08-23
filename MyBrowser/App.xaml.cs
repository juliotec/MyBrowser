using System;
using System.Windows;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;
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

        public static string[] UserAgents
        {
            get
            {
                if (_userAgents == null)
                {
                    _userAgents = File.ReadAllLines("UserAgents.txt");
                }

                return _userAgents;
            }
        }
        private static string[] _userAgents;

        public static string UserAgentRandom
        {
            get
            {
                if (string.IsNullOrEmpty(_userAgentRandom))
                {
                    _userAgentRandom = UserAgents[Random.Next(0, UserAgents.Length)];
                }

                return _userAgentRandom;
            }
        }
        private static string _userAgentRandom;

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
                    process.Kill(true);
                    process.WaitForExit();
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
                UserAgent = UserAgents[Random.Next(0, UserAgents.Length)]
            };

            Cef.Initialize(cefsettings);
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (Cef.IsInitialized)
            {
                Cef.Shutdown();
            }

            base.OnExit(e);
        }


        #endregion
    }
}
