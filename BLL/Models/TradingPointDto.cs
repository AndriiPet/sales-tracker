using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class TradingPointDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string PhoneNumber { get; set; }
        public int CustomerId { get; set; }
        public CustomerDto Customer { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public int WorkRegionId { get; set; }
        public WorkRegionDto WorkRegion { get; set; }
        public ICollection<VisitDto> Visits { get; set; }
    }

    public class CreateTradingPointDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public string PhoneNumber { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public int WorkRegionId { get; set; }
    }

    public class UpdateTradingPointDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public string? PhoneNumber { get; set; }
        public int? CustomerId { get; set; }
        public int? UserId { get; set; }
        public int? WorkRegionId { get; set; }
    }
}
