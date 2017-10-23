using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamAPI.Data
{
    public class Result
    {
        public string ErrorMsg { get; set; }
        public int StatusCode { get; set; }
        public SteamResponse Response { get; set; }
    }
}
