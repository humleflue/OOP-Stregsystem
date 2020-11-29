using System;
using System.Collections.Generic;

namespace DashSystem.Models.Users
{
    public class User : IUser
    {
        public event UserBalanceNotification UserBalanceChangedEvent;
        public uint ID { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Username { get; }
        public string Email { get; }
        private decimal balance;
        public decimal Balance
        {
            get => balance;
            set
            {
                balance = value;
                UserBalanceChangedEvent?.Invoke(this);
            }
        }

        public User(uint id, string firstName, string lastName, string username, decimal balance, string email)
        {
            ID = id > 0 ? id : throw new ArgumentException("ID has to be greater than 0.");
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Email = IsValidEmail(email) ? email : throw new ArgumentException($"Invalid email adress: {email}");
            Balance = balance;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is User other)
            {
                return ID.CompareTo(other.ID) == 0;
            }

            return false;
        }
        public int CompareTo(IUser other)
        {
            return other == null ? 1 : ID.CompareTo(other.ID);
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} ({Email})";
        }

        public List<string> GetCollumnNames()
        {
            return new List<string>() { $"{ID}", $"{FirstName}", $"{LastName}", $"{Username}", $"{Balance}", $"{Email}" };
        }

        // TODO: LowPrio: Check email for invalid characters
        private bool IsValidEmail(string email)
        {
            string domain = email.Substring(email.IndexOf('@') + 1);
            // Domain will be identical to email, if no '@' character is found as IndexOf('@') will return -1
            return domain != email // if(true): email doesn't contain @
                   && domain.IndexOf('@') == -1 // if(true): domain doesn't contain '@'
                   && domain.IndexOf('.') != -1 // if(true): domain contains a '.'
                   && !domain.StartsWith('-')
                   && !domain.EndsWith('-')
                   && !domain.StartsWith('.')
                   && !domain.EndsWith('.');
        }
    }
}