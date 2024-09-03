using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class VisitDto
    {
        public int id { get; set; }
        public bool isVisited { get; set; }
        public bool isPriority { get; set; }
        public DateTime timeStart { get; set; }
        public DateTime timeEnd { get; set; }
        public DateTime visitDate { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string comment { get; set; }
        public int TradingPointId { get; set; }
        public TradingPointDto tradingPoint { get; set; }
        public int TripId { get; set; }
        public TripDto Trip { get; set; }
        public ICollection<PhotoDto> Photos { get; set; }
    }

    public class CreateVisitDto
    {
        public DateTime visitDate { get; set; }
        public int TradingPointId { get; set; }
        public int TripId { get; set; }

    }

    public class UpdateVisitDto
    {
        public bool? isVisited { get; set; }
        public bool? isPriority { get; set; }
        public DateTime? timeStart { get; set; }
        public DateTime? timeEnd { get; set; }
        public DateTime? visitDate { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public string? comment { get; set; }
        public int? TradingPointId { get; set; }
        public TradingPointDto? tradingPoint { get; set; }
        public int? TripId { get; set; }
        public TripDto? Trip { get; set; }
    }

    public class UpdateVisitWithPhotosDto
    {
        public DateTime? timeEnd { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public string? comment { get; set; }
    }
}
