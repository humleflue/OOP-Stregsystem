using System.Collections.Generic;
using System.Linq;

namespace DashSystem.Models.Products
{
    public sealed class Product : ProductBase
    {
        public override bool IsActive { get; set; }

        public Product(uint id, string name, decimal price, bool isActive)
            : base(id, name, price)
        {
            IsActive = isActive;
        }


        public override string ToString()
        {
            return $"Regular product: {base.ToString()}";
        }

        public override List<string> GetCollumnNames()
        {
            List<string> collumnNames = base.GetCollumnNames().ToList(); // Copy
            collumnNames.Add(""); // No deactivation date in regular products
            collumnNames.Add(""); // No activation date in regular products
            return collumnNames;
        }
    }
}
