using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTicketBookingApp
{
    public class Invoice
    {
        public int UserID { get; set; }

        public int InvoiceID { get; set; }

        public int ScheduleID { get; set; }

        public int TicketID { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string Seat { get; set; }

        public bool IsPaid { get; set; }
    }
}
