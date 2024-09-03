using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class TradingPoint : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string PhoneNumber { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int WorkRegionId { get; set; }
        public WorkRegion WorkRegion { get; set; }
        public ICollection<Visit> Visits { get; set; }
    }
}
