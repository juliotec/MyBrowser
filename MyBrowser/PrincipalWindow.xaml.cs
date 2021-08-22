using System;
using System.Windows;
using CefSharp;
using CefSharp.Wpf;

namespace MyBrowser
{
    public partial class PrincipalWindow : Window
    {
        #region Campos

        private ChromiumWebBrowser _browser;

        #endregion
        #region Constructores

        public PrincipalWindow()
        {
            InitializeComponent();
            IniciarBrowser();
        }

        #endregion
        #region Metodos

        private void IniciarBrowser()
        {
            _browser = new ChromiumWebBrowser()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch
            };
            _window.Content = _browser;
        }

        private void Limpiar()
        {
            if (_browser != null && !_browser.IsDisposed)
            {
                _browser.Stop();
                _browser.Dispose();
            }
        }

        private void PrincipalWindowClosed(object sender, EventArgs e)
        {
            Limpiar();
        }


        #endregion
    }
}
