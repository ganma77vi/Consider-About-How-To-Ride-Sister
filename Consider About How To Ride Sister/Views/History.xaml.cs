using Consider_About_How_To_Ride_Sister.HelpClass;
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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace Consider_About_How_To_Ride_Sister.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class History : Page
    {
        ObservableCollection<Model.History> HistoryList = new ObservableCollection<Model.History>();
        SQLiteHelper1 sqliteHelper = new SQLiteHelper1();
        public History()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            HistoryList.Clear();
            HistoryList = sqliteHelper.ReadData(HistoryList);
            double[] time=new double[HistoryList.Count];
            Model.History temp;
            for(int i =0;i<HistoryList.Count;i++)
            {
                time[i]=Convert.ToDouble(HistoryList[i].time.Replace("/","").Replace(":", "").Replace(" ", ""));
            }
            for(int i=0; i<time.Length;i++)
            {
                for (int j = i + 1; j < time.Length; j++)
                {

                    temp = HistoryList[j];
                    HistoryList[j] = HistoryList[i];
                    HistoryList[i] = temp;
                }
            }
            Mylistview.ItemsSource = HistoryList;
            if (HistoryList.Count == 0)
            {
                TB.Opacity = 1;
            }
            else
            {
                TB.Opacity = 0;
            }
        }
    }
}
