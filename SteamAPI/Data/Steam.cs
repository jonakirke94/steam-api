using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SteamAPI.Data
{
    public class Steam
    {

        public string BuildURL(string id)
        {
            string format = "json";
            string key = "43949746928DAF0DA13E22AA55890547";
            string app_info = "1";

            return "http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key="+ key + "&steamid=" + id + "&format=" + format + "&include_appinfo=" + app_info;
  
        }

        public Result MakeRequest(string link)
        {
            var resp = new Result();
            var client = new HttpClient();

            try { 
            var content = client.GetAsync(link).Result;

                resp.ErrorMsg = content.StatusCode.ToString();
                resp.StatusCode = (int)content.StatusCode;

                var responseString = content.Content.ReadAsStringAsync().Result;
            var checkRes = JsonConvert.DeserializeObject<SteamResponse>(responseString);
            if (checkRes != null)
            {
                resp.Response = checkRes;
            }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Something went wrong in getrequest..: " + e.Message);
            }

            return resp;
}
    }
}
