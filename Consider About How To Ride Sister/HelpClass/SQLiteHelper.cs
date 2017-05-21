using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Consider_About_How_To_Ride_Sister.Model;
using System.Collections.ObjectModel;

namespace Consider_About_How_To_Ride_Sister.HelpClass
{
    class SQLiteHelper
    {
        public string DbName = "DownLoadData.db";
        public string DbPath;
        internal SQLite.Net.SQLiteConnection GetCreateConn()
        {
            DbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DbName);
            var con = new SQLite.Net.SQLiteConnection(new SQLitePlatformWinRT(), DbPath);

            return con;

        }
        internal void CreateDB()
        {
            DbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DbName);
            using (var conn = GetCreateConn())
            {
                conn.CreateTable<Download>();
            }
        }
        internal int AddData(Download AddDownload)
        {
            int result = 0;
            using (var conn = GetCreateConn())
            {
                result = conn.Insert(AddDownload);
            }

            return result;
        }
        internal int DeleteData(Download DownloadUID)
        {
            int result = 0;
            DbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DbName);
            using (var conn = GetCreateConn())
            {
                result = conn.Delete(DownloadUID);
            }
            return result;
        }
        internal void UpadateData(string deleteSqliteSequence)
        {
            DbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DbName);
        }
        internal int UpadateData(Download updataDownload)
        {
            int result = 0;
            DbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DbName);
            using (var conn = GetCreateConn())
            {
                result = conn.Update(updataDownload);
            }
            return result;
        }
        internal List<Download> CheckData(string conditions)
        {
            string Condition = "%" + conditions + "%";
            #region 
            using (var conn = GetCreateConn())
            {
                return conn.Query<Download>("select * from Download where name like?;", Condition);

            }
            #endregion
        }
        internal ObservableCollection<Download> ReadData(ObservableCollection<Download> VideoList)
        {
            VideoList.Clear();
            DbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DbName);
            CreateDB();
            using (var conn = GetCreateConn())
            {
                var dbFavoriteData = conn.Table<Download>();
                foreach (var item in dbFavoriteData)
                {
                    VideoList.Add(item);
                }
            }
            return VideoList;
        }
    }
}
