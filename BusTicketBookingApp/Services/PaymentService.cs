using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTicketBookingApp.Interfaces
{
    public class PaymentService : IPaymentService
    {
        private readonly IScheduleService _scheduleService;
        private readonly IUserService _userService;
        private readonly IBusService _busService;
        private readonly IBookingService _bookingService;
        public PaymentService(IScheduleService scheduleService, IUserService userService, IBusService busService, IBookingService bookingService)
        {
            _scheduleService = scheduleService;
            _userService = userService;
            _busService = busService;
            _bookingService = bookingService;
        }
        public void ProcessPayment()
        {
            Console.WriteLine("Enter Invoice ID: ");
            int invID = Convert.ToInt32(Console.ReadLine());
            Invoice? invoice = _bookingService.Invoices.FirstOrDefault(i => i.InvoiceID == invID);
            if (invoice.IsPaid)
            {
                Console.WriteLine("Invoice already paid.");
                return;
            }
            invoice.IsPaid = true;

            Schedule? schedule = _scheduleService.GetScheduleByID(invoice.ScheduleID);
            Bus? bus = _busService.GetBusByID(schedule.BusID);
            Console.WriteLine($"Payment successful");
            Ticket ticket = new()
            {
                TicketID = invoice.TicketID,
                UserID = invoice.UserID,
                CoachNumber = bus.CoachNumber,
                JourneyDate = invoice.Date,
                Seat = invoice.Seat,
            };
            User? user = _userService.GetUserByID(invoice.UserID);
            user.Tickets.Add(ticket);
            Console.WriteLine();
        }
    }
}
