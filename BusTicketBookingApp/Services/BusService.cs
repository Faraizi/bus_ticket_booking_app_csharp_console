using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTicketBookingApp.Interfaces
{
    public class BusService : IBusService
    {
        private readonly List<Bus> _buses = new ();

        public void CreateBus()
        {
            Bus bus = new Bus();
            Console.WriteLine("Enter Bus ID: ");
            bus.ID = Convert.ToInt32(Console.ReadLine());
            foreach(var bs in _buses)
            {
                if(bs.ID == bus.ID)
                {
                    Console.WriteLine("Bus ID Already Exists");
                    return;
                }
            }
            Console.WriteLine("Enter Coach Number: ");
            bus.CoachNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Bus Type (0 for Economy, 1 for Business): ");
            bus.Type = (BusType)Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Total Seats: ");
            bus.TotalSeats = Convert.ToInt32(Console.ReadLine());
            _buses.Add(bus);
            Console.WriteLine();
        }

        public void DisplayAllBus()
        {
            Console.WriteLine("Displaying all available buses: \n");
            if (_buses.Count == 0) Console.WriteLine("No buses found!");
            foreach (var bus in _buses)
            {
                Console.WriteLine($"{bus.ID} - {bus.CoachNumber} - {bus.Type} - {bus.TotalSeats}");
            }
            Console.WriteLine();
        }
        public Bus? GetBusByID(int id) => _buses.FirstOrDefault(b => b.ID == id);
    }
}
