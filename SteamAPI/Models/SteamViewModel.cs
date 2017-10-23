using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamAPI.Models
{
    public class SteamViewModel
    {
        public string Id { get; set; }

        public List<GameInfo> Games { get; set; }
        public int GameCount { get; set; }
        public double Playtime2weeks { get; set; }
        public double PlaytimeForever { get; set; }
        public string ErrorMsg { get; set; }

        public string Filter { get; set; }

        public SteamViewModel()
        {
            Games = new List<GameInfo>();
        }
    }
}
