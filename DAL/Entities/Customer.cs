namespace DAL.Entities
{
    public class Customer : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<TradingPoint> TradingPoints { get; set; }
    }
}