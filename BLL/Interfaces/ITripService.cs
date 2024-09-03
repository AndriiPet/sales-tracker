using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITripService : IService< TripDto, CreateTripWithVisitsDto, UpdateTripDto>
    {
        Task<IEnumerable<TripDto>> getTripByWorkRegion(int workRegionId);
        Task<IEnumerable<TripDto>> getTripForUser(int userId);
        Task<TripDto> getCurrentTripForUser(int userId, DateTime date);
    }
}
