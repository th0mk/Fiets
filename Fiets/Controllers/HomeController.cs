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
                .Select(s => new CurrentRideListItem
                {
                    RideID = s.RideID,
                    RideStartKm = s.RideStartKm,
                    RideStartTime = s.RideStartTimeUtc.ToLocalTime(),
                    User = db.Users.FirstOrDefault(f => f.Id == s.UserID).UserName
                }).ToList();

            var lastRides = db.Rides
                .Where(w => w.RideFinished)
                .OrderBy(o => o.RideEndTimeUtc)
                .Take(5)
                .Select(s => new FinishedRideListItem
                {
                    RideID = s.RideID,
                    KMDriven = (s.RideEndKm - s.RideStartKm).Value,
                    TimeDriven = (s.RideEndTimeUtc - s.RideStartTimeUtc).Value.Hours + ":" + (s.RideEndTimeUtc - s.RideStartTimeUtc).Value.Minutes + (s.RideEndTimeUtc - s.RideStartTimeUtc).Value.Seconds,
                    User = db.Users.FirstOrDefault(f => f.Id == s.UserID).UserName
                }).ToList();

            var topRides = db.Rides
                .Where(w => w.RideFinished)
                .OrderBy(o => (o.RideEndKm - o.RideStartKm).Value)
                .Take(5)
                .Select(s => new FinishedRideListItem
                {
                    RideID = s.RideID,
                    KMDriven = (s.RideEndKm - s.RideStartKm).Value,
                    TimeDriven = (s.RideEndTimeUtc - s.RideStartTimeUtc).Value.Hours + ":" + (s.RideEndTimeUtc - s.RideStartTimeUtc).Value.Minutes + ":" +(s.RideEndTimeUtc - s.RideStartTimeUtc).Value.Seconds,
                    User = db.Users.FirstOrDefault(f => f.Id == s.UserID).UserName
                }).ToList();

            var viewmodel = new IndexViewModel
            {
                TopRides = topRides,
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
