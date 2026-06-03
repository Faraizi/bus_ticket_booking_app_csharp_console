using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTicketBookingApp.Interfaces
{
    public class BookingService : IBookingService
    {
        private readonly IScheduleService _scheduleService;
        private readonly IUserService _userService;
        private readonly IBusService _busService;
        public BookingService(IScheduleService scheduleService, IUserService userService, IBusService busService) { 
            _scheduleService = scheduleService;
            _userService = userService;
            _busService = busService;
        }
        public List<Invoice> Invoices = new List<Invoice>();
        private int _nextInvoiceId = 1;
        private int _nextTicketId = 1;
        public void BookTicket()
        {
            Console.WriteLine("Enter User ID");
            int userID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Schedule ID");
            int scheduleID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Seat Number");
            string seatNumber = Console.ReadLine() ?? string.Empty;

            Schedule? schedule = _scheduleService.GetScheduleByID(scheduleID);
            User? user = _userService.GetUserByID(userID);
            Bus? bus = _busService.GetBusByID(schedule.BusID);

            if (!_scheduleService.IsValidSeat(bus, seatNumber))
            {
                Console.WriteLine("Invalid seat number.");
                return;
            }

            if (schedule == null)
            {
                Console.WriteLine("Schedule not found.");
                return;
            }

            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            if (schedule.ReservedSeats.Contains(seatNumber))
            {
                Console.WriteLine("Seat already booked.");
                return;
            }
            int invID = _nextInvoiceId++;
            int ticketID = _nextTicketId++;

            Invoice invoice = new Invoice
            {
                InvoiceID = invID,
                TicketID = ticketID,
                ScheduleID = scheduleID,
                UserID = userID,
                Amount = schedule.TicketPrice,
                Date = schedule.DepartureDate,
                Seat = seatNumber,
                IsPaid = false
            };
            schedule.ReservedSeats.Add(seatNumber);
            Invoices.Add(invoice);
            Console.WriteLine($"Ticket booked successfully\nTicket ID: {invoice.TicketID}, Invoice ID: {invoice.InvoiceID}, Seat: {seatNumber}, Amount: {invoice.Amount}");
        }

        public void DisplayInvoices()
        {
            Console.WriteLine("Enter user ID to view Invoices:");
            int userID = Convert.ToInt32(Console.ReadLine());
            var invoices = Invoices.Where(i => i.UserID == userID).ToList();
            Console.WriteLine("All Invoices: ");
            if (Invoices.Count == 0) Console.WriteLine("No invoices found!");
            foreach (var invoice in invoices)
            {
                string payment = invoice.IsPaid ? "Yes" : "No";
                Console.WriteLine($"Invoice ID: {invoice.InvoiceID}, Ticket ID: {invoice.TicketID}, Amount: {invoice.Amount}, Date: {invoice.Date}, Paid: {payment}");
            }
        }

        public void ProcessPayment()
        {
            Console.WriteLine("Enter Invoice ID: ");
            int invID = Convert.ToInt32(Console.ReadLine());
            Invoice? invoice = Invoices.FirstOrDefault(i => i.InvoiceID == invID);
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
        }

        public  void DisplayUserTickets()
        {
            Console.WriteLine("Enter user ID to view Tickets:");
            int userID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("All Tickets: ");
            User? user = _userService.GetUserByID(userID);
            if (user.Tickets.Count == 0) Console.WriteLine("No Paid tickets found for this user");
            foreach (var ticket in user.Tickets)
            {
                Console.WriteLine($"Ticket ID: {ticket.TicketID}, Coach Number: {ticket.CoachNumber}, Journey Date: {ticket.JourneyDate}, Seat: {ticket.Seat}");
            }
        }
    }
}
