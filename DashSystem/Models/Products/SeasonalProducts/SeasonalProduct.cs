using System.Collections.Generic;
using System.Linq;

namespace DashSystem.Models.Products.SeasonalProducts
{
    public sealed class SeasonalProduct : ProductBase
    {
        public SeasonalDate SeasonStartDate { get; }
        public SeasonalDate SeasonEndDate { get; }

        //public override bool IsActive
        //{
        //    get => SeasonalDate.Now.IsWithinRangeOf(SeasonStartDate, SeasonEndDate);
        //    set => throw new NotSupportedException("Cannot set IsActive on a SeasonalProduct.");
        //}

        private bool isActive;
        public override bool IsActive
        {
            get => isActive && SeasonalDate.Now.IsWithinRangeOf(SeasonStartDate, SeasonEndDate);
            set => isActive = value;
        }

        public SeasonalProduct(uint id, string name, decimal price, bool isActive, SeasonalDate seasonStartDate, SeasonalDate seasonEndDate)
            : base(id, name, price)
        {
            IsActive = isActive;
            SeasonStartDate = seasonStartDate;
            SeasonEndDate = seasonEndDate;
        }

        public override string ToString()
        {
            return $"Seasonal product: {base.ToString()}";
        }

        public override List<string> GetCollumnNames()
        {
            List<string> collumnNames = base.GetCollumnNames().ToList(); // Copy
            collumnNames.Add($"{SeasonEndDate}");
            collumnNames.Add($"{SeasonStartDate}");
            return collumnNames;
        }
    }
}