using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Consider_About_How_To_Ride_Sister.Views;
using System.Threading.Tasks;
using Consider_About_How_To_Ride_Sister.NetWork;
using Consider_About_How_To_Ride_Sister.Model;
using Newtonsoft.Json;
using System.Net.Http;
using Windows.UI.Popups;
using System.Collections.ObjectModel;
using Windows.Storage;
using Consider_About_How_To_Ride_Sister.HelpClass;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace Consider_About_How_To_Ride_Sister
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    /// 
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.SizeChanged += MainPage_SizeChanged;
        }

        private async void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            await Task.Delay(500);
            if(e.NewSize.Width<=800)
            {
                SecondFrame.Visibility = Visibility.Collapsed;
                Grid.SetColumnSpan(MyFrame, 2);
                SecondFrame sf = SecondFrame.Content as SecondFrame;
                Grid gr4 = sf.Content as Grid;
                Grid gr5 = gr4.Children[1] as Grid;
                MediaElement player = gr5.Children[0] as MediaElement;
                player.Pause();
            }
            else if(e.NewSize.Width > 800)
            {
                SecondFrame.Visibility = Visibility.Visible;
                Grid.SetColumnSpan(MyFrame, 1);
                //SecondFrame sf = SecondFrame.Content as SecondFrame;
                //Grid gr4 = sf.Content as Grid;
                //Grid gr5 = gr4.Children[1] as Grid;
                //MediaElement player = gr5.Children[0] as MediaElement;
                //if (player.Source!=null)
                //{
                //    player.Play();
                //}
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (HamburgerListBox.SelectedIndex == 0)
            {
                if (this.Frame != null)
                {
                    MyFrame.Navigate(typeof(Home));
                }
            }
            if (HamburgerListBox.SelectedIndex == 1)
            {
                if (this.Frame != null)
                {
                    MyFrame.Navigate(typeof(Views.History));
                }
            }
            if (HamburgerListBox.SelectedIndex == 2)
            {
                if (this.Frame != null)
                {
                    MyFrame.Navigate(typeof(Views.Download));
                }
            }
            if (this.Frame != null)
            {
                SecondFrame.Navigate(typeof(SecondFrame));
            }
        }
        private void HamburgerListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HamburgerListBox.SelectedIndex == 0)
            {
                if (this.Frame != null)
                {
                    MyFrame.Navigate(typeof(Home));
                }
            }
            if (HamburgerListBox.SelectedIndex == 1)
            {
                if (this.Frame != null)
                {
                    MyFrame.Navigate(typeof(Views.History));
                }
            }
            if (HamburgerListBox.SelectedIndex == 2)
            {
                if (this.Frame != null)
                {
                    MyFrame.Navigate(typeof(Views.Download));
                }
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySpiltView.IsPaneOpen = !MySpiltView.IsPaneOpen;
        }
    }
}
