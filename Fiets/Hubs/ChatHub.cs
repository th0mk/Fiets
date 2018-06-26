using Fiets.Data;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Security.Claims;

namespace Fiets.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext db;

        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.SendAsync("broadcastMessage", name, message);
        }


        public void StartRide(string id, int rideStartKm)
        {
            DateTime utcNow = DateTime.UtcNow;

            var user = db.Users.Find(id);

            Clients.All.SendAsync("addRide", id, user.UserName, rideStartKm, utcNow);

            Ride ride = new Ride
            {
                UserID = id,
                RideStartTimeUtc = DateTime.UtcNow,
                RideStartKm = rideStartKm
            };

            db.Rides.Add(ride);

            db.SaveChangesAsync();
        }
    }
}