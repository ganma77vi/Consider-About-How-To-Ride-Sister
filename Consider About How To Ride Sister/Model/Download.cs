using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using SQLite.Net.Interop;
using SQLite.Net.Attributes;
using System.Collections.ObjectModel;

namespace Consider_About_How_To_Ride_Sister.Model
{
    internal class Download
    {
        public Download() { }
        public Download(int ID,string profile_image,string name,string create_time,string text,string video_uri
                        , string hate, string love)
        {
            this.UID = ID;
            this.text = text;
            this.profile_image = profile_image;
            this.name = name;
            this.create_time = create_time;
            this.video_uri = video_uri;
            this.hate = hate;
            this.love = love;
        }
        [PrimaryKey]
        [AutoIncrement]
        [NotNull]
        public int UID { get; set; }
        public string text { get; set; }
        public string hate { get; set; }
        public string profile_image { get; set; }
        public string create_time { get; set; }
        public string love { get; set; }
        public string name { get; set; }
        public string video_uri { get; set; }
    }
}
