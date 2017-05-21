using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Consider_About_How_To_Ride_Sister.Model;
using Consider_About_How_To_Ride_Sister.HelpClass;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace Consider_About_How_To_Ride_Sister.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Download : Page
    {
        ObservableCollection<Model.Download> DownloadList = new ObservableCollection<Model.Download>();
        SQLiteHelper sqliteHelper = new SQLiteHelper();
        string Uri;
        public Download()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            DownloadList.Clear();
            DownloadList = sqliteHelper.ReadData(DownloadList);
            Mylistview.ItemsSource = DownloadList;
            if (DownloadList.Count == 0)
            {
                TB.Opacity = 1;
            }
            else
            {
                TB.Opacity = 0;
            }
        }

        private void page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Width.Width = page.ActualWidth - 47;
        }

        private async void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Frame fr = this.Parent as Frame;
                Grid gr1 = e.ClickedItem as Grid;
                Model.Download cl = gr1.DataContext as Model.Download;
                Uri = cl.video_uri;
                string[] str = cl.video_uri.Split('/');
                string filename = str[str.Length - 1];
                string Path = @"C:\Users\Jin\AppData\Local\Packages\39360532-96e5-4fa1-98dd-49dc39195039_bphk9g7bbxc1j\LocalCache\" + filename;
                Uri uri = new Uri(Path);
                if (Grid.GetColumnSpan(fr) == 2)
                {
                    if (this.Frame != null)
                    {
                        fr.Navigate(typeof(SecondFrame));
                        SecondFrame sf1 = fr.Content as SecondFrame;
                        Grid gr2 = sf1.Content as Grid;
                        Grid gr3 = gr2.Children[1] as Grid;
                        MediaElement Player = gr3.Children[0] as MediaElement;
                        Player.Source = uri;
                    }
                    return;
                }
                else
                {
                    Grid gr2 = fr.Parent as Grid;
                    Grid gr3 = gr2.Parent as Grid;
                    MainPage mainpage = gr3.Parent as MainPage;
                    Grid gr4 = mainpage.Content as Grid;
                    Grid gr5 = gr4.Children[0] as Grid;
                    Frame fr1 = gr5.Children[0] as Frame;
                    if (this.Frame != null)
                    {
                        fr1.Navigate(typeof(SecondFrame));
                    }
                    SecondFrame fr2 = fr1.Content as SecondFrame;
                    Grid gr6 = fr2.Content as Grid;
                    Grid gr7 = gr6.Children[1] as Grid;
                    MediaElement player1 = gr7.Children[0] as MediaElement;

                    player1.Source = uri;
                }
            }
            catch (Exception)
            {
                var dialog = new MessageDialog("打开失败", "yoho");
                dialog.Commands.Add(new UICommand("确定", cmd => { }));
                dialog.Commands.Add(new UICommand("重新下载", new UICommandInvokedHandler(this.CommandInvokedHandler)));
                await dialog.ShowAsync();
            }

}
        private async void CommandInvokedHandler(IUICommand command)
        {
            await NetWork.DownLoad.DownLoadVideo(Uri);
            Uri = null;
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                Model.Download Vediodata = button.DataContext as Model.Download;
                sqliteHelper.DeleteData(Vediodata);
                string sql = "delete from sqlite_sequence where name='Download';";
                sqliteHelper.UpadateData(sql);
                DownloadList.Remove(Vediodata);
                if (DownloadList.Count == 0)
                {
                    TB.Opacity = 1;
                }
                else
                {
                    TB.Opacity = 0;
                }
                string FileName = Vediodata.video_uri;
                string[] str = FileName.Split('/');
                string filename = str[str.Length - 1];
                IStorageFolder Folder = ApplicationData.Current.LocalCacheFolder;
                IStorageFile File = await Folder.GetFileAsync(filename);
                await File.DeleteAsync();
            }
            catch (Exception)
            {
                var dialog = new MessageDialog("删除时出错", "yoho");
                dialog.Commands.Add(new UICommand("确定", cmd => { }));
                await dialog.ShowAsync();
            }
        }
    }//左划的动画优化可能不该放这
}
