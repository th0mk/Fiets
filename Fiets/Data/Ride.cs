using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fiets.Data
{
    public class Ride
    {
        [Key]
        public int RideID { get; set; }

        public string UserID { get; set; }

        public bool RideFinished { get; set; }

        public DateTime RideStartTimeUtc { get; set; }

        public int RideStartKm { get; set; }

        public int? RideEndKm { get; set; }

        public DateTime? RideEndTimeUtc { get; set; }

        [ForeignKey("UserID")]
        public ApplicationUser User { get; set; }
    }
}
