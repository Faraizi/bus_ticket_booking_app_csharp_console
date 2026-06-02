using BusTicketBookingApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BusTicketBookingApp
{
    sealed public class Bus
    {
        public int ID { get; set; }
        public int CoachNumber { get; set; }
        public BusType Type { get; set; }
        public int TotalSeats { get; set; }
        public List<Schedule> Schedules { get; set; } = new ();
    }

    public static class BusService {
        public static List<Bus> Buses = new List<Bus>();
        public static void CreateBus()
        {
            var bus = new Bus();
            Console.WriteLine("Enter Bus ID: ");
            bus.ID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Coach Number: ");
            bus.CoachNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Bus Type (0 for Economy, 1 for Business): ");
            bus.Type = (BusType)Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Total Seats: ");
            bus.TotalSeats = Convert.ToInt32(Console.ReadLine());
            Buses.Add(bus);
        }
        public static void DisplayAllBus()
        {
            Console.WriteLine("Displaying all available buses: \n");
            if(Buses.Count == 0) Console.WriteLine("No buses found!");
            foreach (var bus in Buses)
            {
                Console.WriteLine($"{bus.ID}| {bus.CoachNumber}| {bus.Type}| {bus.TotalSeats}");
            }
            Console.WriteLine();
        }
        
    }
}

//The system must accurately monitor and distinguish between available and reserved seats for each bus.
