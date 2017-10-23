using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SteamAPI.Models;
using SteamAPI.Data;

namespace SteamAPI.Controllers
{
    public class HomeController : Controller
    {
        public const string DefeaultID = "76561197960434622"; //gaben

        public IActionResult Index(string sortOrder, string searchString)
        {
            ViewData["GameSortParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder == "game_asc" ? "game_desc" : "game_asc";
            ViewData["2WSortParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder == "2w_asc" ? "2w_desc" : "2w_asc";
            ViewData["AlltimeSortParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder == "alltime_asc" ? "alltime_desc" : "alltime_asc";
            ViewData["CurrentFilter"] = searchString;

            Steam data = new Steam();
            SteamViewModel model = new SteamViewModel();

            string id = DefeaultID;

            if (TempData["SteamID"] != null)
            {
                id = TempData["SteamID"].ToString();
            }

            var resp = data.MakeRequest(data.BuildURL(id));

            

            //success
            if (resp.StatusCode == 200)
            {
                SetModelData(resp, model);

                if (!String.IsNullOrEmpty(searchString))
                {
                    model.Games = model.Games.Where(s => s.Name.ToLower().Contains(searchString.ToLower())).ToList();
                }

                switch (sortOrder)
                {
                    case "game_desc":
                        model.Games = model.Games.OrderByDescending(s => s.Name).ToList();
                        break;
                    case "game_asc":
                        model.Games = model.Games.OrderBy(s => s.Name).ToList();
                        break;
                    case "2w_desc":
                        model.Games = model.Games.OrderByDescending(s => s.Playtime2weeks).ToList();
                        break;
                    case "2w_asc":
                        model.Games = model.Games.OrderBy(s => s.Playtime2weeks).ToList();
                        break;
                    case "alltime_desc":
                        model.Games = model.Games.OrderByDescending(s => s.PlaytimeForever).ToList();
                        break;
                    case "alltime_asc":
                        model.Games = model.Games.OrderBy(s => s.PlaytimeForever).ToList();
                        break;
                    default:
                        model.Games = model.Games.OrderByDescending(s => s.Name).ToList();
                        break;

                }



            }
            //failure
            else
            {
                model.ErrorMsg = resp.ErrorMsg;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(string id, string sortOrder, string searchString)
        {
            ViewData["GameSortParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder == "game_asc" ? "game_desc" : "game_asc";
            ViewData["2WSortParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder == "2w_asc" ? "2w_desc" : "2w_asc";
            ViewData["AlltimeSortParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder == "alltime_asc" ? "alltime_desc" : "alltime_asc";
            ViewData["CurrentFilter"] = searchString;

            if (id == null)
            {
                id = DefeaultID;
            }

            Steam data = new Steam();
            SteamViewModel model = new SteamViewModel();

            var respNew = data.MakeRequest(data.BuildURL(id));

            //success
            if (respNew.StatusCode == 200)
            {             
                SetModelData(respNew, model);

                if (!String.IsNullOrEmpty(searchString))
                {
                    model.Games = model.Games.Where(s => s.Name.ToLower().Contains(searchString.ToLower())).ToList();
                }

                switch (sortOrder)
                {
                    case "game_desc":
                        model.Games = model.Games.OrderByDescending(s => s.Name).ToList();
                        break;
                    case "game_asc":
                        model.Games = model.Games.OrderBy(s => s.Name).ToList();
                        break;
                    case "2w_desc":
                        model.Games = model.Games.OrderByDescending(s => s.Playtime2weeks).ToList();
                        break;
                    case "2w_asc":
                        model.Games = model.Games.OrderBy(s => s.Playtime2weeks).ToList();
                        break;
                    case "alltime_desc":
                        model.Games = model.Games.OrderByDescending(s => s.PlaytimeForever).ToList();
                        break;
                    case "alltime_asc":
                        model.Games = model.Games.OrderBy(s => s.PlaytimeForever).ToList();
                        break;
                }
            }
            //failure
            else
            {
                model.ErrorMsg = respNew.ErrorMsg;
            }
            return View(model);
        }

        private static void SetModelData(Result resp, SteamViewModel model)
        {
            double TotalPlayTime = 0.0d;
            double TotalPlayTime2W = 0.0d;
            foreach (var game in resp.Response.Response.Games)
            {
                TotalPlayTime += game.PlaytimeForever;
                TotalPlayTime2W  += game.Playtime2weeks ?? 0;

                double GamePlaytime = Math.Round(game.PlaytimeForever / 1440.0d, 1);
                int IntGamePlaytime2W = game.Playtime2weeks ?? 0;
                double GamePlaytime2W = Math.Round(IntGamePlaytime2W / 60.0d, 1);
                
                model.Games.Add(new GameInfo(game.Appid, game.Name, game.ImgIconUrl, GamePlaytime2W, GamePlaytime));
            }
            model.GameCount = resp.Response.Response.GameCount;         

            model.PlaytimeForever = Math.Round(TotalPlayTime / 1440.0d, 1);
            model.Playtime2weeks = Math.Round(TotalPlayTime2W / 60, 1);
        }

        

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
