using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class WorkRegion: BaseEntity
    {
        public int id { get; set; }
        public string name { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Trip> Trips { get; set; }
        public ICollection<TradingPoint> TradingPoints { get; set; }
    }
}
