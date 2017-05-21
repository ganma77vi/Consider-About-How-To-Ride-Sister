using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Windows.Storage;
using Windows.Web.Http;

namespace Consider_About_How_To_Ride_Sister.NetWork
{
    class DownLoad
    {
        public static async Task<StorageFile> DownLoadVideo(string uri)
        {
            try
            {
                var httpClient = new HttpClient();
                var buffer = await httpClient.GetBufferAsync(new Uri(uri));
                if (buffer != null && buffer.Length > 0u)
                {
                    string[] str = uri.Split('/');
                    string filename = str[str.Length-1];
                    var file = await ApplicationData.Current.LocalCacheFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                    using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        await stream.WriteAsync(buffer);
                        await stream.FlushAsync();
                    }
                    return file;
                }
            }
            catch(Exception)
            {

            }
            return null;
        }
    }
}
