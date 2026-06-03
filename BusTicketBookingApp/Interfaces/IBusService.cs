using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTicketBookingApp.Interfaces
{
    public interface IBusService
    {
        void CreateBus();
        void DisplayAllBus();
        Bus GetBusByID(int id);
    }
}
