namespace SteamAPI.Models
{
    public class GameInfo
    {
        public int AppId { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public double Playtime2weeks { get; set; }
        public double PlaytimeForever { get; set; }


        public GameInfo(int appId, string name, string logo, double pt2w, double pt4e)
        {
            AppId = appId;
            Name = name;
            LogoUrl = logo;
            Playtime2weeks = pt2w;
            PlaytimeForever = pt4e;
        }
    }

    
}