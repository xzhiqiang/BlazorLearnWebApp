using System.Windows;
using WebView2 = Microsoft.Web.WebView2.Wpf.WebView2;

namespace WPFToBlazor
{

    public partial class MainWindow : Window
    {
        WebView2 WebView2 { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            InitializeWebView();
        }

        private async void InitializeWebView()
        {
            WebView2 = new WebView2();
            MainGrid.Children.Add(WebView2);
            await WebView2.EnsureCoreWebView2Async(null);
            WebView2.CoreWebView2.Navigate("http://localhost:5249");
        }

    }
}