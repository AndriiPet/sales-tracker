using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class WorkRegionDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public ICollection<UserDto> Users { get; set; }
        public ICollection<TripDto> Trips { get; set; }
        public ICollection<TradingPointDto> TradingPoints { get; set; }
    }
    public class CreateWorkRegionDto
    {
        public string name { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class UpdateWorkRegionDto
    {
        public string? name { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
    }
}
