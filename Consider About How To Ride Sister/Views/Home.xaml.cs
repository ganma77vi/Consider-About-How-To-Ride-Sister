using Consider_About_How_To_Ride_Sister.HelpClass;
using Consider_About_How_To_Ride_Sister.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
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
    public sealed partial class Home : Page
    {
        ViewModel.HomeViewModel viewmodel;
        ObservableCollection<Contentlist> templist;
        ObservableCollection<Contentlist> templist1;
        Rootobject VideoResult;
        bool IsLoading = false;
        object o = new object();
        int count;
        SQLiteHelper sqliteHelper = new SQLiteHelper();
        ScrollViewer sc;
        SQLiteHelper1 sqliteHelper1 = new SQLiteHelper1();
        public Home()
        {
            this.InitializeComponent();
            viewmodel = new ViewModel.HomeViewModel();
            this.DataContext = viewmodel;
        }
        private void MyListView_Loaded(object sender, RoutedEventArgs e)
        {
            Get_Child((DependencyObject)sender);
            sc.ViewChanged += Sc_ViewChanged;
        }
        private void Get_Child(DependencyObject o)
        {
            try
            {
                int count = VisualTreeHelper.GetChildrenCount(o);
                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var child = VisualTreeHelper.GetChild(o, count - 1);
                        if (child is ScrollViewer)
                        {
                            sc = child as ScrollViewer;
                        }
                        else
                        {
                            Get_Child(VisualTreeHelper.GetChild(o, count - 1));
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            InternetStaus.Text=InternetStatus.GetConnectionGeneration();
            base.OnNavigatedTo(e);
            await FirstStep();
        }
        private async Task FirstStep()
        {
            try
            {
                templist = new ObservableCollection<Contentlist>();
                templist1 = new ObservableCollection<Contentlist>();
                count = 1;
                string json;
                json = await NetWork.HttpRequest.GetVideo(1);
                json = json.Replace("\\n", "");
                json = json.Replace(" ", "");
                if (!string.IsNullOrWhiteSpace(json))
                {
                    VideoResult = JsonConvert.DeserializeObject<Rootobject>(json);
                    switch (VideoResult.showapi_res_code)
                    {
                        case 0:
                            {
                                templist = VideoResult.showapi_res_body.pagebean.contentlist;
                                for (int i = 0; i < 5; i++)
                                {
                                    templist1.Add(templist[i]);
                                }
                                viewmodel.VideoResult = templist1;
                                break;
                            }
                        #region 错误码
                        case -1:
                            {
                                var dialog = new MessageDialog("系统调用错误", "异常提示");
                                dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                var a = await dialog.ShowAsync();
                                break;
                            }
                        case -2:
                            {
                                var dialog = new MessageDialog("可调用次数或金额为0", "异常提示");
                                dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                var a = await dialog.ShowAsync();
                                break;
                            }
                        case -3:
                            {
                                var dialog = new MessageDialog("读取超时", "异常提示");
                                dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                var a = await dialog.ShowAsync();
                                break;
                            }
                        case -4:
                            {
                                var dialog = new MessageDialog("服务端返回数据解析错误", "异常提示");
                                dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                var a = await dialog.ShowAsync();
                                break;
                            }
                        case -5:
                            {
                                var dialog = new MessageDialog("后端服务器DNS解析错误", "异常提示");
                                dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                var a = await dialog.ShowAsync();
                                break;
                            }
                        case -6:
                            {
                                var dialog = new MessageDialog("服务不存在或未上线", "异常提示");
                                dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                var a = await dialog.ShowAsync();
                                break;
                            }
                        case -1000:
                            {
                                var dialog = new MessageDialog("系统维护", "异常提示");
                                dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                var a = await dialog.ShowAsync();
                                break;
                            }
                        case -1002:
                            {
                                var dialog = new MessageDialog("showapi_appid字段必传", "异常提示");
                                dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                var a = await dialog.ShowAsync();
                                break;
                            }
                        case -1003:
                            {
                                var dialog = new MessageDialog("showapi_sign字段必传", "异常提示");
                                dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                var a = await dialog.ShowAsync();
                                break;
                            }
                        case -1004:
                            {
                                var dialog = new MessageDialog("签名sign验证有误", "异常提示");
                                dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                var a = await dialog.ShowAsync();
                                break;
                            }
                        case -1005:
                            {
                                var dialog = new MessageDialog("showapi_timestamp无效", "异常提示");
                                dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                var a = await dialog.ShowAsync();
                                break;
                            }
                        case -1006:
                            {
                                var dialog = new MessageDialog("app无权限调用接口 ", "异常提示");
                                dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                var a = await dialog.ShowAsync();
                                break;
                            }
                        case -1007:
                            {
                                var dialog = new MessageDialog("没有订购套餐 ", "异常提示");
                                dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                var a = await dialog.ShowAsync();
                                break;
                            }
                        case -1008:
                            {
                                var dialog = new MessageDialog("服务商关闭对您的调用权限 ", "异常提示");
                                dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                var a = await dialog.ShowAsync();
                                break;
                            }
                        case -1010:
                            {
                                var dialog = new MessageDialog("找不到您的应用", "异常提示");
                                dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                var a = await dialog.ShowAsync();
                                break;
                            }
                        case -1011:
                            {
                                var dialog = new MessageDialog("子授权app_child_id无效", "异常提示");
                                dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                var a = await dialog.ShowAsync();
                                break;
                            }
                        case -1012:
                            {
                                var dialog = new MessageDialog("子授权已过期或失效", "异常提示");
                                dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                var a = await dialog.ShowAsync();
                                break;
                            }
                        case -1013:
                            {
                                var dialog = new MessageDialog("子授权ip受限", "异常提示");
                                dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                var a = await dialog.ShowAsync();
                                break;
                            }
                            #endregion
                    }
                    await Task.Delay(1000);
                    sc.ChangeView(null, 30, null);
                }
                else
                {
                    await Task.Delay(1000);
                    sc.ChangeView(null, 30, null);
                    return;
                }
            }
            catch (HttpRequestException)
            {
                var dialog = new MessageDialog("网络出现异常", "异常提示");
                dialog.Commands.Add(new UICommand("确定", cmd => { }));
                await dialog.ShowAsync();
                return;
            }
            catch (Exception)
            {
                await Task.Delay(1000);
                sc.ChangeView(null, 30, null);
            }
        }
        private void Sc_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            lock (o)
            {
                if (!IsLoading)
                {
                    if (sc.VerticalOffset == sc.ScrollableHeight)
                    {
                        IsLoading = true;
                        Task.Factory.StartNew(async () =>
                        {
                            await Task.Delay(1000);
                            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                            {
                                if (count % 4 != 0 || templist.Count / 5 > count)
                                {
                                    templist1.Clear();
                                    for (int i = count * 5; i < (count * 5) + 5; i++)
                                    {
                                        templist1.Add(templist[i]);
                                    }
                                    count++;
                                }
                                else
                                {
                                    string json = await NetWork.HttpRequest.GetVideo((templist.Count / 20) + 1);
                                    json = json.Replace("\\n", "");
                                    json = json.Replace(" ", "");
                                    if (!string.IsNullOrWhiteSpace(json))
                                    {
                                        VideoResult = JsonConvert.DeserializeObject<Rootobject>(json);
                                        switch (VideoResult.showapi_res_code)
                                        {
                                            case 0:
                                                {
                                                    for (int i = 0; i < 20; i++)
                                                    {
                                                        templist.Add(VideoResult.showapi_res_body.pagebean.contentlist[i]);
                                                    }
                                                    templist1.Clear();
                                                    for (int i = (count * 5); i < (count * 5) + 5; i++)
                                                    {
                                                        templist1.Add(templist[i]);
                                                    }
                                                    count++;
                                                    viewmodel.VideoResult = templist1;
                                                    break;
                                                }
                                            #region 错误码
                                            case -1:
                                                {
                                                    var dialog = new MessageDialog("系统调用错误", "异常提示");
                                                    dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                                    var a = await dialog.ShowAsync();
                                                    break;
                                                }
                                            case -2:
                                                {
                                                    var dialog = new MessageDialog("可调用次数或金额为0", "异常提示");
                                                    dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                                    var a = await dialog.ShowAsync();
                                                    break;
                                                }
                                            case -3:
                                                {
                                                    var dialog = new MessageDialog("读取超时", "异常提示");
                                                    dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                                    var a = await dialog.ShowAsync();
                                                    break;
                                                }
                                            case -4:
                                                {
                                                    var dialog = new MessageDialog("服务端返回数据解析错误", "异常提示");
                                                    dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                                    var a = await dialog.ShowAsync();
                                                    break;
                                                }
                                            case -5:
                                                {
                                                    var dialog = new MessageDialog("后端服务器DNS解析错误", "异常提示");
                                                    dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                                    var a = await dialog.ShowAsync();
                                                    break;
                                                }
                                            case -6:
                                                {
                                                    var dialog = new MessageDialog("服务不存在或未上线", "异常提示");
                                                    dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                                    var a = await dialog.ShowAsync();
                                                    break;
                                                }
                                            case -1000:
                                                {
                                                    var dialog = new MessageDialog("系统维护", "异常提示");
                                                    dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                                    var a = await dialog.ShowAsync();
                                                    break;
                                                }
                                            case -1002:
                                                {
                                                    var dialog = new MessageDialog("showapi_appid字段必传", "异常提示");
                                                    dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                                    var a = await dialog.ShowAsync();
                                                    break;
                                                }
                                            case -1003:
                                                {
                                                    var dialog = new MessageDialog("showapi_sign字段必传", "异常提示");
                                                    dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                                    var a = await dialog.ShowAsync();
                                                    break;
                                                }
                                            case -1004:
                                                {
                                                    var dialog = new MessageDialog("签名sign验证有误", "异常提示");
                                                    dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                                    var a = await dialog.ShowAsync();
                                                    break;
                                                }
                                            case -1005:
                                                {
                                                    var dialog = new MessageDialog("showapi_timestamp无效", "异常提示");
                                                    dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                                    var a = await dialog.ShowAsync();
                                                    break;
                                                }
                                            case -1006:
                                                {
                                                    var dialog = new MessageDialog("app无权限调用接口 ", "异常提示");
                                                    dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                                    var a = await dialog.ShowAsync();
                                                    break;
                                                }
                                            case -1007:
                                                {
                                                    var dialog = new MessageDialog("没有订购套餐 ", "异常提示");
                                                    dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                                    var a = await dialog.ShowAsync();
                                                    break;
                                                }
                                            case -1008:
                                                {
                                                    var dialog = new MessageDialog("服务商关闭对您的调用权限 ", "异常提示");
                                                    dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                                    var a = await dialog.ShowAsync();
                                                    break;
                                                }
                                            case -1010:
                                                {
                                                    var dialog = new MessageDialog("找不到您的应用", "异常提示");
                                                    dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                                    var a = await dialog.ShowAsync();
                                                    break;
                                                }
                                            case -1011:
                                                {
                                                    var dialog = new MessageDialog("子授权app_child_id无效", "异常提示");
                                                    dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                                    var a = await dialog.ShowAsync();
                                                    break;
                                                }
                                            case -1012:
                                                {
                                                    var dialog = new MessageDialog("子授权已过期或失效", "异常提示");
                                                    dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                                    var a = await dialog.ShowAsync();
                                                    break;
                                                }
                                            case -1013:
                                                {
                                                    var dialog = new MessageDialog("子授权ip受限", "异常提示");
                                                    dialog.Commands.Add(new UICommand("确定", cmd => { }));
                                                    var a = await dialog.ShowAsync();
                                                    break;
                                                }
                                                #endregion
                                        }
                                    }
                                }
                                viewmodel.VideoResult = templist1;
                                await Task.Delay(300);
                                sc.ChangeView(null, 30, null);
                                IsLoading = false;
                            });
                        });
                    }
                    if (!e.IsIntermediate)
                    {
                        if (sc.VerticalOffset == 0.0)
                        {
                            IsLoading = true;
                            Task.Factory.StartNew(async () =>
                            {
                                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                                {
                                    if (count != 1)
                                    {
                                        templist1.Clear();
                                        for (int i = (count - 2) * 5; i < ((count - 2) * 5) + 5; i++)
                                        {
                                            templist1.Add(templist[i]);
                                        }
                                        count--;
                                    }
                                    else
                                    {
                                        await FirstStep();
                                        IsLoading = false;
                                        return;
                                    }
                                    viewmodel.VideoResult = templist1;
                                    await Task.Delay(300);
                                    sc.ChangeView(null, 30, null);
                                    IsLoading = false;
                                });
                            });
                        }
                    }
                }
            }
        }
        private async void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            Contentlist cl = bt.DataContext as Contentlist;
            sqliteHelper.CreateDB();
            List<Model.Download> DL = sqliteHelper.CheckData(cl.name);
            bool checkresult = false;
            foreach (Model.Download i in DL)
            {
                if (i.name == cl.name)
                    checkresult = true;
            }
            if (checkresult)
            {
                var dialog = new MessageDialog("已经添加过下载了", "yoho");
                dialog.Commands.Add(new UICommand("确定", cmd => { }));
                await dialog.ShowAsync();
                return;
            }
            Model.Download AddDownload = new Model.Download();
            AddDownload.name = cl.name;
            AddDownload.love = cl.love;
            AddDownload.hate = cl.hate;
            AddDownload.profile_image = cl.profile_image;
            AddDownload.text = cl.text;
            AddDownload.video_uri = cl.video_uri;
            AddDownload.create_time = cl.create_time;
            sqliteHelper.AddData(AddDownload);
            StorageFile File = await NetWork.DownLoad.DownLoadVideo(cl.video_uri);

        }
        private void MyListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ListView lv = sender as ListView;
            Contentlist cl = e.ClickedItem as Contentlist;
            Uri uri = new Uri(cl.video_uri);
            Grid gr1 = lv.Parent as Grid;
            Grid gr2 = gr1.Parent as Grid;
            Home home = gr2.Parent as Home;
            Frame fr = home.Parent as Frame;
            if (Grid.GetColumnSpan(fr) == 2)
            {
                if(this.Frame!=null)
                {
                    fr.Navigate(typeof(SecondFrame));
                    SecondFrame sf2 = fr.Content as SecondFrame;
                    Grid gr6 = sf2.Content as Grid;
                    Grid gr7 = gr6.Children[1] as Grid;
                    MediaElement player2 = gr7.Children[0] as MediaElement;
                    player2.Source = uri;
                }
                return;
            }
            Grid gr3 = fr.Parent as Grid;
            Frame fr2 = gr3.Children[0] as Frame;
            SecondFrame sf = fr2.Content as SecondFrame;
            Grid gr4 = sf.Content as Grid;
            Grid gr5 = gr4.Children[1] as Grid;
            MediaElement player = gr5.Children[0] as MediaElement;
            player.Source = uri;
            sqliteHelper1.CreateDB();
            List<Model.History> His = sqliteHelper1.CheckData(cl.name);
            Model.History AddHistory = new Model.History();
            AddHistory.name = cl.name;
            AddHistory.love = cl.love;
            AddHistory.hate = cl.hate;
            AddHistory.profile_image = cl.profile_image;
            AddHistory.text = cl.text;
            AddHistory.video_uri = cl.video_uri;
            AddHistory.create_time = cl.create_time;
            AddHistory.time=DateTime.Now.ToString();
            sqliteHelper1.AddData(AddHistory);
        }
    }
}
