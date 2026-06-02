using BusTicketBookingApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BusTicketBookingApp
{
    sealed public class Schedule
    {
        public int BusID { get; set; }
        public int ScheduleID { get; set; }
        public string DepartureCity { get; set; } = string.Empty;
        public string ArrivalCity { get; set; } = string.Empty;
        public DateTime DepartureDate { get; set; }
        public decimal TicketPrice { get; set; }
        public List<string> ReservedSeats { get; set; } = [];

    }
}