using System;

namespace DashSystem.Exceptions
{
    class ProductInactiveException : Exception
    {
        public ProductInactiveException()
        {
        }

        public ProductInactiveException(string message)
            : base(message)
        {
        }
        public ProductInactiveException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
