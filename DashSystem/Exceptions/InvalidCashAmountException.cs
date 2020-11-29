using System;

namespace DashSystem.Exceptions
{
    public class InvalidCashAmountException : Exception
    {
        public InvalidCashAmountException()
        {
        }

        public InvalidCashAmountException(string message)
            : base(message)
        {
        }
        public InvalidCashAmountException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
