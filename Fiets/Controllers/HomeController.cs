using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fiets.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Fiets.Data;
using Fiets.ViewModels;

namespace Fiets.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db;

        public HomeController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var currentRides = db.Rides
                .Where(w => !w.RideFinished)
                .Select(s => new RideListItem
                {
                    RideID = s.RideID,
                    RideStartKm = s.RideStartKm,
                    RideFinished = s.RideFinished,
                    User = db.Users.FirstOrDefault(f => f.Id == s.UserID).UserName
                }).ToList();

            var lastRides = db.Rides
                .Where(w => w.RideFinished)
                .OrderBy(o => o.RideEndTimeUtc)
                .Take(5)
                .Select(s => new RideListItem
                {
                    RideID = s.RideID,
                    RideEndKm = s.RideEndKm,
                    RideEndTimeUtc = s.RideEndTimeUtc,
                    RideStartKm = s.RideStartKm,
                    RideStartTimeUtc = s.RideStartTimeUtc,
                    RideFinished = s.RideFinished,
                    User = db.Users.FirstOrDefault(f => f.Id == s.UserID).UserName
                }).ToList();



            var viewmodel = new IndexViewModel
            {
                CurrentRides = currentRides,
                LastRides = lastRides,
                Riding = false,
            };

            if (User.Identity.IsAuthenticated)
            {
                var currentRide = db.Rides.FirstOrDefault(f => f.UserID == User.FindFirstValue(ClaimTypes.NameIdentifier) && !f.RideFinished);
                if (currentRide != null)
                {
                    viewmodel.RideID = currentRide.RideID;
                    viewmodel.Riding = true;
                }
            }



            return View(viewmodel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
