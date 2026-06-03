using BusTicketBookingApp.Interfaces;
using System;

namespace BusTicketBookingApp
{
   class Program
    {
        
        public static void Main(string[] args)
        {
            UserService userService = new UserService();
            BusService busService = new BusService();
            ScheduleService scheduleService = new ScheduleService(busService);
            BookingService bookingService = new BookingService(scheduleService, userService, busService);

            while (true) {
                Console.WriteLine("E-Ticket Booking System\n");

                Console.WriteLine("Press 1: Create User.");
                Console.WriteLine("Press 2: Displlay all User.");
                Console.WriteLine("Press 3: Create Bus.");
                Console.WriteLine("Press 4: Display all Buses.");
                Console.WriteLine("Press 5: Create Schedule.");
                Console.WriteLine("Press 6: Display all Schedules.");
                Console.WriteLine("Press 7: Display Schedule Details.");
                Console.WriteLine("Press 8: Book Ticket.");
                Console.WriteLine("Press 9: Display User Invoices.");
                Console.WriteLine("Press 10: Process Payment.");
                Console.WriteLine("Press 11: Display User Tickets.");
                Console.WriteLine("Press X : Exit.");
                Console.WriteLine();

                var input = Console.ReadLine();

                if (input?.ToLower() == "x")
                {
                    Console.WriteLine("Exiting application");
                    return;
                }

                switch (input)
                {
                    case "1":
                        userService.CreateUser();
                        break;
                    case "2":
                        userService.DisplayAllUsers();
                        break;
                    case "3":
                        busService.CreateBus();
                        break;
                    case "4":
                        busService.DisplayAllBus();
                        break;
                    case "5":
                        scheduleService.CreateSchedule();
                        break;
                    case "6":
                        scheduleService.DisplayAllSchedules();
                        break;
                    case "7":
                        scheduleService.DisplayScheduleDetails();
                        break;
                    case "8":
                        bookingService.BookTicket();
                        break;
                    case "9":
                        bookingService.DisplayInvoices();
                        break;
                    case "10":
                        bookingService.ProcessPayment();
                        break;
                    case "11":
                        bookingService.DisplayUserTickets();
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }
        }
    }
}