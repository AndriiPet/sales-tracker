using BLL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IVisitService : IService<VisitDto, CreateVisitDto, UpdateVisitDto>
    {
        Task<IEnumerable<VisitDto>> GetVisitsByTripId(int tripId);
        Task<IEnumerable<VisitDto>> GetVisitByUserAndDate(int userId, DateTime date);
        Task UpdateWithPhotoAsync(int visitId, UpdateVisitWithPhotosDto model, List<IFormFile> photos);
    }
}
