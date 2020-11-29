using System;
using System.Collections.Generic;

namespace DashSystem.Models.Products
{
    public abstract class ProductBase : IProduct
    {
        public uint ID { get; }
        public string Name { get; }
        public decimal Price { get; set; }
        public abstract bool IsActive { get; set; }
        public bool CanBeBoughtOnCredit { get; set; } = false;

        protected ProductBase(uint id, string name, decimal price)
        {
            ID = id > 0 ? id : throw new ArgumentException("ID has to be greater than 0.");
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Price = price;
        }

        public override string ToString()
        {
            return $"<{ID}> {Name} ({Price} kr)";
        }

        public virtual List<string> GetCollumnNames()
        {
            return new List<string>() { $"{ID}", $"{Name}", $"{Price}", $"{IsActive}" };
        }
    }
}