using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamAPI.Data
{
    public class Game
    {

        [JsonProperty("appid")]
        public int Appid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("playtime_forever")]
        public int PlaytimeForever { get; set; }

        [JsonProperty("img_icon_url")]
        public string ImgIconUrl { get; set; }

        [JsonProperty("img_logo_url")]
        public string ImgLogoUrl { get; set; }

        [JsonProperty("has_community_visible_stats")]
        public bool HasCommunityVisibleStats { get; set; }

        [JsonProperty("playtime_2weeks")]
        public int? Playtime2weeks { get; set; }
    }

    public class Response
    {

        [JsonProperty("game_count")]
        public int GameCount { get; set; }

        [JsonProperty("games")]
        public IList<Game> Games { get; set; }
    }

    public class SteamResponse
    {

        [JsonProperty("response")]
        public Response Response { get; set; }
    }

}
