using BusTicketBookingApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BusTicketBookingApp
{
    sealed public class Bus
    {
        public int ID { get; set; }

        public int CoachNumber { get; set; }

        public BusType Type { get; set; }

        public int TotalSeats { get; set; }

        public List<Schedule> Schedules { get; set; } = new ();
    }
}

