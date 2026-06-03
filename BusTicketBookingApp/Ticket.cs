using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTicketBookingApp
{
    public class Ticket
    {
        public int TicketID { get; set; }

        public int UserID { get; set; }  

        public int CoachNumber { get; set; }

        public DateTime JourneyDate { get; set; }

        public string Seat { get; set; }
    }
}