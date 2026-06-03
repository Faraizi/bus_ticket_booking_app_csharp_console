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
                    //throw new ArgumentException($"UserID: {user.UserID} already exists, ID must be unique");
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
            Users.Add(user);
        }
        public void DisplayAllUsers()
        {
            Console.WriteLine("Displaying all registered users: ");
            if (Users.Count == 0) Console.WriteLine("No users found!");
            foreach (var user in Users)
            {
                Console.WriteLine($"{user.UserID} | {user.FullName} | {user.MobileNumber} | {user.EmailAddress}\n");
            }
        }
        public User? GetUserByID(int id)
        {
            return Users.FirstOrDefault(u => u.UserID == id);
        }
    }
}
