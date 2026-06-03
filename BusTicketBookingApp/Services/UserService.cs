using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTicketBookingApp.Interfaces
{
    public class UserService : IUserService
    {
        private readonly List<User> Users = new ();
        public void CreateUser()
        {
            var user = new User();
            Console.WriteLine("Enter User ID: ");
            user.UserID = Convert.ToInt32(Console.ReadLine());
            foreach (var usr in Users)
            {
                if (usr.UserID == user.UserID)
                {
                    Console.WriteLine($"User ID {usr.UserID} already exists, Enter a unique user ID\n");
                    return;
                }
            }

            Console.WriteLine("Enter Full Name: ");
            user.FullName = Console.ReadLine();
            Console.WriteLine("Enter Mobile: ");
            user.MobileNumber = Console.ReadLine();
            Console.WriteLine("Enter Email: ");
            user.EmailAddress = Console.ReadLine();
            if(!user.EmailAddress.Contains('@') || !user.EmailAddress.Contains('.'))
            {
                Console.WriteLine("Please enter a valid email address\n");
                return;
            }
            Users.Add(user);
            Console.WriteLine();
        }
        public void DisplayAllUsers()
        {
            Console.WriteLine("Displaying all registered users: ");
            if (Users.Count == 0) Console.WriteLine("No users found!");
            foreach (var user in Users)
            {
                Console.WriteLine($"{user.UserID}. {user.FullName} - {user.MobileNumber} - {user.EmailAddress}");
            }
            Console.WriteLine();
        }
        public void DisplayUserTickets()
        {
            Console.WriteLine("Enter user ID to view Tickets:");
            int userID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("All Tickets: ");
            User? user = GetUserByID(userID);
            if (user.Tickets.Count == 0) Console.WriteLine("No Paid tickets found for this user.\n");
            foreach (var ticket in user.Tickets)
            {
                Console.WriteLine($"Ticket ID: {ticket.TicketID}, Coach Number: {ticket.CoachNumber}, Journey Date: {ticket.JourneyDate}, Seat: {ticket.Seat}\n");
            }
        }
        public User? GetUserByID(int id) => Users.FirstOrDefault(u => u.UserID == id);
    }
}