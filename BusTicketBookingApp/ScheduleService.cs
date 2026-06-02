using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTicketBookingApp
{
    public static class ScheduleService
    {
        public static List<Schedule> Schedules = new List<Schedule>();
        public static void CreateSchedule()
        {
            var schedule = new Schedule();
            Console.WriteLine("Enter Bus ID: ");
            schedule.BusID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Schedule ID: ");
            schedule.ScheduleID = Convert.ToInt32(Console.ReadLine());
            foreach (var sch in Schedules)
            {
                if (sch.ScheduleID == schedule.ScheduleID)
                {
                    Console.WriteLine("Schedule ID already exists.");
                    return;
                }
            }

            Console.WriteLine("Enter Departure City: ");
            schedule.DepartureCity = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter Arrival City: ");
            schedule.ArrivalCity = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter Departure Date and Time (yyyy-MM-dd HH:mm): ");
            schedule.DepartureDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm", null);
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
            if (Schedules.Count != 0 && Schedules != null)
            {
                Console.WriteLine("All available bus schedules: ");
                foreach (var schedule in Schedules)
                {
                    Console.WriteLine($"{schedule.BusID} | {schedule.DepartureCity} | {schedule.ArrivalCity} | {schedule.DepartureDate} | {schedule.TicketPrice}");
                }
            }
            //if(Schedules.Count == 0 || Schedules == null)
            else
            {
                Console.WriteLine("No schedules found");
            }
            Console.WriteLine();
        }

        public static void DisplayScheduleDetails()
        {
            Console.WriteLine("Enter Schedule ID to view Details.");
            int scheduleId = Convert.ToInt32(Console.ReadLine());
            if (!Schedules.Any(sc => sc.ScheduleID == scheduleId))
            {
                Console.WriteLine("Schedule not found.");
                return;
            }
            Schedule? schedule;
            Bus? bus;
            schedule = Schedules.FirstOrDefault(sc => sc.ScheduleID == scheduleId);
            bus = BusService.Buses.FirstOrDefault(sb => sb.ID == schedule.BusID);
            Console.WriteLine();
            Console.WriteLine("Schedule Details\n");
            Console.WriteLine($"Schedule ID:{schedule.ScheduleID} | Bus ID:{schedule.BusID} | From: {schedule.DepartureCity} => To {schedule.ArrivalCity}");
            Console.WriteLine($"Departure:{schedule.DepartureDate}\nTaka:{schedule.TicketPrice} | Total Seats:{bus?.TotalSeats}");

            // Seating Layout Design for Different type of bus. ex: Business , Economy

            if (bus.Type == BusType.Business)
            {
                BusinessSeating(bus.TotalSeats, schedule);
            }
            if (bus.Type == BusType.Economy)
            {
                EconomySeating(bus.TotalSeats, schedule);
            }

        }

        public static void BusinessSeating(int totalSeats, Schedule schedule)
        {
            Console.WriteLine("Choose your preferred seat 'X' means the seat is booked\n");

            int rows = (int)Math.Ceiling(totalSeats / 3.0);

            for (int i = 1; i <= rows; i++)
            {
                string a = schedule.ReservedSeats.Contains($"A{i}") ? $"X A{i}" : $"  A{i}";
                string b = schedule.ReservedSeats.Contains($"B{i}") ? $"X B{i}" : $"  B{i}";
                string c = schedule.ReservedSeats.Contains($"C{i}") ? $"X C{i}" : $"  C{i}";

                Console.WriteLine($"[{a}]  [{b}] [{c}]");
            }
        }

        public static void EconomySeating(int totalSeats, Schedule schedule)
        {
            Console.WriteLine("Choose your preferred seat");

            int rows = (int)Math.Ceiling(totalSeats / 4.0);

            for (int i = 1; i <= rows; i++)
            {
                string a = schedule.ReservedSeats.Contains($"A{i}") ? "X" : $"A{i}";
                string b = schedule.ReservedSeats.Contains($"B{i}") ? "X" : $"B{i}";
                string c = schedule.ReservedSeats.Contains($"C{i}") ? "X" : $"C{i}";
                string d = schedule.ReservedSeats.Contains($"D{i}") ? "X" : $"D{i}";

                Console.WriteLine($"[{a}] [{b}]    [{c}] [{d}]");
            }
        }

        public static bool IsValidSeat(Bus bus, string seat)
        {
            int rows;

            if (bus.Type == BusType.Business)
            {
                rows = (int)Math.Ceiling(bus.TotalSeats / 3.0);

                for (int i = 1; i <= rows; i++)
                {
                    if (seat == $"A{i}" || seat == $"B{i}" || seat == $"C{i}")
                        return true;
                }
            }
            else
            {
                rows = (int)Math.Ceiling(bus.TotalSeats / 4.0);

                for (int i = 1; i <= rows; i++)
                {
                    if (seat == $"A{i}" || seat == $"B{i}" || seat == $"C{i}" || seat == $"D{i}")
                        return true;
                }
            }

            return false;
        }
    }
}
