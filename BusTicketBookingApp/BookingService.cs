using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTicketBookingApp
{
    public static class BookingService
    {
        public static List<Invoice> Invoices = new List<Invoice>();
        private static int _nextInvoiceId = 1;
        private static int _nextTicketId = 1;
        public static void BookTicket()
        {
            Console.WriteLine("Enter User ID");
            int userID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Schedule ID");
            int scheduleID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Seat Number");
            string seatNumber = Console.ReadLine() ?? string.Empty;

            Schedule? schedule = ScheduleService.Schedules.FirstOrDefault(s => s.ScheduleID == scheduleID);
            User? user = UserService.Users.FirstOrDefault(u => u.UserID == userID);
            Bus? bus = BusService.Buses.FirstOrDefault(b => b.ID == schedule.BusID);

            if (!ScheduleService.IsValidSeat(bus, seatNumber))
            {
                Console.WriteLine("Invalid seat number.");
                return;
            }

            if (schedule == null)
            {
                Console.WriteLine("Schedule not found.");
                return;
            }

            if(user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            if(schedule.ReservedSeats.Contains(seatNumber))
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

        public static void DisplayInvoices()
        {
            Console.WriteLine("Enter user ID to view Invoices:");
            int userID = Convert.ToInt32(Console.ReadLine());
            var invoices = Invoices.Where(i => i.UserID == userID).ToList();
            Console.WriteLine("All Invoices: ");
            if(Invoices.Count == 0) Console.WriteLine("No invoices found!");
            foreach(var invoice in invoices)
            {
                string payment = invoice.IsPaid ? "Yes" : "No";
                Console.WriteLine($"Invoice ID: {invoice.InvoiceID}, Ticket ID: {invoice.TicketID}, Amount: {invoice.Amount}, Date: {invoice.Date}, Paid: {payment}");
            }
        }

        public static void ProcessPayment()
        {
            Console.WriteLine("Enter Invoice ID: ");
            int invID = Convert.ToInt32(Console.ReadLine());
            Invoice? invoice = Invoices.FirstOrDefault(i => i.InvoiceID == invID);
            if(invoice.IsPaid)
            {
                Console.WriteLine("Invoice already paid.");
                return;
            }
            invoice.IsPaid = true;

            Schedule? schedule = ScheduleService.Schedules.FirstOrDefault(s => s.ScheduleID == invoice.ScheduleID);
            Bus? bus = BusService.Buses.FirstOrDefault(b => b.ID == schedule.BusID);
            Console.WriteLine($"Payment successful");
            Ticket ticket = new()
            {
                TicketID = invoice.TicketID,
                UserID = invoice.UserID,
                CoachNumber =  bus.CoachNumber,
                JourneyDate = invoice.Date,
                Seat = invoice.Seat,
            };
            User? user = UserService.Users.FirstOrDefault(u => u.UserID == invoice.UserID);
            user.Tickets.Add(ticket);
        }

        public static void DisplayUserTickets()
        {
            Console.WriteLine("Enter user ID to view Tickets:");
            int userID = Convert.ToInt32(Console.ReadLine());
            User? user = UserService.Users.FirstOrDefault(u => u.UserID == userID);
            Console.WriteLine("All Tickets: ");
            if(user.Tickets.Count == 0) Console.WriteLine("No Paid tickets found for this user");
            foreach(var ticket in user.Tickets)
            {
                Console.WriteLine($"Ticket ID: {ticket.TicketID}, Coach Number: {ticket.CoachNumber}, Journey Date: {ticket.JourneyDate}, Seat: {ticket.Seat}");
            }
        }
    }
}
