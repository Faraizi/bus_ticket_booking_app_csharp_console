using System;

namespace BusTicketBookingApp
{
   class Program
    {
        public static void Main(string[] args)
        {
            while (true) {
                Console.WriteLine("**All in one ticket solution**\n");

                Console.WriteLine("Press 1: Create User.");
                Console.WriteLine("Press 2: Displlay all User.");
                Console.WriteLine("Press 3: Create Bus.");
                Console.WriteLine("Press 4: Display all Bus.");
                Console.WriteLine("Press 5: Create Schedules.");
                Console.WriteLine("Press 6: Display all Schedules.");
                Console.WriteLine("Press 7: Display Schedules Details.");
                Console.WriteLine("Press 8: Book Ticket.");
                Console.WriteLine("Press 9: Display User Invoices.");
                Console.WriteLine("Press 10: Process Invoice Payment.");
                Console.WriteLine("Press 11: Display User Tickets.");
                Console.WriteLine("Press X to Exit.");
                Console.WriteLine();

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        UserService.CreateUser();
                        break;
                    case "2":
                        UserService.DisplayAllUsers();
                        break;
                    case "3":
                        BusService.CreateBus();
                        break;
                    case "4":
                        BusService.DisplayAllBus();
                        break;
                    case "5":
                        ScheduleService.CreateSchedule();
                        break;
                    case "6":
                        ScheduleService.DisplayAllSchedules();
                        break;
                    case "7":
                        ScheduleService.DisplayScheduleDetails();
                        break;
                    case "8":
                        BookingService.BookTicket();
                        break;
                    case "9":
                        BookingService.DisplayInvoices();
                        break;
                    case "10":
                        BookingService.ProcessPayment();
                        break;
                    case "11":
                        BookingService.DisplayUserTickets();
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }

                if (input?.ToLower() == "x")
                {
                    Console.WriteLine("Exiting application");
                    return;
                }
            }
        }
    }
}