using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class TripDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public int WorkRegionId { get; set; }
        public WorkRegionDto WorkRegion { get; set; }
        public ICollection<VisitDto> Visits { get; set; }
    }
    public class CreateTripWithVisitsDto
    {
        public CreateTripDto TripDto { get; set; }
        public List<CreateVisitDto> VisitDtos { get; set; }
    }

    public class CreateTripDto
    {
        public DateTime StartDate { get; set; }
        public int UserId { get; set; }
        public int WorkRegionId { get; set; }
    }

    public class UpdateTripDto
    {
        public DateTime StartDate { get; set; }
        public int UserId { get; set; }
        public int WorkRegionId { get; set; }
    }
}
