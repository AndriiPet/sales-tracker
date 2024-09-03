using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService : IService<UserDto, CreateUserDto, UpdateUserDto>
    {
        Task<IEnumerable<UserDto>> GetAllWithPagination(int page, int limit);
        Task<UserDto> GetByName(string name);
        Task<IEnumerable<UserDto>> GetAllManagers();
        Task<IEnumerable<UserDto>> GetAllSubordinates(int userId);
        Task<IEnumerable<IGrouping<WorkRegionDto, UserDto>>> GetSubordinatesByRegion(int userId);
        Task<IEnumerable<UserDto>> GetSubordinatesInRegion(int regionId, int managerId);
        Task<WorkRegionDto> GetUserLocation(int id);
        Task<IEnumerable<UserDto>> GetUsersWithoutRegion();
        Task<(int CompletedVisits, int TotalVisits)> GetVisitsStats(int userId, DateTime currentDate);
        Task<IEnumerable<VisitDto>> GetVisitsForUser(int userId);
    }
}
