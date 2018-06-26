using Fiets.Data;
using Microsoft.AspNetCore.SignalR;
using System;
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
    }
}