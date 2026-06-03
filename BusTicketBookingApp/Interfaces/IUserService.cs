using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTicketBookingApp.Interfaces
{
    public interface IUserService
    {
        User GetUserByID(int id);
        void CreateUser();
        void DisplayAllUsers();
        void DisplayUserTickets();
    }
}
