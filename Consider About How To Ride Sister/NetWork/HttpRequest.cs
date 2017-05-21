using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Consider_About_How_To_Ride_Sister.NetWork
{
    class HttpRequest
    {
        public static async Task<string> GetVideo(int page)
        {
            const string RecentMovieInformation_Api = " http://route.showapi.com/255-1?showapi_appid=38524&showapi_sign=afccd492bbdd48d58e0be6b58fada87a&type=41&page={0}";
            HttpClient httpclient = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();
            string result = null;
            string Api = RecentMovieInformation_Api.Replace("{0}",page.ToString());
            response = await httpclient.GetAsync(Api);
            result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
