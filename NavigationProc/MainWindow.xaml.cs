using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using WinUIEx;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NavigationProc
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            // Set up window title bar
            ExtendsContentIntoTitleBar = true;

            // Set up frame
            _ = Frame.Navigate(typeof(MainPage));
            Frame.Navigated += OnNavigated;

            // Set up back button
            AppTitleBar.IsBackButtonVisible = false;
            AppTitleBar.BackRequested += BackRequested;
        }

        private void BackRequested(TitleBar sender, object args)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            AppTitleBar.IsBackButtonVisible = e.SourcePageType != typeof(MainPage);
        }
    }
}
