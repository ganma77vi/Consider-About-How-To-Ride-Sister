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
    class SQLiteHelper1
    {
        public string DbName = "History.db";
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
                conn.CreateTable<History>();
            }
        }
        internal int AddData(History AddHistory)
        {
            int result = 0;
            using (var conn = GetCreateConn())
            {
                result = conn.Insert(AddHistory);
            }

            return result;
        }
        internal int DeleteData(History HistoryUID)
        {
            int result = 0;
            DbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DbName);
            using (var conn = GetCreateConn())
            {
                result = conn.Delete(HistoryUID);
            }
            return result;
        }
        internal void UpadateData(string deleteSqliteSequence)
        {
            DbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DbName);
        }
        internal int UpadateData(History updataHistory)
        {
            int result = 0;
            DbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DbName);
            using (var conn = GetCreateConn())
            {
                result = conn.Update(updataHistory);
            }
            return result;
        }
        internal List<History> CheckData(string conditions)
        {
            string Condition = "%" + conditions + "%";
            #region 
            using (var conn = GetCreateConn())
            {
                return conn.Query<History>("select * from History where name like?;", Condition);

            }
            #endregion
        }
        internal ObservableCollection<History> ReadData(ObservableCollection<History> HistoryVideoList)
        {
            HistoryVideoList.Clear();
            DbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DbName);
            CreateDB();
            using (var conn = GetCreateConn())
            {
                var dbHistoryData = conn.Table<History>();
                foreach (var item in dbHistoryData)
                {
                    HistoryVideoList.Add(item);
                }
            }
            return HistoryVideoList;
        }
    }
}
