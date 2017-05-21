using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace Consider_About_How_To_Ride_Sister.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ExtendedSplashScreen : Page
    {
        private SplashScreen splash;
        internal bool dismissed = false;
        internal Rect splashImageRect;
        internal Frame rootFrame;
        public ExtendedSplashScreen(SplashScreen splashscreen, bool loadState)
        {
            this.InitializeComponent();
            Window.Current.SizeChanged += new WindowSizeChangedEventHandler(ExtendedSplashScreen_OnResize);
            splash = splashscreen;
            if (splash != null)
            {
                splash.Dismissed += new TypedEventHandler<SplashScreen, Object>(DismissedEventHandler);
                splashImageRect = splash.ImageLocation;
                PositionImage();
            }
            rootFrame = new Frame();
            Title_SB.Begin();
        }
        void ExtendedSplashScreen_OnResize(Object sender, WindowSizeChangedEventArgs e)
        {
            if (splash != null)
            {
                splashImageRect = splash.ImageLocation;
                PositionImage();
            }
        }
        void DismissedEventHandler(SplashScreen sender, object e)
        {
            dismissed = true;
        }
        void PositionImage()
        {
            extendedSplashImage.SetValue(Canvas.LeftProperty, splashImageRect.X);
            extendedSplashImage.SetValue(Canvas.TopProperty, splashImageRect.Y);
            extendedSplashImage.Height = splashImageRect.Height;
            extendedSplashImage.Width = splashImageRect.Width;
        }
        private async void Title_SB_Completed(object sender, object e)
        {
            await Task.Delay(1500);
            DismissExtendedSplash();
        }
        void DismissExtendedSplash()
        {
            rootFrame.Navigate(typeof(MainPage));
            Window.Current.Content = rootFrame;
        }
    }
}
