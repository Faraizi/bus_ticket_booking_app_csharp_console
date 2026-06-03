using BusTicketBookingApp;
using System;
using System.Collections.Generic;
using System;

namespace BusTicketBookingApp
{
    sealed public class User
    {
        public int UserID { get; set; }

        public string? FullName { get; set; } = string.Empty;

        public string? MobileNumber { get; set; } = string.Empty;

        public string? EmailAddress { get; set; } = string.Empty;

        public List<Ticket> Tickets {get; set; } = new List<Ticket>();
    }
    
}