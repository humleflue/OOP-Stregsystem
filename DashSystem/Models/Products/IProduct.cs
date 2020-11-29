namespace DashSystem.Models.Products
{
    public interface IProduct : ICollumnNameGetable
    {
        public uint ID { get; }
        public string Name { get; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public bool CanBeBoughtOnCredit { get; set; }
    }
}