using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BusTicketBookingApp
{
    sealed public class Schedule
    {
        public int BusID { get; set; }
        public string DepartureCity { get; set; } = string.Empty;
        public string ArrivalCity { get; set; } = string.Empty;
        public DateTime DepartureDate { get; set; }
        public decimal TicketPrice { get; set; }
    }
    public static class ScheduleService
    {
        public static List<Schedule> Schedules = new List<Schedule>();
        public static void CreateSchedule()
        {
            var schedule = new Schedule();
            Console.WriteLine("Enter Bus ID: ");
            schedule.BusID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Departure City: ");
            schedule.DepartureCity = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter Arrival City: ");
            schedule.ArrivalCity = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter Departure Date and Time (yyyy-MM-dd HH:mm): ");
            schedule.DepartureDate = DateTime.ParseExact(Console.ReadLine() ?? string.Empty, "yyyy-MM-dd HH:mm", null);
            Console.WriteLine("Enter Ticket Price: ");
            schedule.TicketPrice = Convert.ToDecimal(Console.ReadLine());
            Bus bus = BusService.Buses.FirstOrDefault(b => b.ID == schedule.BusID);
            if (bus != null)
            {
                bus.Schedules.Add(schedule);
                Schedules.Add(schedule);
                Console.WriteLine("Schedule created successfully.");
            }
            else
            {
                Console.WriteLine("Bus not found. Schedule creation failed.");
            }
        }
        public static void DisplayAllSchedules()
        {
            Console.WriteLine("All available bus schedules: ");
            if(Schedules != null)
            {
                foreach(var schedule in Schedules)
                {
                    Console.WriteLine($"{schedule.BusID} | {schedule.DepartureCity} | {schedule.ArrivalCity} | {schedule.DepartureDate} | {schedule.TicketPrice}");
                }
            }
            Console.WriteLine();

        }
    }
}

//3.SCHEDULE MANAGEMENT
//Each bus may operate on multiple schedules.
//A schedule shall consist of:
//   -Departure city
//   - Arrival city
//   - Departure date and time
//   - Ticket price
//Every schedule must be explicitly associated with a corresponding bus in the fleet.
