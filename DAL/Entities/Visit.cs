using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Visit : BaseEntity
    {
        public int id { get; set; }
        public bool isVisited { get; set; }
        public bool isPriority { get; set; }
        public DateTime  timeStart { get; set; }
        public DateTime timeEnd { get; set; }
        public DateTime visitDate { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string comment { get; set; }
        public int TradingPointId { get; set; }
        public TradingPoint tradingPoint { get; set; }
        public int TripId { get; set; }
        public Trip Trip { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}
