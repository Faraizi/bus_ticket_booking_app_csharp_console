using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTicketBookingApp.Interfaces
{
    public interface IBusService
    {
        Bus GetBusByID(int id);
        void CreateBus();
        void DisplayAllBus();
    }
}
