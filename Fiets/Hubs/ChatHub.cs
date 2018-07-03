using Fiets.Data;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Security.Claims;

namespace Fiets.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext db;

        public ChatHub(ApplicationDbContext context)
        {
            db = context;
        }

        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.SendAsync("broadcastMessage", name, message);
        }


        public void StartRide(string id, string rideStartKm)
        {
            DateTime utcNow = DateTime.UtcNow;

            //var user = db.Users.Find(id);

            Clients.All.SendAsync("addRide", id, "Jan", rideStartKm, utcNow);

            Ride ride = new Ride
            {
                UserID = id,
                RideStartTimeUtc = DateTime.UtcNow,
                RideFinished = false,
                RideStartKm = Int32.Parse(rideStartKm)
            };

            db.Rides.Add(ride);

            db.SaveChanges();
        }


        public void EndRide(string id, string rideEndKm)
        {
            DateTime utcNow = DateTime.UtcNow;

            Clients.All.SendAsync("removeRide", id, "Jan", rideEndKm, utcNow);

            var ride = db.Rides.FirstOrDefault(f => f.UserID == id && !f.RideFinished);

            ride.RideFinished = true;
            ride.RideEndKm = Int32.Parse(rideEndKm);
            ride.RideEndTimeUtc = utcNow;

            db.Entry(ride).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            db.SaveChanges();
        }
    }
}