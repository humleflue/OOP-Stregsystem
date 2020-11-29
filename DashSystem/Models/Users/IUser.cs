using System;

namespace DashSystem.Models.Users
{
    public delegate void UserBalanceNotification(IUser user);

    public interface IUser : IComparable<IUser>, ICollumnNameGetable
    {
        public event UserBalanceNotification UserBalanceChangedEvent;

        public uint ID { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Username { get; }
        public string Email { get; }
        public decimal Balance { get; set; }
    }
}