using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTicketBookingApp.Interfaces
{
    public class BookingService : IBookingService
    {

        private readonly List<Invoice> _invoices = new();
        public IReadOnlyList<Invoice> Invoices => _invoices;

        private readonly IScheduleService _scheduleService;
        private readonly IUserService _userService;
        private readonly IBusService _busService;
        public BookingService(IScheduleService scheduleService, IUserService userService, IBusService busService)
        {
            _scheduleService = scheduleService;
            _userService = userService;
            _busService = busService;
        }
        private int _nextInvoiceId = 1;
        private int _nextTicketId = 1;

        public void BookTicket()
        {
            Console.WriteLine("Enter User ID");
            int userID = Convert.ToInt32(Console.ReadLine());
            User? user = _userService.GetUserByID(userID);
            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }
            Console.WriteLine("Enter Schedule ID");
            int scheduleID = Convert.ToInt32(Console.ReadLine());
            Schedule? schedule = _scheduleService.GetScheduleByID(scheduleID);
            if (schedule == null)
            {
                Console.WriteLine("Schedule not found.");
                return;
            }
            Console.WriteLine("Enter Seat Number");
            string seatNumber = Console.ReadLine() ?? string.Empty;

            Bus? bus = _busService.GetBusByID(schedule.BusID);
            if (bus == null)
            {
                Console.WriteLine("Bus not found");
                return;
            }

            if (!_scheduleService.IsValidSeat(bus, seatNumber))
            {
                Console.WriteLine("Invalid seat number.");
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
            _scheduleService.AddReservedSeat(schedule, seatNumber);
            _invoices.Add(invoice);
            Console.WriteLine($"Ticket booked successfully\nTicket ID: {invoice.TicketID}, Invoice ID: {invoice.InvoiceID}, Seat: {seatNumber}, Amount: {invoice.Amount}\n");
        }

        public void DisplayInvoices()
        {
            Console.WriteLine("Enter user ID to view Invoices:");
            int userID = Convert.ToInt32(Console.ReadLine());
            User user = _userService.GetUserByID(userID);
            if (user == null)
            {
                Console.WriteLine("User not found.\n");
                return;
            }
            var invoices = Invoices.Where(i => i.UserID == userID).ToList();
            Console.WriteLine("All Invoices: ");
            if (Invoices.Count == 0) Console.WriteLine("No invoices found!\n");
            foreach (var invoice in invoices)
            {
                string payment = invoice.IsPaid ? "Yes" : "No";
                Console.WriteLine($"Invoice ID: {invoice.InvoiceID}, Ticket ID: {invoice.TicketID}, Amount: {invoice.Amount} Taka, Date: {invoice.Date}, Paid: {payment}\n");
            }
        }
    }
}
