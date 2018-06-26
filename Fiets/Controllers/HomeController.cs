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

namespace Fiets.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> StartRideAsync(int? rideStartKm)
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Ride ride = new Ride
            {
                RideStartTimeUtc = DateTime.UtcNow,
                RideStartKm = rideStartKm.Value,
            };

            return View();
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
