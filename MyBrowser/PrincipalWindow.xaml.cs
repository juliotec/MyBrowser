using System;
using System.Windows;
using System.Windows.Input;
using CefSharp;

namespace MyBrowser
{
    public partial class PrincipalWindow : Window
    {
        #region Constructores

        public PrincipalWindow()
        {
            InitializeComponent();
        }

        #endregion
        #region Metodos

        private void Limpiar()
        {
            if (_browserChromiumWebBrowser != null && !_browserChromiumWebBrowser.IsDisposed)
            {
                _browserChromiumWebBrowser.Stop();
                _browserChromiumWebBrowser.Dispose();
            }
        }

        private void Navigate()
        {
            if (Uri.TryCreate(_urlTextBox.Text, UriKind.Absolute, out var uri))
            {
                _browserChromiumWebBrowser.Load(uri.AbsoluteUri);
            }
        }

        private void PrincipalWindowClosed(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void BuscarButtonClick(object sender, RoutedEventArgs e)
        {
            Navigate();
        }

        private void UrlTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    Navigate();
                    break;
            }
        }

        #endregion
    }
}
