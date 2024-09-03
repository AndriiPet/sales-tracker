using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPhotoService
    {
        Task AddAsync(int visitId, Stream fileStream, string fileExtension);
        Task DeleteByIdAsync(int id);
        Task<PhotoDto> GetByIdAsync(int id);
        Task<IEnumerable<PhotoDto>> GetByVisitIdAsync(int visitId);
    }
}
