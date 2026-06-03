using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTicketBookingApp.Interfaces
{
    public interface IScheduleService
    {
        void CreateSchedule();
        void DisplayAllSchedules();

        void DisplayScheduleDetails();

        void BusinessSeating(int totalSeats, Schedule schedule);

        void EconomySeating(int totalSeats, Schedule schedule);

        bool IsValidSeat(Bus bus, string seat);
        Schedule? GetScheduleByID(int id);
    }
}
