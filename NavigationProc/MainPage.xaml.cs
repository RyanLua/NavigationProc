using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.Input.KeyboardAndMouse;
using Windows.Win32.UI.WindowsAndMessaging;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NavigationProc
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        private WNDPROC? origHotKeyProc;
        private WNDPROC? hotKeyProcD;

        private LRESULT HotKeyProc(HWND hWnd, uint Msg, WPARAM wParam, LPARAM lParam)
        {
            uint WM_HOTKEY = 0x0312; // HotKey Window Message

            if (Msg == WM_HOTKEY)
            {
                if (ToggleButton.IsEnabled)
                {
                    ToggleButton.IsChecked = !ToggleButton.IsChecked;
                }
            }

            return PInvoke.CallWindowProc(origHotKeyProc, hWnd, Msg, wParam, lParam);
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Get window handle
            MainWindow window = App.MainWindow;
            HWND hWnd = new(WinRT.Interop.WindowNative.GetWindowHandle(window));

            // Register hotkey
            int id = 0x0000;
            _ = PInvoke.RegisterHotKey(hWnd, id, HOT_KEY_MODIFIERS.MOD_NOREPEAT, 0x75); // F6

            // Add hotkey function pointer to window procedure
            hotKeyProcD = HotKeyProc;
            IntPtr hotKeyProcPtr = Marshal.GetFunctionPointerForDelegate(hotKeyProcD);
            IntPtr wndPtr = PInvoke.SetWindowLongPtr(hWnd, WINDOW_LONG_PTR_INDEX.GWL_WNDPROC, hotKeyProcPtr);
            origHotKeyProc = Marshal.GetDelegateForFunctionPointer<WNDPROC>(wndPtr);
        }

    }
}
