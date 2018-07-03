using Fiets.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fiets.ViewModels
{
    public class IndexViewModel
    {
        public bool Riding { get; set; }

        public int? RideID { get; set; }

        public List<CurrentRideListItem> CurrentRides { get; set; }

        public List<FinishedRideListItem> TopRides { get; set; }

        public List<FinishedRideListItem> LastRides { get; set; }
    }

    public class CurrentRideListItem
    {
        [Key]
        public int RideID { get; set; }

        public string User { get; set; }

        public DateTime RideStartTimeUtc { get; set; }

        public int RideStartKm { get; set; }
    }

    public class FinishedRideListItem
    {
        [Key]
        public int RideID { get; set; }

        public string User { get; set; }

        public int KMDriven { get; set; }

        public string TimeDriven { get; set; }
    }
}
