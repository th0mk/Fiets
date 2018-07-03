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

        public List<RideListItem> CurrentRides { get; set; }

        public List<RideListItem> TopRides { get; set; }

        public List<RideListItem> LastRides { get; set; }
    }

    public class RideListItem
    {
        [Key]
        public int RideID { get; set; }

        public string User { get; set; }

        public bool RideFinished { get; set; }

        public DateTime RideStartTimeUtc { get; set; }

        public int RideStartKm { get; set; }

        public int? RideEndKm { get; set; }

        public DateTime? RideEndTimeUtc { get; set; }
    }
}
